using System.Drawing;
using System.Windows.Forms;

namespace SmartAttendanceClassroomMonitoringVersion2.contents
{
    public class ReportsView : Form
    {
        public ReportsView()
        {
            Text = "Reports";
            BackColor = Color.WhiteSmoke;
            StartPosition = FormStartPosition.CenterScreen;
            InitializeLayout();
        }

        private void InitializeLayout()
        {
            var title = new Label
            {
                Text = "Reports will appear here.",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 50,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(16, 0, 0, 0)
            };

            var info = new Label
            {
                Text = "Display attendance summaries, performance, or export options in this section.",
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                Dock = DockStyle.Top,
                Height = 40,
                Padding = new Padding(16, 0, 0, 0),
                ForeColor = Color.DimGray
            };

            Controls.Add(info);
            Controls.Add(title);
        }
    }
}
