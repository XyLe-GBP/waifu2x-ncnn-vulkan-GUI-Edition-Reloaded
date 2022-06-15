using BitmapUtils;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace NVGE
{
    public partial class FormShowPicture : Form
    {
        #region winAPI override
        private double fixedRate = 0d;
        const int WM_SIZING = 0x214;
        //const int WM_NCLBUTTONDOWN = 0xA1;
        const int WMSZ_LEFT = 1;
        const int WMSZ_RIGHT = 2;
        const int WMSZ_TOP = 3;
        const int WMSZ_TOPLEFT = 4;
        const int WMSZ_TOPRIGHT = 5;
        const int WMSZ_BOTTOM = 6;
        const int WMSZ_BOTTOMLEFT = 7;
        const int WMSZ_BOTTOMRIGHT = 8;
        [StructLayout(LayoutKind.Sequential)]
        struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }
        #endregion
        #region point
        //private PointF _originPoint;
        private PointF _op;
        #endregion

        internal struct SBGroup
        {
            public SBGroup() { }
            public StringBuilder strZR1 = new("Zoom Ratio: x");
            public StringBuilder strZR2 = new("Zoom Ratio: -");
            public StringBuilder strZR3 = new("Zoom Ratio: Default");
        }

        private Bitmap canvas;
        private Graphics graph;
        private Matrix _matAffine;
        private Cursor RotateCursor;
        private bool _mdf = false;
        private bool _skf = false;
        private float zoomRatio = 1.0f;
        private float _defaultM11;
        private readonly string pp;

        public FormShowPicture(string Imagepath)
        {
            InitializeComponent();

            using var ms = new System.IO.MemoryStream(Properties.Resources.cur_rotate_32);
            RotateCursor = new Cursor(ms);
            pp = Imagepath;
            lblImageXY.Alignment = ToolStripItemAlignment.Right;
            lblMousePosition.Alignment = ToolStripItemAlignment.Right;
            pictureBox_Main.MouseWheel += new MouseEventHandler(PictureBox_Main_MouseWheel);
            pictureBox_Main.MouseDoubleClick += new MouseEventHandler(PictureBox_Main_MouseDoubleClick);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }

        private void FormShowPicture_Load(object sender, EventArgs e)
        {
            Text = "Viewer";
            Config.Load(Common.xmlpath);

            CreateGraphics(pictureBox_Main, ref graph);

            _matAffine = new Matrix();
            canvas = new Bitmap(pp);

            lblImageSize.Text = string.Concat("     [", canvas.Width, "x", canvas.Height, "] pixels.");
            _matAffine.ZoomFit(pictureBox_Main, canvas);
            _defaultM11 = _matAffine.MatrixElements.M11;

            RedrawImage();
        }

        private void PictureBox_Main_Paint(object sender, PaintEventArgs e)
        {
            DrawInformation();
        }

        /*protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_SIZING:
                    RECT r = (RECT)Marshal.PtrToStructure(m.LParam, typeof(RECT));
                    int w = r.right - r.left - (Size.Width - ClientSize.Width);
                    int h = r.bottom - r.top - (Size.Height - ClientSize.Height);
                    int dw = (int)(h * fixedRate + 0.5) - w;
                    int dh = (int)(w / fixedRate + 0.5) - h;
                    switch (m.WParam.ToInt32())
                    {
                        case WMSZ_TOP:
                        case WMSZ_BOTTOM:
                            r.right += dw;
                            pictureBox_Main.ClientSize = new Size(ClientSize.Width, ClientSize.Height);
                            break;
                        case WMSZ_LEFT:
                        case WMSZ_RIGHT:
                            r.bottom += dh;
                            pictureBox_Main.ClientSize = new Size(ClientSize.Width, ClientSize.Height);
                            break;
                        case WMSZ_TOPLEFT:
                            if (dw > 0)
                            {
                                r.left -= dw;
                                pictureBox_Main.ClientSize = new Size(ClientSize.Width, ClientSize.Height);
                            }
                            else
                            {
                                r.top -= dh;
                                pictureBox_Main.ClientSize = new Size(ClientSize.Width, ClientSize.Height);
                            }
                            break;
                        case WMSZ_TOPRIGHT:
                            if (dw > 0)
                            {
                                r.right += dw;
                                pictureBox_Main.ClientSize = new Size(ClientSize.Width, ClientSize.Height);
                            }
                            else
                            {
                                r.top -= dh;
                                pictureBox_Main.ClientSize = new Size(ClientSize.Width, ClientSize.Height);
                            }
                            break;
                        case WMSZ_BOTTOMLEFT:
                            if (dw > 0)
                            {
                                r.left -= dw;
                                pictureBox_Main.ClientSize = new Size(ClientSize.Width, ClientSize.Height);
                            }
                            else
                            {
                                r.bottom += dh;
                                pictureBox_Main.ClientSize = new Size(ClientSize.Width, ClientSize.Height);
                            }
                            break;
                        case WMSZ_BOTTOMRIGHT:
                            if (dw > 0)
                            {
                                r.right += dw;
                                pictureBox_Main.ClientSize = new Size(ClientSize.Width, ClientSize.Height);
                            }
                            else
                            {
                                r.bottom += dh;
                                pictureBox_Main.ClientSize = new Size(ClientSize.Width, ClientSize.Height);
                            }
                            break;
                    }

                    zoomRatio = 1.0f;
                    _originPoint.X = 0;
                    _originPoint.Y = 0;
                    Refresh();

                    Marshal.StructureToPtr(r, m.LParam, false);
                    goto default;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }*/

        private void PictureBox_Main_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox_Main.Focus();
            _op.X = e.X;
            _op.Y = e.Y;

            if (e.Button == MouseButtons.Left)
            {
                if (_matAffine.MatrixElements.M11 >  _defaultM11 || _matAffine.MatrixElements.M11 < _defaultM11)
                {
                    Cursor = Cursors.NoMove2D;
                }
                else
                {
                    Cursor = Cursors.No;
                }
                _mdf = true;
            }
            else if (e.Button == MouseButtons.Right)
            {
            }
        }

        private void PictureBox_Main_MouseMove(object sender, MouseEventArgs e)
        {
            if (_mdf && e.Button == MouseButtons.Left)
            {
                if (_matAffine.MatrixElements.M11 > _defaultM11 || _matAffine.MatrixElements.M11 < _defaultM11)
                {
                    _matAffine.Translate(e.X - _op.X, e.Y - _op.Y, System.Drawing.Drawing2D.MatrixOrder.Append);
                    RedrawImage();

                    _op.X = e.X;
                    _op.Y = e.Y;
                }
                else
                {

                }

            }
            lblMousePosition.Text = $"Mouse {e.Location}";
            var invert = _matAffine.Clone();
            invert.Invert();

            var pf = new PointF[] { e.Location };
            invert.TransformPoints(pf);

            lblImageXY.Text = $"Image {pf[0]}";
        }

        private void PictureBox_Main_MouseUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    _mdf = false;
                    Cursor = Cursors.Default;
                    break;
            }
        }

        private void PictureBox_Main_MouseWheel(object sender, MouseEventArgs e)
        {
            if ((ModifierKeys & Keys.Shift) == Keys.Shift)
            {
                _skf = true;
            }
            else
            {
                _skf = false;
            }

            if (e.Delta > 0)
            {
                if (_skf)
                {
                    _matAffine.RotateAt(5f, e.Location, MatrixOrder.Append);
                }
                else
                {
                    if (zoomRatio < 1000f)
                    {
                        _matAffine.ScaleAt(1.5f, e.Location);
                    }
                    else
                    {
                        System.Media.SystemSounds.Asterisk.Play();
                        zoomRatio = 1000f;
                    }
                }
            }
            else
            {
                if (_skf)
                {
                    _matAffine.RotateAt(-5f, e.Location, MatrixOrder.Append);
                }
                else
                {
                    if (zoomRatio > 10f)
                    {
                        _matAffine.ScaleAt(1.0f / 1.5f, e.Location);
                    }
                    else
                    {
                        System.Media.SystemSounds.Asterisk.Play();
                        zoomRatio = 10f;
                    }
                }
            }
            RedrawImage();
        }

        private void PictureBox_Main_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            zoomRatio = _defaultM11;
            _matAffine.ZoomFit(pictureBox_Main, canvas);
            RedrawImage();
        }

        private void DrawInformation()
        {
            SBGroup sbg = new();
            if (_matAffine.MatrixElements.M11 > _defaultM11)
            {
                statusLabel_size.Text = sbg.strZR1.Append(Math.Truncate(zoomRatio * 100.0) / 100.0).ToString();
            }
            else if (_matAffine.MatrixElements.M11 < _defaultM11)
            {
                statusLabel_size.Text = sbg.strZR2.Append(Math.Truncate(zoomRatio * 100.0) / 100.0).ToString();
            }
            else
            {
                statusLabel_size.Text = sbg.strZR3.ToString();
            }
            zoomRatio = _matAffine.MatrixElements.M11 / _defaultM11 * 100;
        }

        private static void CreateGraphics(PictureBox pic, ref Graphics g)
        {
            if (g != null)
            {
                g.Dispose();
                g = null;
            }
            if (pic.Image != null)
            {
                pic.Image.Dispose();
                pic.Image = null;
            }

            if ((pic.Width == 0) || (pic.Height == 0))
            {
                return;
            }

            pic.Image = new Bitmap(pic.Width, pic.Height);

            g = Graphics.FromImage(pic.Image);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

        }

        private void RedrawImage()
        {
            if (graph is null)
            {

            }
            else
            {
                graph.Clear(pictureBox_Main.BackColor);
                graph.DrawImage(canvas, _matAffine);
                pictureBox_Main.Refresh();
            }
        }

        private void FormShowPicture_FormClosing(object sender, FormClosingEventArgs e)
        {
            graph.Dispose();
            canvas.Dispose();
        }

        private void FormShowPicture_Resize(object sender, EventArgs e)
        {
            CreateGraphics(pictureBox_Main, ref graph);
            RedrawImage();
        }

        private void FormShowPicture_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Modifiers & Keys.Shift) == Keys.Shift)
                Cursor = RotateCursor;
        }

        private void FormShowPicture_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.Modifiers & Keys.Shift) == Keys.Shift)
                Cursor = Cursors.Default;
        }
    }
}
