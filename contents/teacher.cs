using System.Data;
using System.Linq;
using MySql.Data.MySqlClient;

namespace SmartAttendanceClassroomMonitoringVersion2.contents
{
    public partial class TeacherForm : Form
    {
        private int? _selectedId;
        private string? _imagePath;
        private bool _createMode = true;
        private bool _suppressSelection;
        private DataTable? _teacherCache;

        private int? _selectedAccountId;
        private bool _accountCreateMode = true;
        private bool _accountSuppressSelection;
        private string? _selectedTeacherNameForAccount;
        private record AccountListItem(int? AccountId, string Name, string? Username, string? Password);
        private static string FormatTeacherName(DataRow row) =>
            $"{row["LastName"]}, {row["FirstName"]}".Trim();

        public TeacherForm()
        {
            InitializeComponent();
            ConfigureGrid();
            ConfigureAccountsList();
        }

        private void ConfigureGrid()
        {
            dgvTeachers.AutoGenerateColumns = false;
            dgvTeachers.Columns.Clear();
            dgvTeachers.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.Gainsboro;

            var colId = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "idteachers_tbl",
                HeaderText = "ID",
                Visible = false,
                Name = "Id"
            };
            var colFirst = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "FirstName",
                HeaderText = "First Name",
                FillWeight = 50,
                Name = "FirstName"
            };
            var colLast = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "LastName",
                HeaderText = "Last Name",
                FillWeight = 50,
                Name = "LastName"
            };

            dgvTeachers.Columns.AddRange(colId, colFirst, colLast);
        }

        private void teacher_Load(object? sender, EventArgs e)
        {
            LoadTeachers();
            ShowDetailsPanel(false);
            EnsureAccountTable();
            LoadAccounts();
        }

        private void panelList_MouseClick(object? sender, MouseEventArgs e)
        {
            if (panelDetails.Visible)
            {
                ShowDetailsPanel(false);
            }
        }

        private void LoadTeachers(string? searchTerm = null, int? selectId = null)
        {
            try
            {
                using var conn = Database.GetConnection();
                conn.Open();

                var sql = @"SELECT idteachers_tbl, FirstName, LastName, Gender, DateOfBirth, Email, PhoneNumber, Address, Image
                            FROM teachers_tbl";
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    sql += " WHERE LOWER(FirstName) LIKE @search OR LOWER(LastName) LIKE @search";
                }
                sql += " ORDER BY LastName, FirstName";

                using var cmd = new MySqlCommand(sql, conn);
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    cmd.Parameters.AddWithValue("@search", $"%{searchTerm.ToLower()}%");
                }

                using var da = new MySqlDataAdapter(cmd);
                var table = new DataTable();
                da.Fill(table);

                _teacherCache = table;
                _suppressSelection = true;
                dgvTeachers.DataSource = table;
                lblNoRecords.Visible = table.Rows.Count == 0;
                HighlightMatches(searchTerm);
                _suppressSelection = false;

                if (table.Rows.Count == 0)
                {
                    _selectedId = null;
                    ShowDetailsPanel(false);
                    return;
                }



               








                var targetId = selectId ?? _selectedId;
                if (targetId.HasValue)
                {
                    foreach (DataGridViewRow row in dgvTeachers.Rows)
                    {
                        if (row.Cells["Id"].Value is object val && Convert.ToInt32(val) == targetId.Value)
                        {
                            row.Selected = true;
                            dgvTeachers.CurrentCell = row.Cells["FirstName"];
                            PopulateDetails(((DataRowView)row.DataBoundItem).Row);
                            return;
                        }
                    }
                }

                dgvTeachers.ClearSelection();
                ShowDetailsPanel(false);
                LoadAccounts();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading teachers: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HighlightMatches(string? term)
        {
            term = term?.Trim();
            foreach (DataGridViewRow row in dgvTeachers.Rows)
            {
                row.DefaultCellStyle.BackColor = System.Drawing.Color.White;
                if (!string.IsNullOrWhiteSpace(term))
                {
                    var first = row.Cells["FirstName"].Value?.ToString() ?? string.Empty;
                    var last = row.Cells["LastName"].Value?.ToString() ?? string.Empty;
                    if (first.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                        last.Contains(term, StringComparison.OrdinalIgnoreCase))
                    {
                        row.DefaultCellStyle.BackColor = System.Drawing.Color.LightGoldenrodYellow;
                    }
                }
            }
        }

        private void txtSearch_TextChanged(object? sender, EventArgs e)
        {
            LoadTeachers(txtSearch.Text);
        }

        private void btnSearch_Click(object? sender, EventArgs e)
        {
            LoadTeachers(txtSearch.Text);
        }

        private void btnAddTeacher_Click(object? sender, EventArgs e)
        {
            ShowDetailsPanel(true);
            ResetForm();
            SetMode(createMode: true);
        }

        private void dgvTeachers_SelectionChanged(object? sender, EventArgs e)
        {
            if (_suppressSelection) return;

            if (dgvTeachers.CurrentRow == null || dgvTeachers.CurrentRow.DataBoundItem == null)
            {
                return;
            }

            if (dgvTeachers.CurrentRow.DataBoundItem is DataRowView drv)
            {
                PopulateDetails(drv.Row);
            }
        }

        private void PopulateDetails(DataRow row)
        {
            _selectedId = Convert.ToInt32(row["idteachers_tbl"]);
            txtFirstName.Text = row["FirstName"]?.ToString();
            txtLastName.Text = row["LastName"]?.ToString();
            cboGender.SelectedItem = row["Gender"]?.ToString();
            if (row["DateOfBirth"] != DBNull.Value)
            {
                dtpDOB.Value = Convert.ToDateTime(row["DateOfBirth"]);
            }
            txtEmail.Text = row["Email"]?.ToString();
            txtPhone.Text = row["PhoneNumber"]?.ToString();
            txtAddress.Text = row["Address"]?.ToString();
            _imagePath = row["Image"]?.ToString();
            LoadPicture(_imagePath);

            ShowDetailsPanel(true);
            SetMode(createMode: false);
        }

        private void ShowDetailsPanel(bool show)
        {
            panelDetails.Visible = show;
        }

        private void SetMode(bool createMode)
        {
            _createMode = createMode;
            btnCreate.Enabled = createMode;
            btnUpdate.Enabled = !createMode;
            btnDelete.Enabled = !createMode;
            lblMode.Text = createMode ? "Create Mode" : "Edit Mode";
        }

        private void ResetForm()
        {
            _selectedId = null;
            _imagePath = null;
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            cboGender.SelectedIndex = -1;
            dtpDOB.Value = DateTime.Now.Date;
            txtEmail.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtAddress.Text = string.Empty;
            picTeacher.Image = null;
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("First name is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Last name is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtPhone.Text) && !long.TryParse(txtPhone.Text, out _))
            {
                MessageBox.Show("Phone number must be numeric.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void btnCreate_Click(object? sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                using var conn = Database.GetConnection();
                conn.Open();

                var sql = @"INSERT INTO teachers_tbl
                            (FirstName, LastName, Gender, DateOfBirth, Email, PhoneNumber, Address, Image)
                            VALUES (@FirstName, @LastName, @Gender, @DateOfBirth, @Email, @PhoneNumber, @Address, @Image)";

                using var cmd = new MySqlCommand(sql, conn);
                AddCommonParameters(cmd);
                cmd.ExecuteNonQuery();

                var newId = (int)cmd.LastInsertedId;
                LoadTeachers(txtSearch.Text, newId);
                MessageBox.Show("Teacher created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetForm();
                SetMode(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating teacher: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object? sender, EventArgs e)
        {
            if (!_selectedId.HasValue)
            {
                MessageBox.Show("Select a teacher first.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!ValidateInput()) return;

            try
            {
                using var conn = Database.GetConnection();
                conn.Open();

                var sql = @"UPDATE teachers_tbl
                            SET FirstName=@FirstName, LastName=@LastName, Gender=@Gender, DateOfBirth=@DateOfBirth,
                                Email=@Email, PhoneNumber=@PhoneNumber, Address=@Address, Image=@Image
                            WHERE idteachers_tbl=@Id";

                using var cmd = new MySqlCommand(sql, conn);
                AddCommonParameters(cmd);
                cmd.Parameters.AddWithValue("@Id", _selectedId.Value);
                cmd.ExecuteNonQuery();

                LoadTeachers(txtSearch.Text, _selectedId);
                MessageBox.Show("Teacher updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _selectedId = null;
                ResetForm();
                SetMode(true);
                ShowDetailsPanel(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating teacher: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object? sender, EventArgs e)
        {
            if (!_selectedId.HasValue)
            {
                MessageBox.Show("Select a teacher first.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show("Are you sure you want to delete this teacher?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                using var conn = Database.GetConnection();
                conn.Open();

                using var cmd = new MySqlCommand("DELETE FROM teachers_tbl WHERE idteachers_tbl=@Id", conn);
                cmd.Parameters.AddWithValue("@Id", _selectedId.Value);
                cmd.ExecuteNonQuery();

                // remove matching credentials (keep account list in sync)
                using var cmdAcc = new MySqlCommand("DELETE FROM teacher_account_tbl WHERE name=@Name", conn);
                cmdAcc.Parameters.AddWithValue("@Name", $"{txtLastName.Text.Trim()}, {txtFirstName.Text.Trim()}");
                cmdAcc.ExecuteNonQuery();

                LoadTeachers(txtSearch.Text);
                ResetForm();
                ShowDetailsPanel(false);
                SetMode(true);
                MessageBox.Show("Teacher deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting teacher: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object? sender, EventArgs e)
        {
            ResetForm();
            SetMode(true);
        }

        private void btnAccAdd_Click(object? sender, EventArgs e)
        {
            if (!ValidateAccountInput()) return;

            try
            {
                using var conn = Database.GetConnection();
                conn.Open();

                string sql;
                if (_selectedAccountId.HasValue)
                {
                    sql = @"UPDATE teacher_account_tbl
                            SET name=@Name, username=@Username, password=@Password
                            WHERE idteacher_account=@Id";
                }
                else
                {
                    sql = @"INSERT INTO teacher_account_tbl
                            (name, username, password)
                            VALUES (@Name, @Username, @Password)";
                }

                using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Name", txtAccName.Text.Trim());
                cmd.Parameters.AddWithValue("@Username", txtAccUsername.Text.Trim());
                cmd.Parameters.AddWithValue("@Password", txtAccPassword.Text.Trim());
                if (_selectedAccountId.HasValue)
                {
                    cmd.Parameters.AddWithValue("@Id", _selectedAccountId.Value);
                }

                cmd.ExecuteNonQuery();

                var newId = _selectedAccountId ?? (int)cmd.LastInsertedId;
                _selectedAccountId = newId;
                LoadAccounts(newId);
                MessageBox.Show("Account saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SetAccountMode(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating account: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAccUpdate_Click(object? sender, EventArgs e)
        {
            if (!_selectedAccountId.HasValue)
            {
                MessageBox.Show("Select a teacher row that already has an account.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!ValidateAccountInput()) return;

            try
            {
                using var conn = Database.GetConnection();
                conn.Open();

                const string sql = @"UPDATE teacher_account_tbl
                                     SET name=@Name, username=@Username, password=@Password
                                     WHERE idteacher_account=@Id";

                using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Name", txtAccName.Text.Trim());
                cmd.Parameters.AddWithValue("@Username", txtAccUsername.Text.Trim());
                cmd.Parameters.AddWithValue("@Password", txtAccPassword.Text.Trim());
                cmd.Parameters.AddWithValue("@Id", _selectedAccountId.Value);
                cmd.ExecuteNonQuery();

                LoadAccounts(_selectedAccountId);
                MessageBox.Show("Account updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetAccountForm();
                SetAccountMode(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating account: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAccDelete_Click(object? sender, EventArgs e)
        {
            if (!_selectedAccountId.HasValue)
            {
                MessageBox.Show("Select a teacher row that has credentials.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show("Are you sure you want to delete this account?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                using var conn = Database.GetConnection();
                conn.Open();

                using var cmd = new MySqlCommand("UPDATE teacher_account_tbl SET username=NULL, password=NULL WHERE idteacher_account=@Id", conn);
                cmd.Parameters.AddWithValue("@Id", _selectedAccountId.Value);
                cmd.ExecuteNonQuery();

                _selectedAccountId = null;
                txtAccUsername.Text = string.Empty;
                txtAccPassword.Text = string.Empty;
                LoadAccounts();
                SetAccountMode(true);
                MessageBox.Show("Username and password removed. Teacher name retained.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting account: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAccClear_Click(object? sender, EventArgs e)
        {
            ResetAccountForm();
            SetAccountMode(true);
        }

        private void AddCommonParameters(MySqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text.Trim());
            cmd.Parameters.AddWithValue("@LastName", txtLastName.Text.Trim());
            cmd.Parameters.AddWithValue("@Gender", cboGender.SelectedItem?.ToString());
            cmd.Parameters.AddWithValue("@DateOfBirth", dtpDOB.Value.Date);
            cmd.Parameters.AddWithValue("@Email", string.IsNullOrWhiteSpace(txtEmail.Text) ? DBNull.Value : txtEmail.Text.Trim());
            cmd.Parameters.AddWithValue("@PhoneNumber", string.IsNullOrWhiteSpace(txtPhone.Text) ? DBNull.Value : txtPhone.Text.Trim());
            cmd.Parameters.AddWithValue("@Address", string.IsNullOrWhiteSpace(txtAddress.Text) ? DBNull.Value : txtAddress.Text.Trim());
            cmd.Parameters.AddWithValue("@Image", string.IsNullOrWhiteSpace(_imagePath) ? DBNull.Value : _imagePath);
        }

        private void picTeacher_Click(object? sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog();
            dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                _imagePath = dialog.FileName;
                LoadPicture(_imagePath);
            }
        }

        private void LoadPicture(string? path)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(path) && File.Exists(path))
                {
                    using var imgTemp = System.Drawing.Image.FromFile(path);
                    picTeacher.Image = new System.Drawing.Bitmap(imgTemp);
                }
                else
                {
                    picTeacher.Image = null;
                }
            }
            catch
            {
                picTeacher.Image = null;
            }
        }

        private void ConfigureAccountsList()
        {
            lvAccounts.FullRowSelect = true;
            lvAccounts.GridLines = true;
            lvAccounts.View = View.Details;
            lvAccounts.HeaderStyle = ColumnHeaderStyle.Nonclickable;
        }

        private void EnsureAccountTable()
        {
            try
            {
                using var conn = Database.GetConnection();
                conn.Open();
                const string sql = @"CREATE TABLE IF NOT EXISTS teacher_account_tbl (
                                        idteacher_account INT NOT NULL AUTO_INCREMENT,
                                        name VARCHAR(255) NULL,
                                        username VARCHAR(100) NULL,
                                        password VARCHAR(100) NULL,
                                        PRIMARY KEY (idteacher_account)
                                     );";
                using var cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error ensuring account table: {ex.Message}", "Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadAccounts(int? selectId = null)
        {
            try
            {
                using var conn = Database.GetConnection();
                conn.Open();

                const string sql = @"SELECT idteacher_account, name, username, password
                                     FROM teacher_account_tbl";

                using var da = new MySqlDataAdapter(sql, conn);
                var accountTable = new DataTable();
                da.Fill(accountTable);

                var accountLookup = new Dictionary<string, DataRow>(StringComparer.OrdinalIgnoreCase);
                foreach (DataRow row in accountTable.Rows)
                {
                    var name = row["name"]?.ToString()?.Trim() ?? string.Empty;
                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        accountLookup[name] = row;
                    }
                }

                var items = new List<AccountListItem>();

                if (_teacherCache != null && _teacherCache.Rows.Count > 0)
                {
                    var teachers = _teacherCache.AsEnumerable()
                        .OrderBy(r => r["LastName"].ToString())
                        .ThenBy(r => r["FirstName"].ToString());

                    foreach (var teacher in teachers)
                    {
                        var name = FormatTeacherName(teacher);
                        accountLookup.TryGetValue(name, out var accRow);

                        items.Add(new AccountListItem(
                            accRow == null ? null : Convert.ToInt32(accRow["idteacher_account"]),
                            name,
                            accRow?["username"]?.ToString(),
                            accRow?["password"]?.ToString()));

                        if (accRow != null)
                        {
                            accountLookup.Remove(name);
                        }
                    }
                }

                // Remove orphaned accounts (teachers deleted)
                if (accountLookup.Count > 0)
                {
                    var idsToDelete = accountLookup.Values
                        .Select(r => Convert.ToInt32(r["idteacher_account"]))
                        .ToList();
                    if (idsToDelete.Count > 0)
                    {
                        var idList = string.Join(",", idsToDelete);
                        using var cleanCmd = new MySqlCommand($"DELETE FROM teacher_account_tbl WHERE idteacher_account IN ({idList})", conn);
                        cleanCmd.ExecuteNonQuery();
                    }
                }

                _accountSuppressSelection = true;
                lvAccounts.BeginUpdate();
                lvAccounts.Items.Clear();

                for (int i = 0; i < items.Count; i++)
                {
                    var a = items[i];
                    var lvItem = new ListViewItem((i + 1).ToString());
                    lvItem.SubItems.Add(a.Name);
                    lvItem.SubItems.Add(a.Username ?? string.Empty);
                    lvItem.SubItems.Add(a.Password ?? string.Empty); // plain text, no masking
                    lvItem.Tag = a;
                    lvItem.BackColor = i % 2 == 0 ? System.Drawing.Color.White : System.Drawing.Color.WhiteSmoke;
                    lvAccounts.Items.Add(lvItem);
                }

                lvAccounts.EndUpdate();
                _accountSuppressSelection = false;

                if (items.Count == 0)
                {
                    _selectedAccountId = null;
                    _selectedTeacherNameForAccount = null;
                    ResetAccountForm();
                    SetAccountMode(true);
                    return;
                }

                var targetId = selectId ?? _selectedAccountId;
                string? targetName = _selectedTeacherNameForAccount;
                if (targetId.HasValue || !string.IsNullOrWhiteSpace(targetName))
                {
                    foreach (ListViewItem item in lvAccounts.Items)
                    {
                        if (item.Tag is AccountListItem tag)
                        {
                            if (targetId.HasValue && tag.AccountId == targetId.Value ||
                                (!targetId.HasValue && !string.IsNullOrWhiteSpace(targetName) &&
                                 string.Equals(tag.Name, targetName, StringComparison.OrdinalIgnoreCase)))
                            {
                                item.Selected = true;
                                item.EnsureVisible();
                                PopulateAccountFields(tag);
                                return;
                            }
                        }
                    }
                }

                lvAccounts.SelectedItems.Clear();
                ResetAccountForm();
                SetAccountMode(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading accounts: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lvAccounts_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_accountSuppressSelection) return;
            if (lvAccounts.SelectedItems.Count == 0) return;
            if (lvAccounts.SelectedItems[0].Tag is AccountListItem item)
            {
                PopulateAccountFields(item);
            }
        }

        private void PopulateAccountFields(AccountListItem item)
        {
            _selectedAccountId = item.AccountId;
            _selectedTeacherNameForAccount = item.Name;
            txtAccName.Text = item.Name;
            txtAccUsername.Text = item.Username ?? string.Empty;
            txtAccPassword.Text = item.Password ?? string.Empty;

            var hasCredentials = !string.IsNullOrWhiteSpace(item.Username) || !string.IsNullOrWhiteSpace(item.Password);
            SetAccountMode(!hasCredentials);
        }

        private void ResetAccountForm()
        {
            _selectedAccountId = null;
            _selectedTeacherNameForAccount = null;
            txtAccName.Text = string.Empty;
            txtAccUsername.Text = string.Empty;
            txtAccPassword.Text = string.Empty;
            lvAccounts.SelectedItems.Clear();
        }

        private void SetAccountMode(bool createMode)
        {
            _accountCreateMode = createMode;
            btnAccAdd.Enabled = createMode;
            btnAccUpdate.Enabled = !createMode;
            btnAccDelete.Enabled = !createMode;
            lblAccountMode.Text = createMode ? "Create Mode" : "Edit Mode";
        }

        private bool ValidateAccountInput()
        {
            if (string.IsNullOrWhiteSpace(_selectedTeacherNameForAccount))
            {
                MessageBox.Show("Select a teacher row first.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtAccName.Text))
            {
                MessageBox.Show("Name is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtAccUsername.Text))
            {
                MessageBox.Show("Username is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtAccPassword.Text))
            {
                MessageBox.Show("Password is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
    }
}
