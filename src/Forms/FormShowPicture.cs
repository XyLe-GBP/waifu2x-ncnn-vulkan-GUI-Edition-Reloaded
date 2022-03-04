using BitmapUtils;
using System;
using System.Drawing;
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
        private PointF _originPoint;
        private PointF _oldPoint;
        private RectangleF _originRect;
        #endregion

        internal struct StrB
        {
            public StrB() { }
            public StringBuilder strZR1 = new("Zoom Ratio: x");
            public StringBuilder strZR2 = new("Zoom Ratio: -");
            public StringBuilder strZR3 = new("Zoom Ratio: Default");
            public StringBuilder strPosX = new("posX: ");
            public StringBuilder strPosY = new("posY: ");
        }

        private bool _mdf = false;
        private float zoomRatio = 1.0f;
        private readonly string pp;
        private Image bitmap;
        private Bitmap canvas;
        private Graphics graph;

        public FormShowPicture(string Imagepath)
        {
            InitializeComponent();

            pp = Imagepath;
            statusLabel_X.Alignment = ToolStripItemAlignment.Right;
            statusLabel_Y.Alignment = ToolStripItemAlignment.Right;
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

            bitmap = Image.FromFile(pp);
            canvas = new Bitmap(bitmap.Width, bitmap.Height);
            BitmapPlus bmpP = new(canvas);

            bmpP.BeginAccess();
            for (int i = 0; i < canvas.Width; i++)
            {
                for (int j = 0; j < canvas.Height; j++)
                {
                    Color bmpCol = bmpP.GetPixel(i, j);
                    if (bmpCol.R == (byte)0)
                    {
                        bmpP.SetPixel(i, j, Color.FromArgb(255, 255, 0, 0));
                    }
                    else
                    {
                        bmpP.SetPixel(i, j, Color.FromArgb(255, 0, 0, 0));
                    }
                }
            }
            bmpP.EndAccess();

            graph = Graphics.FromImage(canvas);

            fixedRate = (double)GetAspectRetio(bitmap.Width, bitmap.Height)[0] / GetAspectRetio(bitmap.Width, bitmap.Height)[1];
            FormSizeAlgorithm();

            graph.DrawImage(bitmap, 0, 0, ClientSize.Width, ClientSize.Height);
            graph.Clear(Color.Transparent);
            pictureBox_Main.Image = canvas;
            Refresh();
        }

        private void PictureBox_Main_Paint(object sender, PaintEventArgs e)
        {
            if (zoomRatio > 1.0f || zoomRatio < 1.0f)
            {
                e.Graphics.TranslateTransform((float)_originPoint.X, (float)_originPoint.Y);
                e.Graphics.ScaleTransform(zoomRatio, zoomRatio);
                e.Graphics.DrawImage(bitmap, _originRect);
                e.Graphics.ResetTransform();
            }
            else
            {
                e.Graphics.DrawImage(bitmap, 0, 0, ClientSize.Width, ClientSize.Height);
            }

            pictureBox_Main.Image = canvas;

            DrawInformation();
        }

        protected override void WndProc(ref Message m)
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
                    _originRect = new Rectangle(0, 0, ClientSize.Width, ClientSize.Height);
                    Refresh();

                    Marshal.StructureToPtr(r, m.LParam, false);
                    goto default;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        private void PictureBox_Main_MouseDown(object sender, MouseEventArgs e)
        {
            _oldPoint.X = e.X;
            _oldPoint.Y = e.Y;

            if (e.Button == MouseButtons.Left)
            {
                if (zoomRatio > 1.0f || zoomRatio < 1.0f)
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
                if (zoomRatio > 1.0f || zoomRatio < 1.0f)
                {
                    _originPoint.X += e.X - _oldPoint.X;
                    _originPoint.Y += e.Y - _oldPoint.Y;

                    ResizeAlgorithm(_originPoint.X, _originPoint.Y);

                    _oldPoint.X = e.X;
                    _oldPoint.Y = e.Y;
                }
                else
                {

                }

            }
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
            float prevRatio = zoomRatio;

            if (e.Delta > 0)
            {
                if (zoomRatio < 2.0f)
                {
                    zoomRatio += 0.01f;
                }
                else
                {
                    System.Media.SystemSounds.Asterisk.Play();
                    zoomRatio = 2.0f;
                }
            }
            else
            {
                if (zoomRatio > 0.7f)
                {
                    zoomRatio -= 0.01f;
                }
                else
                {
                    System.Media.SystemSounds.Asterisk.Play();
                    zoomRatio = Math.Max(0.7f, zoomRatio - 0.7f);
                }
            }

            _originRect.Width = (float)(pictureBox_Main.Width * zoomRatio);
            _originRect.Height = (float)(pictureBox_Main.Height * zoomRatio);
            _originRect.X = (float)(e.X - (e.X - _originRect.X) * zoomRatio / prevRatio);
            _originRect.Y = (float)(e.Y - (e.Y - _originRect.Y) * zoomRatio / prevRatio);
            Refresh();
        }

        private void PictureBox_Main_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            zoomRatio = 1.0f;
            _originPoint.X = 0;
            _originPoint.Y = 0;
            _originRect = new Rectangle(0, 0, ClientSize.Width, ClientSize.Height);
            Refresh();
        }

        private void ResizeAlgorithm(float x, float y)
        {
            if (_mdf)
            {
                if (zoomRatio > 1.0f)
                {
                    var tl = -_originRect.X;
                    var tt = -_originRect.Y;
                    var tr = pictureBox_Main.Width - _originRect.Width + zoomRatio;
                    var tb = pictureBox_Main.Height - _originRect.Height + zoomRatio;

                    if (_originPoint.X > tl)
                    {
                        _originPoint.X -= tl;
                        _originPoint.X = tl;
                    }
                    else if (_originPoint.X < tr)
                    {
                        _originPoint.X += tr;
                        _originPoint.X = tr;
                    }
                    else if (_originPoint.Y > tt)
                    {
                        _originPoint.Y -= tt;
                        _originPoint.Y = tt;
                    }
                    else if (_originPoint.Y < tb)
                    {
                        _originPoint.Y += tb;
                        _originPoint.Y = tb;
                    }
                    else if (_originPoint.Y > tt && _originPoint.X == tl)
                    {
                        _originPoint.Y -= tt;
                        _originPoint.Y = tt;
                    }
                    else if (_originPoint.Y > tt && _originPoint.X == tr)
                    {
                        _originPoint.Y -= tt;
                        _originPoint.Y = tt;
                    }
                    else if (_originPoint.X == tl)
                    {
                        if (tb > tt)
                        {
                            _originPoint.Y += tb;
                            _originPoint.Y = tb;
                        }
                        else
                        {
                            _originPoint.Y -= tt;
                            _originPoint.Y = tt;
                        }
                    }
                    else if (_originPoint.X == tr)
                    {
                        if (tb > tt)
                        {
                            _originPoint.Y += tb;
                            _originPoint.Y = tb;
                        }
                        else
                        {
                            _originPoint.Y -= tt;
                            _originPoint.Y = tt;
                        }
                    }
                    else
                    {
                        //_originPoint.X = x;
                        //_originPoint.Y = y;
                    }
                }
                else if (zoomRatio < 1.0f)
                {
                    var tl = _originRect.X / zoomRatio;
                    var tt = _originRect.Y / zoomRatio;
                    var tr = pictureBox_Main.Width - _originRect.Width;
                    var tb = pictureBox_Main.Height - _originRect.Height;

                    if (_originPoint.X < tl)
                    {
                        _originPoint.X += tl;
                        _originPoint.X = tl;
                    }
                    else if (_originPoint.X > tr)
                    {
                        _originPoint.X -= tr;
                        _originPoint.X = tr;
                    }
                    else if (_originPoint.Y < tt)
                    {
                        _originPoint.Y += tt;
                        _originPoint.Y = tt;
                    }
                    else if (_originPoint.Y > tb)
                    {
                        _originPoint.Y -= tb;
                        _originPoint.Y = tb;
                    }
                    else if (_originPoint.X == tl)
                    {
                        if (tb < tt)
                        {
                            _originPoint.Y -= tb;
                            _originPoint.Y = tb;
                        }
                        else
                        {
                            _originPoint.Y += tt;
                            _originPoint.Y = tt;
                        }
                    }
                    else if (_originPoint.X == tr)
                    {
                        if (tb < tt)
                        {
                            _originPoint.Y -= tb;
                            _originPoint.Y = tb;
                        }
                        else
                        {
                            _originPoint.Y += tt;
                            _originPoint.Y = tt;
                        }
                    }
                    else
                    {
                        //_originPoint.X = x;
                        //_originPoint.Y = y;
                    }
                }
            }
            else
            {
                return;
            }
        }

        private void DrawInformation()
        {
            StrB strB = new();
            if (zoomRatio > 1.0f)
            {
                statusLabel_size.Text = strB.strZR1.Append(Math.Truncate(zoomRatio * 100.0) / 100.0).ToString();
            }
            else if (zoomRatio < 1.0f)
            {
                statusLabel_size.Text = strB.strZR2.Append(Math.Truncate(zoomRatio * 100.0) / 100.0).ToString();
            }
            else
            {
                statusLabel_size.Text = strB.strZR3.ToString();
            }
            statusLabel_X.Text = strB.strPosX.Append(Math.Truncate(_originPoint.X * 100.0) / 100.0).ToString();
            statusLabel_Y.Text = strB.strPosY.Append(Math.Truncate(_originPoint.Y * 100.0) / 100.0).ToString();
        }

        private void FormSizeAlgorithm()
        {
            int w, h;

            if (bitmap.Width > 3840)
            {
                w = bitmap.Width / 16;
            }
            else if (bitmap.Width >= 2160 && bitmap.Width <= 3840)
            {
                w = bitmap.Width / 8;
            }
            else if (bitmap.Width < 2159 && bitmap.Width >= 1920)
            {
                w = bitmap.Width / 4;
            }
            else if (bitmap.Width < 1921 && bitmap.Width > 1280)
            {
                w = bitmap.Width / 3;
            }
            else
            {
                w = bitmap.Width;
            }

            if (bitmap.Height > 3840)
            {
                h = bitmap.Height / 16;
            }
            else if (bitmap.Height >= 2160 && bitmap.Height <= 3840)
            {
                h = bitmap.Height / 8;
            }
            else if (bitmap.Height < 2159 && bitmap.Width >= 1920)
            {
                h = bitmap.Height / 4;
            }
            else if (bitmap.Height < 1921 && bitmap.Height > 1280)
            {
                h = bitmap.Height / 3;
            }
            else if (bitmap.Height < 1279 && bitmap.Height > 1000)
            {
                h = bitmap.Height / 2;
            }
            else
            {
                h = bitmap.Height;
            }

            ClientSize = new Size(w, h);
            MinimumSize = new Size(w, h);
            MaximumSize = new Size(w * 2, h * 2);

            pictureBox_Main.Size = new Size(ClientSize.Width, ClientSize.Height);
            _originRect = new Rectangle(0, 0, ClientSize.Width, ClientSize.Height);
        }

        private static int[] GetAspectRetio(int width, int height)
        {
            int d = Common.Gcd(width, height);
            return new int[] { width / d, height / d };
        }

        private void FormShowPicture_FormClosing(object sender, FormClosingEventArgs e)
        {
            graph.Dispose();
            bitmap.Dispose();
            canvas.Dispose();
        }
    }
}
