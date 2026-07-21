using BitmapUtils;
using NVGE.Localization;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace NVGE
{
    public partial class FormShowPicture : Form
    {
        private const float MinimumZoom = 0.1f;
        private const float MaximumZoom = 100f;
        private const float ZoomStep = 1.25f;

        private readonly string imagePath;
        private Bitmap canvas;
        private Matrix transform;
        private Cursor rotateCursor;
        private PointF dragOrigin;
        private bool dragging;
        private bool fitToWindow = true;
        private float fittedScale = 1f;

        public FormShowPicture(string imagePath)
        {
            InitializeComponent();
            ModernWinFormsTheme.Apply(this);

            this.imagePath = imagePath;
            using var stream = new MemoryStream(Properties.Resources.cur_rotate_32);
            rotateCursor = new Cursor(stream);

            lblImageXY.Alignment = ToolStripItemAlignment.Right;
            lblMousePosition.Alignment = ToolStripItemAlignment.Right;
            pictureBox_Main.Dock = DockStyle.Fill;
            statusStrip1.Dock = DockStyle.Bottom;
            statusStrip1.BringToFront();
            pictureBox_Main.TabStop = true;
            pictureBox_Main.MouseWheel += PictureBox_Main_MouseWheel;
            pictureBox_Main.MouseDoubleClick += PictureBox_Main_MouseDoubleClick;

            typeof(Control)
                .GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic)
                ?.SetValue(pictureBox_Main, true);
        }

        private void FormShowPicture_Load(object sender, EventArgs e)
        {
            Text = "Viewer";

            try
            {
                canvas = ImageConvert.LoadBitmapForPreview(imagePath);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is IOException)
            {
                MessageBox.Show(
                    this,
                    $"The image could not be loaded.\n{ex.Message}",
                    Strings.MSGError,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Close();
                return;
            }

            transform = new Matrix();
            lblImageSize.Text = $"     [{canvas.Width}x{canvas.Height}] pixels.";
            ResetView();
        }

        private void PictureBox_Main_Paint(object sender, PaintEventArgs e)
        {
            if (canvas is null || transform is null)
            {
                return;
            }

            float zoom = GetZoomRatio();
            e.Graphics.Clear(pictureBox_Main.BackColor);
            e.Graphics.CompositingMode = CompositingMode.SourceOver;
            e.Graphics.CompositingQuality = CompositingQuality.HighSpeed;
            e.Graphics.PixelOffsetMode = zoom >= 1f ? PixelOffsetMode.Half : PixelOffsetMode.HighQuality;
            e.Graphics.InterpolationMode = zoom >= 1f
                ? InterpolationMode.NearestNeighbor
                : InterpolationMode.HighQualityBilinear;
            e.Graphics.DrawImage(canvas, transform);

            statusLabel_size.Text = Math.Abs(zoom - 1f) < 0.001f
                ? "Zoom Ratio: Default"
                : $"Zoom Ratio: {zoom:0.##}x";
        }

        private void PictureBox_Main_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox_Main.Focus();

            if (e.Button != MouseButtons.Left || transform is null)
            {
                return;
            }

            dragOrigin = e.Location;
            dragging = true;
            Cursor = Cursors.NoMove2D;
        }

        private void PictureBox_Main_MouseMove(object sender, MouseEventArgs e)
        {
            if (transform is null)
            {
                return;
            }

            if (dragging && e.Button == MouseButtons.Left)
            {
                transform.Translate(e.X - dragOrigin.X, e.Y - dragOrigin.Y, MatrixOrder.Append);
                dragOrigin = e.Location;
                fitToWindow = false;
                pictureBox_Main.Invalidate();
            }

            lblMousePosition.Text = $"Mouse {e.Location}";

            using Matrix inverse = transform.Clone();
            try
            {
                inverse.Invert();
                PointF[] point = { e.Location };
                inverse.TransformPoints(point);
                lblImageXY.Text = point[0].X >= 0 && point[0].Y >= 0 &&
                    point[0].X < canvas.Width && point[0].Y < canvas.Height
                    ? $"Image {{X={point[0].X:0}, Y={point[0].Y:0}}}"
                    : "Image {-,-}";
            }
            catch (InvalidOperationException)
            {
                lblImageXY.Text = "Image {-,-}";
            }
        }

        private void PictureBox_Main_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                dragging = false;
                Cursor = Cursors.Default;
            }
        }

        private void PictureBox_Main_MouseWheel(object sender, MouseEventArgs e)
        {
            if (transform is null)
            {
                return;
            }

            if ((ModifierKeys & Keys.Shift) == Keys.Shift)
            {
                transform.RotateAt(e.Delta > 0 ? 5f : -5f, e.Location, MatrixOrder.Append);
            }
            else
            {
                float currentZoom = GetZoomRatio();
                float factor = e.Delta > 0 ? ZoomStep : 1f / ZoomStep;
                float targetZoom = Math.Clamp(currentZoom * factor, MinimumZoom, MaximumZoom);
                factor = targetZoom / currentZoom;
                transform.ScaleAt(factor, e.Location);
            }

            fitToWindow = false;
            pictureBox_Main.Invalidate();
        }

        private void PictureBox_Main_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ResetView();
        }

        private void ResetView()
        {
            if (canvas is null || transform is null || pictureBox_Main.ClientSize.Width == 0 ||
                pictureBox_Main.ClientSize.Height == 0)
            {
                return;
            }

            transform.ZoomFit(pictureBox_Main, canvas);
            fittedScale = transform.Elements[0];
            fitToWindow = true;
            pictureBox_Main.Invalidate();
        }

        private float GetZoomRatio()
        {
            if (transform is null || fittedScale <= 0f)
            {
                return 1f;
            }

            float[] elements = transform.Elements;
            float currentScale = MathF.Sqrt(elements[0] * elements[0] + elements[1] * elements[1]);
            return currentScale / fittedScale;
        }

        private void FormShowPicture_FormClosing(object sender, FormClosingEventArgs e)
        {
            canvas?.Dispose();
            transform?.Dispose();
            rotateCursor?.Dispose();
        }

        private void FormShowPicture_Resize(object sender, EventArgs e)
        {
            if (fitToWindow)
            {
                ResetView();
            }
            else
            {
                pictureBox_Main.Invalidate();
            }
        }

        private void FormShowPicture_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Modifiers & Keys.Shift) == Keys.Shift)
            {
                Cursor = rotateCursor;
            }
        }

        private void FormShowPicture_KeyUp(object sender, KeyEventArgs e)
        {
            Cursor = Cursors.Default;
        }
    }
}
