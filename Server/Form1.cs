using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Server;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

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

        // Xóa chữ 'async' ở đây đi
        // 1. Đổi void thành async Task
        private async void HandleClientComm(object? client)
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
                                string userSender = "Ẩn danh";
                                lock (connectedUsers) { if (connectedUsers.ContainsKey(clientStream)) userSender = connectedUsers[clientStream]; }
                                string content = (requestParts.Length > 1) ? requestParts[1] : "";
                                // Chat không cần await cũng được, nhưng Broadcast nên nhanh
                                BroadcastMessage($"CHAT|{userSender}|{content}", clientStream);
                                UpdateUILabel($"{userSender}: {content}");
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

                            // Các case khác nhớ thêm 'await' nếu gọi hàm async
                            case "DANGKI":
                                await HandleRegistration(requestParts, clientStream);
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
                lock (clientStreams) { clientStreams.Remove(clientStream); }
                // Xóa khỏi danh sách user online
                lock (connectedUsers) { if (connectedUsers.ContainsKey(clientStream)) connectedUsers.Remove(clientStream); }

                tcpClient.Close();
                UpdateUILabel("Client đã ngắt kết nối.");
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

                string userID = await Database.LayIDNguoiDung(taiKhoan);
                SendResponse(clientStream, "DANGNHAP_SUCCESS|" + taiKhoan);
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
            if (label1.InvokeRequired) label1.Invoke(new Action(() => label1.Text = message));
            else label1.Text = message;
        }

        private void SendResponse(NetworkStream clientStream, string response)
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
                    Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
                    clientThread.Start(client);
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }
    }
}