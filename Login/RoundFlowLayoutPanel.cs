using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Login
{
    [ToolboxItem(true)]
    public class RoundFlowLayoutPanel : FlowLayoutPanel
    {
        private int _borderRadius = 20;
        private float _borderThickness = 0; // Không viền
        private Color _borderColor = Color.Transparent;

        public RoundFlowLayoutPanel()
        {
            this.DoubleBuffered = true;
        }

        [Category("Code Của Tao")]
        public int BorderRadius
        {
            get { return _borderRadius; }
            set { _borderRadius = value; this.Invalidate(); }
        }

        [Category("Code Của Tao")]
        public float BorderThickness
        {
            get { return _borderThickness; }
            set { _borderThickness = value; this.Invalidate(); }
        }

        [Category("Code Của Tao")]
        public Color BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; this.Invalidate(); }
        }

        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            if (radius <= 0) { path.AddRectangle(rect); return path; }
            path.StartFigure();
            path.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90);
            path.AddArc(rect.Right - radius * 2, rect.Y, radius * 2, radius * 2, 270, 90);
            path.AddArc(rect.Right - radius * 2, rect.Bottom - radius * 2, radius * 2, radius * 2, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radius * 2, radius * 2, radius * 2, 90, 90);
            path.CloseFigure();
            return path;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            Rectangle rect = this.ClientRectangle;
            Rectangle borderRect = new Rectangle(0, 0, rect.Width - 1, rect.Height - 1);

            using (GraphicsPath path = GetRoundedRectPath(borderRect, _borderRadius))
            {
                this.Region = new Region(path);
                if (_borderThickness > 0)
                {
                    using (Pen pen = new Pen(_borderColor, _borderThickness))
                    {
                        e.Graphics.DrawPath(pen, path);
                    }
                }
            }
        }
    }
}