using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Login
{
    [ToolboxItem(true)]
    public class RoundLabel : Label
    {
        private int _borderRadius = 10;
        private float _borderThickness = 0; // Không viền
        private Color _borderColor = Color.Transparent;

        public RoundLabel()
        {
            this.DoubleBuffered = true;
            this.AutoSize = false; // Tắt auto size để dễ chỉnh khung
            this.Size = new Size(100, 30);
            this.TextAlign = ContentAlignment.MiddleCenter;
            this.BackColor = Color.Transparent;
        }

        [Category("Code Của Tao")]
        public int BorderRadius
        {
            get { return _borderRadius; }
            set { _borderRadius = value; Invalidate(); }
        }

        [Category("Code Của Tao")]
        public float BorderThickness
        {
            get { return _borderThickness; }
            set { _borderThickness = value; Invalidate(); }
        }

        [Category("Code Của Tao")]
        public Color BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; Invalidate(); }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            TextRenderer.DrawText(e.Graphics, this.Text, this.Font, this.ClientRectangle, this.ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            using (GraphicsPath path = new GraphicsPath())
            {
                Rectangle rect = this.ClientRectangle;
                // Tạo path bo tròn
                path.AddArc(rect.X, rect.Y, _borderRadius * 2, _borderRadius * 2, 180, 90);
                path.AddArc(rect.Right - _borderRadius * 2, rect.Y, _borderRadius * 2, _borderRadius * 2, 270, 90);
                path.AddArc(rect.Right - _borderRadius * 2, rect.Bottom - _borderRadius * 2, _borderRadius * 2, _borderRadius * 2, 0, 90);
                path.AddArc(rect.X, rect.Bottom - _borderRadius * 2, _borderRadius * 2, _borderRadius * 2, 90, 90);
                path.CloseFigure();

                this.Region = new Region(path); // Cắt form

                // Vẽ nền (quan trọng)
                using (SolidBrush brush = new SolidBrush(this.BackColor))
                    e.Graphics.FillPath(brush, path);

                // Vẽ chữ
                string text = this.Text;
                SizeF textSize = e.Graphics.MeasureString(text, this.Font);
                float x = (this.Width - textSize.Width) / 2;
                float y = (this.Height - textSize.Height) / 2;

                if (this.TextAlign == ContentAlignment.MiddleLeft) x = 5;

                using (Brush textBrush = new SolidBrush(this.ForeColor))
                    e.Graphics.DrawString(text, this.Font, textBrush, x, y);

                // Vẽ viền (nếu có)
                if (_borderThickness > 0)
                {
                    using (Pen pen = new Pen(_borderColor, _borderThickness))
                        e.Graphics.DrawPath(pen, path);
                }
            }
        }
        protected override void OnTextChanged(EventArgs e) { base.OnTextChanged(e); Invalidate(); }
    }
}