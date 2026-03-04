using System.Data;
using System.Drawing;
using MySql.Data.MySqlClient;
using QRCoder;

namespace SmartAttendanceClassroomMonitoringVersion2.contents
{
    public partial class StudentsForm : Form
    {
        private int? _selectedId;
        private string? _imagePath;
        private byte[]? _qrBytes;
        private bool _createMode = true;
        private bool _suppressSelection;

        public StudentsForm()
        {
            InitializeComponent();
            ConfigureGrid();
        }

        private void ConfigureGrid()
        {
            dgvStudents.AutoGenerateColumns = false;
            dgvStudents.Columns.Clear();

            dgvStudents.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "idstudents_tbl",
                Name = "Id",
                HeaderText = "ID",
                Visible = false
            });
            dgvStudents.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "FirstName",
                Name = "FirstName",
                HeaderText = "First Name",
                FillWeight = 35
            });
            dgvStudents.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "LastName",
                Name = "LastName",
                HeaderText = "Last Name",
                FillWeight = 35
            });
            dgvStudents.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Section",
                Name = "Section",
                HeaderText = "Section",
                FillWeight = 15
            });
            dgvStudents.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "YearLevel",
                Name = "YearLevel",
                HeaderText = "Year",
                FillWeight = 15
            });
        }

        private void StudentsForm_Load(object? sender, EventArgs e)
        {
            LoadStudents();
            ShowDetailsPanel(false);
        }

        private void LoadStudents(string? searchTerm = null, int? selectId = null)
        {
            try
            {
                using var conn = Database.GetConnection();
                conn.Open();

                var sql = @"SELECT idstudents_tbl, FirstName, MiddleName, LastName, Gender, YearLevel, Section, LRN,
                                   BirthDate, Email, Address, Image, QRCode
                            FROM students_tbl";
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    sql += @" WHERE LOWER(FirstName) LIKE @search
                              OR LOWER(LastName) LIKE @search
                              OR LOWER(Section) LIKE @search
                              OR CAST(YearLevel AS CHAR) LIKE @search";
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

                _suppressSelection = true;
                dgvStudents.DataSource = table;
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
                    foreach (DataGridViewRow row in dgvStudents.Rows)
                    {
                        if (row.Cells["Id"].Value is object val && Convert.ToInt32(val) == targetId.Value)
                        {
                            row.Selected = true;
                            dgvStudents.CurrentCell = row.Cells["FirstName"];
                            PopulateDetails(((DataRowView)row.DataBoundItem).Row);
                            return;
                        }
                    }
                }

                dgvStudents.ClearSelection();
                ShowDetailsPanel(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void HighlightMatches(string? term)
        {
            term = term?.Trim();
            foreach (DataGridViewRow row in dgvStudents.Rows)
            {
                row.DefaultCellStyle.BackColor = System.Drawing.Color.White;
                if (!string.IsNullOrWhiteSpace(term))
                {
                    var first = row.Cells["FirstName"].Value?.ToString() ?? string.Empty;
                    var last = row.Cells["LastName"].Value?.ToString() ?? string.Empty;
                    var section = row.Cells["Section"].Value?.ToString() ?? string.Empty;
                    var year = row.Cells["YearLevel"].Value?.ToString() ?? string.Empty;
                    if (first.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                        last.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                        section.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                        year.Contains(term, StringComparison.OrdinalIgnoreCase))
                    {
                        row.DefaultCellStyle.BackColor = System.Drawing.Color.LightGoldenrodYellow;
                    }
                }
            }
        }

        private void txtSearch_TextChanged(object? sender, EventArgs e)
        {
            LoadStudents(txtSearch.Text);
        }

        private void btnSearch_Click(object? sender, EventArgs e)
        {
            LoadStudents(txtSearch.Text);
        }

        private void btnAddStudent_Click(object? sender, EventArgs e)
        {
            ResetForm();
            SetMode(true);
            ShowDetailsPanel(true);
        }

        private void panelList_MouseClick(object? sender, MouseEventArgs e)
        {
            HideDetailsIfOutside(sender as Control, e.Location);
        }

        private void StudentsForm_MouseClick(object? sender, MouseEventArgs e)
        {
            HideDetailsIfOutside(sender as Control, e.Location);
        }

        private void HideDetailsIfOutside(Control? source, Point location)
        {
            if (!panelDetails.Visible || source == null) return;
            var formPoint = PointToClient(source.PointToScreen(location));
            if (!panelDetails.Bounds.Contains(formPoint))
            {
                ShowDetailsPanel(false);
            }
        }

        private void dgvStudents_SelectionChanged(object? sender, EventArgs e)
        {
            if (_suppressSelection) return;
            if (dgvStudents.CurrentRow == null || dgvStudents.CurrentRow.DataBoundItem == null) return;

            if (dgvStudents.CurrentRow.DataBoundItem is DataRowView drv)
            {
                PopulateDetails(drv.Row);
            }
        }

        private void PopulateDetails(DataRow row)
        {
            _selectedId = Convert.ToInt32(row["idstudents_tbl"]);
            txtFirstName.Text = row["FirstName"]?.ToString();
            txtMiddleName.Text = row["MiddleName"]?.ToString();
            txtLastName.Text = row["LastName"]?.ToString();
            cboGender.SelectedItem = row["Gender"]?.ToString();
            if (row["YearLevel"] != DBNull.Value) numYearLevel.Value = Convert.ToDecimal(row["YearLevel"]);
            txtSection.Text = row["Section"]?.ToString();
            txtLRN.Text = row["LRN"]?.ToString();
            if (row["BirthDate"] != DBNull.Value) dtBirthDate.Value = Convert.ToDateTime(row["BirthDate"]);
            txtEmail.Text = row["Email"]?.ToString();
            txtAddress.Text = row["Address"]?.ToString();
            _imagePath = row["Image"]?.ToString();
            LoadPicture(_imagePath);

            if (row["QRCode"] != DBNull.Value)
            {
                _qrBytes = (byte[])row["QRCode"];
                LoadQrImage(_qrBytes);
            }
            else
            {
                _qrBytes = null;
                picQr.Image = null;
            }

            ShowDetailsPanel(true);
            SetMode(false);
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
            _qrBytes = null;
            txtFirstName.Text = string.Empty;
            txtMiddleName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            cboGender.SelectedIndex = -1;
            numYearLevel.Value = 1;
            txtSection.Text = string.Empty;
            txtLRN.Text = string.Empty;
            dtBirthDate.Value = DateTime.Now.Date;
            txtEmail.Text = string.Empty;
            txtAddress.Text = string.Empty;
            picStudent.Image = null;
            picQr.Image = null;
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
            if (!string.IsNullOrWhiteSpace(txtLRN.Text) && !long.TryParse(txtLRN.Text, out _))
            {
                MessageBox.Show("LRN must be numeric.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                var sql = @"INSERT INTO students_tbl
                            (FirstName, MiddleName, LastName, Gender, YearLevel, Section, LRN, BirthDate, Email, Address, Image, QRCode)
                            VALUES (@FirstName, @MiddleName, @LastName, @Gender, @YearLevel, @Section, @LRN, @BirthDate, @Email, @Address, @Image, @QRCode)";

                using var cmd = new MySqlCommand(sql, conn);
                AddCommonParameters(cmd);
                cmd.Parameters.Add("@QRCode", MySqlDbType.LongBlob).Value = (object?)_qrBytes ?? DBNull.Value;

                cmd.ExecuteNonQuery();
                var newId = (int)cmd.LastInsertedId;

                LoadStudents(txtSearch.Text, newId);
                MessageBox.Show("Student created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetForm();
                SetMode(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating student: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object? sender, EventArgs e)
        {
            if (!_selectedId.HasValue)
            {
                MessageBox.Show("Select a student first.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!ValidateInput()) return;

            try
            {
                using var conn = Database.GetConnection();
                conn.Open();

                var sql = @"UPDATE students_tbl
                            SET FirstName=@FirstName, MiddleName=@MiddleName, LastName=@LastName, Gender=@Gender, YearLevel=@YearLevel,
                                Section=@Section, LRN=@LRN, BirthDate=@BirthDate, Email=@Email, Address=@Address, Image=@Image, QRCode=@QRCode
                            WHERE idstudents_tbl=@Id";

                using var cmd = new MySqlCommand(sql, conn);
                AddCommonParameters(cmd);
                cmd.Parameters.Add("@QRCode", MySqlDbType.LongBlob).Value = (object?)_qrBytes ?? DBNull.Value;
                cmd.Parameters.AddWithValue("@Id", _selectedId.Value);
                cmd.ExecuteNonQuery();

                LoadStudents(txtSearch.Text, _selectedId);
                MessageBox.Show("Student updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _selectedId = null;
                ResetForm();
                SetMode(true);
                ShowDetailsPanel(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating student: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object? sender, EventArgs e)
        {
            if (!_selectedId.HasValue)
            {
                MessageBox.Show("Select a student first.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show("Are you sure you want to delete this student?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                using var conn = Database.GetConnection();
                conn.Open();

                using var cmd = new MySqlCommand("DELETE FROM students_tbl WHERE idstudents_tbl=@Id", conn);
                cmd.Parameters.AddWithValue("@Id", _selectedId.Value);
                cmd.ExecuteNonQuery();

                LoadStudents(txtSearch.Text);
                ResetForm();
                ShowDetailsPanel(false);
                SetMode(true);
                MessageBox.Show("Student deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting student: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object? sender, EventArgs e)
        {
            ResetForm();
            SetMode(true);
        }

        private void AddCommonParameters(MySqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text.Trim());
            cmd.Parameters.AddWithValue("@MiddleName", string.IsNullOrWhiteSpace(txtMiddleName.Text) ? DBNull.Value : txtMiddleName.Text.Trim());
            cmd.Parameters.AddWithValue("@LastName", txtLastName.Text.Trim());
            cmd.Parameters.AddWithValue("@Gender", cboGender.SelectedItem?.ToString());
            cmd.Parameters.AddWithValue("@YearLevel", Convert.ToInt32(numYearLevel.Value));
            cmd.Parameters.AddWithValue("@Section", string.IsNullOrWhiteSpace(txtSection.Text) ? DBNull.Value : txtSection.Text.Trim());
            cmd.Parameters.AddWithValue("@LRN", string.IsNullOrWhiteSpace(txtLRN.Text) ? DBNull.Value : txtLRN.Text.Trim());
            cmd.Parameters.AddWithValue("@BirthDate", dtBirthDate.Value.Date);
            cmd.Parameters.AddWithValue("@Email", string.IsNullOrWhiteSpace(txtEmail.Text) ? DBNull.Value : txtEmail.Text.Trim());
            cmd.Parameters.AddWithValue("@Address", string.IsNullOrWhiteSpace(txtAddress.Text) ? DBNull.Value : txtAddress.Text.Trim());
            cmd.Parameters.AddWithValue("@Image", string.IsNullOrWhiteSpace(_imagePath) ? DBNull.Value : _imagePath);
        }

        private void picStudent_Click(object? sender, EventArgs e)
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
                    picStudent.Image = new System.Drawing.Bitmap(imgTemp);
                }
                else
                {
                    picStudent.Image = null;
                }
            }
            catch
            {
                picStudent.Image = null;
            }
        }

        private void btnGenerateQr_Click(object? sender, EventArgs e)
        {
            var content = $"{txtFirstName.Text.Trim()} {txtLastName.Text.Trim()}".Trim();
            if (string.IsNullOrWhiteSpace(content))
            {
                MessageBox.Show("Enter first and last name before generating QR code.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using var generator = new QRCodeGenerator();
                using QRCodeData data = generator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);
                using var qr = new QRCode(data);
                using var bmp = qr.GetGraphic(20);
                picQr.Image = new Bitmap(bmp);

                using var ms = new MemoryStream();
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                _qrBytes = ms.ToArray();

                if (_selectedId.HasValue)
                {
                    // Persist QR immediately for edit mode
                    using var conn = Database.GetConnection();
                    conn.Open();
                    using var cmd = new MySqlCommand("UPDATE students_tbl SET QRCode=@qr WHERE idstudents_tbl=@Id", conn);
                    cmd.Parameters.Add("@qr", MySqlDbType.LongBlob).Value = _qrBytes;
                    cmd.Parameters.AddWithValue("@Id", _selectedId.Value);
                    cmd.ExecuteNonQuery();
                    LoadStudents(txtSearch.Text, _selectedId);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating QR code: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadQrImage(byte[]? qrBytes)
        {
            try
            {
                if (qrBytes != null && qrBytes.Length > 0)
                {
                    using var ms = new MemoryStream(qrBytes);
                    picQr.Image = new Bitmap(ms);
                }
                else
                {
                    picQr.Image = null;
                }
            }
            catch
            {
                picQr.Image = null;
            }
        }
    }
}
