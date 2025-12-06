using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Login
{
    public partial class TimKiemNguoiDung : Form
    {
        public TimKiemNguoiDung()
        {
            InitializeComponent();
        }

        // ✅ Hàm xử lý sự kiện khi nhấn nút "Tìm kiếm"
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string ten = txtTimKiem.Text.Trim();

            // Giả lập dữ liệu mẫu (bạn có thể thay bằng dữ liệu từ DB)
            var ds = new List<dynamic>()
            {
                new { ID = 1, Ten = "Nguyen Van A", Email = "a@gmail.com" },
                new { ID = 2, Ten = "Tran Thi B", Email = "b@gmail.com" },
                new { ID = 3, Ten = "Le Van C", Email = "c@gmail.com" }
            };

            // Lọc theo tên người dùng
            var ketQua = ds.Where(x => x.Ten.IndexOf(ten, StringComparison.OrdinalIgnoreCase) >= 0).ToList();

            // Hiển thị kết quả trong DataGridView
            dgvNguoiDung.DataSource = ketQua;
        }

        private void TimKiemNguoiDung_Load(object sender, EventArgs e)
        {

        }
    }
}
