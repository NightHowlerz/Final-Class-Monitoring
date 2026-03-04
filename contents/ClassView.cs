using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SmartAttendanceClassroomMonitoringVersion2.contents
{
    public class ClassView : Form
    {
        private readonly string _teacherName;
        private int? _teacherId;

        private readonly (int Slot, string Label, TimeSpan Start, TimeSpan End)[] _timeSlots =
        {
            (1, "08:00 - 09:00", new TimeSpan(8, 0, 0), new TimeSpan(9, 0, 0)),
            (2, "09:00 - 10:00", new TimeSpan(9, 0, 0), new TimeSpan(10, 0, 0)),
            (3, "10:00 - 11:00", new TimeSpan(10, 0, 0), new TimeSpan(11, 0, 0)),
            (4, "11:00 - 12:00", new TimeSpan(11, 0, 0), new TimeSpan(12, 0, 0)),
            (5, "12:00 - 01:00", new TimeSpan(12, 0, 0), new TimeSpan(13, 0, 0)),
            (6, "01:00 - 02:00", new TimeSpan(13, 0, 0), new TimeSpan(14, 0, 0)),
            (7, "02:00 - 03:00", new TimeSpan(14, 0, 0), new TimeSpan(15, 0, 0)),
            (8, "03:00 - 04:00", new TimeSpan(15, 0, 0), new TimeSpan(16, 0, 0))
        };

        private readonly string[] _subjectNames = { "English", "Filipino", "Mathematics", "Science", "Araling Panlipunan", "MAPEH", "ICT" };

        private Label lblDayTitle = null!;
        private Label lblCurrRoom = null!;
        private Label lblCurrSubject = null!;
        private Label lblCurrTime = null!;

        private Label lblNextRoom = null!;
        private Label lblNextSubject = null!;
        private Label lblNextTime = null!;

        private Button btnCheckIn = null!;

        private Panel _cardsPanel = null!;
        private Panel _attendanceHost = null!;
        private AttendanceForm? _attendanceForm;

        private AssignmentDto? _currentAssignment;
        private string? _currentSectionName;
        private string? _scheduleKey;

        private System.Windows.Forms.Timer? _timer;

        public ClassView(string teacherName)
        {
            _teacherName = teacherName;
            Text = "Class";
            BackColor = Color.WhiteSmoke;
            StartPosition = FormStartPosition.CenterScreen;
            DoubleBuffered = true;
            InitializeLayout();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ResolveTeacherId();
            RefreshScheduleCards();
            SetupAutoRefresh();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            _timer?.Dispose();
        }

        private void SetupAutoRefresh()
        {
            _timer = new System.Windows.Forms.Timer { Interval = 60_000 }; // refresh every minute
            _timer.Tick += (_, _) => RefreshScheduleCards();
            _timer.Start();
        }

        #region UI

        private void InitializeLayout()
        {
            var root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                Padding = new Padding(24),
                BackColor = Color.WhiteSmoke
            };
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            lblDayTitle = new Label
            {
                Text = DateTime.Now.DayOfWeek.ToString(),
                Dock = DockStyle.Top,
                Height = 42,
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.FromArgb(33, 37, 41),
                Margin = new Padding(0, 0, 0, 16)
            };

            var main = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                BackColor = Color.WhiteSmoke
            };
            main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

            var currentCard = CreateCardWithButton("Current Schedule", out lblCurrRoom, out lblCurrSubject, out lblCurrTime, out btnCheckIn);
            var nextCard = CreateCard("Next Schedule", out lblNextRoom, out lblNextSubject, out lblNextTime);

            main.Controls.Add(currentCard, 0, 0);
            main.Controls.Add(nextCard, 1, 0);

            _cardsPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.WhiteSmoke
            };
            _cardsPanel.Controls.Add(main);

            _attendanceHost = new Panel
            {
                Dock = DockStyle.Fill,
                Visible = false,
                BackColor = Color.WhiteSmoke
            };

            var contentHost = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.WhiteSmoke
            };
            contentHost.Controls.Add(_attendanceHost);
            contentHost.Controls.Add(_cardsPanel);

            root.Controls.Add(lblDayTitle, 0, 0);
            root.Controls.Add(contentHost, 0, 1);

            Controls.Add(root);
        }

        private Panel CreateCard(string title,
            out Label lblRoom, out Label lblSubject, out Label lblTime)
        {
            lblRoom = CreateValueLabel();
            lblSubject = CreateValueLabel();
            lblTime = CreateValueLabel();

            var header = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 32,
                TextAlign = ContentAlignment.MiddleLeft
            };

            var grid = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 3,
                Padding = new Padding(0, 8, 0, 0)
            };
            grid.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80));
            grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            grid.RowStyles.Add(new RowStyle(SizeType.Percent, 33));
            grid.RowStyles.Add(new RowStyle(SizeType.Percent, 33));
            grid.RowStyles.Add(new RowStyle(SizeType.Percent, 34));

            grid.Controls.Add(CreateLabel("Room"), 0, 0);
            grid.Controls.Add(lblRoom, 1, 0);
            grid.Controls.Add(CreateLabel("Subject"), 0, 1);
            grid.Controls.Add(lblSubject, 1, 1);
            grid.Controls.Add(CreateLabel("Time"), 0, 2);
            grid.Controls.Add(lblTime, 1, 2);

            var card = new Panel
            {
                BackColor = Color.White,
                Dock = DockStyle.Fill,
                Padding = new Padding(18),
                Margin = new Padding(12),
                BorderStyle = BorderStyle.FixedSingle
            };

            card.Controls.Add(grid);
            card.Controls.Add(header);

            return card;
        }

        private Panel CreateCardWithButton(string title,
            out Label lblRoom, out Label lblSubject, out Label lblTime, out Button btnAction)
        {
            var card = CreateCard(title, out lblRoom, out lblSubject, out lblTime);

            btnAction = new Button
            {
                Text = "Check In",
                Dock = DockStyle.Bottom,
                Height = 40,
                Enabled = false
            };
            btnAction.Click += BtnCheckIn_Click;

            card.Controls.Add(btnAction);
            return card;
        }

        private static Label CreateLabel(string text) =>
            new()
            {
                Text = text,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                ForeColor = Color.DimGray
            };

        private static Label CreateValueLabel() =>
            new()
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 37, 41)
            };

        #endregion

        #region Data loading

        private void RefreshScheduleCards()
        {
            var now = DateTime.Now;
            string today = now.DayOfWeek.ToString();
            lblDayTitle.Text = today;

            var assignments = LoadAssignments();
            var subjectLookup = BuildSubjectLookup();

            var todays = assignments
                .Where(a => string.Equals(a.Day, today, StringComparison.OrdinalIgnoreCase)
                            && a.TeacherId == _teacherId)
                .ToList();

            var current = FindCurrent(now, todays);
            var next = FindNext(now, todays);

            _currentAssignment = current;
            _currentSectionName = current != null ? SectionNameFromRoom(current.Room) : null;
            _scheduleKey = current != null && _teacherId.HasValue
                ? $"{current.Day}_{current.Slot}_R{current.Room}_T{_teacherId.Value}"
                : null;

            ApplySchedule(current, lblCurrRoom, lblCurrSubject, lblCurrTime, subjectLookup, emptyText: "No ongoing class");
            ApplySchedule(next, lblNextRoom, lblNextSubject, lblNextTime, subjectLookup, emptyText: "No upcoming class");

            btnCheckIn.Enabled = CanCheckIn(now, current);
            if (!btnCheckIn.Enabled && _attendanceHost.Visible && (_attendanceForm == null || _attendanceForm.IsDisposed))
            {
                ShowCards();
            }
        }

        private void ApplySchedule(AssignmentDto? assignment, Label lblRoom, Label lblSubject, Label lblTime,
            Dictionary<int, string> subjectLookup, string emptyText)
        {
            if (assignment == null)
            {
                lblRoom.Text = "—";
                lblSubject.Text = emptyText;
                lblTime.Text = "—";
                lblRoom.ForeColor = lblSubject.ForeColor = lblTime.ForeColor = Color.DimGray;
                return;
            }

            lblRoom.Text = $"Room {assignment.Room}";
            lblSubject.Text = subjectLookup.TryGetValue(assignment.SubjectId ?? 0, out var subj)
                ? subj
                : "N/A";
            lblTime.Text = _timeSlots.FirstOrDefault(t => t.Slot == assignment.Slot).Label;
            lblRoom.ForeColor = lblSubject.ForeColor = lblTime.ForeColor = Color.FromArgb(33, 37, 41);
        }

        private AssignmentDto? FindCurrent(DateTime now, List<AssignmentDto> todays)
        {
            var currentSlot = _timeSlots.FirstOrDefault(t =>
                now.TimeOfDay >= t.Start && now.TimeOfDay < t.End);
            if (currentSlot.Slot == 0)
                return null;

            return todays.FirstOrDefault(a => a.Slot == currentSlot.Slot);
        }

        private AssignmentDto? FindNext(DateTime now, List<AssignmentDto> todays)
        {
            return todays
                .Select(a => (a, slot: _timeSlots.FirstOrDefault(t => t.Slot == a.Slot)))
                .Where(x => x.slot.Slot != 0 && x.slot.Start > now.TimeOfDay)
                .OrderBy(x => x.slot.Start)
                .Select(x => x.a)
                .FirstOrDefault();
        }

        private bool CanCheckIn(DateTime now, AssignmentDto? assignment)
        {
            if (assignment == null) return false;
            var slot = _timeSlots.FirstOrDefault(t => t.Slot == assignment.Slot);
            if (slot.Slot == 0) return false;
            var start = slot.Start - TimeSpan.FromMinutes(10);
            var end = slot.End;
            return now.TimeOfDay >= start && now.TimeOfDay <= end;
        }

        private static string SectionNameFromRoom(int room) => room switch
        {
            1 => "Java",
            2 => "Python",
            3 => "Csharp",
            4 => "Rust",
            _ => "Unknown"
        };

        private void BtnCheckIn_Click(object? sender, EventArgs e)
        {
            if (_currentAssignment == null || !_teacherId.HasValue)
            {
                MessageBox.Show("No current schedule to check in.", "Check In", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var expectedQr = $"Room {_currentAssignment.Room} Section {SectionNameFromRoom(_currentAssignment.Room)}";

            using var form = new CheckInForm(expectedQr);
            if (form.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            var scanned = form.ScannedText?.Trim() ?? string.Empty;
            if (!string.Equals(scanned, expectedQr, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Invalid room or schedule. Please scan the correct room QR code.", "Check In", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Valid check-in -> show attendance module
            OpenAttendance();
        }

        private void OpenAttendance()
        {
            if (_currentAssignment == null)
                return;

            var subjectLookup = BuildSubjectLookup();
            var subjectName = subjectLookup.TryGetValue(_currentAssignment.SubjectId ?? 0, out var name)
                ? name
                : "N/A";

            var key = _scheduleKey ?? Guid.NewGuid().ToString();
            var section = _currentSectionName ?? "Unknown";

            _attendanceForm?.Dispose();
            _attendanceForm = new AttendanceForm(
                teacherName: _teacherName,
                sectionName: section,
                scheduleKey: key,
                day: _currentAssignment.Day,
                slot: _currentAssignment.Slot,
                room: _currentAssignment.Room,
                subjectName: subjectName);

            _attendanceForm.FormClosed += (_, _) => ShowCards();

            _attendanceHost.Controls.Clear();
            _attendanceForm.TopLevel = false;
            _attendanceForm.FormBorderStyle = FormBorderStyle.None;
            _attendanceForm.Dock = DockStyle.Fill;
            _attendanceHost.Controls.Add(_attendanceForm);
            _attendanceForm.Show();

            _cardsPanel.Visible = false;
            _attendanceHost.Visible = true;
        }

        private void ShowCards()
        {
            _attendanceHost.Visible = false;
            _cardsPanel.Visible = true;
        }

        private void ResolveTeacherId()
        {
            try
            {
                using var conn = Database.GetConnection();
                conn.Open();
                using var cmd = new MySqlCommand(
                    "SELECT idteachers_tbl FROM teachers_tbl WHERE CONCAT(LastName, ', ', FirstName) = @name LIMIT 1",
                    conn);
                cmd.Parameters.AddWithValue("@name", _teacherName);
                var result = cmd.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int id))
                {
                    _teacherId = id;
                }
            }
            catch
            {
                _teacherId = null;
            }
        }

        private List<AssignmentDto> LoadAssignments()
        {
            try
            {
                var dataFolder = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "SmartAttendance");
                var file = Path.Combine(dataFolder, "schedules.json");
                if (!File.Exists(file))
                    return new List<AssignmentDto>();

                var json = File.ReadAllText(file);
                return JsonSerializer.Deserialize<List<AssignmentDto>>(json) ?? new List<AssignmentDto>();
            }
            catch
            {
                return new List<AssignmentDto>();
            }
        }

        private Dictionary<int, string> BuildSubjectLookup()
        {
            var dict = new Dictionary<int, string>();
            for (int i = 0; i < _subjectNames.Length; i++)
                dict[i + 1] = _subjectNames[i];
            return dict;
        }

        #endregion
    }

    internal class AssignmentDto
    {
        public int Id { get; set; }
        public int Room { get; set; }
        public string Day { get; set; } = string.Empty;
        public int Slot { get; set; }
        public int? SubjectId { get; set; }
        public int? TeacherId { get; set; }
    }
}
