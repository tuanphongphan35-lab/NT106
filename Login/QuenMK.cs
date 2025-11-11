using System;
// Loại bỏ: using Microsoft.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.IO;
using BCrypt.Net;
using System.Windows.Forms;
using System.Threading.Tasks; // Cần thiết cho các thao tác Firebase
using System.Collections.Generic;
using Server; // Cần để gọi lớp FirestoreDatabase và các hàm Helper

namespace Login
{
    public partial class QuenMK : Form
    {
        // Loại bỏ: private string connectionString = ...; (Không dùng LocalDB nữa)
        private string? verificationCode;

        public QuenMK()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        // --- HÀM 1: GỬI OTP XÁC THỰC (buttonOTP_Click) ---
        private async void buttonOTP_Click(object sender, EventArgs e)
        {
            string email = textBox2.Text.Trim();
            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Vui lòng nhập địa chỉ email.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // THAY ĐỔI: Gọi lớp FirestoreDatabase mới
                bool emailExists = await Server.Database.KiemTraTonTaiEmail(email);

                if (!emailExists)
                {
                    MessageBox.Show("Email không tồn tại trong hệ thống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Giữ nguyên logic gửi email (Giả sử các hàm GeneraiVerificationCode/GuiEmailXacThuc nằm trong DangKy.cs và là public static)
                this.verificationCode = DangKy.GeneraiVerificationCode();
                DangKy.GuiEmailXacThuc(email, this.verificationCode);
                MessageBox.Show("Mã xác nhận đã được gửi đến email của bạn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi gửi email: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- HÀM 2: ĐẶT LẠI MẬT KHẨU (button1_Click) ---
        // Cần chuyển đổi sang async để thực hiện các thao tác Firebase
        private async void button1_Click(object sender, EventArgs e)
        {
            string email = textBox2.Text.Trim();
            string maOTP = textBoxOTP.Text.Trim();
            string matKhau = textBox3.Text.Trim();
            string xacNhanMatKhau = textBox4.Text.Trim();

            // 1. Kiểm tra dữ liệu đầu vào và OTP
            if (string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(matKhau) ||
                string.IsNullOrWhiteSpace(xacNhanMatKhau) ||
                string.IsNullOrWhiteSpace(maOTP))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (maOTP != this.verificationCode)
            {
                MessageBox.Show("Mã OTP xác nhận không đúng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (matKhau != xacNhanMatKhau)
            {
                MessageBox.Show("Mật khẩu và xác nhận mật khẩu không khớp.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2. Mã hóa mật khẩu mới
            string matKhauDaBam = BCrypt.Net.BCrypt.HashPassword(matKhau);

            try
            {
                // THAY ĐỔI LOGIC CẬP NHẬT FIRESTORE:

                // Bước A: Tìm Username (hoặc ID Document) từ Email
                // Chúng ta sẽ cần một hàm mới trong Database để làm việc này:
                string? username = await Server.Database.LayUsernameTuEmail(email);

                if (string.IsNullOrEmpty(username))
                {
                    MessageBox.Show("Không tìm thấy email trong hệ thống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Bước B: Lấy ID Document (string) từ Username
                string? userID = await Server.Database.LayIDNguoiDung(username);

                if (string.IsNullOrEmpty(userID))
                {
                    MessageBox.Show("Lỗi nội bộ: Không thể xác định ID người dùng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Bước C: Cập nhật mật khẩu bằng SetOptions.Merge
                // Chúng ta dùng lại hàm LuuThongTinNguoiDung hoặc tạo hàm UpdatePassword riêng:

                // Giả định bạn đã tạo hàm UpdatePasswordAsync trong FirestoreDatabase:
                bool updateSuccess = await Server.Database.UpdatePasswordAsync(userID, matKhauDaBam);

                if (updateSuccess)
                {
                    MessageBox.Show("Mật khẩu đã được đặt lại thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close(); // Đóng form sau khi đổi mật khẩu thành công
                }
                else
                {
                    MessageBox.Show("Cập nhật mật khẩu thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi cập nhật mật khẩu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}