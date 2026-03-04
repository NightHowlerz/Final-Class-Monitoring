using System.Windows.Forms;

namespace SmartAttendanceClassroomMonitoringVersion2.contents
{
    partial class SchedulesForm
    {
        private System.ComponentModel.IContainer components = null;
        private Panel scrollContainer;
        private TableLayoutPanel tableRooms;
        private Panel panelRoom1;
        private Panel panelRoom2;
        private Panel panelRoom3;
        private Panel panelRoom4;
        private Label lblRoom1;
        private Label lblRoom2;
        private Label lblRoom3;
        private Label lblRoom4;
        private DataGridView dgvRoom1;
        private DataGridView dgvRoom2;
        private DataGridView dgvRoom3;
        private DataGridView dgvRoom4;

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
            scrollContainer = new Panel();
            tableRooms = new TableLayoutPanel();
            panelRoom1 = new Panel();
            dgvRoom1 = new DataGridView();
            lblRoom1 = new Label();
            panelRoom2 = new Panel();
            dgvRoom2 = new DataGridView();
            lblRoom2 = new Label();
            panelRoom3 = new Panel();
            dgvRoom3 = new DataGridView();
            lblRoom3 = new Label();
            panelRoom4 = new Panel();
            dgvRoom4 = new DataGridView();
            lblRoom4 = new Label();
            scrollContainer.SuspendLayout();
            tableRooms.SuspendLayout();
            panelRoom1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRoom1).BeginInit();
            panelRoom2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRoom2).BeginInit();
            panelRoom3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRoom3).BeginInit();
            panelRoom4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRoom4).BeginInit();
            SuspendLayout();
            // 
            // scrollContainer
            // 
            scrollContainer.AutoScroll = true;
            scrollContainer.BackColor = Color.WhiteSmoke;
            scrollContainer.Controls.Add(tableRooms);
            scrollContainer.Dock = DockStyle.Fill;
            scrollContainer.Location = new Point(0, 0);
            scrollContainer.Name = "scrollContainer";
            scrollContainer.Padding = new Padding(8);
            scrollContainer.Size = new Size(1000, 700);
            scrollContainer.TabIndex = 0;
            // 
            // tableRooms
            // 
            tableRooms.AutoSize = true;
            tableRooms.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableRooms.ColumnCount = 1;
            tableRooms.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableRooms.Controls.Add(panelRoom1, 0, 0);
            tableRooms.Controls.Add(panelRoom2, 0, 1);
            tableRooms.Controls.Add(panelRoom3, 0, 2);
            tableRooms.Controls.Add(panelRoom4, 0, 3);
            tableRooms.Dock = DockStyle.Top;
            tableRooms.Location = new Point(8, 8);
            tableRooms.Name = "tableRooms";
            tableRooms.RowCount = 4;
            tableRooms.RowStyles.Add(new RowStyle(SizeType.Absolute, 210F));
            tableRooms.RowStyles.Add(new RowStyle(SizeType.Absolute, 210F));
            tableRooms.RowStyles.Add(new RowStyle(SizeType.Absolute, 210F));
            tableRooms.RowStyles.Add(new RowStyle(SizeType.Absolute, 210F));
            tableRooms.Size = new Size(967, 840);
            tableRooms.TabIndex = 1;
            // 
            // panelRoom1
            // 
            panelRoom1.Controls.Add(dgvRoom1);
            panelRoom1.Controls.Add(lblRoom1);
            panelRoom1.Dock = DockStyle.Fill;
            panelRoom1.Location = new Point(3, 3);
            panelRoom1.Name = "panelRoom1";
            panelRoom1.Padding = new Padding(8);
            panelRoom1.Size = new Size(961, 204);
            panelRoom1.TabIndex = 0;
            // 
            // dgvRoom1
            // 
            dgvRoom1.Dock = DockStyle.Fill;
            dgvRoom1.Location = new Point(8, 28);
            dgvRoom1.Name = "dgvRoom1";
            dgvRoom1.Size = new Size(945, 168);
            dgvRoom1.TabIndex = 1;
            // 
            // lblRoom1
            // 
            lblRoom1.AutoSize = true;
            lblRoom1.Dock = DockStyle.Top;
            lblRoom1.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblRoom1.Location = new Point(8, 8);
            lblRoom1.Name = "lblRoom1";
            lblRoom1.Size = new Size(64, 20);
            lblRoom1.TabIndex = 0;
            lblRoom1.Text = "Room 1";
            // 
            // panelRoom2
            // 
            panelRoom2.Controls.Add(dgvRoom2);
            panelRoom2.Controls.Add(lblRoom2);
            panelRoom2.Dock = DockStyle.Fill;
            panelRoom2.Location = new Point(3, 213);
            panelRoom2.Name = "panelRoom2";
            panelRoom2.Padding = new Padding(8);
            panelRoom2.Size = new Size(961, 204);
            panelRoom2.TabIndex = 1;
            // 
            // dgvRoom2
            // 
            dgvRoom2.Dock = DockStyle.Fill;
            dgvRoom2.Location = new Point(8, 28);
            dgvRoom2.Name = "dgvRoom2";
            dgvRoom2.Size = new Size(945, 168);
            dgvRoom2.TabIndex = 1;
            // 
            // lblRoom2
            // 
            lblRoom2.AutoSize = true;
            lblRoom2.Dock = DockStyle.Top;
            lblRoom2.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblRoom2.Location = new Point(8, 8);
            lblRoom2.Name = "lblRoom2";
            lblRoom2.Size = new Size(64, 20);
            lblRoom2.TabIndex = 0;
            lblRoom2.Text = "Room 2";
            // 
            // panelRoom3
            // 
            panelRoom3.Controls.Add(dgvRoom3);
            panelRoom3.Controls.Add(lblRoom3);
            panelRoom3.Dock = DockStyle.Fill;
            panelRoom3.Location = new Point(3, 423);
            panelRoom3.Name = "panelRoom3";
            panelRoom3.Padding = new Padding(8);
            panelRoom3.Size = new Size(961, 204);
            panelRoom3.TabIndex = 2;
            // 
            // dgvRoom3
            // 
            dgvRoom3.Dock = DockStyle.Fill;
            dgvRoom3.Location = new Point(8, 28);
            dgvRoom3.Name = "dgvRoom3";
            dgvRoom3.Size = new Size(945, 168);
            dgvRoom3.TabIndex = 1;
            // 
            // lblRoom3
            // 
            lblRoom3.AutoSize = true;
            lblRoom3.Dock = DockStyle.Top;
            lblRoom3.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblRoom3.Location = new Point(8, 8);
            lblRoom3.Name = "lblRoom3";
            lblRoom3.Size = new Size(64, 20);
            lblRoom3.TabIndex = 0;
            lblRoom3.Text = "Room 3";
            // 
            // panelRoom4
            // 
            panelRoom4.Controls.Add(dgvRoom4);
            panelRoom4.Controls.Add(lblRoom4);
            panelRoom4.Dock = DockStyle.Fill;
            panelRoom4.Location = new Point(3, 633);
            panelRoom4.Name = "panelRoom4";
            panelRoom4.Padding = new Padding(8);
            panelRoom4.Size = new Size(961, 204);
            panelRoom4.TabIndex = 3;
            // 
            // dgvRoom4
            // 
            dgvRoom4.Dock = DockStyle.Fill;
            dgvRoom4.Location = new Point(8, 28);
            dgvRoom4.Name = "dgvRoom4";
            dgvRoom4.Size = new Size(945, 168);
            dgvRoom4.TabIndex = 1;
            // 
            // lblRoom4
            // 
            lblRoom4.AutoSize = true;
            lblRoom4.Dock = DockStyle.Top;
            lblRoom4.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblRoom4.Location = new Point(8, 8);
            lblRoom4.Name = "lblRoom4";
            lblRoom4.Size = new Size(64, 20);
            lblRoom4.TabIndex = 0;
            lblRoom4.Text = "Room 4";
            // 
            // SchedulesForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 700);
            Controls.Add(scrollContainer);
            Name = "SchedulesForm";
            Text = "Schedule";
            scrollContainer.ResumeLayout(false);
            scrollContainer.PerformLayout();
            tableRooms.ResumeLayout(false);
            panelRoom1.ResumeLayout(false);
            panelRoom1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRoom1).EndInit();
            panelRoom2.ResumeLayout(false);
            panelRoom2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRoom2).EndInit();
            panelRoom3.ResumeLayout(false);
            panelRoom3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRoom3).EndInit();
            panelRoom4.ResumeLayout(false);
            panelRoom4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRoom4).EndInit();
            ResumeLayout(false);

        }

        #endregion

    }
}
