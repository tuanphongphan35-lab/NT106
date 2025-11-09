using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Login
{
    internal class RoundPanel : Panel
    {
        private int _borderRadius = 10;
        private Color _borderColor = Color.Gray;
        private float _borderThickness = 1.0f;

        // ----- Tạo các thuộc tính để bạn có thể chỉnh trong [Properties] -----

        public int BorderRadius
        {
            get { return _borderRadius; }
            set { _borderRadius = value; this.Invalidate(); }
        }

        public Color BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; this.Invalidate(); }
        }

        public float BorderThickness
        {
            get { return _borderThickness; }
            set { _borderThickness = value; this.Invalidate(); }
        }

        // -----------------------------------------------------------------

        // Hàm này dùng để vẽ hình chữ nhật bo góc
        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(rect.Left, rect.Top, radius * 2, radius * 2, 180, 90);
            path.AddArc(rect.Right - radius * 2, rect.Top, radius * 2, radius * 2, 270, 90);
            path.AddArc(rect.Right - radius * 2, rect.Bottom - radius * 2, radius * 2, radius * 2, 0, 90);
            path.AddArc(rect.Left, rect.Bottom - radius * 2, radius * 2, radius * 2, 90, 90);
            path.CloseFigure();
            return path;
        }

        // Ghi đè hàm OnPaint để vẽ lại Panel
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e); // Vẽ nền (BackColor) trước

            Rectangle rect = this.ClientRectangle;

            // Giảm kích thước đi 1 chút để vẽ viền không bị lẹm
            Rectangle borderRect = new Rectangle(
                rect.Location.X, rect.Location.Y,
                rect.Width - 1, rect.Height - 1);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Lấy đường dẫn bo tròn
            using (GraphicsPath path = GetRoundedRectPath(borderRect, _borderRadius))
            {
                // Cắt vùng vẽ theo đường dẫn bo tròn
                // Bất cứ thứ gì được vẽ (kể cả control con) sẽ bị cắt theo
                this.Region = new Region(path);

                // (Tùy chọn) Vẽ đường viền
                using (Pen pen = new Pen(_borderColor, _borderThickness))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            }
        }
    }
}
