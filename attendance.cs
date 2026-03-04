using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SmartAttendanceClassroomMonitoringVersion2
{
    public class AttendanceForm : Form
    {
        private readonly string _teacherName;
        private readonly string _sectionName;
        private readonly string _scheduleKey;
        private readonly string _day;
        private readonly int _slot;
        private readonly int _room;
        private readonly string _subjectName;

        private readonly string _dataFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "SmartAttendance",
            "attendance");

        private readonly List<StudentRow> _students = new();
        private readonly List<AttendanceEntry> _entries = new();

        private ListView lvScan = null!;
        private TextBox txtScan = null!;
        private ListView lvManual = null!;

        public AttendanceForm(string teacherName, string sectionName, string scheduleKey, string day, int slot, int room, string subjectName)
        {
            _teacherName = teacherName;
            _sectionName = sectionName;
            _scheduleKey = scheduleKey;
            _day = day;
            _slot = slot;
            _room = room;
            _subjectName = subjectName;

            Text = "Attendance";
            BackColor = Color.WhiteSmoke;
            FormBorderStyle = FormBorderStyle.None;
            BuildUi();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadStudents();
            LoadEntries();
            PopulateManual();
            PopulateScan();
            txtScan.Focus();
        }

        #region UI
        private void BuildUi()
        {
            var header = new Label
            {
                Text = $"Attendance - {_sectionName} | Room {_room} | {_subjectName}",
                Dock = DockStyle.Top,
                Height = 36,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(12, 8, 12, 0)
            };

            var tabs = new TabControl
            {
                Dock = DockStyle.Fill
            };

            var tabScan = new TabPage("Scan");
            var tabManual = new TabPage("Manual");

            tabs.TabPages.Add(tabScan);
            tabs.TabPages.Add(tabManual);

            // Scan tab
            lvScan = new ListView
            {
                Dock = DockStyle.Fill,
                View = View.Details,
                FullRowSelect = true,
                GridLines = true
            };
            lvScan.Columns.Add("Name", 200);
            lvScan.Columns.Add("Section", 120);
            lvScan.Columns.Add("Source", 120);

            txtScan = new TextBox
            {
                Dock = DockStyle.Top,
                PlaceholderText = "Scan student QR / type and press Enter",
                Height = 28
            };
            txtScan.KeyDown += TxtScan_KeyDown;

            var scanPanel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(12) };
            scanPanel.Controls.Add(lvScan);
            scanPanel.Controls.Add(txtScan);
            tabScan.Controls.Add(scanPanel);

            // Manual tab
            lvManual = new ListView
            {
                Dock = DockStyle.Fill,
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                CheckBoxes = true
            };
            lvManual.Columns.Add("First Name", 140);
            lvManual.Columns.Add("Last Name", 140);
            lvManual.Columns.Add("Section", 120);
            lvManual.ItemChecked += LvManual_ItemChecked;

            var manualPanel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(12) };
            manualPanel.Controls.Add(lvManual);
            tabManual.Controls.Add(manualPanel);

            Controls.Add(tabs);
            Controls.Add(header);
        }
        #endregion

        #region Data loading
        private void LoadStudents()
        {
            try
            {
                using var conn = Database.GetConnection();
                conn.Open();
                using var cmd = new MySqlCommand(
                    "SELECT idstudents_tbl, FirstName, LastName, Section, LRN FROM students_tbl WHERE Section = @section ORDER BY LastName, FirstName",
                    conn);
                cmd.Parameters.AddWithValue("@section", _sectionName);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    _students.Add(new StudentRow(
                        reader.GetInt32("idstudents_tbl"),
                        reader.GetString("FirstName"),
                        reader.GetString("LastName"),
                        reader["Section"]?.ToString() ?? string.Empty,
                        reader["LRN"]?.ToString() ?? string.Empty));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to load students: {ex.Message}", "Attendance", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadEntries()
        {
            try
            {
                Directory.CreateDirectory(_dataFolder);
                var file = GetDataFile();
                if (!File.Exists(file))
                    return;

                var json = File.ReadAllText(file);
                var data = JsonSerializer.Deserialize<List<AttendanceEntry>>(json);
                if (data != null)
                    _entries.AddRange(data);
            }
            catch
            {
                // ignore parse errors, start fresh
            }
        }
        #endregion

        #region Populate UI
        private void PopulateManual()
        {
            lvManual.BeginUpdate();
            lvManual.Items.Clear();
            foreach (var s in _students)
            {
                var item = new ListViewItem(new[] { s.FirstName, s.LastName, s.Section })
                {
                    Tag = s,
                    Checked = IsPresent(s.Id)
                };
                lvManual.Items.Add(item);
            }
            lvManual.EndUpdate();
        }

        private void PopulateScan()
        {
            lvScan.BeginUpdate();
            lvScan.Items.Clear();
            foreach (var e in _entries.Where(x => x.Status == "Present"))
            {
                var item = new ListViewItem(e.DisplayName)
                {
                    Tag = e
                };
                item.SubItems.Add(e.Section);
                item.SubItems.Add(e.Source ?? "Scan");
                lvScan.Items.Add(item);
            }
            lvScan.EndUpdate();
        }
        #endregion

        #region Event handlers
        private void TxtScan_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            e.SuppressKeyPress = true;

            var text = txtScan.Text.Trim();
            if (string.IsNullOrWhiteSpace(text)) return;

            AddScan(text);
            txtScan.Clear();
        }

        private void LvManual_ItemChecked(object? sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Tag is not StudentRow student) return;
            var entry = _entries.FirstOrDefault(x => x.StudentId == student.Id) ??
                        new AttendanceEntry { StudentId = student.Id, FirstName = student.FirstName, LastName = student.LastName, Section = student.Section };

            entry.Status = e.Item.Checked ? "Present" : "Absent";
            entry.Source = "Manual";

            if (!_entries.Contains(entry))
                _entries.Add(entry);

            SaveEntries();
            PopulateScan(); // reflect in scan list for present students
        }
        #endregion

        #region Attendance logic
        private void AddScan(string scanned)
        {
            var student = MatchStudent(scanned);
            AttendanceEntry entry;

            if (student != null)
            {
                entry = _entries.FirstOrDefault(x => x.StudentId == student.Id) ??
                        new AttendanceEntry
                        {
                            StudentId = student.Id,
                            FirstName = student.FirstName,
                            LastName = student.LastName,
                            Section = student.Section
                        };
                entry.Status = "Present";
                entry.Source = "Scan";
            }
            else
            {
                entry = _entries.FirstOrDefault(x => string.Equals(x.DisplayName, scanned, StringComparison.OrdinalIgnoreCase)) ??
                        new AttendanceEntry { FirstName = scanned, LastName = string.Empty, Section = _sectionName };
                entry.Status = "Present";
                entry.Source = "Scan";
            }

            if (!_entries.Contains(entry))
                _entries.Add(entry);

            SaveEntries();
            PopulateManual();
            PopulateScan();
        }

        private StudentRow? MatchStudent(string scanned)
        {
            // Match against LRN, "LastName, FirstName", or FirstName LastName
            var byLrn = _students.FirstOrDefault(s => !string.IsNullOrWhiteSpace(s.LRN) &&
                                                      string.Equals(s.LRN, scanned, StringComparison.OrdinalIgnoreCase));
            if (byLrn != null) return byLrn;

            var byFormatted = _students.FirstOrDefault(s =>
                string.Equals($"{s.LastName}, {s.FirstName}", scanned, StringComparison.OrdinalIgnoreCase));
            if (byFormatted != null) return byFormatted;

            var bySimple = _students.FirstOrDefault(s =>
                string.Equals($"{s.FirstName} {s.LastName}", scanned, StringComparison.OrdinalIgnoreCase));
            return bySimple;
        }

        private bool IsPresent(int studentId)
        {
            return _entries.Any(e => e.StudentId == studentId && e.Status == "Present");
        }

        private void SaveEntries()
        {
            Directory.CreateDirectory(_dataFolder);
            var file = GetDataFile();
            var json = JsonSerializer.Serialize(_entries, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(file, json);
        }

        private string GetDataFile() => Path.Combine(_dataFolder, $"{_scheduleKey}.json");
        #endregion
    }

    internal record StudentRow(int Id, string FirstName, string LastName, string Section, string LRN);

    internal class AttendanceEntry
    {
        public int? StudentId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Section { get; set; } = string.Empty;
        public string Status { get; set; } = "Present";
        public string? Source { get; set; }
        public string DisplayName => string.IsNullOrWhiteSpace(LastName)
            ? FirstName
            : $"{LastName}, {FirstName}";
    }
}
