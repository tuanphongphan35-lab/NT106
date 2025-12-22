using Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class Form1 : Form
    {
        private static List<NetworkStream> clientStreams = new List<NetworkStream>();

        // --- THAY ĐỔI 1: Thêm Dictionary để map giữa Stream và Tên tài khoản ---
        private static Dictionary<NetworkStream, string> connectedUsers = new Dictionary<NetworkStream, string>();

        private TcpListener tcpListener = null;
        private Thread listenThread = null;

        public Form1()
        {
            InitializeComponent();
        }

        // ... (Giữ nguyên các hàm Form1_Load, StartServer) ...

        private void Form1_Load(object sender, EventArgs e)
        {
            StartServer();
            Invoke((MethodInvoker)(() => label1.Text = "Server đang chạy. Kết nối..."));
        }

        private void StartServer()
        {
            tcpListener = new TcpListener(IPAddress.Any, 8080);
            listenThread = new Thread(new ThreadStart(ListenForClients));
            listenThread.Start();
        }

        // --- THAY ĐỔI 2: Cập nhật hàm BroadcastMessage ---
        // Hàm này gửi tin nhắn đến tất cả client
        private void BroadcastMessage(string message, NetworkStream senderStream)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(message);

            lock (clientStreams)
            {
                for (int i = clientStreams.Count - 1; i >= 0; i--)
                {
                    // --- QUAN TRỌNG: Kiểm tra nếu là người gửi thì BỎ QUA ---
                    if (clientStreams[i] == senderStream)
                    {
                        continue; // Không gửi lại cho chính người này
                    }

                    try
                    {
                        clientStreams[i].Write(buffer, 0, buffer.Length);
                    }
                    catch
                    {
                        clientStreams.RemoveAt(i);
                    }
                }
            }
        }


        private async Task HandleClientComm(object? client)
        {
            TcpClient tcpClient = (TcpClient)client!;
            NetworkStream clientStream = tcpClient.GetStream();

            lock (clientStreams) { clientStreams.Add(clientStream); }
            UpdateUILabel("Kết nối mới được chấp nhận.");

            byte[] message = new byte[4096];
            int bytesRead;

            try
            {
                while (true)
                {
                    bytesRead = 0;
                    try
                    {
                        // 2. Dùng ReadAsync thay vì Read (Quan trọng!)
                        // Để Server không bị treo khi đợi tin nhắn
                        bytesRead = await clientStream.ReadAsync(message, 0, 4096);
                    }
                    catch
                    {
                        break;
                    }

                    if (bytesRead == 0) break;

                    string dataReceived = Encoding.UTF8.GetString(message, 0, bytesRead);
                    Console.WriteLine($"[SERVER NHẬN]: {dataReceived}");

                    try
                    {
                        string[] requestParts = dataReceived.Split(new char[] { '|' }, StringSplitOptions.None);
                        string command = requestParts[0];
                        switch (command)
                        {
                            case "DANGNHAP":
                                // 3. Có chữ 'await' để đợi đăng nhập xong mới làm việc khác
                                await HandleLogin(requestParts, clientStream);
                                break;

                            case "CHAT":
                                await HandelChatMessage(requestParts, clientStream);
                                break;

                            case "LAY_LICH_SU":
                                await HandelLayLS(requestParts, clientStream);
                                break;
                            case "TIM_KIEM":
                                // 1. Lấy từ khóa từ gói tin (Format: TIM_KIEM|TuKhoa)
                                string keyword = (requestParts.Length > 1) ? requestParts[1] : "";

                                // Log ra Server biết (Dùng UpdateUILabel thay vì MessageBox để đỡ bị treo Server)
                                UpdateUILabel($"[Search] Client đang tìm: {keyword}");

                                // 2. Gọi Database 
                                // Lưu ý: Hàm Database.TimKiemNguoiDung cần trả về List dạng "TenUser:ID" như đã sửa ở bài trước
                                List<string> ketQua = await Database.TimKiemNguoiDung(keyword);

                                // 3. Nối chuỗi kết quả bằng dấu chấm phẩy ";"
                                // Kết quả sẽ là: "TenA:ID1;TenB:ID2;TenC:ID3"
                                string dataTraVe = string.Join(";", ketQua);

                                // 4. Gửi phản hồi về Client
                                // Header phải là "TIM_THAY" để khớp với code Client: if (message.StartsWith("TIM_THAY|"))
                                SendResponse(clientStream, "TIM_THAY|" + dataTraVe);

                                Console.WriteLine($"[Server] Đã trả về {ketQua.Count} kết quả.");
                                break;
                            case "KET_BAN":
                                // Xử lý lời mời kết bạn
                                await HandleFriendRequest(requestParts, clientStream);
                                break;
                            case "PHAN_HOI_KET_BAN":
                                // Xử lý phản hồi lời mời kết bạn
                                await HandleFriendResponse(requestParts, clientStream);
                                break;
                            // Các case khác nhớ thêm 'await' nếu gọi hàm async
                            case "DANGKI":
                                await HandleRegistration(requestParts, clientStream);
                                break;
                            case "REQUEST_CALL":
                                await HandleRequestCall(requestParts, clientStream);
                                break;

                            case "RESPONSE_CALL":
                                await HandleResponseCall(requestParts, clientStream);
                                break;

                            case "END_CALL":
                                HandleEndCall(requestParts, clientStream);
                                break;
                        }
                    }
                    catch (Exception exLogic)
                    {
                        Console.WriteLine($"[LỖI LOGIC]: {exLogic.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[LỖI KẾT NỐI]: {ex.Message}");
            }
            finally
            {
                lock (connectedUsers)
                {
                    // Kiểm tra xem ai vừa thoát
                    if (connectedUsers.ContainsKey(clientStream))
                    {
                        string userDisconnected = connectedUsers[clientStream];

                        // 1. Báo cho tất cả người còn lại biết: User này đã OFFLINE
                        foreach (var user in connectedUsers)
                        {
                            // Không gửi cho chính người vừa thoát
                            if (user.Key != clientStream)
                            {
                                SendResponse(user.Key, $"STATUS|{userDisconnected}|OFFLINE");
                            }
                        }

                        // 2. Giờ mới xóa khỏi danh sách
                        connectedUsers.Remove(clientStream);
                        UpdateUILabel($"User {userDisconnected} đã ngắt kết nối.");
                    }
                }

                lock (clientStreams) { clientStreams.Remove(clientStream); }
                tcpClient.Close();
                UpdateUILabel("Client đã ngắt kết nối.");
            }
        }
        // --- 1. HÀM XỬ LÝ TIN NHẮN (CHAT) ---
        private  async Task HandelChatMessage(string[] parts, NetworkStream clientStream)
        {
            try
            {
                // parts[0] là "CHAT"
                // parts[1] là Nội dung
                // parts[2] là Người nhận (nếu có)

                string content = (parts.Length > 1) ? parts[1] : "";
                string receiver = (parts.Length > 2) ? parts[2] : "ALL";

                // Lấy tên người gửi từ danh sách đang kết nối
                string sender = "AnDanh";
                if (connectedUsers.ContainsKey(clientStream))
                {
                    sender = connectedUsers[clientStream];
                }

                // --- TRƯỜNG HỢP 1: CHAT CHUNG (ALL) ---
                if (string.IsNullOrEmpty(receiver) || receiver == "ALL")
                {
                    // Gửi cho tất cả mọi người (trừ người gửi)
                    BroadcastMessage($"CHAT|{sender}|{content}",
                                     clientStream);
                    Console.WriteLine($"[Chat Chung] {sender}: {content}");
                }
                // --- TRƯỜNG HỢP 2: CHAT RIÊNG ---
                else
                {
                    // Tìm người nhận trong danh sách đang online
                    var clientNhan = connectedUsers.FirstOrDefault(x => x.Value == receiver);

                    if (clientNhan.Key != null)
                    {
                        // Gửi tin nhắn sang cho người nhận
                        // Format: CHAT | Người Gửi | Nội Dung
                        SendResponse(clientNhan.Key, $"CHAT|{sender}|{content}");
                    }

                    // QUAN TRỌNG: Lưu tin nhắn vào Firebase để sau này tải lại
                    // (Chạy ngầm không cần đợi)
                    _ = Database.LuuTinNhan(sender, receiver, content);

                    Console.WriteLine($"[Chat Riêng] {sender} -> {receiver}: {content}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi xử lý tin nhắn: " + ex.Message);
            }
        }

        // --- 2. HÀM XỬ LÝ TẢI LỊCH SỬ (LAY_LICH_SU) ---
        private  async Task HandelLayLS(string[] parts, NetworkStream clientStream)
        {
            try
            {
                // parts[1] là tên người bạn muốn xem lịch sử
                string friendName = parts[1];

                // Lấy tên mình
                string myName = connectedUsers[clientStream];

                Console.WriteLine($"[Server] {myName} đang tải lịch sử với {friendName}...");

                // 1. Gọi Database lấy danh sách tin nhắn cũ
                List<string> history = await Database.LayLichSuChat(myName, friendName);

                // 2. Gửi lần lượt từng dòng về cho Client
                foreach (string msg in history)
                {
                    // msg có dạng: "TênNgườiGửi|NộiDung" (do hàm Database trả về)
                    // Gửi về Client: HISTORY_DATA | TênNgườiGửi | NộiDung
                    SendResponse(clientStream, $"HISTORY_DATA|{msg}\n");
                }

                // 3. Báo hiệu đã gửi xong
                SendResponse(clientStream, "HISTORY_END\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi tải lịch sử: " + ex.Message);
            }
        }
        private async Task HandleFriendRequest(string[] requestParts, NetworkStream senderStream)
        {
            // Cấu trúc nhận: KET_BAN | Ten_Nguoi_Muon_Ket (B)
            string targetUserName = requestParts[1];

            // Lấy tên người gửi (A) từ danh sách đã lưu lúc đăng nhập
            string senderName = connectedUsers[senderStream];

            Console.WriteLine($"[Server] {senderName} muốn kết bạn với {targetUserName}");

            // 1. Tìm xem người B có đang online không
            var targetClientPair = connectedUsers.FirstOrDefault(x => x.Value == targetUserName);

            if (targetClientPair.Key != null) // Nếu B đang online
            {
                // Gửi lệnh xuống cho B: LOI_MOI | Tên_A
                string msg = $"LOI_MOI|{senderName}";
                SendResponse(targetClientPair.Key, msg); // Dùng hàm gửi có sẵn của bạn
            }
            else
            {
                // Báo lại cho A biết là B không online (Tùy chọn)
                SendResponse(senderStream, "SERVER_MSG|Người này hiện không online.");
            }
            UpdateUILabel($"[Friend] {senderName} đã gửi lời mời kết bạn tới {targetUserName}.");
            await Task.CompletedTask; // Để thỏa mãn kiểu trả về async Task
        }
        private async Task HandleFriendResponse(string[] requestParts, NetworkStream responderStream)
        {
            // Cấu trúc nhận: PHAN_HOI_KET_BAN | Ten_Nguoi_Gui_Loi_Moi (A) | DONG_Y (hoặc TU_CHOI)
            string originalSenderName = requestParts[1];
            string decision = requestParts[2];
            string responderName = connectedUsers[responderStream]; // Tên người trả lời (B)

            // 1. Nếu Đồng ý -> Lưu vào Database (QUAN TRỌNG)
            if (decision == "DONG_Y")
            {
                // Lưu quan hệ bạn bè vào DB
                await Database.ThemBanBe(originalSenderName, responderName);

                Console.WriteLine($"[Server] Đã thiết lập quan hệ bạn bè: {originalSenderName} - {responderName}");
            }

            // 2. Báo kết quả về cho người A
            var senderPair = connectedUsers.FirstOrDefault(x => x.Value == originalSenderName);
            if (senderPair.Key != null)
            {
                // Gửi: KET_QUA_KET_BAN | Tên_B | DONG_Y
                string msg = $"KET_QUA_KET_BAN|{responderName}|{decision}";
                SendResponse(senderPair.Key, msg);
            }
            UpdateUILabel($"[Friend] {responderName} đã {decision} lời mời kết bạn từ {originalSenderName}.");
            await Task.CompletedTask;
        }
        private async void HandleEndCall(string[] parts, NetworkStream senderStream)
        {
            // Cấu trúc nhận: END_CALL | TênNgườiNhận | IDPhòng
            string receiverName = parts[1];

            // Tìm người nhận (người cần bị ngắt kết nối)
            NetworkStream receiverStream = GetStreamByUsername(receiverName);

            if (receiverStream != null)
            {
                // --- SỬA LẠI: Gửi lệnh END_CALL (chứ không phải INCOMING_CALL) ---
                string msg = "END_CALL\n";
                SendResponse(receiverStream, msg);

                // Cập nhật Log Server cho đúng
                string senderName = "";
                if (connectedUsers.ContainsKey(senderStream)) senderName = connectedUsers[senderStream];
                UpdateUILabel($"[Call] Cuộc gọi giữa {senderName} và {receiverName} đã kết thúc.");
            }
            else
            {
                Console.WriteLine("Người nhận không online.");
            }
        }

        private async Task HandleResponseCall(string[] parts, NetworkStream receiverStream)
        {
            string callerName = parts[1];
            string status = parts[2]; // "ACCEPT" hoặc "REJECT"

            // Tìm stream của người gọi (người lúc nãy đã gọi)
            NetworkStream callerStream = GetStreamByUsername(callerName);

            // Lấy tên người nhận (chính là người đang gửi response này)
            string receiverName = "";
            lock (connectedUsers) { if (connectedUsers.ContainsKey(receiverStream)) receiverName = connectedUsers[receiverStream]; }

            if (callerStream != null)
            {
                // Chuyển tiếp phản hồi về cho người gọi
                // Gói tin gửi đi: CALL_RESPONSE | ReceiverName | ACCEPT (hoặc REJECT)
                SendResponse(callerStream, $"CALL_RESPONSE|{receiverName}|{status}");

                UpdateUILabel($"[Call] {receiverName} đã {status} cuộc gọi của {callerName}.");
            }
        }

        private async Task HandleRequestCall(string[] parts, NetworkStream senderStream)
        {
            // Cấu trúc nhận: REQUEST_CALL | Người Nhận | ID Phòng
            string targetName = parts[1];
            string channelID = parts[2];

            // Lấy tên người gọi (Sender)
            string senderName = "";
            if (connectedUsers.ContainsKey(senderStream))
            {
                senderName = connectedUsers[senderStream];
            }

            // Tìm Stream của người nhận
            NetworkStream targetStream = GetStreamByUsername(targetName);

            if (targetStream != null)
            {
                // --- SỬA LẠI ĐOẠN NÀY QUAN TRỌNG NHẤT ---

                // 1. Gửi lệnh INCOMING_CALL (báo có cuộc gọi đến)
                // Thay vì gửi lệnh CALL_ENDED (ngắt cuộc gọi) như code cũ
                string msg = $"INCOMING_CALL|{senderName}|{channelID}\n";
                SendResponse(targetStream, msg);

                // 2. Cập nhật Log Server cho đúng
                UpdateUILabel($"[Call] {senderName} đang gọi cho {targetName}...");
            }
            else
            {
                SendResponse(senderStream, "CALL_ERROR|User Offline\n");
            }
        }

        private async Task HandleRegistration(string[] requestParts, NetworkStream clientStream)
        {
            // ... (Code đăng ký giữ nguyên như cũ) ...
            // Copy lại logic cũ vào đây
            string taiKhoan = requestParts[1];
            string matKhau = requestParts[2];
            string email = requestParts[3];
            byte[] fileAnh = Convert.FromBase64String(requestParts[4]);

            if (await Database.KiemTraTonTaiTaiKhoan(taiKhoan)) { SendResponse(clientStream, "TAIKHOAN_EXIST"); return; }
            if (await Database.KiemTraTonTaiEmail(email)) { SendResponse(clientStream, "EMAIL_EXIST"); return; }

            bool themThanhCong = await Database.ThemTaiKhoan(taiKhoan, matKhau, email, fileAnh);
            if (themThanhCong) SendResponse(clientStream, "DANGKI_SUCCESS");
            else SendResponse(clientStream, "DANGKI_FAILED_DB");
        }

        // --- THAY ĐỔI 5: Cập nhật HandleLogin để lưu tên người dùng ---
        private async Task HandleLogin(string[] requestParts, NetworkStream clientStream)
        {
            string taiKhoan = requestParts[1];
            string matKhau = requestParts[2];

            bool ketQuaDangNhap = await Database.KiemTraDangNhap(taiKhoan, matKhau);

            if (ketQuaDangNhap)
            {
                // --- QUAN TRỌNG: Lưu mapping giữa Stream và Tên tài khoản ---
                lock (connectedUsers)
                {
                    if (connectedUsers.ContainsKey(clientStream))
                    {
                        connectedUsers[clientStream] = taiKhoan;
                    }
                    else
                    {
                        connectedUsers.Add(clientStream, taiKhoan);
                    }
                }
                List<string> danhSachBanBe = await Database.LayDanhSachBanBe(taiKhoan);
                if (danhSachBanBe.Count > 0)
                {
                    string friendString = string.Join(";", danhSachBanBe);
                    SendResponse(clientStream, "LIST_BAN_BE|" + friendString);
                }
                string userID = await Database.LayIDNguoiDung(taiKhoan);
                SendResponse(clientStream, "DANGNHAP_SUCCESS|" + taiKhoan);
                // 1. Báo cho MỌI NGƯỜI biết TÔI vừa Online
                foreach (var user in connectedUsers)
                {
                    // Gửi cho người khác: STATUS | Phong12345 | ONLINE
                    if (user.Value != taiKhoan)
                    {
                        SendResponse(user.Key, $"STATUS|{taiKhoan}|ONLINE");
                    }
                }

                // 2. Báo cho TÔI biết ai ĐANG Online sẵn
                foreach (var user in connectedUsers)
                {
                    if (user.Value != taiKhoan)
                    {
                        // Gửi về cho mình: STATUS | NgườiKhác | ONLINE
                        SendResponse(clientStream, $"STATUS|{user.Value}|ONLINE");
                    }
                }
                UpdateUILabel($"User {taiKhoan} đã đăng nhập.");
            }
            else
            {
                SendResponse(clientStream, "DANGNHAP_FAILED");
                UpdateUILabel($"Đăng nhập thất bại: {taiKhoan}");
            }
        }

        private async Task HandleQMK(string[] requestParts, NetworkStream clientStream)
        {
            // ... (Code Quên MK giữ nguyên như cũ) ...
            string email = requestParts[1];
            if (!await Database.KiemTraTonTaiEmail(email)) { SendResponse(clientStream, "EMAIL_NOT_FOUND"); return; }
            string matKhauDaLuu = await Database.LayMatKhauQuenMatKhau(email);
            if (!string.IsNullOrEmpty(matKhauDaLuu)) SendResponse(clientStream, "QUENMK_SUCCESS");
            else SendResponse(clientStream, "QUENMK_FAILED");
        }

        private void UpdateUILabel(string message)
        {
            string msgWithNewLine = message + Environment.NewLine;

            if (label1.InvokeRequired)
            {
                // Sửa "=" thành "+="
                label1.Invoke(new Action(() => label1.Text += msgWithNewLine));
            }
            else
            {
                // Sửa "=" thành "+="
                label1.Text += msgWithNewLine;
            }
        }
        private NetworkStream GetStreamByUsername(string username)
        {
            lock (connectedUsers)
            {
                foreach (var item in connectedUsers)
                {
                    if (item.Value.Equals(username, StringComparison.OrdinalIgnoreCase))
                    {
                        return item.Key; // Trả về stream của người đó
                    }
                }
            }
            return null; // Không tìm thấy (người đó offline)
        }
        private  void SendResponse(NetworkStream clientStream, string response)
        {
            byte[] responseData = Encoding.UTF8.GetBytes(response);
            try { clientStream.Write(responseData, 0, responseData.Length); }
            catch (Exception ex) { Console.WriteLine("Lỗi gửi: " + ex.Message); }
        }

        private void ListenForClients()
        {
            // ... (Giữ nguyên logic ListenForClients) ...
            try
            {
                tcpListener.Start();
                UpdateUILabel("Đang lắng nghe port 8080...");
                while (true)
                {
                    TcpClient client = tcpListener.AcceptTcpClient();
                    Thread clientThread = new Thread(() => HandleClientComm(client).Wait());
                    clientThread.Start();
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }
    }
}