using System.Data;
using MySql.Data.MySqlClient;

namespace SmartAttendanceClassroomMonitoringVersion2
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void Dashboard_Load(object? sender, EventArgs e)
        {
            LoadDashboard();
        }

        private void btnRefresh_Click(object? sender, EventArgs e)
        {
            LoadDashboard();
        }

        private void LoadDashboard()
        {
            LoadCounts();
            LoadRecentTeachers();
            LoadRecentStudents();
        }

        private void LoadCounts()
        {
            lblTeachersCount.Text = SafeCount("teachers_tbl");
            lblStudentsCount.Text = SafeCount("students_tbl");
            lblSubjectsCount.Text = SafeCount("subjects_tbl");     // placeholder table name
            lblSchedulesCount.Text = SafeCount("schedules_tbl");   // placeholder table name
        }

        private string SafeCount(string tableName)
        {
            try
            {
                using var conn = Database.GetConnection();
                conn.Open();
                using var cmd = new MySqlCommand($"SELECT COUNT(*) FROM {tableName}", conn);
                var result = cmd.ExecuteScalar();
                return Convert.ToInt32(result).ToString();
            }
            catch
            {
                return "N/A";
            }
        }

        private void LoadRecentTeachers()
        {
            try
            {
                using var conn = Database.GetConnection();
                conn.Open();
                var sql = @"SELECT FirstName, LastName, Gender 
                            FROM teachers_tbl 
                            ORDER BY idteachers_tbl DESC 
                            LIMIT 5";
                using var cmd = new MySqlCommand(sql, conn);
                using var da = new MySqlDataAdapter(cmd);
                var table = new DataTable();
                da.Fill(table);
                dgvRecentTeachers.DataSource = table;
            }
            catch
            {
                dgvRecentTeachers.DataSource = null;
            }
        }

        private void LoadRecentStudents()
        {
            try
            {
                using var conn = Database.GetConnection();
                conn.Open();
                var sql = @"SELECT FirstName, LastName, Section, YearLevel 
                            FROM students_tbl 
                            ORDER BY idstudents_tbl DESC 
                            LIMIT 5";
                using var cmd = new MySqlCommand(sql, conn);
                using var da = new MySqlDataAdapter(cmd);
                var table = new DataTable();
                da.Fill(table);
                dgvRecentStudents.DataSource = table;
            }
            catch
            {
                dgvRecentStudents.DataSource = null;
            }
        }
    }
}
