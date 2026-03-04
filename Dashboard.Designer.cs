namespace SmartAttendanceClassroomMonitoringVersion2
{
    partial class Dashboard
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private Label lblHeader;
        private FlowLayoutPanel flowMetrics;
        private Panel cardTeachers;
        private Panel cardStudents;
        private Panel cardSubjects;
        private Panel cardSchedules;
        private Label lblTeachersCount;
        private Label lblStudentsCount;
        private Label lblSubjectsCount;
        private Label lblSchedulesCount;
        private Label lblTeachersTitle;
        private Label lblStudentsTitle;
        private Label lblSubjectsTitle;
        private Label lblSchedulesTitle;
        private Panel panelRecentTeachers;
        private Panel panelRecentStudents;
        private DataGridView dgvRecentTeachers;
        private DataGridView dgvRecentStudents;
        private Label lblRecentTeachers;
        private Label lblRecentStudents;
        private Button btnRefresh;
        private TableLayoutPanel mainLayout;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblHeader = new Label();
            flowMetrics = new FlowLayoutPanel();
            cardTeachers = new Panel();
            lblTeachersCount = new Label();
            lblTeachersTitle = new Label();
            cardStudents = new Panel();
            lblStudentsCount = new Label();
            lblStudentsTitle = new Label();
            cardSubjects = new Panel();
            lblSubjectsCount = new Label();
            lblSubjectsTitle = new Label();
            cardSchedules = new Panel();
            lblSchedulesCount = new Label();
            lblSchedulesTitle = new Label();
            panelRecentTeachers = new Panel();
            dgvRecentTeachers = new DataGridView();
            lblRecentTeachers = new Label();
            panelRecentStudents = new Panel();
            dgvRecentStudents = new DataGridView();
            lblRecentStudents = new Label();
            btnRefresh = new Button();
            mainLayout = new TableLayoutPanel();
            flowMetrics.SuspendLayout();
            cardTeachers.SuspendLayout();
            cardStudents.SuspendLayout();
            cardSubjects.SuspendLayout();
            cardSchedules.SuspendLayout();
            panelRecentTeachers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRecentTeachers).BeginInit();
            panelRecentStudents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRecentStudents).BeginInit();
            mainLayout.SuspendLayout();
            SuspendLayout();
            // 
            // lblHeader
            // 
            lblHeader.AutoSize = true;
            lblHeader.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point);
            lblHeader.Location = new Point(18, 18);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new Size(121, 30);
            lblHeader.TabIndex = 0;
            lblHeader.Text = "Dashboard";
            // 
            // flowMetrics
            // 
            flowMetrics.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            flowMetrics.Controls.Add(cardTeachers);
            flowMetrics.Controls.Add(cardStudents);
            flowMetrics.Controls.Add(cardSubjects);
            flowMetrics.Controls.Add(cardSchedules);
            flowMetrics.Location = new Point(18, 60);
            flowMetrics.Name = "flowMetrics";
            flowMetrics.Size = new Size(864, 110);
            flowMetrics.TabIndex = 1;
            // 
            // cardTeachers
            // 
            cardTeachers.BackColor = Color.FromArgb(76, 132, 255);
            cardTeachers.Controls.Add(lblTeachersCount);
            cardTeachers.Controls.Add(lblTeachersTitle);
            cardTeachers.ForeColor = Color.White;
            cardTeachers.Margin = new Padding(0, 0, 10, 0);
            cardTeachers.Name = "cardTeachers";
            cardTeachers.Size = new Size(200, 100);
            cardTeachers.TabIndex = 0;
            // 
            // lblTeachersCount
            // 
            lblTeachersCount.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            lblTeachersCount.AutoSize = true;
            lblTeachersCount.Font = new Font("Segoe UI", 22F, FontStyle.Bold, GraphicsUnit.Point);
            lblTeachersCount.Location = new Point(14, 40);
            lblTeachersCount.Name = "lblTeachersCount";
            lblTeachersCount.Size = new Size(51, 41);
            lblTeachersCount.TabIndex = 1;
            lblTeachersCount.Text = "0";
            // 
            // lblTeachersTitle
            // 
            lblTeachersTitle.AutoSize = true;
            lblTeachersTitle.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Regular, GraphicsUnit.Point);
            lblTeachersTitle.Location = new Point(14, 12);
            lblTeachersTitle.Name = "lblTeachersTitle";
            lblTeachersTitle.Size = new Size(72, 20);
            lblTeachersTitle.TabIndex = 0;
            lblTeachersTitle.Text = "Teachers";
            // 
            // cardStudents
            // 
            cardStudents.BackColor = Color.FromArgb(45, 206, 137);
            cardStudents.Controls.Add(lblStudentsCount);
            cardStudents.Controls.Add(lblStudentsTitle);
            cardStudents.ForeColor = Color.White;
            cardStudents.Margin = new Padding(0, 0, 10, 0);
            cardStudents.Name = "cardStudents";
            cardStudents.Size = new Size(200, 100);
            cardStudents.TabIndex = 2;
            // 
            // lblStudentsCount
            // 
            lblStudentsCount.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            lblStudentsCount.AutoSize = true;
            lblStudentsCount.Font = new Font("Segoe UI", 22F, FontStyle.Bold, GraphicsUnit.Point);
            lblStudentsCount.Location = new Point(14, 40);
            lblStudentsCount.Name = "lblStudentsCount";
            lblStudentsCount.Size = new Size(51, 41);
            lblStudentsCount.TabIndex = 1;
            lblStudentsCount.Text = "0";
            // 
            // lblStudentsTitle
            // 
            lblStudentsTitle.AutoSize = true;
            lblStudentsTitle.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Regular, GraphicsUnit.Point);
            lblStudentsTitle.Location = new Point(14, 12);
            lblStudentsTitle.Name = "lblStudentsTitle";
            lblStudentsTitle.Size = new Size(69, 20);
            lblStudentsTitle.TabIndex = 0;
            lblStudentsTitle.Text = "Students";
            // 
            // cardSubjects
            // 
            cardSubjects.BackColor = Color.FromArgb(255, 174, 79);
            cardSubjects.Controls.Add(lblSubjectsCount);
            cardSubjects.Controls.Add(lblSubjectsTitle);
            cardSubjects.ForeColor = Color.White;
            cardSubjects.Margin = new Padding(0, 0, 10, 0);
            cardSubjects.Name = "cardSubjects";
            cardSubjects.Size = new Size(200, 100);
            cardSubjects.TabIndex = 3;
            // 
            // lblSubjectsCount
            // 
            lblSubjectsCount.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            lblSubjectsCount.AutoSize = true;
            lblSubjectsCount.Font = new Font("Segoe UI", 22F, FontStyle.Bold, GraphicsUnit.Point);
            lblSubjectsCount.Location = new Point(14, 40);
            lblSubjectsCount.Name = "lblSubjectsCount";
            lblSubjectsCount.Size = new Size(51, 41);
            lblSubjectsCount.TabIndex = 1;
            lblSubjectsCount.Text = "0";
            // 
            // lblSubjectsTitle
            // 
            lblSubjectsTitle.AutoSize = true;
            lblSubjectsTitle.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Regular, GraphicsUnit.Point);
            lblSubjectsTitle.Location = new Point(14, 12);
            lblSubjectsTitle.Name = "lblSubjectsTitle";
            lblSubjectsTitle.Size = new Size(64, 20);
            lblSubjectsTitle.TabIndex = 0;
            lblSubjectsTitle.Text = "Subjects";
            // 
            // cardSchedules
            // 
            cardSchedules.BackColor = Color.FromArgb(133, 102, 255);
            cardSchedules.Controls.Add(lblSchedulesCount);
            cardSchedules.Controls.Add(lblSchedulesTitle);
            cardSchedules.ForeColor = Color.White;
            cardSchedules.Margin = new Padding(0);
            cardSchedules.Name = "cardSchedules";
            cardSchedules.Size = new Size(200, 100);
            cardSchedules.TabIndex = 4;
            // 
            // lblSchedulesCount
            // 
            lblSchedulesCount.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            lblSchedulesCount.AutoSize = true;
            lblSchedulesCount.Font = new Font("Segoe UI", 22F, FontStyle.Bold, GraphicsUnit.Point);
            lblSchedulesCount.Location = new Point(14, 40);
            lblSchedulesCount.Name = "lblSchedulesCount";
            lblSchedulesCount.Size = new Size(51, 41);
            lblSchedulesCount.TabIndex = 1;
            lblSchedulesCount.Text = "0";
            // 
            // lblSchedulesTitle
            // 
            lblSchedulesTitle.AutoSize = true;
            lblSchedulesTitle.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Regular, GraphicsUnit.Point);
            lblSchedulesTitle.Location = new Point(14, 12);
            lblSchedulesTitle.Name = "lblSchedulesTitle";
            lblSchedulesTitle.Size = new Size(75, 20);
            lblSchedulesTitle.TabIndex = 0;
            lblSchedulesTitle.Text = "Schedules";
            // 
            // panelRecentTeachers
            // 
            panelRecentTeachers.BackColor = Color.White;
            panelRecentTeachers.Controls.Add(dgvRecentTeachers);
            panelRecentTeachers.Controls.Add(lblRecentTeachers);
            panelRecentTeachers.Dock = DockStyle.Fill;
            panelRecentTeachers.Location = new Point(3, 3);
            panelRecentTeachers.Name = "panelRecentTeachers";
            panelRecentTeachers.Padding = new Padding(12);
            panelRecentTeachers.Size = new Size(426, 321);
            panelRecentTeachers.TabIndex = 2;
            // 
            // dgvRecentTeachers
            // 
            dgvRecentTeachers.AllowUserToAddRows = false;
            dgvRecentTeachers.AllowUserToDeleteRows = false;
            dgvRecentTeachers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRecentTeachers.BackgroundColor = Color.White;
            dgvRecentTeachers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRecentTeachers.Dock = DockStyle.Fill;
            dgvRecentTeachers.Location = new Point(12, 43);
            dgvRecentTeachers.MultiSelect = false;
            dgvRecentTeachers.Name = "dgvRecentTeachers";
            dgvRecentTeachers.ReadOnly = true;
            dgvRecentTeachers.RowHeadersVisible = false;
            dgvRecentTeachers.RowTemplate.Height = 25;
            dgvRecentTeachers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRecentTeachers.AlternatingRowsDefaultCellStyle.BackColor = Color.Gainsboro;
            dgvRecentTeachers.TabIndex = 1;
            // 
            // lblRecentTeachers
            // 
            lblRecentTeachers.AutoSize = true;
            lblRecentTeachers.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblRecentTeachers.Location = new Point(12, 12);
            lblRecentTeachers.Name = "lblRecentTeachers";
            lblRecentTeachers.Size = new Size(140, 21);
            lblRecentTeachers.TabIndex = 0;
            lblRecentTeachers.Text = "Recent Teachers";
            // 
            // panelRecentStudents
            // 
            panelRecentStudents.BackColor = Color.White;
            panelRecentStudents.Controls.Add(dgvRecentStudents);
            panelRecentStudents.Controls.Add(lblRecentStudents);
            panelRecentStudents.Dock = DockStyle.Fill;
            panelRecentStudents.Location = new Point(435, 3);
            panelRecentStudents.Name = "panelRecentStudents";
            panelRecentStudents.Padding = new Padding(12);
            panelRecentStudents.Size = new Size(426, 321);
            panelRecentStudents.TabIndex = 3;
            // 
            // dgvRecentStudents
            // 
            dgvRecentStudents.AllowUserToAddRows = false;
            dgvRecentStudents.AllowUserToDeleteRows = false;
            dgvRecentStudents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRecentStudents.BackgroundColor = Color.White;
            dgvRecentStudents.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRecentStudents.Dock = DockStyle.Fill;
            dgvRecentStudents.Location = new Point(12, 43);
            dgvRecentStudents.MultiSelect = false;
            dgvRecentStudents.Name = "dgvRecentStudents";
            dgvRecentStudents.ReadOnly = true;
            dgvRecentStudents.RowHeadersVisible = false;
            dgvRecentStudents.RowTemplate.Height = 25;
            dgvRecentStudents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRecentStudents.AlternatingRowsDefaultCellStyle.BackColor = Color.Gainsboro;
            dgvRecentStudents.TabIndex = 1;
            // 
            // lblRecentStudents
            // 
            lblRecentStudents.AutoSize = true;
            lblRecentStudents.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblRecentStudents.Location = new Point(12, 12);
            lblRecentStudents.Name = "lblRecentStudents";
            lblRecentStudents.Size = new Size(139, 21);
            lblRecentStudents.TabIndex = 0;
            lblRecentStudents.Text = "Recent Students";
            // 
            // btnRefresh
            // 
            btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRefresh.Location = new Point(807, 18);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(75, 30);
            btnRefresh.TabIndex = 4;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // mainLayout
            // 
            mainLayout.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            mainLayout.ColumnCount = 2;
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            mainLayout.Controls.Add(panelRecentTeachers, 0, 0);
            mainLayout.Controls.Add(panelRecentStudents, 1, 0);
            mainLayout.Location = new Point(18, 182);
            mainLayout.Name = "mainLayout";
            mainLayout.RowCount = 1;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainLayout.Size = new Size(864, 327);
            mainLayout.TabIndex = 5;
            // 
            // Dashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(900, 550);
            Controls.Add(mainLayout);
            Controls.Add(btnRefresh);
            Controls.Add(flowMetrics);
            Controls.Add(lblHeader);
            Name = "Dashboard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Dashboard";
            Load += Dashboard_Load;
            flowMetrics.ResumeLayout(false);
            cardTeachers.ResumeLayout(false);
            cardTeachers.PerformLayout();
            cardStudents.ResumeLayout(false);
            cardStudents.PerformLayout();
            cardSubjects.ResumeLayout(false);
            cardSubjects.PerformLayout();
            cardSchedules.ResumeLayout(false);
            cardSchedules.PerformLayout();
            panelRecentTeachers.ResumeLayout(false);
            panelRecentTeachers.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRecentTeachers).EndInit();
            panelRecentStudents.ResumeLayout(false);
            panelRecentStudents.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRecentStudents).EndInit();
            mainLayout.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}
