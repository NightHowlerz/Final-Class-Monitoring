using System.Drawing;
using System.Windows.Forms;

namespace SmartAttendanceClassroomMonitoringVersion2
{
    public partial class TeacherRecordModule : Form
    {
        private readonly string _teacherName;
        private Form? _activeForm;

        public TeacherRecordModule(string teacherName)
        {
            _teacherName = string.IsNullOrWhiteSpace(teacherName) ? "Teacher" : teacherName;
            InitializeComponent();
            lblTeacherName.Text = _teacherName;
        }

        private void TeacherRecordModule_Load(object? sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            SetActiveMenu(btnClass);
            LoadContent(new contents.ClassView(_teacherName));
        }

        private void SetActiveMenu(Button button)
        {
            foreach (Control ctrl in panelSidebar.Controls)
            {
                if (ctrl is Button btn && btn != btnLogout)
                {
                    btn.BackColor = Color.FromArgb(33, 37, 41);
                }
            }

            button.BackColor = Color.FromArgb(52, 58, 64);
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

        private void btnClass_Click(object? sender, EventArgs e)
        {
            SetActiveMenu(btnClass);
            LoadContent(new contents.ClassView(_teacherName));
        }

        private void btnReports_Click(object? sender, EventArgs e)
        {
            SetActiveMenu(btnReports);
            LoadContent(new contents.ReportsView());
        }

        private void btnLogout_Click(object? sender, EventArgs e)
        {
            var confirm = MessageBox.Show("Log out from teacher portal?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            Hide();
            var login = new Form1();
            login.FormClosed += (_, __) => Close();
            login.Show();
        }

        private void lblTeacherName_Click(object? sender, EventArgs e)
        {
            teacherMenu.Show(lblTeacherName, new Point(lblTeacherName.Width - teacherMenu.Width, lblTeacherName.Height));
        }

        private void profileToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            LoadContent(new contents.ProfileForm());
        }
    }
}
