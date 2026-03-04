namespace SmartAttendanceClassroomMonitoringVersion2.contents
{
    partial class TeacherForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private Panel panelList;
        private Panel panelDetails;
        private SplitContainer splitTeacher;
        private TabControl tabRibbon;
        private TabPage tabTeacher;
        private TabPage tabAccount;
        private TextBox txtSearch;
        private Button btnSearch;
        private DataGridView dgvTeachers;
        private Button btnAddTeacher;
        private Label lblSearch;
        private Label lblNoRecords;
        private PictureBox picTeacher;
        private TextBox txtFirstName;
        private TextBox txtLastName;
        private ComboBox cboGender;
        private DateTimePicker dtpDOB;
        private TextBox txtEmail;
        private TextBox txtPhone;
        private TextBox txtAddress;
        private Button btnCreate;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnClear;
        private Label lblFirstName;
        private Label lblLastName;
        private Label lblGender;
        private Label lblDOB;
        private Label lblEmail;
        private Label lblPhone;
        private Label lblAddress;
        private Label lblImage;
        private Label lblMode;
        private TableLayoutPanel accountLayout;
        private GroupBox grpAccountForm;
        private Label lblAccName;
        private TextBox txtAccName;
        private Label lblAccUsername;
        private TextBox txtAccUsername;
        private Label lblAccPassword;
        private TextBox txtAccPassword;
        private FlowLayoutPanel flowAccountButtons;
        private Button btnAccAdd;
        private Button btnAccUpdate;
        private Button btnAccDelete;
        private Button btnAccClear;
        private Label lblAccountMode;
        private GroupBox grpAccountList;
        private ListView lvAccounts;
        private ColumnHeader colAccNo;
        private ColumnHeader colAccName;
        private ColumnHeader colAccUsername;
        private ColumnHeader colAccPassword;

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
            splitTeacher = new SplitContainer();
            panelList = new Panel();
            lblNoRecords = new Label();
            btnAddTeacher = new Button();
            dgvTeachers = new DataGridView();
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
            txtPhone = new TextBox();
            lblPhone = new Label();
            txtEmail = new TextBox();
            lblEmail = new Label();
            dtpDOB = new DateTimePicker();
            lblDOB = new Label();
            cboGender = new ComboBox();
            lblGender = new Label();
            txtLastName = new TextBox();
            lblLastName = new Label();
            txtFirstName = new TextBox();
            lblFirstName = new Label();
            picTeacher = new PictureBox();
            lblImage = new Label();
            tabRibbon = new TabControl();
            tabTeacher = new TabPage();
            tabAccount = new TabPage();
            accountLayout = new TableLayoutPanel();
            grpAccountList = new GroupBox();
            lvAccounts = new ListView();
            colAccNo = new ColumnHeader();
            colAccName = new ColumnHeader();
            colAccUsername = new ColumnHeader();
            colAccPassword = new ColumnHeader();
            grpAccountForm = new GroupBox();
            lblAccountMode = new Label();
            flowAccountButtons = new FlowLayoutPanel();
            btnAccAdd = new Button();
            btnAccUpdate = new Button();
            btnAccDelete = new Button();
            btnAccClear = new Button();
            lblAccPassword = new Label();
            txtAccPassword = new TextBox();
            lblAccUsername = new Label();
            txtAccUsername = new TextBox();
            lblAccName = new Label();
            txtAccName = new TextBox();
            ((System.ComponentModel.ISupportInitialize)splitTeacher).BeginInit();
            splitTeacher.Panel1.SuspendLayout();
            splitTeacher.Panel2.SuspendLayout();
            splitTeacher.SuspendLayout();
            panelList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTeachers).BeginInit();
            panelDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picTeacher).BeginInit();
            tabRibbon.SuspendLayout();
            tabTeacher.SuspendLayout();
            tabAccount.SuspendLayout();
            accountLayout.SuspendLayout();
            grpAccountList.SuspendLayout();
            grpAccountForm.SuspendLayout();
            flowAccountButtons.SuspendLayout();
            SuspendLayout();
            // 
            // splitTeacher
            // 
            splitTeacher.Dock = DockStyle.Fill;
            splitTeacher.FixedPanel = FixedPanel.Panel1;
            splitTeacher.Location = new Point(3, 3);
            splitTeacher.MinimumSize = new Size(600, 0);
            splitTeacher.Name = "splitTeacher";
            // 
            // splitTeacher.Panel1
            // 
            splitTeacher.Panel1.Controls.Add(panelList);
            // 
            // splitTeacher.Panel2
            // 
            splitTeacher.Panel2.Controls.Add(panelDetails);
            splitTeacher.Size = new Size(986, 604);
            splitTeacher.SplitterDistance = 360;
            splitTeacher.TabIndex = 0;
            // 
            // panelList
            // 
            panelList.BackColor = Color.White;
            panelList.Controls.Add(lblNoRecords);
            panelList.Controls.Add(btnAddTeacher);
            panelList.Controls.Add(dgvTeachers);
            panelList.Controls.Add(lblSearch);
            panelList.Controls.Add(btnSearch);
            panelList.Controls.Add(txtSearch);
            panelList.Dock = DockStyle.Fill;
            panelList.Location = new Point(0, 0);
            panelList.Name = "panelList";
            panelList.Padding = new Padding(12);
            panelList.Size = new Size(360, 604);
            panelList.TabIndex = 1;
            panelList.MouseClick += panelList_MouseClick;
            // 
            // lblNoRecords
            // 
            lblNoRecords.Dock = DockStyle.Top;
            lblNoRecords.ForeColor = Color.Gray;
            lblNoRecords.Location = new Point(12, 12);
            lblNoRecords.Name = "lblNoRecords";
            lblNoRecords.Size = new Size(336, 23);
            lblNoRecords.TabIndex = 5;
            lblNoRecords.Text = "No record found.";
            lblNoRecords.TextAlign = ContentAlignment.MiddleCenter;
            lblNoRecords.Visible = false;
            // 
            // btnAddTeacher
            // 
            btnAddTeacher.Dock = DockStyle.Bottom;
            btnAddTeacher.Location = new Point(12, 557);
            btnAddTeacher.Name = "btnAddTeacher";
            btnAddTeacher.Size = new Size(336, 35);
            btnAddTeacher.TabIndex = 4;
            btnAddTeacher.Text = "Add Teacher";
            btnAddTeacher.UseVisualStyleBackColor = true;
            btnAddTeacher.Click += btnAddTeacher_Click;
            // 
            // dgvTeachers
            // 
            dgvTeachers.AllowUserToAddRows = false;
            dgvTeachers.AllowUserToDeleteRows = false;
            dgvTeachers.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvTeachers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTeachers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTeachers.Location = new Point(12, 92);
            dgvTeachers.MultiSelect = false;
            dgvTeachers.Name = "dgvTeachers";
            dgvTeachers.ReadOnly = true;
            dgvTeachers.RowHeadersVisible = false;
            dgvTeachers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTeachers.Size = new Size(336, 453);
            dgvTeachers.TabIndex = 3;
            dgvTeachers.SelectionChanged += dgvTeachers_SelectionChanged;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Location = new Point(12, 16);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(135, 15);
            lblSearch.TabIndex = 0;
            lblSearch.Text = "Search (first / last name)";
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(273, 48);
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
            txtSearch.Size = new Size(255, 23);
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
            panelDetails.Controls.Add(txtPhone);
            panelDetails.Controls.Add(lblPhone);
            panelDetails.Controls.Add(txtEmail);
            panelDetails.Controls.Add(lblEmail);
            panelDetails.Controls.Add(dtpDOB);
            panelDetails.Controls.Add(lblDOB);
            panelDetails.Controls.Add(cboGender);
            panelDetails.Controls.Add(lblGender);
            panelDetails.Controls.Add(txtLastName);
            panelDetails.Controls.Add(lblLastName);
            panelDetails.Controls.Add(txtFirstName);
            panelDetails.Controls.Add(lblFirstName);
            panelDetails.Controls.Add(picTeacher);
            panelDetails.Controls.Add(lblImage);
            panelDetails.Dock = DockStyle.Fill;
            panelDetails.Location = new Point(0, 0);
            panelDetails.Name = "panelDetails";
            panelDetails.Padding = new Padding(16);
            panelDetails.Size = new Size(622, 604);
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
            lblMode.TabIndex = 21;
            lblMode.Text = "Create Mode";
            // 
            // btnClear
            // 
            btnClear.Location = new Point(406, 550);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(100, 34);
            btnClear.TabIndex = 20;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(282, 550);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(100, 34);
            btnDelete.TabIndex = 19;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(158, 550);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(100, 34);
            btnUpdate.TabIndex = 18;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnCreate
            // 
            btnCreate.Location = new Point(34, 550);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(100, 34);
            btnCreate.TabIndex = 17;
            btnCreate.Text = "Create";
            btnCreate.UseVisualStyleBackColor = true;
            btnCreate.Click += btnCreate_Click;
            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(34, 495);
            txtAddress.Multiline = true;
            txtAddress.Name = "txtAddress";
            txtAddress.PlaceholderText = "Address";
            txtAddress.Size = new Size(472, 40);
            txtAddress.TabIndex = 16;
            // 
            // lblAddress
            // 
            lblAddress.AutoSize = true;
            lblAddress.Location = new Point(34, 477);
            lblAddress.Name = "lblAddress";
            lblAddress.Size = new Size(49, 15);
            lblAddress.TabIndex = 15;
            lblAddress.Text = "Address";
            // 
            // txtPhone
            // 
            txtPhone.Location = new Point(310, 431);
            txtPhone.Name = "txtPhone";
            txtPhone.PlaceholderText = "Phone Number";
            txtPhone.Size = new Size(196, 23);
            txtPhone.TabIndex = 14;
            // 
            // lblPhone
            // 
            lblPhone.AutoSize = true;
            lblPhone.Location = new Point(310, 413);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(88, 15);
            lblPhone.TabIndex = 13;
            lblPhone.Text = "Phone Number";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(34, 431);
            txtEmail.Name = "txtEmail";
            txtEmail.PlaceholderText = "Email";
            txtEmail.Size = new Size(260, 23);
            txtEmail.TabIndex = 12;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(34, 413);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(36, 15);
            lblEmail.TabIndex = 11;
            lblEmail.Text = "Email";
            // 
            // dtpDOB
            // 
            dtpDOB.Location = new Point(310, 360);
            dtpDOB.Name = "dtpDOB";
            dtpDOB.Size = new Size(196, 23);
            dtpDOB.TabIndex = 10;
            // 
            // lblDOB
            // 
            lblDOB.AutoSize = true;
            lblDOB.Location = new Point(310, 342);
            lblDOB.Name = "lblDOB";
            lblDOB.Size = new Size(73, 15);
            lblDOB.TabIndex = 9;
            lblDOB.Text = "Date of Birth";
            // 
            // cboGender
            // 
            cboGender.DropDownStyle = ComboBoxStyle.DropDownList;
            cboGender.FormattingEnabled = true;
            cboGender.Items.AddRange(new object[] { "Male", "Female", "Other" });
            cboGender.Location = new Point(34, 360);
            cboGender.Name = "cboGender";
            cboGender.Size = new Size(260, 23);
            cboGender.TabIndex = 8;
            // 
            // lblGender
            // 
            lblGender.AutoSize = true;
            lblGender.Location = new Point(34, 342);
            lblGender.Name = "lblGender";
            lblGender.Size = new Size(45, 15);
            lblGender.TabIndex = 7;
            lblGender.Text = "Gender";
            // 
            // txtLastName
            // 
            txtLastName.Location = new Point(310, 295);
            txtLastName.Name = "txtLastName";
            txtLastName.PlaceholderText = "Last Name";
            txtLastName.Size = new Size(196, 23);
            txtLastName.TabIndex = 6;
            // 
            // lblLastName
            // 
            lblLastName.AutoSize = true;
            lblLastName.Location = new Point(310, 277);
            lblLastName.Name = "lblLastName";
            lblLastName.Size = new Size(63, 15);
            lblLastName.TabIndex = 5;
            lblLastName.Text = "Last Name";
            // 
            // txtFirstName
            // 
            txtFirstName.Location = new Point(34, 295);
            txtFirstName.Name = "txtFirstName";
            txtFirstName.PlaceholderText = "First Name";
            txtFirstName.Size = new Size(260, 23);
            txtFirstName.TabIndex = 4;
            // 
            // lblFirstName
            // 
            lblFirstName.AutoSize = true;
            lblFirstName.Location = new Point(34, 277);
            lblFirstName.Name = "lblFirstName";
            lblFirstName.Size = new Size(64, 15);
            lblFirstName.TabIndex = 3;
            lblFirstName.Text = "First Name";
            // 
            // picTeacher
            // 
            picTeacher.BackColor = Color.White;
            picTeacher.BorderStyle = BorderStyle.FixedSingle;
            picTeacher.Location = new Point(34, 72);
            picTeacher.Name = "picTeacher";
            picTeacher.Size = new Size(160, 180);
            picTeacher.SizeMode = PictureBoxSizeMode.StretchImage;
            picTeacher.TabIndex = 1;
            picTeacher.TabStop = false;
            picTeacher.Click += picTeacher_Click;
            // 
            // lblImage
            // 
            lblImage.AutoSize = true;
            lblImage.Location = new Point(34, 54);
            lblImage.Name = "lblImage";
            lblImage.Size = new Size(148, 15);
            lblImage.TabIndex = 0;
            lblImage.Text = "Image (click to upload file)";
            // 
            // tabRibbon
            // 
            tabRibbon.Appearance = TabAppearance.Buttons;
            tabRibbon.Controls.Add(tabTeacher);
            tabRibbon.Controls.Add(tabAccount);
            tabRibbon.Dock = DockStyle.Fill;
            tabRibbon.ItemSize = new Size(120, 32);
            tabRibbon.Location = new Point(0, 0);
            tabRibbon.Name = "tabRibbon";
            tabRibbon.Padding = new Point(14, 6);
            tabRibbon.SelectedIndex = 0;
            tabRibbon.Size = new Size(1000, 650);
            tabRibbon.SizeMode = TabSizeMode.Fixed;
            tabRibbon.TabIndex = 0;
            // 
            // tabTeacher
            // 
            tabTeacher.Controls.Add(splitTeacher);
            tabTeacher.Location = new Point(4, 36);
            tabTeacher.Name = "tabTeacher";
            tabTeacher.Padding = new Padding(3);
            tabTeacher.Size = new Size(992, 610);
            tabTeacher.TabIndex = 0;
            tabTeacher.Text = "Teacher";
            tabTeacher.UseVisualStyleBackColor = true;
            // 
            // tabAccount
            // 
            tabAccount.Controls.Add(accountLayout);
            tabAccount.Location = new Point(4, 36);
            tabAccount.Name = "tabAccount";
            tabAccount.Padding = new Padding(10);
            tabAccount.Size = new Size(992, 610);
            tabAccount.TabIndex = 1;
            tabAccount.Text = "Account";
            tabAccount.UseVisualStyleBackColor = true;
            // 
            // accountLayout
            // 
            accountLayout.ColumnCount = 2;
            accountLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 53.9094658F));
            accountLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 46.0905342F));
            accountLayout.Controls.Add(grpAccountList, 0, 0);
            accountLayout.Controls.Add(grpAccountForm, 1, 0);
            accountLayout.Dock = DockStyle.Fill;
            accountLayout.Location = new Point(10, 10);
            accountLayout.Name = "accountLayout";
            accountLayout.RowCount = 1;
            accountLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            accountLayout.Size = new Size(972, 590);
            accountLayout.TabIndex = 0;
            // 
            // grpAccountList
            // 
            grpAccountList.Controls.Add(lvAccounts);
            grpAccountList.Dock = DockStyle.Fill;
            grpAccountList.Font = new Font("Segoe UI", 9F);
            grpAccountList.Location = new Point(3, 3);
            grpAccountList.Name = "grpAccountList";
            grpAccountList.Padding = new Padding(10);
            grpAccountList.Size = new Size(518, 584);
            grpAccountList.TabIndex = 0;
            grpAccountList.TabStop = false;
            grpAccountList.Text = "Teacher Accounts";
            // 
            // lvAccounts
            // 
            lvAccounts.Columns.AddRange(new ColumnHeader[] { colAccNo, colAccName, colAccUsername, colAccPassword });
            lvAccounts.Dock = DockStyle.Fill;
            lvAccounts.FullRowSelect = true;
            lvAccounts.GridLines = true;
            lvAccounts.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            lvAccounts.Location = new Point(10, 26);
            lvAccounts.MultiSelect = false;
            lvAccounts.Name = "lvAccounts";
            lvAccounts.Size = new Size(498, 548);
            lvAccounts.TabIndex = 0;
            lvAccounts.UseCompatibleStateImageBehavior = false;
            lvAccounts.View = View.Details;
            lvAccounts.SelectedIndexChanged += lvAccounts_SelectedIndexChanged;
            // 
            // colAccNo
            // 
            colAccNo.Text = "#";
            colAccNo.Width = 40;
            // 
            // colAccName
            // 
            colAccName.Text = "Name";
            colAccName.Width = 150;
            // 
            // colAccUsername
            // 
            colAccUsername.Text = "Username";
            colAccUsername.Width = 150;
            // 
            // colAccPassword
            // 
            colAccPassword.Text = "Password";
            colAccPassword.Width = 150;
            // 
            // grpAccountForm
            // 
            grpAccountForm.Controls.Add(lblAccountMode);
            grpAccountForm.Controls.Add(flowAccountButtons);
            grpAccountForm.Controls.Add(lblAccPassword);
            grpAccountForm.Controls.Add(txtAccPassword);
            grpAccountForm.Controls.Add(lblAccUsername);
            grpAccountForm.Controls.Add(txtAccUsername);
            grpAccountForm.Controls.Add(lblAccName);
            grpAccountForm.Controls.Add(txtAccName);
            grpAccountForm.Dock = DockStyle.Fill;
            grpAccountForm.Font = new Font("Segoe UI", 9F);
            grpAccountForm.Location = new Point(527, 3);
            grpAccountForm.Name = "grpAccountForm";
            grpAccountForm.Padding = new Padding(12);
            grpAccountForm.Size = new Size(442, 584);
            grpAccountForm.TabIndex = 1;
            grpAccountForm.TabStop = false;
            grpAccountForm.Text = "Account Details";
            // 
            // lblAccountMode
            // 
            lblAccountMode.AutoSize = true;
            lblAccountMode.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblAccountMode.ForeColor = Color.DimGray;
            lblAccountMode.Location = new Point(15, 26);
            lblAccountMode.Name = "lblAccountMode";
            lblAccountMode.Size = new Size(79, 15);
            lblAccountMode.TabIndex = 9;
            lblAccountMode.Text = "Create Mode";
            // 
            // flowAccountButtons
            // 
            flowAccountButtons.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            flowAccountButtons.Controls.Add(btnAccAdd);
            flowAccountButtons.Controls.Add(btnAccUpdate);
            flowAccountButtons.Controls.Add(btnAccDelete);
            flowAccountButtons.Controls.Add(btnAccClear);
            flowAccountButtons.Location = new Point(15, 194);
            flowAccountButtons.Name = "flowAccountButtons";
            flowAccountButtons.Size = new Size(510, 40);
            flowAccountButtons.TabIndex = 8;
            // 
            // btnAccAdd
            // 
            btnAccAdd.Location = new Point(3, 3);
            btnAccAdd.Name = "btnAccAdd";
            btnAccAdd.Size = new Size(100, 32);
            btnAccAdd.TabIndex = 0;
            btnAccAdd.Text = "Add";
            btnAccAdd.UseVisualStyleBackColor = true;
            btnAccAdd.Click += btnAccAdd_Click;
            // 
            // btnAccUpdate
            // 
            btnAccUpdate.Enabled = false;
            btnAccUpdate.Location = new Point(109, 3);
            btnAccUpdate.Name = "btnAccUpdate";
            btnAccUpdate.Size = new Size(100, 32);
            btnAccUpdate.TabIndex = 1;
            btnAccUpdate.Text = "Update";
            btnAccUpdate.UseVisualStyleBackColor = true;
            btnAccUpdate.Click += btnAccUpdate_Click;
            // 
            // btnAccDelete
            // 
            btnAccDelete.Enabled = false;
            btnAccDelete.Location = new Point(215, 3);
            btnAccDelete.Name = "btnAccDelete";
            btnAccDelete.Size = new Size(100, 32);
            btnAccDelete.TabIndex = 2;
            btnAccDelete.Text = "Delete";
            btnAccDelete.UseVisualStyleBackColor = true;
            btnAccDelete.Click += btnAccDelete_Click;
            // 
            // btnAccClear
            // 
            btnAccClear.Location = new Point(321, 3);
            btnAccClear.Name = "btnAccClear";
            btnAccClear.Size = new Size(100, 32);
            btnAccClear.TabIndex = 3;
            btnAccClear.Text = "Clear";
            btnAccClear.UseVisualStyleBackColor = true;
            btnAccClear.Click += btnAccClear_Click;
            // 
            // lblAccPassword
            // 
            lblAccPassword.AutoSize = true;
            lblAccPassword.Location = new Point(20, 148);
            lblAccPassword.Name = "lblAccPassword";
            lblAccPassword.Size = new Size(57, 15);
            lblAccPassword.TabIndex = 7;
            lblAccPassword.Text = "Password";
            // 
            // txtAccPassword
            // 
            txtAccPassword.Location = new Point(20, 166);
            txtAccPassword.Name = "txtAccPassword";
            txtAccPassword.PlaceholderText = "Password";
            txtAccPassword.Size = new Size(260, 23);
            txtAccPassword.TabIndex = 6;
            // 
            // lblAccUsername
            // 
            lblAccUsername.AutoSize = true;
            lblAccUsername.Location = new Point(20, 97);
            lblAccUsername.Name = "lblAccUsername";
            lblAccUsername.Size = new Size(60, 15);
            lblAccUsername.TabIndex = 5;
            lblAccUsername.Text = "Username";
            // 
            // txtAccUsername
            // 
            txtAccUsername.Location = new Point(20, 115);
            txtAccUsername.Name = "txtAccUsername";
            txtAccUsername.PlaceholderText = "Username";
            txtAccUsername.Size = new Size(200, 23);
            txtAccUsername.TabIndex = 4;
            // 
            // lblAccName
            // 
            lblAccName.AutoSize = true;
            lblAccName.Location = new Point(20, 57);
            lblAccName.Name = "lblAccName";
            lblAccName.Size = new Size(83, 15);
            lblAccName.TabIndex = 1;
            lblAccName.Text = "Teacher Name";
            // 
            // txtAccName
            // 
            txtAccName.BackColor = Color.WhiteSmoke;
            txtAccName.Location = new Point(20, 75);
            txtAccName.Name = "txtAccName";
            txtAccName.PlaceholderText = "Full Name";
            txtAccName.ReadOnly = true;
            txtAccName.Size = new Size(260, 23);
            txtAccName.TabIndex = 0;
            txtAccName.TabStop = false;
            // 
            // TeacherForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gainsboro;
            ClientSize = new Size(1000, 650);
            Controls.Add(tabRibbon);
            Name = "TeacherForm";
            Text = "Teachers";
            Load += teacher_Load;
            splitTeacher.Panel1.ResumeLayout(false);
            splitTeacher.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitTeacher).EndInit();
            splitTeacher.ResumeLayout(false);
            panelList.ResumeLayout(false);
            panelList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTeachers).EndInit();
            panelDetails.ResumeLayout(false);
            panelDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picTeacher).EndInit();
            tabRibbon.ResumeLayout(false);
            tabTeacher.ResumeLayout(false);
            tabAccount.ResumeLayout(false);
            accountLayout.ResumeLayout(false);
            grpAccountList.ResumeLayout(false);
            grpAccountForm.ResumeLayout(false);
            grpAccountForm.PerformLayout();
            flowAccountButtons.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion
    }
}
