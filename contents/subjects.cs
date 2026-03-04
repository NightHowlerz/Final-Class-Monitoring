using System.Data;
using MySql.Data.MySqlClient;

namespace SmartAttendanceClassroomMonitoringVersion2.contents
{
    public partial class SubjectsForm : Form
    {
        private class Teacher
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        private List<Teacher> teachers = new();
        private Dictionary<string, (Label lbl, ComboBox cb1, ComboBox cb2, ComboBox cb3, Button btn)> subjectControls = new();
        private string[] subjects = { "English", "Filipino", "Mathematics", "Science", "Araling Panlipunan", "MAPEH", "ICT" };
        private bool isUpdatingFilter = false;

        // Map subject names to database column names
        private Dictionary<string, string> subjectColumnMap = new()
        {
            { "English", "english" },
            { "Filipino", "filipino" },
            { "Mathematics", "math" },
            { "Science", "science" },
            { "Araling Panlipunan", "araling_panlipunan" },
            { "MAPEH", "mapeh" },
            { "ICT", "ict" }
        };

        public SubjectsForm()
        {
            InitializeComponent();
            CreateSubjectPanels();
            LoadTeachers();
            LoadAssignments();
        }

        private void CreateSubjectPanels()
        {
            int y = 10;
            foreach (var subject in subjects)
            {
                var panel = new Panel { Location = new Point(10, y), Size = new Size(780, 50), BorderStyle = BorderStyle.FixedSingle };
                var lbl = new Label { Text = subject, Location = new Point(10, 15), Size = new Size(150, 20) };
                var cb1 = new ComboBox { Location = new Point(170, 10), Size = new Size(150, 20) };
                var cb2 = new ComboBox { Location = new Point(330, 10), Size = new Size(150, 20) };
                var cb3 = new ComboBox { Location = new Point(490, 10), Size = new Size(150, 20) };
                var btn = new Button { Text = "Save", Location = new Point(650, 10), Size = new Size(100, 30) };
                btn.Click += (s, e) => SaveSubject(subject);

                // Add event handlers for filtering duplicate teachers
                cb1.SelectedValueChanged += (s, e) => UpdateTeacherFiltering(subject);
                cb2.SelectedValueChanged += (s, e) => UpdateTeacherFiltering(subject);
                cb3.SelectedValueChanged += (s, e) => UpdateTeacherFiltering(subject);

                panel.Controls.AddRange(new Control[] { lbl, cb1, cb2, cb3, btn });
                panelContainer.Controls.Add(panel);
                subjectControls[subject] = (lbl, cb1, cb2, cb3, btn);
                y += 60;
            }
        }

