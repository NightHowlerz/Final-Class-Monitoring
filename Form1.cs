namespace SmartAttendanceClassroomMonitoringVersion2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object? sender, EventArgs e)
        {
            var username = txtUsername.Text.Trim();
            var password = txtPassword.Text;

            // First, try to match teacher credentials from the database.
            try
            {
                using var conn = Database.GetConnection();
                conn.Open();

                const string sql = @"SELECT name 
                                     FROM teacher_account_tbl 
                                     WHERE username = @username AND password = @password 
                                     LIMIT 1";

                using var cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                var result = cmd.ExecuteScalar();
                if (result is string teacherName && !string.IsNullOrWhiteSpace(teacherName))
                {
                    lblError.Text = string.Empty;
                    var teacherModule = new TeacherRecordModule(teacherName.Trim());
                    teacherModule.FormClosed += (_, __) => Close();
                    Hide();
                    teacherModule.Show();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Login error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // If no matching teacher account, fall back to the main (admin) view.
            lblError.Text = string.Empty;
            var main = new MainView(string.IsNullOrWhiteSpace(username) ? "Admin" : username);
            main.FormClosed += (_, __) => Close();
            Hide();
            main.Show();
        }
    }
}
