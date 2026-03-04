using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using SmartAttendanceClassroomMonitoringVersion2;

namespace SmartAttendanceClassroomMonitoringVersion2.contents
{
    public partial class SchedulesForm : Form
    {
        private readonly string[] _days = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
        private readonly (int Slot, string Label)[] _timeSlots =
        {
            (1, "08:00 - 09:00"),
            (2, "09:00 - 10:00"),
            (3, "10:00 - 11:00"),
            (4, "11:00 - 12:00"),
            (5, "12:00 - 01:00"),
            (6, "01:00 - 02:00"),
            (7, "02:00 - 03:00"),
            (8, "03:00 - 04:00")
        };

        private readonly Dictionary<int, DataGridView> _roomGrids = new();
        private readonly string[] _subjectNames = { "English", "Filipino", "Mathematics", "Science", "Araling Panlipunan", "MAPEH", "ICT" };
        private readonly Dictionary<string, string> _subjectColumnMap = new()
        {
            { "English", "english" },
            { "Filipino", "filipino" },
            { "Mathematics", "math" },
            { "Science", "science" },
            { "Araling Panlipunan", "araling_panlipunan" },
            { "MAPEH", "mapeh" },
            { "ICT", "ict" }
        };
        private List<SubjectOption> _subjects = new();
        private List<TeacherOption> _teachers = new();
        private readonly Dictionary<int, List<int>> _subjectTeachers = new(); // subjectId -> teacherIds
        private readonly object _storageLock = new();
        private List<Assignment> _assignments = new();
        private static readonly string DataFolder =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SmartAttendance");
        private static readonly string ScheduleFile = Path.Combine(DataFolder, "schedules.json");

        private bool _uiBuilt;

        public SchedulesForm()
        {
            InitializeComponent();
            this.Load += SchedulesForm_Load;
        }

        private void SchedulesForm_Load(object? sender, EventArgs e)
        {
            if (_uiBuilt)
                return;

            BuildRoomPanels();
            LoadLookups();
            LoadSchedules();

            _uiBuilt = true;
        }

        #region UI construction

        private void BuildRoomPanels()
        {
            _roomGrids.Clear();

            var grids = new (int Room, DataGridView Grid)[]
            {
                (1, dgvRoom1),
                (2, dgvRoom2),
                (3, dgvRoom3),
                (4, dgvRoom4)
            };

            foreach (var (room, grid) in grids)
            {
                if (grid == null) continue;
                ConfigureGrid(grid);
                grid.Tag = room;
                grid.CellClick += (s, e) => GridCellClick(room, grid, e);
                _roomGrids[room] = grid;
            }
        }

        private void ConfigureGrid(DataGridView grid)
        {
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.RowHeadersVisible = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.SelectionMode = DataGridViewSelectionMode.CellSelect;
            grid.MultiSelect = false;
            grid.ReadOnly = true;
            grid.BackgroundColor = Color.White;
            grid.BorderStyle = BorderStyle.FixedSingle;
            grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            grid.Columns.Clear();
            grid.Rows.Clear();

            var timeCol = new DataGridViewTextBoxColumn
            {
                Name = "Time",
                HeaderText = "Time",
                ReadOnly = true,
                FillWeight = 40,
                SortMode = DataGridViewColumnSortMode.NotSortable
            };
            grid.Columns.Add(timeCol);

            foreach (var day in _days)
            {
                grid.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = day,
                    HeaderText = day,
                    ReadOnly = true,
                    FillWeight = 60,
                    SortMode = DataGridViewColumnSortMode.NotSortable
                });
            }

            grid.Rows.Add(_timeSlots.Length);
            for (int i = 0; i < _timeSlots.Length; i++)
            {
                grid.Rows[i].Cells[0].Value = _timeSlots[i].Label;
            }

            grid.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            grid.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        #endregion

        #region Lookup loading

        private void LoadLookups()
        {
            LoadSubjects();
            LoadTeachers();
            LoadSubjectTeacherAssignments();
        }

        private void LoadSubjects()
        {
            // Try DB; fall back to static list if unavailable.
            _subjects = new List<SubjectOption>();
            if (!TryLoadSubjectsFromDb())
            {
                for (int i = 0; i < _subjectNames.Length; i++)
                    _subjects.Add(new SubjectOption(i + 1, _subjectNames[i]));
            }
        }

