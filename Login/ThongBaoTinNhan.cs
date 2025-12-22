using System;
using System.Drawing;
using System.Windows.Forms;

namespace Login
{
    public partial class ThongBaoTinNhan : Form
    {
        public ThongBaoTinNhan()
        {
            InitializeComponent();
        }

        // Hàm này để ChatForm gọi khi có thông báo mới
        public void ThemThongBaoMoi(UserControl item)
        {
            // Thêm item mới lên ĐẦU danh sách (SetChildIndex = 0)
            flowLayoutPanel1.Controls.Add(item);
            flowLayoutPanel1.Controls.SetChildIndex(item, 0);
        }

        // Sự kiện khi form mất tiêu điểm (bấm ra ngoài) thì tự ẩn đi (Giống Messenger)
        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            this.Hide();
        }
    }
}