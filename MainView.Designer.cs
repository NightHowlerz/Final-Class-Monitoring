namespace SmartAttendanceClassroomMonitoringVersion2
{
    partial class MainView
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private Panel panelSidebar;
        private Panel panelHeader;
        private Panel panelContent;
        private Label lblHeaderTitle;
        private Label lblUser;
        private Button btnDashboard;
        private Button btnTeachers;
        private Button btnStudents;
        private Button btnSubjects;
        private Button btnSchedule;
        private ContextMenuStrip userMenu;
        private ToolStripMenuItem profileToolStripMenuItem;
        private ToolStripMenuItem logoutToolStripMenuItem;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            panelSidebar = new Panel();
            btnSchedule = new Button();
            btnSubjects = new Button();
            btnStudents = new Button();
            btnTeachers = new Button();
            btnDashboard = new Button();
            panelHeader = new Panel();
            lblUser = new Label();
            lblHeaderTitle = new Label();
            panelContent = new Panel();
            userMenu = new ContextMenuStrip(components);
            profileToolStripMenuItem = new ToolStripMenuItem();
            logoutToolStripMenuItem = new ToolStripMenuItem();
            panelSidebar.SuspendLayout();
            panelHeader.SuspendLayout();
            userMenu.SuspendLayout();
            SuspendLayout();
            // 
            // panelSidebar
            // 
            panelSidebar.BackColor = Color.FromArgb(33, 37, 41);
            panelSidebar.Controls.Add(btnSchedule);
            panelSidebar.Controls.Add(btnSubjects);
            panelSidebar.Controls.Add(btnStudents);
            panelSidebar.Controls.Add(btnTeachers);
            panelSidebar.Controls.Add(btnDashboard);
            panelSidebar.Dock = DockStyle.Left;
            panelSidebar.Location = new Point(0, 0);
            panelSidebar.Name = "panelSidebar";
            panelSidebar.Size = new Size(180, 681);
            panelSidebar.TabIndex = 0;
            // 
            // btnSchedule
            // 
            btnSchedule.Dock = DockStyle.Top;
            btnSchedule.FlatAppearance.BorderSize = 0;
            btnSchedule.FlatStyle = FlatStyle.Flat;
            btnSchedule.ForeColor = Color.White;
            btnSchedule.Location = new Point(0, 200);
            btnSchedule.Name = "btnSchedule";
            btnSchedule.Size = new Size(180, 50);
            btnSchedule.TabIndex = 4;
            btnSchedule.Text = "Schedule";
            btnSchedule.UseVisualStyleBackColor = true;
            btnSchedule.Click += btnSchedule_Click;
            // 
            // btnSubjects
            // 
            btnSubjects.Dock = DockStyle.Top;
            btnSubjects.FlatAppearance.BorderSize = 0;
            btnSubjects.FlatStyle = FlatStyle.Flat;
            btnSubjects.ForeColor = Color.White;
            btnSubjects.Location = new Point(0, 150);
            btnSubjects.Name = "btnSubjects";
            btnSubjects.Size = new Size(180, 50);
            btnSubjects.TabIndex = 3;
            btnSubjects.Text = "Subjects";
            btnSubjects.UseVisualStyleBackColor = true;
            btnSubjects.Click += btnSubjects_Click;
            // 
            // btnStudents
            // 
            btnStudents.Dock = DockStyle.Top;
            btnStudents.FlatAppearance.BorderSize = 0;
            btnStudents.FlatStyle = FlatStyle.Flat;
            btnStudents.ForeColor = Color.White;
            btnStudents.Location = new Point(0, 100);
            btnStudents.Name = "btnStudents";
            btnStudents.Size = new Size(180, 50);
            btnStudents.TabIndex = 2;
            btnStudents.Text = "Students";
            btnStudents.UseVisualStyleBackColor = true;
            btnStudents.Click += btnStudents_Click;
            // 
            // btnTeachers
            // 
            btnTeachers.Dock = DockStyle.Top;
            btnTeachers.FlatAppearance.BorderSize = 0;
            btnTeachers.FlatStyle = FlatStyle.Flat;
            btnTeachers.ForeColor = Color.White;
            btnTeachers.Location = new Point(0, 50);
            btnTeachers.Name = "btnTeachers";
            btnTeachers.Size = new Size(180, 50);
            btnTeachers.TabIndex = 1;
            btnTeachers.Text = "Teachers";
            btnTeachers.UseVisualStyleBackColor = true;
            btnTeachers.Click += btnTeachers_Click;
            // 
            // btnDashboard
            // 
            btnDashboard.Dock = DockStyle.Top;
            btnDashboard.FlatAppearance.BorderSize = 0;
            btnDashboard.FlatStyle = FlatStyle.Flat;
            btnDashboard.ForeColor = Color.White;
            btnDashboard.Location = new Point(0, 0);
            btnDashboard.Name = "btnDashboard";
            btnDashboard.Size = new Size(180, 50);
            btnDashboard.TabIndex = 0;
            btnDashboard.Text = "Dashboard";
            btnDashboard.UseVisualStyleBackColor = true;
            btnDashboard.Click += btnDashboard_Click;
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.White;
            panelHeader.Controls.Add(lblUser);
            panelHeader.Controls.Add(lblHeaderTitle);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(180, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(904, 60);
            panelHeader.TabIndex = 1;
            // 
            // lblUser
            // 
            lblUser.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblUser.Cursor = Cursors.Hand;
            lblUser.Location = new Point(747, 18);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(145, 23);
            lblUser.TabIndex = 1;
            lblUser.Text = "Admin";
            lblUser.TextAlign = ContentAlignment.MiddleRight;
            lblUser.Click += lblUser_Click;
            // 
            // lblHeaderTitle
            // 
            lblHeaderTitle.AutoSize = true;
            lblHeaderTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblHeaderTitle.Location = new Point(463, 18);
            lblHeaderTitle.Name = "lblHeaderTitle";
            lblHeaderTitle.Size = new Size(109, 25);
            lblHeaderTitle.TabIndex = 0;
            lblHeaderTitle.Text = "Dashboard";
            // 
            // panelContent
            // 
            panelContent.BackColor = Color.WhiteSmoke;
            panelContent.Dock = DockStyle.Fill;
            panelContent.Location = new Point(180, 60);
            panelContent.Name = "panelContent";
            panelContent.Size = new Size(904, 621);
            panelContent.TabIndex = 2;
            // 
            // userMenu
            // 
            userMenu.Items.AddRange(new ToolStripItem[] { profileToolStripMenuItem, logoutToolStripMenuItem });
            userMenu.Name = "userMenu";
            userMenu.Size = new Size(113, 48);
            // 
            // profileToolStripMenuItem
            // 
            profileToolStripMenuItem.Name = "profileToolStripMenuItem";
            profileToolStripMenuItem.Size = new Size(112, 22);
            profileToolStripMenuItem.Text = "Profile";
            profileToolStripMenuItem.Click += profileToolStripMenuItem_Click;
            // 
            // logoutToolStripMenuItem
            // 
            logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            logoutToolStripMenuItem.Size = new Size(112, 22);
            logoutToolStripMenuItem.Text = "Logout";
            logoutToolStripMenuItem.Click += logoutToolStripMenuItem_Click;
            // 
            // MainView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1084, 681);
            Controls.Add(panelContent);
            Controls.Add(panelHeader);
            Controls.Add(panelSidebar);
            MinimumSize = new Size(900, 600);
            Name = "MainView";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Smart Attendance Monitoring System";
            Load += MainView_Load;
            panelSidebar.ResumeLayout(false);
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            userMenu.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion
    }
}
