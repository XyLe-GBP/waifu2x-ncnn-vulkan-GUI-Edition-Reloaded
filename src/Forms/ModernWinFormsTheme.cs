using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace NVGE
{
    internal static class ModernWinFormsTheme
    {
        private static readonly Color WindowColor = Color.FromArgb(245, 247, 251);
        private static readonly Color SurfaceColor = Color.FromArgb(255, 255, 255);
        private static readonly Color SurfaceHoverColor = Color.FromArgb(239, 246, 255);
        private static readonly Color BorderColor = Color.FromArgb(203, 213, 225);
        private static readonly Color TextColor = Color.FromArgb(30, 41, 59);
        private static readonly Color MutedTextColor = Color.FromArgb(71, 85, 105);
        private static readonly Color AccentColor = Color.FromArgb(37, 99, 235);
        private static readonly Color AccentHoverColor = Color.FromArgb(29, 78, 216);
        private static readonly Color AccentPressedColor = Color.FromArgb(30, 64, 175);
        private static readonly Color DangerColor = Color.FromArgb(220, 38, 38);
        private static readonly Color DangerHoverColor = Color.FromArgb(185, 28, 28);
        private static readonly ToolStripRenderer ToolStripRenderer = new ModernToolStripRenderer();

        public static void Apply(Form form)
        {
            form.SuspendLayout();
            form.BackColor = WindowColor;
            form.ForeColor = TextColor;

            form.HandleCreated -= Form_HandleCreated;
            form.HandleCreated += Form_HandleCreated;

            if (form.IsHandleCreated)
            {
                ApplyWindowChrome(form);
            }

            foreach (Control control in form.Controls)
            {
                ApplyControl(control, form.BackColor);
            }

            form.ResumeLayout(true);
        }

        private static void Form_HandleCreated(object sender, EventArgs e)
        {
            if (sender is Form form)
            {
                ApplyWindowChrome(form);
            }
        }

        private static void ApplyControl(Control control, Color parentBackColor)
        {
            control.ForeColor = TextColor;

            switch (control)
            {
                case Button button:
                    StyleButton(button);
                    break;
                case CheckBox checkBox:
                    StyleCheckBox(checkBox, parentBackColor);
                    break;
                case RadioButton radioButton:
                    StyleRadioButton(radioButton, parentBackColor);
                    break;
                case ComboBox comboBox:
                    StyleComboBox(comboBox);
                    break;
                case TextBox textBox:
                    StyleTextBox(textBox);
                    break;
                case LinkLabel linkLabel:
                    StyleLinkLabel(linkLabel, parentBackColor);
                    break;
                case Label label:
                    StyleLabel(label, parentBackColor);
                    break;
                case GroupBox groupBox:
                    StyleGroupBox(groupBox, parentBackColor);
                    break;
                case TabControl tabControl:
                    StyleTabControl(tabControl);
                    break;
                case TabPage tabPage:
                    StyleTabPage(tabPage);
                    break;
                case MenuStrip menuStrip:
                    StyleToolStrip(menuStrip);
                    break;
                case StatusStrip statusStrip:
                    StyleToolStrip(statusStrip);
                    statusStrip.SizingGrip = false;
                    break;
                case ProgressBar progressBar:
                    progressBar.ForeColor = AccentColor;
                    break;
                case PictureBox pictureBox:
                    if (pictureBox.SizeMode != PictureBoxSizeMode.Zoom && pictureBox.Dock == DockStyle.None)
                    {
                        pictureBox.BackColor = Color.Transparent;
                    }
                    break;
                default:
                    if (control.Controls.Count > 0)
                    {
                        control.BackColor = parentBackColor;
                    }
                    break;
            }

            Color childBackColor = control.BackColor == Color.Transparent ? parentBackColor : control.BackColor;

            foreach (Control child in control.Controls)
            {
                ApplyControl(child, childBackColor);
            }
        }

        private static void StyleButton(Button button)
        {
            button.EnabledChanged -= Button_EnabledChanged;
            button.EnabledChanged += Button_EnabledChanged;
            RemoveRoundedRegion(button);
            button.UseVisualStyleBackColor = false;
            button.FlatStyle = FlatStyle.Flat;
            button.Cursor = Cursors.Hand;
            button.AutoEllipsis = true;
            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.BorderColor = BorderColor;
            UpdateButtonStateColors(button);
        }

        private static void Button_EnabledChanged(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                UpdateButtonStateColors(button);
            }
        }

        private static void UpdateButtonStateColors(Button button)
        {
            bool primary = IsPrimaryButton(button);
            bool danger = IsDangerButton(button);

            if (!button.Enabled)
            {
                button.BackColor = Color.FromArgb(226, 232, 240);
                button.ForeColor = MutedTextColor;
                button.FlatAppearance.MouseOverBackColor = button.BackColor;
                button.FlatAppearance.MouseDownBackColor = button.BackColor;
            }
            else if (danger)
            {
                button.BackColor = DangerColor;
                button.ForeColor = Color.White;
                button.FlatAppearance.MouseOverBackColor = DangerHoverColor;
                button.FlatAppearance.MouseDownBackColor = DangerHoverColor;
            }
            else if (primary)
            {
                button.BackColor = AccentColor;
                button.ForeColor = Color.White;
                button.FlatAppearance.MouseOverBackColor = AccentHoverColor;
                button.FlatAppearance.MouseDownBackColor = AccentPressedColor;
            }
            else
            {
                button.BackColor = SurfaceColor;
                button.ForeColor = TextColor;
                button.FlatAppearance.MouseOverBackColor = SurfaceHoverColor;
                button.FlatAppearance.MouseDownBackColor = Color.FromArgb(219, 234, 254);
            }

            button.FlatAppearance.CheckedBackColor = AccentColor;
        }

        private static void StyleCheckBox(CheckBox checkBox, Color parentBackColor)
        {
            checkBox.UseVisualStyleBackColor = false;
            checkBox.BackColor = parentBackColor;
            checkBox.ForeColor = TextColor;
        }

        private static void StyleRadioButton(RadioButton radioButton, Color parentBackColor)
        {
            radioButton.UseVisualStyleBackColor = false;
            radioButton.BackColor = parentBackColor;
            radioButton.ForeColor = TextColor;
        }

        private static void StyleComboBox(ComboBox comboBox)
        {
            comboBox.BackColor = SurfaceColor;
            comboBox.ForeColor = TextColor;
            comboBox.FlatStyle = FlatStyle.Flat;
        }

        private static void StyleTextBox(TextBox textBox)
        {
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.BackColor = textBox.ReadOnly ? Color.FromArgb(241, 245, 249) : SurfaceColor;
            textBox.ForeColor = TextColor;
        }

        private static void StyleLabel(Label label, Color parentBackColor)
        {
            label.ForeColor = label.Enabled ? TextColor : MutedTextColor;

            if (label.BorderStyle != BorderStyle.None)
            {
                label.BorderStyle = BorderStyle.None;
                label.BackColor = SurfaceColor;
                label.Padding = new Padding(10, 8, 10, 8);
                label.Paint -= BorderedLabel_Paint;
                label.Paint += BorderedLabel_Paint;
                ApplyRoundedRegion(label, 8);
            }
            else
            {
                label.BackColor = parentBackColor;
            }
        }

        private static void StyleLinkLabel(LinkLabel linkLabel, Color parentBackColor)
        {
            linkLabel.BackColor = parentBackColor;
            linkLabel.LinkColor = AccentColor;
            linkLabel.ActiveLinkColor = AccentHoverColor;
            linkLabel.VisitedLinkColor = AccentPressedColor;
        }

        private static void StyleGroupBox(GroupBox groupBox, Color parentBackColor)
        {
            groupBox.BackColor = parentBackColor;
            groupBox.ForeColor = TextColor;
            groupBox.FlatStyle = FlatStyle.Flat;
            groupBox.Paint -= GroupBox_Paint;
            groupBox.Paint += GroupBox_Paint;
        }

        private static void StyleTabControl(TabControl tabControl)
        {
            tabControl.BackColor = WindowColor;
            tabControl.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl.Padding = new Point(18, 6);
            tabControl.DrawItem -= TabControl_DrawItem;
            tabControl.DrawItem += TabControl_DrawItem;
        }

        private static void StyleTabPage(TabPage tabPage)
        {
            tabPage.UseVisualStyleBackColor = false;
            tabPage.BackColor = SurfaceColor;
            tabPage.ForeColor = TextColor;
        }

        private static void StyleToolStrip(ToolStrip toolStrip)
        {
            toolStrip.BackColor = SurfaceColor;
            toolStrip.ForeColor = TextColor;
            toolStrip.RenderMode = ToolStripRenderMode.Professional;
            toolStrip.Renderer = ToolStripRenderer;
            toolStrip.GripStyle = ToolStripGripStyle.Hidden;
            StyleToolStripItems(toolStrip.Items);
        }

        private static void StyleToolStripItems(ToolStripItemCollection items)
        {
            foreach (ToolStripItem item in items)
            {
                item.ForeColor = item.Name.Contains("exit", StringComparison.OrdinalIgnoreCase)
                    ? DangerColor
                    : TextColor;

                if (item is ToolStripMenuItem menuItem)
                {
                    menuItem.DropDown.BackColor = SurfaceColor;
                    menuItem.DropDown.ForeColor = TextColor;
                    menuItem.DropDown.Renderer = ToolStripRenderer;
                    StyleToolStripItems(menuItem.DropDownItems);
                }
            }
        }

        private static bool IsPrimaryButton(Button button)
        {
            string name = button.Name.ToLowerInvariant();
            string text = button.Text.Trim().ToLowerInvariant();

            return name.Contains("_ok")
                || name.EndsWith("ok", StringComparison.Ordinal)
                || text == "ok"
                || text == "done!";
        }

        private static bool IsDangerButton(Button button)
        {
            string name = button.Name.ToLowerInvariant();
            string text = button.Text.Trim().ToLowerInvariant();

            return name.Contains("abort")
                || text.Contains("abort", StringComparison.OrdinalIgnoreCase);
        }

        private static void ApplyRoundedRegion(Control control, int radius)
        {
            control.Resize -= RoundedControl_Resize;
            control.Resize += RoundedControl_Resize;
            UpdateRoundedRegion(control, radius);
        }

        private static void RemoveRoundedRegion(Control control)
        {
            control.Resize -= RoundedControl_Resize;
            Region oldRegion = control.Region;
            control.Region = null;
            oldRegion?.Dispose();
        }

        private static void RoundedControl_Resize(object sender, EventArgs e)
        {
            if (sender is Control control)
            {
                UpdateRoundedRegion(control, 8);
            }
        }

        private static void UpdateRoundedRegion(Control control, int radius)
        {
            if (control.Width <= 1 || control.Height <= 1)
            {
                return;
            }

            Region oldRegion = control.Region;
            using GraphicsPath path = CreateRoundedRectangle(new Rectangle(0, 0, control.Width, control.Height), radius);
            control.Region = new Region(path);
            oldRegion?.Dispose();
        }

        private static void GroupBox_Paint(object sender, PaintEventArgs e)
        {
            if (sender is not GroupBox groupBox)
            {
                return;
            }

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.Clear(groupBox.BackColor);

            Rectangle borderBounds = groupBox.ClientRectangle;
            borderBounds.X += 1;
            borderBounds.Y += 8;
            borderBounds.Width -= 3;
            borderBounds.Height -= 10;

            using GraphicsPath path = CreateRoundedRectangle(borderBounds, 8);
            using SolidBrush fillBrush = new(groupBox.BackColor);
            using Pen borderPen = new(BorderColor);
            e.Graphics.FillPath(fillBrush, path);
            e.Graphics.DrawPath(borderPen, path);

            if (!string.IsNullOrWhiteSpace(groupBox.Text))
            {
                Rectangle textBounds = new(12, 0, Math.Max(0, groupBox.Width - 24), 18);
                TextRenderer.DrawText(
                    e.Graphics,
                    groupBox.Text,
                    groupBox.Font,
                    textBounds,
                    MutedTextColor,
                    TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
            }
        }

        private static void BorderedLabel_Paint(object sender, PaintEventArgs e)
        {
            if (sender is not Label label)
            {
                return;
            }

            Rectangle bounds = label.ClientRectangle;
            bounds.Width -= 1;
            bounds.Height -= 1;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using GraphicsPath path = CreateRoundedRectangle(bounds, 8);
            using Pen pen = new(BorderColor)
            {
                DashStyle = DashStyle.Dash
            };
            e.Graphics.DrawPath(pen, path);
        }

        private static void TabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (sender is not TabControl tabControl || e.Index < 0 || e.Index >= tabControl.TabPages.Count)
            {
                return;
            }

            TabPage page = tabControl.TabPages[e.Index];
            bool selected = e.Index == tabControl.SelectedIndex;
            Rectangle bounds = tabControl.GetTabRect(e.Index);
            bounds.Inflate(-2, -2);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using GraphicsPath path = CreateRoundedRectangle(bounds, 8);
            using SolidBrush fillBrush = new(selected ? SurfaceColor : WindowColor);
            e.Graphics.FillPath(fillBrush, path);

            if (selected)
            {
                using Pen pen = new(BorderColor);
                e.Graphics.DrawPath(pen, path);
            }

            TextRenderer.DrawText(
                e.Graphics,
                page.Text,
                tabControl.Font,
                bounds,
                selected ? TextColor : MutedTextColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
        }

        private static GraphicsPath CreateRoundedRectangle(Rectangle bounds, int radius)
        {
            int diameter = Math.Min(radius * 2, Math.Min(bounds.Width, bounds.Height));
            GraphicsPath path = new();

            if (diameter <= 1)
            {
                path.AddRectangle(bounds);
                path.CloseFigure();
                return path;
            }

            Rectangle arc = new(bounds.Location, new Size(diameter, diameter));
            path.AddArc(arc, 180, 90);
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);
            path.CloseFigure();
            return path;
        }

        private static void ApplyWindowChrome(Form form)
        {
            if (form.FormBorderStyle == FormBorderStyle.None)
            {
                return;
            }

            try
            {
                int cornerPreference = 2;
                int captionColor = ToColorRef(WindowColor);
                int borderColor = ToColorRef(BorderColor);
                int textColor = ToColorRef(TextColor);

                DwmSetWindowAttribute(form.Handle, 33, ref cornerPreference, sizeof(int));
                DwmSetWindowAttribute(form.Handle, 35, ref captionColor, sizeof(int));
                DwmSetWindowAttribute(form.Handle, 34, ref borderColor, sizeof(int));
                DwmSetWindowAttribute(form.Handle, 36, ref textColor, sizeof(int));
            }
            catch (DllNotFoundException)
            {
            }
            catch (EntryPointNotFoundException)
            {
            }
            catch (ExternalException)
            {
            }
        }

        private static int ToColorRef(Color color)
        {
            return color.R | (color.G << 8) | (color.B << 16);
        }

        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attribute, ref int attributeValue, int attributeSize);

        private sealed class ModernToolStripRenderer : ToolStripProfessionalRenderer
        {
            public ModernToolStripRenderer()
                : base(new ModernColorTable())
            {
            }

            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
            {
                if (!e.Item.Selected && !e.Item.Pressed)
                {
                    return;
                }

                Rectangle bounds = new(Point.Empty, e.Item.Size);
                bounds.Inflate(-2, -2);

                using GraphicsPath path = CreateRoundedRectangle(bounds, 6);
                using SolidBrush brush = new(e.Item.Pressed ? SurfaceHoverColor : Color.FromArgb(232, 240, 254));
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.FillPath(brush, path);
            }

            protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
            {
                using Pen pen = new(BorderColor);

                if (e.ToolStrip is StatusStrip)
                {
                    e.Graphics.DrawLine(pen, 0, 0, e.ToolStrip.Width, 0);
                    return;
                }

                if (e.ToolStrip is ToolStripDropDown)
                {
                    Rectangle bounds = new(Point.Empty, e.ToolStrip.Size);
                    bounds.Width -= 1;
                    bounds.Height -= 1;
                    e.Graphics.DrawRectangle(pen, bounds);
                }
            }

            protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
            {
                using Pen pen = new(BorderColor);
                int y = e.Item.Height / 2;
                e.Graphics.DrawLine(pen, 8, y, e.Item.Width - 8, y);
            }
        }

        private sealed class ModernColorTable : ProfessionalColorTable
        {
            public override Color ToolStripDropDownBackground => SurfaceColor;
            public override Color ImageMarginGradientBegin => SurfaceColor;
            public override Color ImageMarginGradientMiddle => SurfaceColor;
            public override Color ImageMarginGradientEnd => SurfaceColor;
            public override Color MenuBorder => BorderColor;
            public override Color MenuItemBorder => Color.Transparent;
            public override Color MenuItemSelected => SurfaceHoverColor;
            public override Color MenuItemSelectedGradientBegin => SurfaceHoverColor;
            public override Color MenuItemSelectedGradientEnd => SurfaceHoverColor;
            public override Color MenuItemPressedGradientBegin => SurfaceHoverColor;
            public override Color MenuItemPressedGradientMiddle => SurfaceHoverColor;
            public override Color MenuItemPressedGradientEnd => SurfaceHoverColor;
            public override Color ToolStripBorder => BorderColor;
            public override Color ToolStripGradientBegin => SurfaceColor;
            public override Color ToolStripGradientMiddle => SurfaceColor;
            public override Color ToolStripGradientEnd => SurfaceColor;
            public override Color StatusStripGradientBegin => SurfaceColor;
            public override Color StatusStripGradientEnd => SurfaceColor;
        }
    }
}
