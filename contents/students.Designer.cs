namespace SmartAttendanceClassroomMonitoringVersion2.contents
{
    partial class StudentsForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private Panel panelList;
        private Panel panelDetails;
        private TextBox txtSearch;
        private Button btnSearch;
        private Label lblSearch;
        private DataGridView dgvStudents;
        private Button btnAddStudent;
        private Label lblNoRecords;

        private PictureBox picStudent;
        private PictureBox picQr;
        private Label lblImage;
        private Label lblQr;
        private TextBox txtFirstName;
        private TextBox txtMiddleName;
        private TextBox txtLastName;
        private ComboBox cboGender;
        private NumericUpDown numYearLevel;
        private TextBox txtSection;
        private TextBox txtLRN;
        private DateTimePicker dtBirthDate;
        private TextBox txtEmail;
        private TextBox txtAddress;
        private Button btnGenerateQr;
        private Label lblMode;
        private Button btnCreate;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnClear;

        private Label lblFirstName;
        private Label lblMiddleName;
        private Label lblLastName;
        private Label lblGender;
        private Label lblYearLevel;
        private Label lblSection;
        private Label lblLRN;
        private Label lblBirthDate;
        private Label lblEmail;
        private Label lblAddress;

        private TabControl ribbonTabs;
        private TabPage tabStudents;
        private TabPage tabSections;
        private TableLayoutPanel sectionsLayout;
        private Panel panelMercury;
        private Panel panelVenus;
        private Panel panelEarth;
        private Panel panelMars;
        private Label lblMercury;
        private Label lblVenus;
        private Label lblEarth;
        private Label lblMars;
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
            panelList = new Panel();
            lblNoRecords = new Label();
            btnAddStudent = new Button();
            dgvStudents = new DataGridView();
            lblSearch = new Label();
            btnSearch = new Button();
            txtSearch = new TextBox();
            panelDetails = new Panel();
            lblMode = new Label();
            btnClear = new Button();
            btnDelete = new Button();
            btnUpdate = new Button();
            btnCreate = new Button();
            txtAddress = new TextBox();
            lblAddress = new Label();
            txtEmail = new TextBox();
            lblEmail = new Label();
            dtBirthDate = new DateTimePicker();
            lblBirthDate = new Label();
            txtLRN = new TextBox();
            lblLRN = new Label();
            txtSection = new TextBox();
            lblSection = new Label();
            numYearLevel = new NumericUpDown();
            lblYearLevel = new Label();
            cboGender = new ComboBox();
            lblGender = new Label();
            txtLastName = new TextBox();
            lblLastName = new Label();
            txtMiddleName = new TextBox();
            lblMiddleName = new Label();
            txtFirstName = new TextBox();
            lblFirstName = new Label();
            btnGenerateQr = new Button();
            picQr = new PictureBox();
            lblQr = new Label();
            picStudent = new PictureBox();
            lblImage = new Label();
            panelList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvStudents).BeginInit();
            panelDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numYearLevel).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picQr).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picStudent).BeginInit();
            SuspendLayout();
            // 
            // panelList
            // 
            panelList.BackColor = Color.White;
            panelList.Controls.Add(lblNoRecords);
            panelList.Controls.Add(btnAddStudent);
            panelList.Controls.Add(dgvStudents);
            panelList.Controls.Add(lblSearch);
            panelList.Controls.Add(btnSearch);
            panelList.Controls.Add(txtSearch);
            panelList.Dock = DockStyle.Left;
            panelList.Location = new Point(0, 0);
            panelList.Name = "panelList";
            panelList.Padding = new Padding(12);
            panelList.Size = new Size(380, 621);
            panelList.TabIndex = 0;
            panelList.MouseClick += panelList_MouseClick;
            // 
            // lblNoRecords
            // 
            lblNoRecords.Dock = DockStyle.Top;
            lblNoRecords.ForeColor = Color.Gray;
            lblNoRecords.Location = new Point(12, 12);
            lblNoRecords.Name = "lblNoRecords";
            lblNoRecords.Size = new Size(356, 23);
            lblNoRecords.TabIndex = 5;
            lblNoRecords.Text = "No record found.";
            lblNoRecords.TextAlign = ContentAlignment.MiddleCenter;
            lblNoRecords.Visible = false;
            // 
            // btnAddStudent
            // 
            btnAddStudent.Dock = DockStyle.Bottom;
            btnAddStudent.Location = new Point(12, 574);
            btnAddStudent.Name = "btnAddStudent";
            btnAddStudent.Size = new Size(356, 35);
            btnAddStudent.TabIndex = 4;
            btnAddStudent.Text = "Add Student";
            btnAddStudent.UseVisualStyleBackColor = true;
            btnAddStudent.Click += btnAddStudent_Click;
            // 
            // dgvStudents
            // 
            dgvStudents.AllowUserToAddRows = false;
            dgvStudents.AllowUserToDeleteRows = false;
            dgvStudents.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvStudents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvStudents.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvStudents.AlternatingRowsDefaultCellStyle.BackColor = Color.Gainsboro;
            dgvStudents.Location = new Point(12, 92);
            dgvStudents.MultiSelect = false;
            dgvStudents.Name = "dgvStudents";
            dgvStudents.ReadOnly = true;
            dgvStudents.RowHeadersVisible = false;
            dgvStudents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStudents.Size = new Size(356, 461);
            dgvStudents.TabIndex = 3;
            dgvStudents.SelectionChanged += dgvStudents_SelectionChanged;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Location = new Point(12, 16);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(192, 15);
            lblSearch.TabIndex = 0;
            lblSearch.Text = "Search (name / section / year level)";
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(293, 48);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(75, 26);
            btnSearch.TabIndex = 2;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(12, 48);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Type to search…";
            txtSearch.Size = new Size(275, 23);
            txtSearch.TabIndex = 1;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // panelDetails
            // 
            panelDetails.AutoScroll = true;
            panelDetails.BackColor = Color.WhiteSmoke;
            panelDetails.Controls.Add(lblMode);
            panelDetails.Controls.Add(btnClear);
            panelDetails.Controls.Add(btnDelete);
            panelDetails.Controls.Add(btnUpdate);
            panelDetails.Controls.Add(btnCreate);
            panelDetails.Controls.Add(txtAddress);
            panelDetails.Controls.Add(lblAddress);
            panelDetails.Controls.Add(txtEmail);
            panelDetails.Controls.Add(lblEmail);
            panelDetails.Controls.Add(dtBirthDate);
            panelDetails.Controls.Add(lblBirthDate);
            panelDetails.Controls.Add(txtLRN);
            panelDetails.Controls.Add(lblLRN);
            panelDetails.Controls.Add(txtSection);
            panelDetails.Controls.Add(lblSection);
            panelDetails.Controls.Add(numYearLevel);
            panelDetails.Controls.Add(lblYearLevel);
            panelDetails.Controls.Add(cboGender);
            panelDetails.Controls.Add(lblGender);
            panelDetails.Controls.Add(txtLastName);
            panelDetails.Controls.Add(lblLastName);
            panelDetails.Controls.Add(txtMiddleName);
            panelDetails.Controls.Add(lblMiddleName);
            panelDetails.Controls.Add(txtFirstName);
            panelDetails.Controls.Add(lblFirstName);
            panelDetails.Controls.Add(btnGenerateQr);
            panelDetails.Controls.Add(picQr);
            panelDetails.Controls.Add(lblQr);
            panelDetails.Controls.Add(picStudent);
            panelDetails.Controls.Add(lblImage);
            panelDetails.Dock = DockStyle.Fill;
            panelDetails.Location = new Point(380, 0);
            panelDetails.Name = "panelDetails";
            panelDetails.Padding = new Padding(16);
            panelDetails.Size = new Size(524, 621);
            panelDetails.TabIndex = 1;
            panelDetails.Visible = false;
            // 
            // lblMode
            // 
            lblMode.AutoSize = true;
            lblMode.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblMode.ForeColor = Color.DimGray;
            lblMode.Location = new Point(19, 12);
            lblMode.Name = "lblMode";
            lblMode.Size = new Size(96, 19);
            lblMode.TabIndex = 29;
            lblMode.Text = "Create Mode";
            // 
            // btnClear
            // 
            btnClear.Location = new Point(418, 560);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(88, 34);
            btnClear.TabIndex = 28;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(308, 560);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(88, 34);
            btnDelete.TabIndex = 27;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(198, 560);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(88, 34);
            btnUpdate.TabIndex = 26;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnCreate
            // 
            btnCreate.Location = new Point(88, 560);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(88, 34);
            btnCreate.TabIndex = 25;
            btnCreate.Text = "Create";
            btnCreate.UseVisualStyleBackColor = true;
            btnCreate.Click += btnCreate_Click;
            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(32, 511);
            txtAddress.Multiline = true;
            txtAddress.Name = "txtAddress";
            txtAddress.PlaceholderText = "Address";
            txtAddress.Size = new Size(474, 40);
            txtAddress.TabIndex = 24;
            // 
            // lblAddress
            // 
            lblAddress.AutoSize = true;
            lblAddress.Location = new Point(32, 493);
            lblAddress.Name = "lblAddress";
            lblAddress.Size = new Size(49, 15);
            lblAddress.TabIndex = 23;
            lblAddress.Text = "Address";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(32, 454);
            txtEmail.Name = "txtEmail";
            txtEmail.PlaceholderText = "Email";
            txtEmail.Size = new Size(214, 23);
            txtEmail.TabIndex = 22;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(32, 436);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(36, 15);
            lblEmail.TabIndex = 21;
            lblEmail.Text = "Email";
            // 
            // dtBirthDate
            // 
            dtBirthDate.Location = new Point(272, 454);
            dtBirthDate.Name = "dtBirthDate";
            dtBirthDate.Size = new Size(234, 23);
            dtBirthDate.TabIndex = 20;
            // 
            // lblBirthDate
            // 
            lblBirthDate.AutoSize = true;
            lblBirthDate.Location = new Point(272, 436);
            lblBirthDate.Name = "lblBirthDate";
            lblBirthDate.Size = new Size(59, 15);
            lblBirthDate.TabIndex = 19;
            lblBirthDate.Text = "Birth Date";
            // 
            // txtLRN
            // 
            txtLRN.Location = new Point(272, 396);
            txtLRN.Name = "txtLRN";
            txtLRN.PlaceholderText = "Learner Reference Number";
            txtLRN.Size = new Size(234, 23);
            txtLRN.TabIndex = 18;
            // 
            // lblLRN
            // 
            lblLRN.AutoSize = true;
            lblLRN.Location = new Point(272, 378);
            lblLRN.Name = "lblLRN";
            lblLRN.Size = new Size(29, 15);
            lblLRN.TabIndex = 17;
            lblLRN.Text = "LRN";
            // 
            // txtSection
            // 
            txtSection.Location = new Point(32, 396);
            txtSection.Name = "txtSection";
            txtSection.PlaceholderText = "Section";
            txtSection.Size = new Size(214, 23);
            txtSection.TabIndex = 16;
            // 
            // lblSection
            // 
            lblSection.AutoSize = true;
            lblSection.Location = new Point(32, 378);
            lblSection.Name = "lblSection";
            lblSection.Size = new Size(46, 15);
            lblSection.TabIndex = 15;
            lblSection.Text = "Section";
            // 
            // numYearLevel
            // 
            numYearLevel.Location = new Point(272, 338);
            numYearLevel.Maximum = new decimal(new int[] { 12, 0, 0, 0 });
            numYearLevel.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numYearLevel.Name = "numYearLevel";
            numYearLevel.Size = new Size(120, 23);
            numYearLevel.TabIndex = 14;
            numYearLevel.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblYearLevel
            // 
            lblYearLevel.AutoSize = true;
            lblYearLevel.Location = new Point(272, 320);
            lblYearLevel.Name = "lblYearLevel";
            lblYearLevel.Size = new Size(59, 15);
            lblYearLevel.TabIndex = 13;
            lblYearLevel.Text = "Year Level";
            // 
            // cboGender
            // 
            cboGender.DropDownStyle = ComboBoxStyle.DropDownList;
            cboGender.FormattingEnabled = true;
            cboGender.Items.AddRange(new object[] { "Male", "Female", "Other" });
            cboGender.Location = new Point(32, 338);
            cboGender.Name = "cboGender";
            cboGender.Size = new Size(214, 23);
            cboGender.TabIndex = 12;
            // 
            // lblGender
            // 
            lblGender.AutoSize = true;
            lblGender.Location = new Point(32, 320);
            lblGender.Name = "lblGender";
            lblGender.Size = new Size(45, 15);
            lblGender.TabIndex = 11;
            lblGender.Text = "Gender";
            // 
            // txtLastName
            // 
            txtLastName.Location = new Point(272, 281);
            txtLastName.Name = "txtLastName";
            txtLastName.PlaceholderText = "Last Name";
            txtLastName.Size = new Size(234, 23);
            txtLastName.TabIndex = 10;
            // 
            // lblLastName
            // 
            lblLastName.AutoSize = true;
            lblLastName.Location = new Point(272, 263);
            lblLastName.Name = "lblLastName";
            lblLastName.Size = new Size(63, 15);
            lblLastName.TabIndex = 9;
            lblLastName.Text = "Last Name";
            // 
            // txtMiddleName
            // 
            txtMiddleName.Location = new Point(32, 281);
            txtMiddleName.Name = "txtMiddleName";
            txtMiddleName.PlaceholderText = "Middle Name";
            txtMiddleName.Size = new Size(214, 23);
            txtMiddleName.TabIndex = 8;
            // 
            // lblMiddleName
            // 
            lblMiddleName.AutoSize = true;
            lblMiddleName.Location = new Point(32, 263);
            lblMiddleName.Name = "lblMiddleName";
            lblMiddleName.Size = new Size(79, 15);
            lblMiddleName.TabIndex = 7;
            lblMiddleName.Text = "Middle Name";
            // 
            // txtFirstName
            // 
            txtFirstName.Location = new Point(32, 225);
            txtFirstName.Name = "txtFirstName";
            txtFirstName.PlaceholderText = "First Name";
            txtFirstName.Size = new Size(474, 23);
            txtFirstName.TabIndex = 6;
            // 
            // lblFirstName
            // 
            lblFirstName.AutoSize = true;
            lblFirstName.Location = new Point(32, 207);
            lblFirstName.Name = "lblFirstName";
            lblFirstName.Size = new Size(64, 15);
            lblFirstName.TabIndex = 5;
            lblFirstName.Text = "First Name";
            // 
            // btnGenerateQr
            // 
            btnGenerateQr.Location = new Point(352, 150);
            btnGenerateQr.Name = "btnGenerateQr";
            btnGenerateQr.Size = new Size(154, 30);
            btnGenerateQr.TabIndex = 4;
            btnGenerateQr.Text = "Generate QR Code";
            btnGenerateQr.UseVisualStyleBackColor = true;
            btnGenerateQr.Click += btnGenerateQr_Click;
            // 
            // picQr
            // 
            picQr.BackColor = Color.White;
            picQr.BorderStyle = BorderStyle.FixedSingle;
            picQr.Location = new Point(352, 44);
            picQr.Name = "picQr";
            picQr.Size = new Size(154, 100);
            picQr.SizeMode = PictureBoxSizeMode.StretchImage;
            picQr.TabIndex = 3;
            picQr.TabStop = false;
            // 
            // lblQr
            // 
            lblQr.AutoSize = true;
            lblQr.Location = new Point(352, 26);
            lblQr.Name = "lblQr";
            lblQr.Size = new Size(54, 15);
            lblQr.TabIndex = 2;
            lblQr.Text = "QR Code";
            // 
            // picStudent
            // 
            picStudent.BackColor = Color.White;
            picStudent.BorderStyle = BorderStyle.FixedSingle;
            picStudent.Location = new Point(32, 44);
            picStudent.Name = "picStudent";
            picStudent.Size = new Size(154, 136);
            picStudent.SizeMode = PictureBoxSizeMode.StretchImage;
            picStudent.TabIndex = 1;
            picStudent.TabStop = false;
            picStudent.Click += picStudent_Click;
            // 
            // lblImage
            // 
            lblImage.AutoSize = true;
            lblImage.Location = new Point(32, 26);
            lblImage.Name = "lblImage";
            lblImage.Size = new Size(148, 15);
            lblImage.TabIndex = 0;
            lblImage.Text = "Image (click to upload file)";
            // 
        // StudentsForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.Gainsboro;
        ClientSize = new Size(904, 621);
        Controls.Add(panelDetails);
        Controls.Add(panelList);
        Name = "StudentsForm";
        Text = "Students";
        Load += StudentsForm_Load;
        MouseClick += StudentsForm_MouseClick;
        panelList.ResumeLayout(false);
        panelList.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvStudents).EndInit();
            panelDetails.ResumeLayout(false);
            panelDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numYearLevel).EndInit();
            ((System.ComponentModel.ISupportInitialize)picQr).EndInit();
            ((System.ComponentModel.ISupportInitialize)picStudent).EndInit();
            ResumeLayout(false);

        }

        #endregion
    }
}
