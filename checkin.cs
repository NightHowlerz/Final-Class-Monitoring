using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;
using ZXing.Windows.Compatibility;

namespace SmartAttendanceClassroomMonitoringVersion2
{
    /// <summary>
    /// Lightweight check-in prompt. Simulates camera scan by accepting scanned text.
    /// </summary>
    public class CheckInForm : Form
    {
        private readonly string _expectedQr;
        private TextBox _txtScan = null!;
        public string? ScannedText { get; private set; }

        public CheckInForm(string expectedQr)
        {
            _expectedQr = expectedQr;
            Text = "Check In";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(380, 200);
            BuildUi();
        }

        private void BuildUi()
        {
            var lblMessage = new Label
            {
                Text = "Scan Room QR Code to Proceed",
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                Margin = new Padding(0, 0, 0, 12)
            };

            var btnCamera = new Button
            {
                Text = "Open Camera",
                Width = 160,
                Height = 36,
                Anchor = AnchorStyles.None,
                Margin = new Padding(0, 0, 0, 12)
            };
            btnCamera.Click += BtnCamera_Click;

            _txtScan = new TextBox
            {
                PlaceholderText = "Scan result will appear here...",
                Width = 260,
                TextAlign = HorizontalAlignment.Center,
                Anchor = AnchorStyles.None,
                Margin = new Padding(0, 0, 0, 12)
            };

            var btnOk = new Button { Text = "Proceed", DialogResult = DialogResult.OK, Width = 100, Height = 32 };
            var btnCancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel, Width = 100, Height = 32 };

            var flow = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                FlowDirection = FlowDirection.RightToLeft,
                Padding = new Padding(12),
                Height = 50
            };
            flow.Controls.Add(btnOk);
            flow.Controls.Add(btnCancel);

            var content = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Padding = new Padding(0),
                Anchor = AnchorStyles.None
            };
            content.Controls.Add(lblMessage);
            content.Controls.Add(btnCamera);
            content.Controls.Add(_txtScan);

            var root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 3
            };
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            root.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            root.Controls.Add(content, 1, 1);

            Controls.Add(root);
            Controls.Add(flow);

            AcceptButton = btnOk;
            CancelButton = btnCancel;
            btnOk.Click += (_, _) => ScannedText = _txtScan.Text.Trim();
        }

        private void InitializeComponent()
        {

        }

        private void BtnCamera_Click(object? sender, EventArgs e)
        {
            using var dialog = new CameraScanForm(_expectedQr);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                _txtScan.Text = dialog.ScannedText;
            }
        }
    }

    /// <summary>
    /// Camera-driven QR scanner dialog (600 x 400 display area).
    /// </summary>
    internal class CameraScanForm : Form
    {
        private readonly string _expected;
        private readonly PictureBox _preview = new() { Height = 400, Width = 600, SizeMode = PictureBoxSizeMode.StretchImage, BackColor = Color.Black, Anchor = AnchorStyles.None };
        private readonly Button _btnCapture = new() { Text = "Capture", Width = 120, Height = 36, Anchor = AnchorStyles.None };
        private readonly Button _btnClose = new() { Text = "Close", Width = 120, Height = 32, DialogResult = DialogResult.Cancel, Anchor = AnchorStyles.None };
        private FilterInfoCollection? _cameras;
        private VideoCaptureDevice? _device;
        private Bitmap? _latestFrame;
        private readonly object _frameLock = new();
        private readonly BarcodeReader _reader = new() { AutoRotate = true, TryInverted = true };

        public string? ScannedText { get; private set; }

        public CameraScanForm(string expected)
        {
            _expected = expected;
            Text = "Camera Scanner";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(640, 520);

            _btnCapture.Click += BtnCapture_Click;
            _btnClose.Click += (_, _) => DialogResult = DialogResult.Cancel;

            var buttons = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Anchor = AnchorStyles.None,
                Margin = new Padding(0, 12, 0, 0)
            };
            buttons.Controls.Add(_btnCapture);
            buttons.Controls.Add(_btnClose);

            var content = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Anchor = AnchorStyles.None,
                Padding = new Padding(0)
            };
            content.Controls.Add(_preview);
            content.Controls.Add(buttons);

            var root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 3
            };
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            root.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            root.Controls.Add(content, 1, 1);

            Controls.Add(root);

            Load += CameraScanForm_Load;
            FormClosing += CameraScanForm_FormClosing;
        }

        private void CameraScanForm_Load(object? sender, EventArgs e)
        {
            try
            {
                _cameras = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                if (_cameras.Count == 0)
                {
                    MessageBox.Show("No camera detected.", "Camera", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    DialogResult = DialogResult.Cancel;
                    Close();
                    return;
                }

                _device = new VideoCaptureDevice(_cameras[0].MonikerString);
                _device.NewFrame += Device_NewFrame;
                _device.VideoResolution = _device.VideoCapabilities
                    .OrderByDescending(v => v.FrameSize.Width * v.FrameSize.Height)
                    .FirstOrDefault();
                _device.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to start camera: {ex.Message}", "Camera", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        private void Device_NewFrame(object? sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                var frame = (Bitmap)eventArgs.Frame.Clone();
                lock (_frameLock)
                {
                    _latestFrame?.Dispose();
                    _latestFrame = (Bitmap)frame.Clone();
                }
                _preview.BeginInvoke(new Action(() =>
                {
                    _preview.Image?.Dispose();
                    _preview.Image = frame;
                }));
            }
            catch
            {
                // ignore frame errors
            }
        }

        private void BtnCapture_Click(object? sender, EventArgs e)
        {
            if (_latestFrame == null)
            {
                MessageBox.Show("No frame available. Ensure the camera is on.", "Capture", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Bitmap frameCopy;
                lock (_frameLock)
                {
                    frameCopy = _latestFrame.Clone(new Rectangle(0, 0, _latestFrame.Width, _latestFrame.Height),
                        System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                }

                var result = _reader.Decode(frameCopy);
                frameCopy.Dispose();

                if (result == null)
                {
                    MessageBox.Show("No QR detected. Try again.", "Scan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                ScannedText = result.Text?.Trim();

                if (!string.Equals(ScannedText, _expected, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Invalid room or schedule. Please scan the correct room QR code.", "Scan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to read QR: {ex.Message}", "Scan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CameraScanForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            try
            {
                if (_device != null && _device.IsRunning)
                {
                    _device.SignalToStop();
                    _device.NewFrame -= Device_NewFrame;
                }
            }
            catch { }

            _latestFrame?.Dispose();
            _preview.Image?.Dispose();
        }
    }
}
