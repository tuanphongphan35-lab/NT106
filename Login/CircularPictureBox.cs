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
    internal class CircularPictureBox : PictureBox
    {
        protected override void OnPaint(PaintEventArgs pe)
        {
            // Tạo một GraphicsPath để định hình vùng cắt
            GraphicsPath g = new GraphicsPath();

            // Vẽ hình Elip (hình tròn) vừa khít với kích thước của control
            // Lưu ý: Phải dùng ClientRectangle để lấy đúng kích thước bên trong, không tính viền
            g.AddEllipse(0, 0, ClientSize.Width, ClientSize.Height);

            // Đặt vùng cắt (Clip) cho Graphics
            pe.Graphics.SetClip(g);

            // Gọi phương thức OnPaint gốc để vẽ ảnh (Image)
            // Bức ảnh sẽ chỉ được vẽ bên trong vùng tròn đã định nghĩa
            base.OnPaint(pe);

            // Thêm phần này để vẽ viền cho đẹp (tùy chọn)
            // Bạn có thể chỉnh màu (Color.RoyalBlue) và độ dày (1.5f)
            pe.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            pe.Graphics.DrawEllipse(new Pen(Color.RoyalBlue, 1.5f), 0, 0, ClientSize.Width - 1, ClientSize.Height - 1);
        }
    }
}
