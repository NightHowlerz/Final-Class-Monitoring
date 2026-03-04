namespace SmartAttendanceClassroomMonitoringVersion2
{
    public partial class MainView : Form
    {
        private readonly string _username;
        private Form? _activeForm;

        public MainView(string username)
        {
            _username = username;
            InitializeComponent();
            lblUser.Text = _username;
        }

        private void MainView_Load(object? sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            LoadContent(new Dashboard());
        }

        private void SetActiveMenu(Button button)
        {
            foreach (Control ctrl in panelSidebar.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.BackColor = System.Drawing.Color.FromArgb(33, 37, 41);
                }
            }

            button.BackColor = System.Drawing.Color.FromArgb(52, 58, 64);
        }

        private void LoadContent(Form form)
        {
            _activeForm?.Close();
            _activeForm = form;
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            panelContent.Controls.Clear();
            panelContent.Controls.Add(form);
            form.Show();
            lblHeaderTitle.Text = form.Text;
        }

        private void btnDashboard_Click(object? sender, EventArgs e)
        {
            SetActiveMenu(btnDashboard);
            LoadContent(new Dashboard());
        }

        private void btnTeachers_Click(object? sender, EventArgs e)
        {
            SetActiveMenu(btnTeachers);
            LoadContent(new contents.TeacherForm());
        }

        private void btnStudents_Click(object? sender, EventArgs e)
        {
            SetActiveMenu(btnStudents);
            LoadContent(new contents.StudentsForm());
        }

        private void btnSubjects_Click(object? sender, EventArgs e)
        {
            SetActiveMenu(btnSubjects);
            LoadContent(new contents.SubjectsForm());
        }

        private void btnSchedule_Click(object? sender, EventArgs e)
        {
            SetActiveMenu(btnSchedule);
            LoadContent(new contents.SchedulesForm());
        }

        private void lblUser_Click(object? sender, EventArgs e)
        {
            userMenu.Show(lblUser, new System.Drawing.Point(lblUser.Width - userMenu.Width, lblUser.Height));
        }

        private void profileToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            LoadContent(new contents.ProfileForm());
        }

        private void logoutToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            Hide();
            var login = new Form1();
            login.FormClosed += (_, __) => Close();
            login.Show();
        }
    }
}
