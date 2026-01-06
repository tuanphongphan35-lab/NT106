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
using System.IO;
using System.IO.Compression;

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
        private readonly string _loggedInUserID;
        private DanhSachBanBe _frmDanhSachBanBe = null;
        private bool _dangChatNhom = false;
        private int _soLuongThongBao = 0; 

        public ChatForm(string userID, string user, string pass)
        {
            InitializeComponent();
            this._loggedInUserID = userID;
            this.currentUserName = user;
            this.currentPassword = pass;
        }

        public ChatForm()
        {
            InitializeComponent();
        }

        private void ChatForm_Load(object sender, EventArgs e)
        {

            lblTenPhong.Click += (s, ev) =>
            {
                ChuyenCheDoChat(""); // Truyền chuỗi rỗng để về chat chung
            };

            // --- [QUAN TRỌNG]: Đăng ký sự kiện vẽ cho nút chuông để hiện số đỏ ---
            roundButton4.Paint += roundButton4_Paint;

            try
            {
                client = new TcpClient("26.178.216.237", 8080);
                stream = client.GetStream();

                // Gửi lệnh đăng nhập kèm \n
                string cmd = $"DANGNHAP|{this.currentUserName}|{this.currentPassword}\n";
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

            SetDoubleBuffered(txtChatBox);
            ChuyenCheDoChat("");
        }

        // --- HÀM VẼ SỐ THÔNG BÁO TRÊN NÚT CHUÔNG ---
        private void roundButton4_Paint(object sender, PaintEventArgs e)
        {
            // Chỉ vẽ khi có thông báo (> 0)
            if (_soLuongThongBao > 0)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                // 1. Cấu hình kích thước và vị trí huy hiệu đỏ
                int badgeSize = 18;
                // Vị trí: Góc trên bên phải
                int x = roundButton4.Width - badgeSize - 2;
                int y = 2;

                // 2. Vẽ hình tròn đỏ
                using (Brush brush = new SolidBrush(Color.Red))
                {
                    e.Graphics.FillEllipse(brush, x, y, badgeSize, badgeSize);
                }

                // 3. Vẽ số lượng bên trong
                string countText = _soLuongThongBao > 9 ? "9+" : _soLuongThongBao.ToString();
                using (Font font = new Font("Arial", 8, FontStyle.Bold))
                using (Brush textBrush = new SolidBrush(Color.White))
                {
                    // Căn giữa số trong hình tròn
                    SizeF textSize = e.Graphics.MeasureString(countText, font);
                    float textX = x + (badgeSize - textSize.Width) / 2;
                    float textY = y + (badgeSize - textSize.Height) / 2;

                    e.Graphics.DrawString(countText, font, textBrush, textX, textY);
                }
            }
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
            // Dùng StreamReader để đọc đồng bộ với Server
            StreamReader reader = new StreamReader(stream);

            try
            {
                while (true)
                {
                    if (client == null || !client.Connected) break;

                    // Đọc từng dòng (Chờ cho đến khi nhận đủ 1 lệnh kết thúc bằng \n)
                    string data = reader.ReadLine();

                    if (data == null) break;

                    string[] parts = data.Split('|');
                    string command = parts[0];

                    switch (command)
                    {
                        case "CHAT":
                            HandleChatMessage(parts);
                            break;

                        case "RECEIVE_FILE":
                            if (parts.Length >= 4)
                            {
                                string senderName = parts[1];
                                string fName = parts[2];
                                string fContent = parts[3];

                                bool isMe = (senderName == _loggedInUserID || senderName == currentUserName);
                                if (!isMe)
                                {
                                    this.Invoke(new Action(() =>
                                    {
                                        AddFileMessage(senderName, fName, fContent, false);
                                    }));
                                }
                            }
                            break;

                        case "LIST_BAN_BE":
                            if (parts.Length > 1)
                            {
                                string[] cacBan = parts[1].Split(';');

                                this.Invoke(new Action(() =>
                                {
                                    roundFlowLayoutPanel2.Controls.Clear();

                                    // 1. Thêm tất cả nút bạn bè vào giao diện
                                    foreach (string ban in cacBan)
                                    {
                                        if (!string.IsNullOrEmpty(ban)) ThemBanVaoList(ban);
                                    }

                                    // 2. [MỚI] Sau khi thêm xong, gọi hàm tự động chọn
                                    XuLySauKhiLoadDanhSach();
                                }));
                            }
                            else
                            {
                                // Trường hợp danh sách rỗng (không có ai)
                                this.Invoke(new Action(() => XuLySauKhiLoadDanhSach()));
                            }
                            break;

                        case "HISTORY_DATA":
                            string hSender = parts[1];
                            string hContent = parts[2];
                            bool isMeHist = (hSender == currentUserName);
                            this.Invoke(new Action(() => AddMessage("", hContent, isMeHist)));
                            break;

                        case "HISTORY_END":
                            this.Invoke(new Action(() =>
                            {
                                try { txtChatBox.ScrollControlIntoView(txtChatBox.Controls[txtChatBox.Controls.Count - 1]); } catch { }
                            }));
                            break;

                        case "STATUS":
                            CapNhatTrangThai(parts[1], (parts[2] == "ONLINE"));
                            break;

                        case "TIM_THAY":
                            HandleSearchResult(data);
                            break;

                        case "INCOMING_CALL":
                            HandleIncomingCall(parts);
                            break;

                        case "CALL_RESULT":
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
                                    MessageBox.Show($"{tenNguoiTraLoi} đã chấp nhận kết bạn!");
                                    ThemBanVaoList(tenNguoiTraLoi);
                                }
                                else MessageBox.Show($"{tenNguoiTraLoi} đã từ chối.");
                            }));
                            break;
                        case "TAO_NHOM_THANH_CONG":
                            // Server trả về: TAO_NHOM_THANH_CONG | ID_Nhom | Ten_Nhom
                            string groupID = parts[1];
                            string groupName = parts[2];

                            this.Invoke(new Action(() =>
                            {
                                MessageBox.Show($"Tạo nhóm '{groupName}' thành công!");
                                // Thêm nút nhóm vào giao diện (Hàm này viết ở Bước 5)
                                ThemNhomVaoList(groupID, groupName);
                            }));
                            break;

                        case "TAO_NHOM_THAT_BAI":
                            string reason = parts[1];
                            this.Invoke(new Action(() => MessageBox.Show("Tạo nhóm thất bại: " + reason)));
                            break;

                        case "NEW_GROUP":
                            // Khi người khác mời mình vào nhóm: NEW_GROUP | ID | Ten | NguoiTao
                            string newGroupID = parts[1];
                            string newGroupName = parts[2];
                            string creator = parts[3];

                            this.Invoke(new Action(() =>
                            {
                                // Hiển thị thông báo nhỏ hoặc Toast notification nếu muốn
                                // MessageBox.Show($"{creator} đã thêm bạn vào nhóm {newGroupName}");
                                ThemNhomVaoList(newGroupID, newGroupName);
                            }));
                            break;
                        case "LIST_NHOM":
                            // Server gửi: LIST_NHOM | ID1:Ten1;ID2:Ten2...
                            if (parts.Length > 1)
                            {
                                string[] rawGroups = parts[1].Split(';'); // Tách từng nhóm

                                this.Invoke(new Action(() =>
                                {
                                    foreach (string g in rawGroups)
                                    {
                                        // Tách ID và Tên (ID:Name)
                                        string[] info = g.Split(':');
                                        if (info.Length == 2)
                                        {
                                            string gID = info[0];
                                            string gName = info[1];

                                            // Gọi hàm vẽ nút nhóm (đã viết ở câu trả lời trước)
                                            ThemNhomVaoList(gID, gName);
                                        }
                                    }
                                }));
                            }
                            break;
                        case "DS_MOI_MEM":
                            // Server trả về: DS_MOI_MEM | GroupID | User1;User2;User3
                            string currentGroupID = parts[1];
                            string rawList = (parts.Length > 2) ? parts[2] : "";

                            this.Invoke(new Action(() =>
                            {
                                // 1. Chuyển chuỗi thành List
                                List<string> candidates = new List<string>();
                                if (!string.IsNullOrEmpty(rawList))
                                {
                                    candidates.AddRange(rawList.Split(';'));
                                }

                                // 2. Mở Form chọn (FormChonThanhVien)
                                FormChonThanhVien frm = new FormChonThanhVien(candidates);
                                frm.StartPosition = FormStartPosition.CenterParent;

                                if (frm.ShowDialog() == DialogResult.OK)
                                {
                                    // 3. Nếu người dùng chọn xong và bấm OK -> Gửi lệnh thêm
                                    List<string> selected = frm.SelectedUsers;
                                    string userStr = string.Join(";", selected);

                                    // Gửi: THEM_THANH_VIEN | GroupID | User1;User2
                                    string cmd = $"THEM_THANH_VIEN|{currentGroupID}|{userStr}\n";
                                    byte[] buff = System.Text.Encoding.UTF8.GetBytes(cmd);
                                    stream.Write(buff, 0, buff.Length);
                                }
                            }));
                            break;

                        case "THEM_MEM_OK":
                            this.Invoke(new Action(() => MessageBox.Show("Đã thêm thành viên thành công!")));
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                if (!this.IsDisposed) Console.WriteLine("Lỗi nhận tin: " + ex.Message);
            }
        }

        private void HandleKetBan(string[] parts)
        {
            string nguoiGui = parts[1];

            this.Invoke(new Action(() =>
            {
                // Thêm vào danh sách thông báo
                var item = new UC_ThongBaoKetBan(nguoiGui, XuLyPhanHoiTuUser);
                _frmThongBao.ThemThongBaoMoi(item);

                // --- LOGIC HIỆN SỐ ĐỎ ---
                // Nếu form thông báo đang ẩn thì tăng số và vẽ lại nút
                if (!_frmThongBao.Visible)
                {
                    _soLuongThongBao++;
                    roundButton4.Invalidate(); // Lệnh này sẽ kích hoạt hàm roundButton4_Paint
                }
            }));
        }

        private void roundButton4_Click(object sender, EventArgs e)
        {
            if (_frmThongBao.Visible)
            {
                _frmThongBao.Hide();
            }
            else
            {
                // --- KHI BẤM VÀO THÌ RESET SỐ LƯỢNG ---
                _soLuongThongBao = 0;
                roundButton4.Invalidate(); // Vẽ lại nút (xóa chấm đỏ)

                // Hiện form
                int x = this.Location.X + 60;
                int y = this.Location.Y + 100;
                _frmThongBao.Location = new Point(x, y);
                _frmThongBao.Show();
                _frmThongBao.BringToFront();
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
                        string receiver = string.IsNullOrEmpty(_nguoiDangChat) ? "ALL" : _nguoiDangChat;
                        // Thêm \n vào cuối lệnh Chat
                        string data = $"CHAT|{message}|{receiver}\n";

                        byte[] buffer = Encoding.UTF8.GetBytes(data);
                        stream.Write(buffer, 0, buffer.Length);
                    }
                    catch { MessageBox.Show("Lỗi kết nối server!"); }
                }

                AddMessage("", message, true);
                txtInput.Texts = "";
            }
        }

        private void GuiFileQuaServer(string filePath)
        {
            try
            {
                FileInfo fi = new FileInfo(filePath);
                if (fi.Length > 10 * 1024 * 1024)
                {
                    MessageBox.Show("File quá lớn! Giới hạn 10MB.");
                    return;
                }

                byte[] fileBytes = File.ReadAllBytes(filePath);
                string base64Content = Convert.ToBase64String(fileBytes);
                string fileName = Path.GetFileName(filePath).Replace("|", "").Replace("\n", "").Replace("\r", "");
                string receiver = string.IsNullOrEmpty(_nguoiDangChat) ? "ALL" : _nguoiDangChat;

                // Thêm \n vào cuối lệnh Gửi File
                string cmd = $"SEND_FILE|{receiver}|{fileName}|{base64Content}\n";
                byte[] buffer = Encoding.UTF8.GetBytes(cmd);

                if (stream != null && client.Connected)
                {
                    stream.Write(buffer, 0, buffer.Length);
                    stream.Flush();
                }

                AddFileMessage("Bạn", fileName, base64Content, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi gửi file: " + ex.Message);
            }
        }

        // Thay đổi tham số đầu vào: thêm string name
        private void AddMessage(string name, string message, bool isMe)
        {
            // Kiểm tra thread (giữ nguyên)
            if (txtChatBox.InvokeRequired)
            {
                txtChatBox.Invoke(new Action(() => AddMessage(name, message, isMe)));
                return;
            }

            // --- PHẦN 1: TÊN NGƯỜI GỬI (Chỉ hiện khi không phải là mình) ---
            if (!isMe)
            {
                Label lblName = new Label();
                lblName.Text = name;

                // [QUAN TRỌNG 1] Đặt nền trong suốt để xóa khung trắng
                lblName.BackColor = Color.Transparent;

                // [QUAN TRỌNG 2] Đặt màu chữ sáng (Trắng) để nổi bật trên nền tối
                lblName.ForeColor = Color.White;

                lblName.Font = new Font("Segoe UI", 8, FontStyle.Bold);
                lblName.AutoSize = true;
                lblName.Margin = new Padding(12, 10, 0, 2); // Căn lề một chút cho đẹp

                txtChatBox.Controls.Add(lblName);
            }

            // --- PHẦN 2: BONG BÓNG CHAT (Giữ nguyên logic vẽ bong bóng) ---
            Label lblBubble = new Label();
            lblBubble.Text = message;
            lblBubble.Font = new Font("Arial", 11, FontStyle.Regular);
            lblBubble.AutoSize = true;
            lblBubble.MaximumSize = new Size(txtChatBox.Width * 2 / 3, 0);
            lblBubble.Padding = new Padding(10, 10, 10, 10);

            if (isMe)
            {
                lblBubble.BackColor = Color.DodgerBlue;
                lblBubble.ForeColor = Color.White;
            }
            else
            {
                lblBubble.BackColor = Color.White; // Hoặc LightGray tùy bạn
                lblBubble.ForeColor = Color.Black;
            }

            // Vẽ bo tròn góc bong bóng chat
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

            Size size = lblBubble.GetPreferredSize(new Size(txtChatBox.Width * 2 / 3, 0));

            if (isMe)
            {
                int marginLeft = txtChatBox.ClientSize.Width - size.Width - 25;
                if (marginLeft < 0) marginLeft = 0;
                lblBubble.Margin = new Padding(marginLeft, 5, 0, 5);
            }
            else
            {
                // Margin top = 0 để bong bóng dính sát vào tên ở trên
                lblBubble.Margin = new Padding(5, 0, 0, 5);
            }

            txtChatBox.Controls.Add(lblBubble);

            // Cuộn xuống tin nhắn mới nhất
            try { txtChatBox.ScrollControlIntoView(lblBubble); } catch { }
        }

        private void AddFileMessage(string sender, string fileName, string base64Content, bool isMe)
        {
            Label lblFile = new Label();
            lblFile.Text = isMe ? $"📁 {fileName}\n(Nhấn để lưu)" : $"📁 {sender} gửi file:\n{fileName}\n(Nhấn để tải)";
            lblFile.Font = new Font("Segoe UI", 10, FontStyle.Underline);
            lblFile.AutoSize = true;
            lblFile.Cursor = Cursors.Hand;
            lblFile.Padding = new Padding(10);
            lblFile.MaximumSize = new Size(txtChatBox.Width * 2 / 3, 0);

            if (isMe)
            {
                lblFile.BackColor = Color.DodgerBlue;
                lblFile.ForeColor = Color.White;
                Size size = lblFile.GetPreferredSize(new Size(txtChatBox.Width * 2 / 3, 0));
                int marginLeft = txtChatBox.ClientSize.Width - size.Width - 25;
                if (marginLeft < 0) marginLeft = 0;
                lblFile.Margin = new Padding(marginLeft, 5, 0, 5);
            }
            else
            {
                lblFile.BackColor = Color.LightYellow;
                lblFile.ForeColor = Color.Blue;
                lblFile.Margin = new Padding(5, 5, 0, 5);
            }

            lblFile.Click += (s, e) =>
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = fileName;
                sfd.Filter = "All Files|*.*";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        byte[] fileData = Convert.FromBase64String(base64Content);
                        System.IO.File.WriteAllBytes(sfd.FileName, fileData);
                        MessageBox.Show("Lưu file thành công!");
                    }
                    catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
                }
            };

            if (txtChatBox.InvokeRequired) txtChatBox.Invoke(new Action(() => txtChatBox.Controls.Add(lblFile)));
            else txtChatBox.Controls.Add(lblFile);

            try { txtChatBox.ScrollControlIntoView(lblFile); } catch { }
        }

        // --- CÁC HÀM XỬ LÝ KHÁC (GIỮ NGUYÊN) ---
        private void HandleChatMessage(string[] parts)
        {
            string senderID = parts[1];
            string content = parts[2];
            if (senderID != _loggedInUserID) AddMessage(senderID, content, false);
        }

        private void HandleSearchResult(string data)
        {
            if (frmTimKiem != null && !frmTimKiem.IsDisposed) frmTimKiem.XuLyKetQuaTuServer(data);
        }

        private void XuLyPhanHoiTuUser(string nguoiGui, string ketQua)
        {
            try
            {
                string msg = $"PHAN_HOI_KET_BAN|{nguoiGui}|{ketQua}\n"; // Có \n
                byte[] buffer = Encoding.UTF8.GetBytes(msg);
                client.GetStream().Write(buffer, 0, buffer.Length);

                if (ketQua == "DONG_Y") ThemBanVaoList(nguoiGui);
            }
            catch (Exception ex) { MessageBox.Show("Lỗi gửi phản hồi: " + ex.Message); }
        }

        private void HandleIncomingCall(string[] parts)
        {
            string callerName = parts[1];
            string channelID = parts[2];

            this.Invoke((MethodInvoker)delegate
            {
                // --- LOGIC MỚI: Mở form PhoneCall (Thông báo) ---
                PhoneCall notifForm = new PhoneCall(this.stream, callerName, channelID);
                notifForm.ShowDialog(); // ShowDialog để bắt buộc chọn Nghe hoặc Tắt
            });
        }

        private void HandleCallResponse(string[] parts)
        {
            string responder = parts[1];
            string decision = parts[2];
            this.Invoke((MethodInvoker)delegate
            {
                if (decision == "REJECT")
                {
                    MessageBox.Show($"{responder} đã từ chối cuộc gọi.");
                    // Ở đây nếu muốn xịn thì tìm form Call đang mở để Close() nó đi
                }
                else if (decision == "ACCEPT")
                {
                    // Bên kia đã vào phòng, Agora sẽ tự hiện hình -> Không cần làm gì thêm
                }
            });
        }

        private void HandleEndCall(string[] parts)
        {
            this.Invoke((MethodInvoker)delegate { MessageBox.Show("Cuộc gọi đã kết thúc."); });
        }

        private void ChatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (client != null) client.Close();
                if (stream != null) stream.Close();
            }
            catch { }
        }

        private void panel1_Paint(object sender, PaintEventArgs e) { }

        private void roundButton2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Restart();
                Environment.Exit(0);
            }
        }

        private void circularPictureBox1_Click(object sender, EventArgs e)
        {
            if (_loggedInUserID == null) return;
            ThongTinNguoiDung profileForm = new ThongTinNguoiDung(_loggedInUserID);
            profileForm.ShowDialog();
        }

        private void roundButton5_Click(object sender, EventArgs e) { roundButton5.Enabled = false; }

        private void roundButton7_Click(object sender, EventArgs e)
        {
            ContextMenuStrip ctxMenu = new ContextMenuStrip();
            var itemFile = ctxMenu.Items.Add("Gửi File");
            itemFile.Click += (s, ev) => ChonVaGuiFile();
            var itemFolder = ctxMenu.Items.Add("Gửi Thư mục");
            itemFolder.Click += (s, ev) => ChonVaGuiFolder();
            ctxMenu.Show(Cursor.Position);
        }

        private void ChonVaGuiFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK) GuiFileQuaServer(ofd.FileName);
        }

        private void ChonVaGuiFolder()
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                string folderPath = fbd.SelectedPath;
                string folderName = new DirectoryInfo(folderPath).Name;
                string tempZipPath = Path.Combine(Path.GetTempPath(), folderName + ".zip");
                if (File.Exists(tempZipPath)) File.Delete(tempZipPath);
                try
                {
                    ZipFile.CreateFromDirectory(folderPath, tempZipPath);
                    GuiFileQuaServer(tempZipPath);
                }
                catch (Exception ex) { MessageBox.Show("Lỗi nén thư mục: " + ex.Message); }
            }
        }

        private void roundButton6_Click(object sender, EventArgs e)
        {
            // 1. Tạo danh sách chứa tên bạn bè đang có
            List<string> listBanBe = new List<string>();

            // 2. Duyệt qua tất cả các nút trong panel danh sách bạn bè
            // (Giả sử panel chứa bạn bè tên là roundFlowLayoutPanel2)
            foreach (Control c in roundFlowLayoutPanel2.Controls)
            {
                if (c is Button btn)
                {
                    // [QUAN TRỌNG]: Vì nút bạn bè của bạn có dấu chấm tròn (●) nên phải xóa đi mới lấy được tên đúng
                    // Ví dụ text là: "● ElicBug" -> Phải xóa "● " đi
                    string tenBanBe = btn.Text.Replace("●", "").Trim();

                    listBanBe.Add(tenBanBe);
                }
            }

            // 3. Mở form tìm kiếm và TRUYỀN DANH SÁCH SANG
            if (frmTimKiem == null || frmTimKiem.IsDisposed)
            {
                // Truyền thêm listBanBe vào constructor mới sửa
                frmTimKiem = new TimKiemNguoiDung(this.stream, this.currentUserName, listBanBe);
                frmTimKiem.Show();
            }

        private void ThemBanVaoList(string tenBanBe)
        {
            Button btnBanBe = new Button();
            // [QUAN TRỌNG] Đặt Name theo quy tắc để sau này dễ lấy
            btnBanBe.Name = "btn_" + tenBanBe;

            // [SỬA] Chỉ gán Text 1 lần. Nếu bạn muốn có dấu chấm thì để "● ", không thì bỏ.
            // Ở đây mình để có dấu chấm cho đẹp
            btnBanBe.Text = "● " + tenBanBe;

            btnBanBe.ForeColor = Color.Gray;
            // btnBanBe.Text = tenBanBe; // <--- XÓA DÒNG NÀY ĐI (Nó đang ghi đè làm mất dấu chấm)

            btnBanBe.Size = new Size(250, 50);
            btnBanBe.BackColor = Color.FromArgb(58, 59, 60);
            btnBanBe.ForeColor = Color.White;
            btnBanBe.FlatStyle = FlatStyle.Flat;
            btnBanBe.FlatAppearance.BorderSize = 0;
            btnBanBe.TextAlign = ContentAlignment.MiddleLeft;
            btnBanBe.Padding = new Padding(10, 0, 0, 0);
            btnBanBe.Cursor = Cursors.Hand;

            btnBanBe.Click += (s, e) => { ChuyenCheDoChat(tenBanBe); };

            if (roundFlowLayoutPanel2.InvokeRequired)
                roundFlowLayoutPanel2.Invoke(new Action(() => roundFlowLayoutPanel2.Controls.Add(btnBanBe)));
            else
                roundFlowLayoutPanel2.Controls.Add(btnBanBe);
        }

        private void CapNhatTrangThai(string tenUser, bool isOnline)
        {
            this.Invoke(new Action(() =>
            {
                Control[] founds = roundFlowLayoutPanel2.Controls.Find("btn_" + tenUser, true);
                if (founds.Length > 0 && founds[0] is Button btn)
                {
                    if (isOnline) btn.ForeColor = Color.LimeGreen;
                    else btn.ForeColor = Color.Gray;
                }
            }));
        }

        private void btnCall_Click_1(object sender, EventArgs e)
        {
            string myName = PhienDangNhap.TaiKhoanHienTai;
            string receiverName = lblTenPhong.Text;
            if (stream == null) return;

            string channelID = (String.Compare(myName, receiverName) < 0) ? $"{myName}_{receiverName}" : $"{receiverName}_{myName}";
            PhoneCall callForm = new PhoneCall(stream, receiverName, channelID);
            callForm.Show();

            try
            {
                string data = $"REQUEST_CALL|{receiverName}|{channelID}\n";
                byte[] buffer = Encoding.UTF8.GetBytes(data);
                stream.Write(buffer, 0, buffer.Length);
            }
            catch (Exception ex) { MessageBox.Show("Lỗi gửi tín hiệu: " + ex.Message); }
        }

        private void lblTenPhong__TextChanged(object sender, EventArgs e) { }


        private void ChuyenCheDoChat(string receiverID, bool isGroup = false)
        {
            txtInput.Enabled = true;
            _nguoiDangChat = receiverID;
            this._dangChatNhom = isGroup;
            this._nguoiDangChat = receiverID;

            this.Invoke(new Action(() =>
            {
                txtChatBox.Controls.Clear();

                // Hiển thị tên
                if (isGroup)
                {
                    // Nếu muốn hiển thị tên Nhóm đẹp thì phải lưu mapping ID->Name
                    // Ở đây tạm thời hiện ID hoặc bạn cần lấy text từ nút vừa bấm
                    lblTenPhong.Texts = "Nhóm: " + GetGroupNameByID(receiverID);
                }
                else
                {
                    if (string.IsNullOrEmpty(receiverID)) lblTenPhong.Texts = "Phòng Chat Chung";
                    else lblTenPhong.Texts = receiverID;
                }

                // Gửi lệnh lấy lịch sử
                try
                {
                    if (stream != null && client.Connected && !string.IsNullOrEmpty(receiverID))
                    {
                        // Server cần phân biệt lấy lịch sử user hay nhóm
                        // Bạn có thể quy ước: Nếu ID bắt đầu bằng "G_" là nhóm
                        // Hoặc sửa lệnh Server: LAY_LICH_SU_NHOM

                        string cmd = "";
                        if (isGroup) cmd = $"LAY_LICH_SU_NHOM|{receiverID}\n";
                        else cmd = $"LAY_LICH_SU|{receiverID}\n";

                        byte[] buffer = Encoding.UTF8.GetBytes(cmd);
                        stream.Write(buffer, 0, buffer.Length);
                    }
                }
                catch { }
            }));
        }

        // Hàm phụ để lấy tên nhóm từ nút bấm (vì receiverID chỉ là mã G_...)
        private string GetGroupNameByID(string groupID)
        {
            Control[] founds = roundFlowLayoutPanel2.Controls.Find("group_" + groupID, true);
            if (founds.Length > 0) return founds[0].Text.Replace("👥 ", "");
            return groupID;
        }



        private void roundButton8_Click(object sender, EventArgs e)
        {
            List<string> listBanBe = new List<string>();

            // Duyệt qua tất cả control trong danh sách
            foreach (Control c in roundFlowLayoutPanel2.Controls)
            {
                string tenLayDuoc = "";

                // Cách 1: Ưu tiên lấy từ Name (Chuẩn xác nhất)
                if (!string.IsNullOrEmpty(c.Name) && c.Name.StartsWith("btn_"))
                {
                    tenLayDuoc = c.Name.Replace("btn_", "");
                }
                // Cách 2: Dự phòng lấy từ Text (nếu nút cũ chưa có Name)
                else
                {
                    tenLayDuoc = c.Text.Replace("●", "").Trim();
                }

                // Nếu lấy được tên thì thêm vào danh sách
                if (!string.IsNullOrEmpty(tenLayDuoc))
                {
                    listBanBe.Add(tenLayDuoc);
                }
            }

            // Logic mở form: Đóng cái cũ (nếu đang mở) để cập nhật danh sách mới nhất
            if (_frmDanhSachBanBe == null || _frmDanhSachBanBe.IsDisposed)
            {
                // [THÊM THAM SỐ CUỐI CÙNG]: this.XoaNutBanBeTrenGiaoDien
                _frmDanhSachBanBe = new DanhSachBanBe(
                    this.stream,
                    this.currentUserName,
                    listBanBe,
                    this.XoaNutBanBeTrenGiaoDien // <--- Truyền hàm này vào
                );
                _frmDanhSachBanBe.Show();
            }
            else
            {
                _frmDanhSachBanBe.Close();
                // Làm tương tự cho trường hợp tạo lại
                _frmDanhSachBanBe = new DanhSachBanBe(
                    this.stream,
                    this.currentUserName,
                    listBanBe,
                    this.XoaNutBanBeTrenGiaoDien
                );
                _frmDanhSachBanBe.Show();
            }
        }

        // Trong ChatForm.cs
        // Hàm này để Form DanhSachBanBe gọi khi xóa thành công
        public void XoaNutBanBeTrenGiaoDien(string tenBan)
        {
            // Tìm nút có tên "btn_TenBan"
            Control[] timthay = roundFlowLayoutPanel2.Controls.Find("btn_" + tenBan, true);

            if (timthay.Length > 0)
            {
                // Xóa nút đó đi
                roundFlowLayoutPanel2.Controls.Remove(timthay[0]);
                timthay[0].Dispose(); // Giải phóng bộ nhớ

                // Nếu đang chat với người đó thì chuyển về màn hình trống
                if (_nguoiDangChat == tenBan)
                {
                    ChuyenCheDoChat(""); // Về chat chung hoặc màn hình chờ
                }
            }
        }
        // --- HÀM 1: QUYẾT ĐỊNH XEM NÊN LÀM GÌ ---
        private void XuLySauKhiLoadDanhSach()
        {
            ChuyenCheDoChat("");
        }
        
        private void roundButton3_Click(object sender, EventArgs e)
        {
            ChuyenCheDoChat("");
        }

        private void btnCall_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_nguoiDangChat) || lblTenPhong.Texts == "Phòng Chat Chung")
            {
                MessageBox.Show("Chọn người để gọi!"); return;
            }

            string myName = this.currentUserName;
            string receiverName = _nguoiDangChat; // Lấy tên từ biến lưu trữ chuẩn

            // Tạo ID phòng
            string channelID = (String.Compare(myName, receiverName) < 0)
                             ? $"{myName}_{receiverName}" : $"{receiverName}_{myName}";

            // --- LOGIC MỚI: Mở form Call (Video) luôn ---
            Call videoForm = new Call(stream, channelID, receiverName);
            videoForm.Show();

            // Gửi yêu cầu
            string data = $"REQUEST_CALL|{receiverName}|{channelID}\n";
            byte[] buffer = Encoding.UTF8.GetBytes(data);
            stream.Write(buffer, 0, buffer.Length);
        }

        private void btnTaoNhom_Click(object sender, EventArgs e)
        {
            // --- TRƯỜNG HỢP 1: ĐANG Ở TRONG NHÓM -> THÊM THÀNH VIÊN ---
            if (_dangChatNhom)
            {
                // Kiểm tra an toàn
                if (string.IsNullOrEmpty(_nguoiDangChat)) return;

                // Gửi lệnh lấy danh sách bạn bè chưa vào nhóm để mời
                try
                {
                    // _nguoiDangChat lúc này chính là GroupID
                    string cmd = $"LAY_DS_MOI_MEM|{_nguoiDangChat}\n";
                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(cmd);
                    stream.Write(buffer, 0, buffer.Length);
                }
                catch
                {
                    MessageBox.Show("Mất kết nối Server!");
                }
            }
            // --- TRƯỜNG HỢP 2: ĐANG CHAT RIÊNG/CHUNG -> TẠO NHÓM MỚI ---
            else
            {
                // (Logic cũ của bạn copy vào đây)
                // 1. Thu thập danh sách bạn bè đang có trên giao diện
                List<string> listBanBe = new List<string>();

                foreach (Control c in roundFlowLayoutPanel2.Controls)
                {
                    string tenLayDuoc = "";

                    // Chỉ lấy những nút là User (có prefix btn_), bỏ qua nút Group (group_)
                    if (!string.IsNullOrEmpty(c.Name) && c.Name.StartsWith("btn_"))
                    {
                        tenLayDuoc = c.Name.Replace("btn_", "");
                    }
                    else if (c.Text.Contains("●")) // Fallback cho các nút cũ
                    {
                        tenLayDuoc = c.Text.Replace("●", "").Trim();
                    }

                    if (!string.IsNullOrEmpty(tenLayDuoc))
                    {
                        listBanBe.Add(tenLayDuoc);
                    }
                }

                // 2. Mở Form Tạo Nhóm
                FormTaoNhom frm = new FormTaoNhom(this.stream, listBanBe);
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
            }
        }

        private void ThemNhomVaoList(string groupID, string groupName)
        {
            Button btnNhom = new Button();

            // [QUAN TRỌNG]: Đặt Name bắt đầu bằng tiền tố khác để phân biệt với user thường
            // Ví dụ: user thì là "btn_", nhóm thì là "group_"
            btnNhom.Name = "group_" + groupID;

            // Thêm icon biểu thị nhóm (nếu muốn)
            btnNhom.Text = "👥 " + groupName;

            // Style cho nút nhóm (Có thể cho màu khác để dễ nhìn)
            btnNhom.Size = new Size(250, 50);
            btnNhom.BackColor = Color.FromArgb(70, 70, 80); // Màu hơi khác user chút
            btnNhom.ForeColor = Color.White;
            btnNhom.FlatStyle = FlatStyle.Flat;
            btnNhom.FlatAppearance.BorderSize = 0;
            btnNhom.TextAlign = ContentAlignment.MiddleLeft;
            btnNhom.Padding = new Padding(10, 0, 0, 0);
            btnNhom.Cursor = Cursors.Hand;

            // Sự kiện Click: Chuyển chế độ chat sang Nhóm
            // Lưu ý: Cần sửa hàm ChuyenCheDoChat để hỗ trợ ID Nhóm (xem Bước 6)
            btnNhom.Click += (s, e) => { ChuyenCheDoChat(groupID, true); };

            if (roundFlowLayoutPanel2.InvokeRequired)
                roundFlowLayoutPanel2.Invoke(new Action(() => roundFlowLayoutPanel2.Controls.Add(btnNhom)));
            else
                roundFlowLayoutPanel2.Controls.Add(btnNhom);
        }
    }
}