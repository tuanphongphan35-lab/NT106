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
        private ThongBaoTinNhan _frmThongBao = new ThongBaoTinNhan();
        private TcpClient client;
        private NetworkStream stream;
        private string currentUserName;
        private string currentPassword;
        private string _nguoiDangChat = "";
        private TimKiemNguoiDung frmTimKiem = null;
        private void ChatForm_Load(object sender, EventArgs e)
        {

            lblTenPhong.Texts = "Phòng Chat Chung";

            lblTenPhong.Click += (s, ev) =>
            {
                ChuyenCheDoChat(""); // Truyền chuỗi rỗng để về chat chung
            };
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
            System.Reflection.PropertyInfo? propertyInfo = typeof(Control).GetProperty("DoubleBuffered",
            System.Reflection.BindingFlags.NonPublic |
            System.Reflection.BindingFlags.Instance);
            System.Reflection.PropertyInfo? aProp = propertyInfo;
            aProp.SetValue(c, true, null);
        }
        private void ReceiveMessages()
        {
            byte[] buffer = new byte[4096];
            while (true)
            {
                try
                {
                    if (stream == null || !client.Connected) break; // Kiểm tra kết nối

                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead > 0)
                    {
                        string data = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        string[] commands = data.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string singleCommand in commands)
                        {
                            string[] parts = singleCommand.Split('|');
                            string command = parts[0];

                            // Dùng Switch Case để xử lý lệnh
                            switch (command)
                            {
                                case "LIST_BAN_BE":
                                    string[] cacBan = parts[1].Split(';');
                                    this.Invoke(new Action(() =>
                                    {
                                        roundFlowLayoutPanel2.Controls.Clear(); // Xóa cũ
                                        foreach (string ban in cacBan)
                                        {
                                            ThemBanVaoList(ban); // Vẽ nút
                                        }
                                    }));
                                    break;
                                case "CHAT":
                                    HandleChatMessage(parts);
                                    break;

                                case "HISTORY_DATA":
                                    // Server gửi: HISTORY_DATA | NguoiGui | NoiDung
                                    string hSender = parts[1];
                                    string hContent = parts[2];

                                    // Kiểm tra xem tin nhắn này là của MÌNH (true) hay BẠN (false)
                                    bool isMe = (hSender == currentUserName);

                                    this.Invoke(new Action(() =>
                                    {
                                        AddMessage(hContent, isMe);
                                    }));
                                    break;

                                case "HISTORY_END":
                                    // Tải xong thì cuộn xuống dưới cùng
                                    this.Invoke(new Action(() =>
                                    {
                                        try { txtChatBox.ScrollControlIntoView(txtChatBox.Controls[txtChatBox.Controls.Count - 1]); } catch { }
                                    }));
                                    break;
                                case "STATUS":
                                    // Server gửi: STATUS | Phong12345 | ONLINE (hoặc OFFLINE)
                                    string userStatus = parts[1];
                                    string status = parts[2];

                                    // Gọi hàm đổi màu
                                    CapNhatTrangThai(userStatus, (status == "ONLINE"));
                                    break;
                                case "TIM_THAY":
                                    HandleSearchResult(data);
                                    break;

                                case "INCOMING_CALL":
                                    HandleIncomingCall(parts);
                                    break;

                                case "CALL_RESULT": // Hoặc RESPONSE_CALL (tùy Server gửi về)
                                    HandleCallResponse(parts);
                                    break;

                                case "END_CALL":
                                    HandleEndCall(parts);
                                    break;
                                case "LOI_MOI":
                                    HandleKetBan(parts);
                                    break;
                                case "KET_QUA_KET_BAN":
                                    string tenNguoiTraLoi = parts[1];
                                    string ketQuaCuoi = parts[2];

                                    this.Invoke(new Action(() =>
                                    {
                                        if (ketQuaCuoi == "DONG_Y")
                                        {
                                            MessageBox.Show($"{tenNguoiTraLoi} đã chấp nhận kết bạn! Giờ 2 bạn có thể chat.");
                                            ThemBanVaoList(tenNguoiTraLoi);
                                        }
                                        else
                                            MessageBox.Show($"{tenNguoiTraLoi} đã từ chối lời mời.");
                                    }));
                                    break;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Tránh hiện MessageBox liên tục khi tắt form
                    if (!this.IsDisposed)
                    {
                        MessageBox.Show("Mất kết nối: " + ex.Message);
                    }
                    break;
                }
            }
        }
        private void HandleChatMessage(string[] parts)
        {
            string senderID = parts[1];
            string content = parts[2];

            if (senderID != _loggedInUserID)
            {
                AddMessage($"{senderID}:\n{content}", false);
            }
        }

        private void HandleSearchResult(string data)
        {
            if (frmTimKiem != null && !frmTimKiem.IsDisposed)
            {
                frmTimKiem.XuLyKetQuaTuServer(data);
            }
        }
        // Hàm xử lý phản hồi từ User (Đồng ý hoặc Từ chối kết bạn)
        private void HandleKetBan(string[] parts)
        {
            string nguoiGui = parts[1];

            this.Invoke(new Action(() =>
            {
                // 1. Tạo UserControl (viên gạch)
                // Truyền hàm XuLyPhanHoiTuUser vào để khi bấm nút nó biết gọi ai
                var item = new UC_ThongBaoKetBan(nguoiGui, XuLyPhanHoiTuUser);

                // 2. Thêm nó vào Form Thông Báo (dù Form đang ẩn vẫn thêm được)
                _frmThongBao.ThemThongBaoMoi(item);

            }));
        }
        private void XuLyPhanHoiTuUser(string nguoiGui, string ketQua)
        {
            try
            {
                // 1. Tạo lệnh gửi đi: PHAN_HOI_KET_BAN | Người_Mời | DONG_Y (hoặc TU_CHOI)
                string msg = $"PHAN_HOI_KET_BAN|{nguoiGui}|{ketQua}";


                byte[] buffer = Encoding.UTF8.GetBytes(msg + "\n"); // Thêm \n cho chắc
                client.GetStream().Write(buffer, 0, buffer.Length);

                // Debug để biết là đã gửi
                Console.WriteLine("Client đã gửi phản hồi: " + msg);
                if (ketQua == "DONG_Y")
                {
                    // Nếu đồng ý, thêm bạn vào danh sách luôn
                    ThemBanVaoList(nguoiGui);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi gửi phản hồi: " + ex.Message);
            }
        }
        // Xử lý khi có người gọi ĐẾN
        private void HandleIncomingCall(string[] parts)
        {
            string callerName = parts[1];
            string agoraRoom = parts[2];

            this.Invoke((MethodInvoker)delegate
            {
                // Mở Form PhoneCall ở chế độ NGƯỜI NHẬN (isCaller = false)
                PhoneCall incomingForm = new PhoneCall(this.stream, callerName, _loggedInUserID, agoraRoom, false);
                incomingForm.Show();
            });
        }

        // Xử lý phản hồi (người kia đồng ý hay từ chối)
        private void HandleCallResponse(string[] parts)
        {
            // Cấu trúc: CALL_RESULT | NguoiTraLoi | ACCEPT/REJECT
            string responder = parts[1];
            string decision = parts[2];

            this.Invoke((MethodInvoker)delegate
            {
                if (decision == "REJECT")
                {
                    MessageBox.Show($"{responder} đang bận hoặc đã từ chối cuộc gọi.");
                    // Tìm Form PhoneCall đang mở để đóng lại (nếu cần)
                }
                else if (decision == "ACCEPT")
                {
                    // Người kia đã đồng ý, Agora tự kết nối, có thể hiện thông báo nhỏ
                    // MessageBox.Show($"{responder} đã bắt máy!"); 
                }
            });
        }

        // Xử lý khi đối phương tắt máy
        private void HandleEndCall(string[] parts)
        {
            this.Invoke((MethodInvoker)delegate
            {
                MessageBox.Show("Cuộc gọi đã kết thúc.");
                // Code này sẽ tự động đóng các Form PhoneCall đang mở (nếu bạn quản lý list form)
                // Hoặc Form PhoneCall tự lắng nghe sự kiện này bên trong nó (như hướng dẫn PhoneCall.cs trước)
            });
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
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                // Khởi động lại toàn bộ ứng dụng
                Application.Restart();
                Environment.Exit(0);
            }
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
            if (_frmThongBao.Visible)
            {
                _frmThongBao.Hide(); // Đóng (Ẩn)
            }
            else
            {
                // 1. Tính toán vị trí để Form thông báo hiện ra ngay cạnh Form chính cho đẹp
                // (Ví dụ: Hiện bên phải Form chính, hoặc đè lên một góc)
                int x = this.Location.X + 60; // Dịch sang phải 60px so với mép trái Form chính
                int y = this.Location.Y + 100; // Dịch xuống 100px so với mép trên

                _frmThongBao.Location = new Point(x, y);

                // 2. Hiện form
                _frmThongBao.Show();
                _frmThongBao.BringToFront(); // Đưa lên trên cùng
            }
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
            string message = txtInput.Texts.Trim();

            if (!string.IsNullOrEmpty(message))
            {
                if (stream != null)
                {
                    try
                    {
                        // Xác định người nhận: Nếu _nguoiDangChat rỗng thì là chat chung (ALL)
                        string receiver = string.IsNullOrEmpty(_nguoiDangChat) ? "ALL" : _nguoiDangChat;

                        // Cấu trúc gửi mới: CHAT | Nội Dung | Người Nhận
                        string data = $"CHAT|{message}|{receiver}";

                        byte[] buffer = Encoding.UTF8.GetBytes(data);
                        stream.Write(buffer, 0, buffer.Length);
                    }
                    catch { MessageBox.Show("Lỗi kết nối server!"); }
                }

                // Hiện tin nhắn của mình lên luôn
                AddMessage(message, true);
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
        private void ChuyenCheDoChat(string tenNguoiNhan)
        {
            Console.WriteLine($"[DEBUG] ChuyenCheDoChat gọi với tên: '{tenNguoiNhan}'");
            _nguoiDangChat = tenNguoiNhan;

            this.Invoke(new Action(() =>
            {
                txtChatBox.Controls.Clear();
                txtChatBox.Invalidate();

                if (string.IsNullOrEmpty(tenNguoiNhan))
                {
                    Console.WriteLine("[DEBUG] Đang về Chat Chung");
                    lblTenPhong.Texts = "Phòng Chat Chung";
                }
                else
                {
                    Console.WriteLine("[DEBUG] Đang về Chat Chung");
                    lblTenPhong.Texts = "Chat với: " + tenNguoiNhan;

                    // --- THÊM ĐOẠN NÀY: Xin Server lịch sử chat cũ ---
                    try
                    {
                        string cmd = $"LAY_LICH_SU|{tenNguoiNhan}";
                        byte[] buffer = Encoding.UTF8.GetBytes(cmd);
                        client.GetStream().Write(buffer, 0, buffer.Length);
                    }
                    catch { }
                }
            }));
        }
        private void lblTenPhong__TextChanged(object sender, EventArgs e)
        {
            // hien thi ten nguoi dung khi thay doi phong

        }
        // Hàm này dùng để thêm 1 người vào danh sách bên trái
        private void ThemBanVaoList(string tenBanBe)
        {
            // 1. Tạo một UserControl mới (hoặc Button) đại diện cho người bạn đó
            // (Ở đây mình dùng Button cho nhanh, nếu bạn có UserControl riêng thì thay vào)
            Button btnBanBe = new Button();
            btnBanBe.Name = "btn_" + tenBanBe;
            btnBanBe.Text = "● " + tenBanBe;
            btnBanBe.ForeColor = Color.Gray;
            // 2. Trang trí cho đẹp (Giống giao diện Discord của bạn)
            btnBanBe.Text = tenBanBe;
            btnBanBe.Size = new Size(250, 50); // Chiều rộng bằng panel, cao 50
            btnBanBe.BackColor = Color.FromArgb(58, 59, 60); // Màu xám tối
            btnBanBe.ForeColor = Color.White;
            btnBanBe.FlatStyle = FlatStyle.Flat;
            btnBanBe.FlatAppearance.BorderSize = 0;
            btnBanBe.TextAlign = ContentAlignment.MiddleLeft;
            btnBanBe.Padding = new Padding(10, 0, 0, 0); // Thụt lề chữ vào
            btnBanBe.Cursor = Cursors.Hand;


            // Thêm icon nếu muốn (Optional)
            // btnBanBe.Image = Properties.Resources.user_icon; 
            // btnBanBe.ImageAlign = ContentAlignment.MiddleLeft;

            // 3. Sự kiện khi bấm vào tên người này (để chat)
            btnBanBe.Click += (s, e) =>
            {
                ChuyenCheDoChat(tenBanBe);
            };

            // 4. Thêm vào Panel danh sách (Cái Panel gạch xanh trong hình bạn gửi)
            if (roundFlowLayoutPanel2.InvokeRequired)
            {
                roundFlowLayoutPanel2.Invoke(new Action(() => roundFlowLayoutPanel2.Controls.Add(btnBanBe)));
            }
            else
            {
                roundFlowLayoutPanel2.Controls.Add(btnBanBe);
            }
        }
        private void CapNhatTrangThai(string tenUser, bool isOnline)
        {
            this.Invoke(new Action(() =>
            {
                // 1. Tìm cái nút có tên là "btn_TenUser" trong danh sách
                Control[] founds = roundFlowLayoutPanel2.Controls.Find("btn_" + tenUser, true);

                if (founds.Length > 0 && founds[0] is Button btn)
                {
                    // 2. Đổi màu và icon
                    if (isOnline)
                    {
                        btn.ForeColor = Color.LimeGreen; // Màu xanh lá sáng
                                                         // Nếu muốn đổi text thì: btn.Text = "● " + tenUser;
                    }
                    else
                    {
                        btn.ForeColor = Color.Gray; // Màu xám tối
                    }
                }
            }));
        }
  
        private void roundButton3_Click_1(object sender, EventArgs e)
        {
            ChuyenCheDoChat("");
        }

        private void btnCall_Click_1(object sender, EventArgs e)
        {
            string myName = PhienDangNhap.TaiKhoanHienTai;
            string receiverName = lblTenPhong.Text; // Tên người muốn gọi (lấy từ Label giao diện)

            // Kiểm tra kết nối
            if (stream == null) // Đảm bảo bạn đang dùng đúng tên biến stream kết nối (có thể là _stream hoặc clientStream)
            {
                MessageBox.Show("Chưa kết nối tới Server!");
                return;
            }

            // 2. Tạo tên phòng (Channel ID) duy nhất
            // Quy tắc: Luôn xếp theo alphabet để A gọi B hay B gọi A thì ID phòng vẫn giống nhau
            string channelID = (String.Compare(myName, receiverName) < 0)
                                ? $"{myName}_{receiverName}"
                                : $"{receiverName}_{myName}";

            // 3. Mở Form PhoneCall ngay lập tức
            // Tham số: Stream, Tên mình, Tên đối phương, ID phòng, isCaller = true (vì mình là người gọi)
            PhoneCall callForm = new PhoneCall(stream, myName, receiverName, channelID, true);
            callForm.Show();

            // 4. Gửi yêu cầu lên Server (QUAN TRỌNG: Dùng lệnh REQUEST_CALL)
            try
            {
                // Gói tin: REQUEST_CALL | Người Nhận | ID Phòng
                // Server sẽ đọc gói tin này và báo cho người nhận biết
                string data = $"REQUEST_CALL|{receiverName}|{channelID}";
                byte[] buffer = Encoding.UTF8.GetBytes(data);
                stream.Write(buffer, 0, buffer.Length);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi gửi tín hiệu: " + ex.Message);
            }
        }
    }
}