        private void LoadTeachers()
        {
            try
            {
                using var conn = Database.GetConnection();
                conn.Open();
                var cmd = new MySqlCommand("SELECT idteachers_tbl, CONCAT(LastName, ', ', FirstName) as Name FROM teachers_tbl ORDER BY LastName, FirstName", conn);
                using var reader = cmd.ExecuteReader();
                teachers.Clear();
                teachers.Add(new Teacher { Id = 0, Name = "Select Teacher" });
                while (reader.Read())
                {
                    teachers.Add(new Teacher { Id = reader.GetInt32(0), Name = reader.GetString(1) });
                }
                foreach (var ctrl in subjectControls.Values)
                {
                    ctrl.cb1.DataSource = new BindingSource(teachers, null);
                    ctrl.cb1.DisplayMember = "Name";
                    ctrl.cb1.ValueMember = "Id";
                    ctrl.cb2.DataSource = new BindingSource(teachers, null);
                    ctrl.cb2.DisplayMember = "Name";
                    ctrl.cb2.ValueMember = "Id";
                    ctrl.cb3.DataSource = new BindingSource(teachers, null);
                    ctrl.cb3.DisplayMember = "Name";
                    ctrl.cb3.ValueMember = "Id";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading teachers: " + ex.Message);
            }
        }

        private void LoadAssignments()
        {
            try
            {
                using var conn = Database.GetConnection();
                conn.Open();
                var cmd = new MySqlCommand("SELECT * FROM subjects_tbl WHERE idsubjects_tbl = 1", conn);
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    foreach (var subject in subjects)
                    {
                        string col = subjectColumnMap[subject];

                        // Check if column exists in the result
                        if (!reader.HasColumn(col))
                            continue;

                        var value = reader[col].ToString() ?? "";
                        var ids = value.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(s => int.TryParse(s, out var id) ? id : 0).ToList();
                        var ctrl = subjectControls[subject];
                        ctrl.cb1.SelectedValue = ids.Count > 0 ? ids[0] : 0;
                        ctrl.cb2.SelectedValue = ids.Count > 1 ? ids[1] : 0;
                        ctrl.cb3.SelectedValue = ids.Count > 2 ? ids[2] : 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading assignments: " + ex.Message);
            }
        }

        private void SaveSubject(string subject)
        {
            try
            {
                var ctrl = subjectControls[subject];
                var ids = new List<int>();
                int id1 = SafeGetSelectedId(ctrl.cb1);
                int id2 = SafeGetSelectedId(ctrl.cb2);
                int id3 = SafeGetSelectedId(ctrl.cb3);

                if (id1 != 0) ids.Add(id1);
                if (id2 != 0) ids.Add(id2);
                if (id3 != 0) ids.Add(id3);

                var value = string.Join(",", ids);
                using var conn = Database.GetConnection();
                conn.Open();
                string col = subjectColumnMap[subject];
                var cmd = new MySqlCommand($"INSERT INTO subjects_tbl (idsubjects_tbl, {col}) VALUES (1, @val) ON DUPLICATE KEY UPDATE {col} = @val", conn);
                cmd.Parameters.AddWithValue("@val", value);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Saved successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving: " + ex.Message);
            }
        }

        private void UpdateTeacherFiltering(string subject)
        {
            try
            {
                // Prevent recursive filtering updates
                if (isUpdatingFilter)
                    return;

                isUpdatingFilter = true;

                if (!subjectControls.ContainsKey(subject))
                    return;

                var ctrl = subjectControls[subject];

                // Get current selections
                int selectedId1 = SafeGetSelectedId(ctrl.cb1);
                int selectedId2 = SafeGetSelectedId(ctrl.cb2);
                int selectedId3 = SafeGetSelectedId(ctrl.cb3);

                // If cb1 changed, update cb2 and cb3
                var filteredList2 = teachers.Where(t => t.Id == 0 || t.Id != selectedId1).ToList();

                // Only update cb2 datasource if needed
                if (!BindingSourceEquals(ctrl.cb2.DataSource as BindingSource, filteredList2))
                {
                    ctrl.cb2.DataSource = new BindingSource(filteredList2, null);
                    ctrl.cb2.DisplayMember = "Name";
                    ctrl.cb2.ValueMember = "Id";
                    // Restore previous selection when still allowed; otherwise clear.
                    ctrl.cb2.SelectedValue = filteredList2.Any(t => t.Id == selectedId2) ? selectedId2 : 0;
                }

                // Update cb3 based on cb1 and cb2
                selectedId2 = SafeGetSelectedId(ctrl.cb2); // Get updated value
                var filteredList3 = teachers.Where(t => t.Id == 0 || (t.Id != selectedId1 && t.Id != selectedId2)).ToList();

                // Only update cb3 datasource if needed
                if (!BindingSourceEquals(ctrl.cb3.DataSource as BindingSource, filteredList3))
                {
                    ctrl.cb3.DataSource = new BindingSource(filteredList3, null);
                    ctrl.cb3.DisplayMember = "Name";
                    ctrl.cb3.ValueMember = "Id";
                    // Restore previous selection when still allowed; otherwise clear.
                    ctrl.cb3.SelectedValue = filteredList3.Any(t => t.Id == selectedId3) ? selectedId3 : 0;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Filtering error: " + ex.Message);
            }
            finally
            {
                isUpdatingFilter = false;
            }
        }

        private bool BindingSourceEquals(BindingSource source, List<Teacher> list)
        {
            if (source?.DataSource is not List<Teacher> sourceList)
                return false;
            return sourceList.Count == list.Count && sourceList.SequenceEqual(list, new TeacherComparer());
        }

        private class TeacherComparer : IEqualityComparer<Teacher>
        {
            public bool Equals(Teacher x, Teacher y) => x?.Id == y?.Id;
            public int GetHashCode(Teacher obj) => obj?.Id.GetHashCode() ?? 0;
        }

        private int SafeGetSelectedId(ComboBox comboBox)
        {
            try
            {
                if (comboBox.SelectedValue == null)
                    return 0;

                if (comboBox.SelectedValue is int intValue)
                    return intValue;

                if (int.TryParse(comboBox.SelectedValue.ToString(), out int result))
                    return result;

                return 0;
            }
            catch
            {
                return 0;
            }
        }

        private void panelContainer_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

// Extension method to check if a column exists in SqlDataReader
public static class SqlDataReaderExtensions
{
    public static bool HasColumn(this MySql.Data.MySqlClient.MySqlDataReader reader, string columnName)
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
}
