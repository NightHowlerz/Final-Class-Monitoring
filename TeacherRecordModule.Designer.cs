using System.Drawing;
using System.Windows.Forms;

namespace SmartAttendanceClassroomMonitoringVersion2
{
    partial class TeacherRecordModule
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer? components = null;

        private Panel panelSidebar;
        private Panel panelHeader;
        private Panel panelContent;
        private Button btnClass;
        private Button btnReports;
        private Button btnLogout;
        private Label lblHeaderTitle;
        private Label lblTeacherName;
        private ContextMenuStrip teacherMenu;
        private ToolStripMenuItem profileToolStripMenuItem;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            panelSidebar = new Panel();
            btnLogout = new Button();
            btnReports = new Button();
            btnClass = new Button();
            panelHeader = new Panel();
            lblTeacherName = new Label();
            lblHeaderTitle = new Label();
            panelContent = new Panel();
            teacherMenu = new ContextMenuStrip(components);
            profileToolStripMenuItem = new ToolStripMenuItem();
            panelSidebar.SuspendLayout();
            panelHeader.SuspendLayout();
            teacherMenu.SuspendLayout();
            SuspendLayout();
            // 
            // panelSidebar
            // 
            panelSidebar.BackColor = Color.FromArgb(33, 37, 41);
            panelSidebar.Controls.Add(btnReports);
            panelSidebar.Controls.Add(btnClass);
            panelSidebar.Controls.Add(btnLogout);
            panelSidebar.Dock = DockStyle.Left;
            panelSidebar.Location = new Point(0, 0);
            panelSidebar.Name = "panelSidebar";
            panelSidebar.Padding = new Padding(0);
            panelSidebar.Size = new Size(180, 681);
            panelSidebar.TabIndex = 0;
            // 
            // btnLogout
            // 
            btnLogout.Dock = DockStyle.Bottom;
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.ForeColor = Color.White;
            btnLogout.Location = new Point(0, 631);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(180, 50);
            btnLogout.TabIndex = 3;
            btnLogout.Text = "Log Out";
            btnLogout.UseVisualStyleBackColor = true;
            btnLogout.Click += btnLogout_Click;
            // 
            // btnReports
            // 
            btnReports.Dock = DockStyle.Top;
            btnReports.FlatAppearance.BorderSize = 0;
            btnReports.FlatStyle = FlatStyle.Flat;
            btnReports.ForeColor = Color.White;
            btnReports.Location = new Point(0, 50);
            btnReports.Name = "btnReports";
            btnReports.Size = new Size(180, 50);
            btnReports.TabIndex = 2;
            btnReports.Text = "Reports";
            btnReports.UseVisualStyleBackColor = true;
            btnReports.Click += btnReports_Click;
            // 
            // btnClass
            // 
            btnClass.Dock = DockStyle.Top;
            btnClass.FlatAppearance.BorderSize = 0;
            btnClass.FlatStyle = FlatStyle.Flat;
            btnClass.ForeColor = Color.White;
            btnClass.Location = new Point(0, 0);
            btnClass.Name = "btnClass";
            btnClass.Size = new Size(180, 50);
            btnClass.TabIndex = 1;
            btnClass.Text = "Class";
            btnClass.UseVisualStyleBackColor = true;
            btnClass.Click += btnClass_Click;
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.White;
            panelHeader.Controls.Add(lblTeacherName);
            panelHeader.Controls.Add(lblHeaderTitle);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(180, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(904, 60);
            panelHeader.TabIndex = 1;
            // 
            // lblTeacherName
            // 
            lblTeacherName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblTeacherName.Cursor = Cursors.Hand;
            lblTeacherName.Location = new Point(704, 18);
            lblTeacherName.Name = "lblTeacherName";
            lblTeacherName.Size = new Size(185, 23);
            lblTeacherName.TabIndex = 1;
            lblTeacherName.Text = "Teacher Name";
            lblTeacherName.TextAlign = ContentAlignment.MiddleRight;
            lblTeacherName.Click += lblTeacherName_Click;
            // 
            // lblHeaderTitle
            // 
            lblHeaderTitle.AutoSize = true;
            lblHeaderTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblHeaderTitle.Location = new Point(18, 18);
            lblHeaderTitle.Name = "lblHeaderTitle";
            lblHeaderTitle.Size = new Size(58, 25);
            lblHeaderTitle.TabIndex = 0;
            lblHeaderTitle.Text = "Class";
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
            // teacherMenu
            // 
            teacherMenu.Items.AddRange(new ToolStripItem[] { profileToolStripMenuItem });
            teacherMenu.Name = "teacherMenu";
            teacherMenu.Size = new Size(113, 26);
            // 
            // profileToolStripMenuItem
            // 
            profileToolStripMenuItem.Name = "profileToolStripMenuItem";
            profileToolStripMenuItem.Size = new Size(112, 22);
            profileToolStripMenuItem.Text = "Profile";
            profileToolStripMenuItem.Click += profileToolStripMenuItem_Click;
            // 
            // TeacherRecordModule
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1084, 681);
            Controls.Add(panelContent);
            Controls.Add(panelHeader);
            Controls.Add(panelSidebar);
            MinimumSize = new Size(900, 600);
            Name = "TeacherRecordModule";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Teacher Record Module";
            Load += TeacherRecordModule_Load;
            panelSidebar.ResumeLayout(false);
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            teacherMenu.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
    }
}
