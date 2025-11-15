using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Login
{
    [DefaultEvent("_TextChanged")]
    [ToolboxItem(true)]
    public class RoundTextBox : UserControl
    {
        private Color borderColor = Color.MediumSlateBlue;
        private int borderSize = 0; // Mặc định KHÔNG VIỀN
        private bool underlinedStyle = false;
        private Color borderFocusColor = Color.HotPink;
        private bool isFocused = false;
        private int borderRadius = 15;
        private TextBox textBox1;

        public event EventHandler _TextChanged;

        public RoundTextBox()
        {
            textBox1 = new TextBox();
            this.SuspendLayout();
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Dock = DockStyle.Fill;
            textBox1.BackColor = this.BackColor;
            textBox1.ForeColor = this.ForeColor;
            textBox1.Enter += (s, e) => { isFocused = true; this.Invalidate(); };
            textBox1.Leave += (s, e) => { isFocused = false; this.Invalidate(); };
            textBox1.TextChanged += (s, e) => { if (_TextChanged != null) _TextChanged.Invoke(s, e); };

            this.Controls.Add(textBox1);
            this.Padding = new Padding(10, 7, 10, 7);
            this.Size = new Size(250, 30);
            this.BackColor = Color.White;
            this.ResumeLayout(false);
        }

        [Category("Code Của Tao")] public Color BorderColor { get => borderColor; set { borderColor = value; Invalidate(); } }
        [Category("Code Của Tao")] public int BorderSize { get => borderSize; set { borderSize = value; Invalidate(); } }
        [Category("Code Của Tao")] public bool UnderlinedStyle { get => underlinedStyle; set { underlinedStyle = value; Invalidate(); } }
        [Category("Code Của Tao")] public bool PasswordChar { get => textBox1.UseSystemPasswordChar; set => textBox1.UseSystemPasswordChar = value; }
        [Category("Code Của Tao")] public bool Multiline { get => textBox1.Multiline; set => textBox1.Multiline = value; }
        [Category("Code Của Tao")] public override Color BackColor { get => base.BackColor; set { base.BackColor = value; textBox1.BackColor = value; } }
        [Category("Code Của Tao")] public override Color ForeColor { get => base.ForeColor; set { base.ForeColor = value; textBox1.ForeColor = value; } }
        [Category("Code Của Tao")] public Color BorderFocusColor { get => borderFocusColor; set => borderFocusColor = value; }
        [Category("Code Của Tao")] public int BorderRadius { get => borderRadius; set { if (value >= 0) { borderRadius = value; Invalidate(); } } }
        [Category("Code Của Tao")] public string Texts { get => textBox1.Text; set => textBox1.Text = value; }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graph = e.Graphics;
            graph.SmoothingMode = SmoothingMode.AntiAlias;
            graph.PixelOffsetMode = PixelOffsetMode.HighQuality;

            using (GraphicsPath pathSurface = GetFigurePath(this.ClientRectangle, borderRadius))
            using (GraphicsPath pathBorder = GetFigurePath(Rectangle.Inflate(this.ClientRectangle, -borderSize, -borderSize), borderRadius))
            using (Pen penSurface = new Pen(this.Parent.BackColor, 2))
            using (Pen penBorder = new Pen(borderColor, borderSize))
            {
                if (underlinedStyle)
                {
                    this.Region = new Region(this.ClientRectangle);
                    if (borderSize >= 1)
                    {
                        if (isFocused) penBorder.Color = borderFocusColor;
                        graph.DrawLine(penBorder, 0, this.Height - 1, this.Width, this.Height - 1);
                    }
                }
                else
                {
                    this.Region = new Region(pathSurface);
                    graph.DrawPath(penSurface, pathSurface); // Vẽ viền nền để khử răng cưa

                    if (borderSize >= 1) // Chỉ vẽ viền nếu size > 0
                    {
                        if (isFocused) penBorder.Color = borderFocusColor;
                        graph.DrawPath(penBorder, pathSurface);
                    }
                }
            }
        }

        private GraphicsPath GetFigurePath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            float curveSize = radius * 2F;
            path.StartFigure();
            path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
            path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
            path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
            path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
            path.CloseFigure();
            return path;
        }
        protected override void OnResize(EventArgs e) { base.OnResize(e); if (this.DesignMode) UpdateControlHeight(); }
        protected override void OnLoad(EventArgs e) { base.OnLoad(e); UpdateControlHeight(); }
        private void UpdateControlHeight() { if (!textBox1.Multiline) { int txtHeight = TextRenderer.MeasureText("Text", this.Font).Height + 1; textBox1.Multiline = true; textBox1.MinimumSize = new Size(0, txtHeight); textBox1.Multiline = false; this.Height = textBox1.Height + this.Padding.Top + this.Padding.Bottom; } }
    }
}