        private void LoadTeachers()
        {
            _teachers = new List<TeacherOption> { new(0, "Select Teacher") };

            if (!TryLoadTeachersFromDb())
            {
                // Fallback to local cache file if present.
                var teacherFile = Path.Combine(DataFolder, "teachers.json");
                if (File.Exists(teacherFile))
                {
                    try
                    {
                        var text = File.ReadAllText(teacherFile);
                        var data = JsonSerializer.Deserialize<List<TeacherOption>>(text) ?? new();
                        _teachers.AddRange(data.Where(t => t.Id != 0));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Unable to load teachers from local file: {ex.Message}", "Warning",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void LoadSubjectTeacherAssignments()
        {
            // default: allow all teachers
            _subjectTeachers.Clear();
            var teacherIds = _teachers.Where(t => t.Id != 0).Select(t => t.Id).ToList();
            foreach (var subj in _subjects)
                _subjectTeachers[subj.Id] = teacherIds;

            TryLoadSubjectTeacherAssignmentsFromDb();
        }

        #endregion

        #region Data load/save (file-backed)

        private void LoadSchedules()
        {
            LoadAssignmentsFromDisk();
            CleanAssignmentsAgainstSubjectTeachers();

            foreach (var grid in _roomGrids.Values)
            {
                ResetGrid(grid);
            }

            foreach (var assignment in _assignments)
            {
                if (!_roomGrids.TryGetValue(assignment.Room, out var grid))
                    continue;

                int rowIndex = assignment.Slot - 1;
                int colIndex = Array.IndexOf(_days, assignment.Day) + 1; // +1 because column 0 is Time
                if (rowIndex < 0 || rowIndex >= grid.Rows.Count || colIndex <= 0 || colIndex >= grid.Columns.Count)
                    continue;

                var data = new CellData
                {
                    AssignmentId = assignment.Id,
                    SubjectId = assignment.SubjectId,
                    TeacherId = assignment.TeacherId,
                    Room = assignment.Room,
                    Day = assignment.Day,
                    Slot = assignment.Slot
                };
                ApplyCellData(grid, rowIndex, colIndex, data);
            }
        }

        private void ResetGrid(DataGridView grid)
        {
            for (int r = 0; r < grid.Rows.Count; r++)
            {
                for (int c = 1; c < grid.Columns.Count; c++)
                {
                    grid.Rows[r].Cells[c].Value = "";
                    grid.Rows[r].Cells[c].Tag = null;
                }
            }
        }

        #endregion

        #region Cell interactions

        private void GridCellClick(int room, DataGridView grid, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex <= 0)
                return; // ignore header / time column

            string day = _days[e.ColumnIndex - 1];
            var slotInfo = _timeSlots[e.RowIndex];
            var currentData = grid.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag as CellData;

            using var editor = new ScheduleCellEditor(_subjects, _teachers, _subjectTeachers, currentData?.SubjectId, currentData?.TeacherId);
            if (editor.ShowDialog(this) != DialogResult.OK)
                return;

            int? subjectId = editor.SelectedSubjectId;
            int? teacherId = editor.SelectedTeacherId;

            try
            {
                var assignmentId = SaveAssignment(room, day, slotInfo.Slot, subjectId, teacherId);

                var data = new CellData
                {
                    AssignmentId = assignmentId,
                    SubjectId = subjectId,
                    TeacherId = teacherId,
                    Room = room,
                    Day = day,
                    Slot = slotInfo.Slot
                };

                ApplyCellData(grid, e.RowIndex, e.ColumnIndex, data);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to save schedule: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyCellData(DataGridView grid, int row, int col, CellData data)
        {
            string subjectName = GetSubjectName(data.SubjectId);
            string teacherName = GetTeacherName(data.TeacherId);
            grid.Rows[row].Cells[col].Value = $"{subjectName}\n{teacherName}".Trim();
            grid.Rows[row].Cells[col].Tag = data;
        }

        #endregion

        #region Persistence

        private int SaveAssignment(int room, string day, int slot, int? subjectId, int? teacherId)
        {
            lock (_storageLock)
            {
                LoadAssignmentsFromDisk();

                // Prevent double booking for the same teacher at the same time in another room.
                if (teacherId.HasValue && teacherId.Value != 0)
                {
                var conflict = _assignments.FirstOrDefault(a =>
                    a.TeacherId == teacherId &&
                    a.Day.Equals(day, StringComparison.OrdinalIgnoreCase) &&
                    a.Slot == slot &&
                    a.Room != room);
                if (conflict != null)
                        throw new InvalidOperationException("That teacher is already assigned to another room at the same time.");
                }

                var existing = _assignments.FirstOrDefault(a =>
                    a.Room == room &&
                    a.Day.Equals(day, StringComparison.OrdinalIgnoreCase) &&
                    a.Slot == slot);

                if (existing != null)
                {
                    existing.SubjectId = subjectId;
                    existing.TeacherId = NormalizeTeacherForSubject(subjectId, teacherId);
                }
                else
                {
                    int nextId = _assignments.Count == 0 ? 1 : _assignments.Max(a => a.Id) + 1;
                    existing = new Assignment
                    {
                        Id = nextId,
                        Room = room,
                        Day = day,
                        Slot = slot,
                        SubjectId = subjectId,
                        TeacherId = NormalizeTeacherForSubject(subjectId, teacherId)
                    };
                    _assignments.Add(existing);
                }

                PersistAssignments();
                return existing.Id;
            }
        }

        private void LoadAssignmentsFromDisk()
        {
            if (!File.Exists(ScheduleFile))
            {
                _assignments = new List<Assignment>();
                return;
            }

            try
            {
                var json = File.ReadAllText(ScheduleFile);
                _assignments = JsonSerializer.Deserialize<List<Assignment>>(json) ?? new List<Assignment>();
            }
            catch
            {
                _assignments = new List<Assignment>();
            }
        }

        private void PersistAssignments()
        {
            Directory.CreateDirectory(DataFolder);
            var json = JsonSerializer.Serialize(_assignments, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(ScheduleFile, json);
        }

        #endregion

        #region Helpers

        private string GetSubjectName(int? subjectId)
        {
            if (!subjectId.HasValue || subjectId.Value == 0) return "";
            return _subjects.FirstOrDefault(s => s.Id == subjectId.Value)?.Name ?? "";
        }

        private string GetTeacherName(int? teacherId)
        {
            if (!teacherId.HasValue || teacherId.Value == 0) return "";
            return _teachers.FirstOrDefault(t => t.Id == teacherId.Value)?.Name ?? "";
        }

        /// <summary>
        /// If the stored teacher is no longer valid for the subject (removed in SubjectsForm or teacher missing), clear it.
        /// </summary>
        private void CleanAssignmentsAgainstSubjectTeachers()
        {
            var teacherSet = _teachers.Select(t => t.Id).ToHashSet();
            bool changed = false;

            foreach (var a in _assignments)
            {
                if (a.TeacherId == null || a.TeacherId == 0 || a.SubjectId == null || a.SubjectId == 0)
                    continue;

                var allowed = _subjectTeachers.TryGetValue(a.SubjectId.Value, out var list) ? list : new List<int>();
                bool teacherExists = teacherSet.Contains(a.TeacherId.Value);
                bool allowedForSubject = allowed.Count == 0 || allowed.Contains(a.TeacherId.Value);

                if (!teacherExists || !allowedForSubject)
                {
                    a.TeacherId = null; // reset to "Select Teacher"
                    changed = true;
                }
            }

            if (changed)
                PersistAssignments();
        }

        /// <summary>
        /// Ensure teacher choice aligns with current subject-teacher mapping; if not valid, return null.
        /// </summary>
        private int? NormalizeTeacherForSubject(int? subjectId, int? teacherId)
        {
            if (!teacherId.HasValue || teacherId.Value == 0)
                return null;
            if (!subjectId.HasValue || subjectId.Value == 0)
                return teacherId;

            var allowed = _subjectTeachers.TryGetValue(subjectId.Value, out var list) ? list : new List<int>();
            if (allowed.Count == 0) // no restriction known -> keep teacher
                return teacherId;

            return allowed.Contains(teacherId.Value) ? teacherId : null;
        }

        private bool TryLoadSubjectsFromDb()
        {
            try
            {
                using var conn = Database.GetConnection();
                conn.Open();

                // subjects are defined by columns; use configured subject names
                _subjects.Clear();
                for (int i = 0; i < _subjectNames.Length; i++)
                {
                    _subjects.Add(new SubjectOption(i + 1, _subjectNames[i]));
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool TryLoadTeachersFromDb()
        {
            try
            {
                using var conn = Database.GetConnection();
                conn.Open();
                using var cmd = new MySqlCommand("SELECT idteachers_tbl, CONCAT(LastName, ', ', FirstName) AS Name FROM teachers_tbl ORDER BY LastName, FirstName", conn);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    _teachers.Add(new TeacherOption(reader.GetInt32(0), reader.GetString(1)));
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to load teachers from database: {ex.Message}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        private void TryLoadSubjectTeacherAssignmentsFromDb()
        {
            try
            {
                using var conn = Database.GetConnection();
                conn.Open();
                using var cmd = new MySqlCommand("SELECT * FROM subjects_tbl WHERE idsubjects_tbl = 1", conn);
                using var reader = cmd.ExecuteReader();
                if (!reader.Read())
                    return;

                foreach (var subj in _subjects)
                {
                    if (!_subjectColumnMap.TryGetValue(subj.Name, out var col) || string.IsNullOrWhiteSpace(col))
                        continue;

                    if (!HasColumn(reader, col))
                        continue;

                    var value = reader[col]?.ToString() ?? string.Empty;
                    var teacherIds = value.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                          .Select(v => int.TryParse(v, out var id) ? id : 0)
                                          .Where(id => id > 0)
                                          .ToList();
                    if (teacherIds.Count > 0)
                    {
                        // Keep only IDs that exist in teacher list.
                        var valid = teacherIds.Where(id => _teachers.Any(t => t.Id == id)).ToList();
                        _subjectTeachers[subj.Id] = valid;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to load subject-teacher assignments from database: {ex.Message}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private static bool HasColumn(MySqlDataReader reader, string columnName)
        {
            try
            {
                return reader.GetOrdinal(columnName) >= 0;
            }
            catch
            {
                return false;
            }
        }

        private class CellData
        {
            public int? AssignmentId { get; set; }
            public int? SubjectId { get; set; }
            public int? TeacherId { get; set; }
            public int Room { get; set; }
            public string Day { get; set; } = string.Empty;
            public int Slot { get; set; }
        }

        #endregion
    }

    /// <summary>
    /// Small modal editor for choosing subject and teacher.
    /// </summary>
    internal class ScheduleCellEditor : Form
    {
        private readonly ComboBox _cbSubject = new() { DropDownStyle = ComboBoxStyle.DropDownList };
        private readonly ComboBox _cbTeacher = new() { DropDownStyle = ComboBoxStyle.DropDownList };
        private readonly Button _btnOk = new() { Text = "Save", DialogResult = DialogResult.OK };
        private readonly Button _btnCancel = new() { Text = "Cancel", DialogResult = DialogResult.Cancel };

        private readonly List<SubjectOption> _subjects;
        private readonly List<TeacherOption> _teachers;
        private readonly Dictionary<int, List<int>> _subjectTeachers;

        public int? SelectedSubjectId => _cbSubject.SelectedValue is int v && v != 0 ? v : null;
        public int? SelectedTeacherId => _cbTeacher.SelectedValue is int v && v != 0 ? v : null;

        public ScheduleCellEditor(
            IEnumerable<SubjectOption> subjects,
            IEnumerable<TeacherOption> teachers,
            Dictionary<int, List<int>> subjectTeachers,
            int? currentSubjectId,
            int? currentTeacherId)
        {
            _subjects = subjects.ToList();
            _teachers = teachers.ToList();
            _subjectTeachers = subjectTeachers;

            Text = "Assign Subject & Teacher";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(360, 180);
            MaximizeBox = false;
            MinimizeBox = false;

            var lblSubject = new Label { Text = "Subject", Location = new Point(15, 20), AutoSize = true };
            var lblTeacher = new Label { Text = "Teacher", Location = new Point(15, 65), AutoSize = true };

            _cbSubject.Location = new Point(100, 15);
            _cbSubject.Size = new Size(230, 25);
            _cbTeacher.Location = new Point(100, 60);
            _cbTeacher.Size = new Size(230, 25);

            _btnOk.Location = new Point(170, 110);
            _btnCancel.Location = new Point(255, 110);
            _btnOk.Click += BtnOk_Click;

            Controls.AddRange(new Control[] { lblSubject, lblTeacher, _cbSubject, _cbTeacher, _btnOk, _btnCancel });
            AcceptButton = _btnOk;
            CancelButton = _btnCancel;

            LoadSubjects();
            _cbSubject.SelectedValue = currentSubjectId ?? 0;
            UpdateTeacherList();
            _cbTeacher.SelectedValue = currentTeacherId ?? 0;

            _cbSubject.SelectedIndexChanged += (s, e) => UpdateTeacherList();
        }

        private void LoadSubjects()
        {
            var data = _subjects.Select(s => new { s.Id, s.Name }).ToList();
            data.Insert(0, new { Id = 0, Name = "Select Subject" });
            _cbSubject.DataSource = data;
            _cbSubject.DisplayMember = "Name";
            _cbSubject.ValueMember = "Id";
        }

        private void UpdateTeacherList()
        {
            int subjectId = _cbSubject.SelectedValue is int v ? v : 0;
            IEnumerable<TeacherOption> allowed;

            if (subjectId != 0 && _subjectTeachers.TryGetValue(subjectId, out var allowedIds) && allowedIds.Count > 0)
            {
                allowed = _teachers.Where(t => t.Id == 0 || allowedIds.Contains(t.Id));
            }
            else
            {
                allowed = _teachers;
            }

            _cbTeacher.DataSource = allowed.Select(t => new { t.Id, t.Name }).ToList();
            _cbTeacher.DisplayMember = "Name";
            _cbTeacher.ValueMember = "Id";
        }

        private void BtnOk_Click(object? sender, EventArgs e)
        {
            // Allow choosing "Select Subject" / "Select Teacher" to clear the assignment.
            // No validation needed because nulls are stored as unassigned.
        }
    }

    internal record SubjectOption(int Id, string Name);
    internal record TeacherOption(int Id, string Name);

    internal class Assignment
    {
        public int Id { get; set; }
        public int Room { get; set; }
        public string Day { get; set; } = string.Empty;
        public int Slot { get; set; }
        public int? SubjectId { get; set; }
        public int? TeacherId { get; set; }
    }
}
