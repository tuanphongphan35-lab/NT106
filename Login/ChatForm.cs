using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Login
{
    public partial class ChatForm : Form
    {

        private TcpClient client;
        private NetworkStream stream;
        private string currentUserName;
        private string currentPassword;
        private TimKiemNguoiDung frmTimKiem = null;
        private void ChatForm_Load(object sender, EventArgs e)
        {
            lblTenPhong.Texts = "Phòng Chat Chung";
            try
            {
                client = new TcpClient("127.0.0.1", 8080);
                stream = client.GetStream();

                // --- SỬA LẠI ĐOẠN NÀY ---
                // Dùng biến currentUserName và currentPassword
                string cmd = $"DANGNHAP|{this.currentUserName}|{this.currentPassword}";

                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(cmd);
                stream.Write(buffer, 0, buffer.Length);

                System.Threading.Thread.Sleep(100);

                Thread receiveThread = new Thread(ReceiveMessages);
                receiveThread.IsBackground = true;
                receiveThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể kết nối đến Server: " + ex.Message);
            }
            txtChatBox.Scroll += (s, ev) =>
            {
                txtChatBox.Invalidate();
                txtChatBox.Update();
            };

            // Bật chế độ chống rung
            SetDoubleBuffered(txtChatBox);
        }
        public static void SetDoubleBuffered(Control c)
        {
            if (System.Windows.Forms.SystemInformation.TerminalServerSession)
                return;
            System.Reflection.PropertyInfo aProp = typeof(Control).GetProperty("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance);
            aProp.SetValue(c, true, null);
        }
        private void ReceiveMessages()
        {
            byte[] buffer = new byte[4096];
            while (true)
            {
                try
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead > 0)
                    {
                        string data = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        string[] parts = data.Split('|');
                        string command = parts[0]; // Lấy lệnh đầu tiên

                        // ----------------------------------------------------
                        // CASE 1: TIN NHẮN CHAT (Code cũ của bạn)
                        // ----------------------------------------------------
                        if (command == "CHAT")
                        {
                            string senderID = parts[1];
                            string content = parts[2];

                            if (senderID != _loggedInUserID)
                            {
                                AddMessage($"{senderID}:\n{content}", false);
                            }
                        }

                        // ----------------------------------------------------
                        // CASE 2: KẾT QUẢ TÌM KIẾM (THÊM MỚI VÀO ĐÂY)
                        // ----------------------------------------------------
                        else if (command == "TIM_THAY")
                        {
                            // Kiểm tra xem Form tìm kiếm có đang mở không
                            if (frmTimKiem != null && !frmTimKiem.IsDisposed)
                            {
                                // Gọi hàm hiển thị bên Form tìm kiếm
                                // data chính là toàn bộ chuỗi: "TIM_THAY|Tuan:001;Lan:002..."
                                frmTimKiem.XuLyKetQuaTuServer(data);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // ... Code xử lý lỗi cũ của bạn ...
                    break;
                }
            }
        }

        private void ChatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // Đóng luồng và kết nối mạng khi tắt app
                if (client != null) client.Close();
                if (stream != null) stream.Close();
            }
            catch
            {
                // Bỏ qua lỗi nếu đã đóng rồi
            }
        }

        private readonly string _loggedInUserID;

        // 2. Chỉnh sửa Constructor để nhận ID người dùng
        public ChatForm(string userID, string user, string pass)
        {
            InitializeComponent();

            // Gán giá trị vào biến toàn cục để dùng ở ChatForm_Load
            this._loggedInUserID = userID;
            this.currentUserName = user;
            this.currentPassword = pass;
        }
        public ChatForm()
        {
            InitializeComponent();
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void roundButton2_Click(object sender, EventArgs e)
        {
            DangNhap loginform = new DangNhap();
            loginform.Show();
        }

        private void circularPictureBox1_Click(object sender, EventArgs e)
        {

            string idToDisplay = _loggedInUserID;

            // Kiểm tra an toàn (chỉ nên xảy ra nếu có lỗi hệ thống)
            if (idToDisplay == null)
            {
                MessageBox.Show("Lỗi Session. Vui lòng đăng nhập lại.");
                return;
            }

            // Truyền ID này vào Form Profile
            ThongTinNguoiDung profileForm = new ThongTinNguoiDung(idToDisplay);
            profileForm.ShowDialog();
        }

        private void roundButton4_Click(object sender, EventArgs e)
        {
            roundButton4.Enabled = false;
            ThongBaoTinNhan thongBaoTinNhan = new ThongBaoTinNhan();
            thongBaoTinNhan.Show();
        }

        private void roundButton5_Click(object sender, EventArgs e)
        {
            roundButton5.Enabled = false;
        }

        private void roundButton7_Click(object sender, EventArgs e)
        {
            roundButton7.Enabled = false;
        }

        private void roundButton6_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu form chưa mở hoặc đã bị tắt thì mới tạo mới
            if (frmTimKiem == null || frmTimKiem.IsDisposed)
            {
                frmTimKiem = new TimKiemNguoiDung(this.stream, this.currentUserName);
                frmTimKiem.Show();
            }
            else
            {
                // Nếu đang mở rồi thì đưa nó lên trên cùng
                frmTimKiem.BringToFront();
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            // Dùng .Texts cho Custom Control của bạn
            string message = txtInput.Texts.Trim();

            if (!string.IsNullOrEmpty(message))
            {
                // 1. Gửi qua Server
                if (stream != null)
                {
                    try
                    {
                        // --- SỬA DÒNG NÀY ---
                        // Chỉ gửi: LỆNH | NỘI DUNG
                        string data = $"CHAT|{message}";

                        byte[] buffer = Encoding.UTF8.GetBytes(data);
                        stream.Write(buffer, 0, buffer.Length);

                        // (Tùy chọn) Xóa ô nhập tin nhắn sau khi gửi
                        // txtMessage.Clear(); 
                    }
                    catch { MessageBox.Show("Lỗi kết nối server!"); }
                }

                // 2. Hiển thị tin nhắn của TÔI (true)
                AddMessage(message, true); // true = Tin nhắn của mình (Bên phải, màu xanh)

                // 3. Xóa ô nhập
                txtInput.Texts = "";
            }
        }

        private void AddMessage(string message, bool isMe)
        {
            // Đảm bảo chạy trên luồng giao diện chính (tránh lỗi Cross-thread)
            if (txtChatBox.InvokeRequired)
            {
                txtChatBox.Invoke(new Action(() => AddMessage(message, isMe)));
                return;
            }

            // 1. Tạo bong bóng chat (Label)
            Label lblBubble = new Label();
            lblBubble.Text = message;
            lblBubble.Font = new Font("Arial", 11, FontStyle.Regular);
            lblBubble.AutoSize = true;
            lblBubble.MaximumSize = new Size(txtChatBox.Width * 2 / 3, 0); // Giới hạn chiều rộng
            lblBubble.Padding = new Padding(10, 10, 10, 10); // Khoảng đệm chữ

            // 2. Cài đặt màu sắc
            if (isMe)
            {
                lblBubble.BackColor = Color.DodgerBlue;
                lblBubble.ForeColor = Color.White;
            }
            else
            {
                lblBubble.BackColor = Color.LightGray;
                lblBubble.ForeColor = Color.Black;
            }

            // 3. Xử lý bo tròn góc (Giữ nguyên)
            lblBubble.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                Label l = s as Label;
                GraphicsPath path = new GraphicsPath();
                int radius = 15;
                path.AddArc(0, 0, radius, radius, 180, 90);
                path.AddArc(l.Width - radius, 0, radius, radius, 270, 90);
                path.AddArc(l.Width - radius, l.Height - radius, radius, radius, 0, 90);
                path.AddArc(0, l.Height - radius, radius, radius, 90, 90);
                path.CloseFigure();
                l.Region = new Region(path);
            };

            // 4. TÍNH TOÁN CĂN LỀ (MARGIN) - ĐÂY LÀ BƯỚC QUAN TRỌNG NHẤT
            // Lấy kích thước dự kiến của Label
            Size size = lblBubble.GetPreferredSize(new Size(txtChatBox.Width * 2 / 3, 0));

            if (isMe)
            {
                // Để căn phải: Margin Trái = Chiều rộng khung - Chiều rộng tin nhắn - 25 (thanh cuộn)
                int marginLeft = txtChatBox.ClientSize.Width - size.Width - 25;

                // Đảm bảo không bị âm
                if (marginLeft < 0) marginLeft = 0;

                // Set Margin: (Trái, Trên, Phải, Dưới)
                // MarginLeft lớn sẽ đẩy Label sang phải
                lblBubble.Margin = new Padding(marginLeft, 5, 0, 5);
            }
            else
            {
                // Để căn trái: Margin bình thường
                lblBubble.Margin = new Padding(5, 5, 0, 5);
            }

            // 5. Thêm TRỰC TIẾP vào txtChatBox (Không qua Panel nữa)
            txtChatBox.Controls.Add(lblBubble);
            txtChatBox.Invalidate();
            txtChatBox.Update();
            // 6. Cuộn xuống dưới cùng
            try
            {
                txtChatBox.ScrollControlIntoView(lblBubble);
            }
            catch { }
        }

        private void lblTenPhong__TextChanged(object sender, EventArgs e)
        {

        }


    }
}
