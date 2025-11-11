using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading; // Cần Thread
using System.Windows.Forms; // Cần Form
using Server; // Cần gọi lớp FirestoreDatabase
using System;
using System.Threading.Tasks; // Cần Task và async/await

namespace Server
{
    // Đảm bảo Form1 thừa kế từ Form
    public partial class Form1 : Form
    {
        private TcpListener tcpListener;
        private Thread listenThread;

        // --- CÁC HÀM KHỞI TẠO TCP GIỮ NGUYÊN ---
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            StartServer();
            Invoke((MethodInvoker)(() => label1.Text = "Server đang chạy. Kết nối..."));
        }

        private void StartServer()
        {
            // Vẫn dùng IPAddress.Any, 8080 để lắng nghe kết nối TCP
            tcpListener = new TcpListener(IPAddress.Any, 8080);
            listenThread = new Thread(new ThreadStart(ListenForClients));
            listenThread.Start();

        }

        private void ListenForClients()
        {
            tcpListener.Start();

            while (true)
            {
                // AcceptTcpClient là hàm chặn (blocking), chấp nhận được gọi đồng bộ
                TcpClient client = tcpListener.AcceptTcpClient();
                // Tạo luồng mới để xử lý mỗi client
                Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
                clientThread.Start(client);
            }
        }

        // --- HÀM XỬ LÝ KẾT NỐI CLIENT (HandleClientComm) ---
        private async void HandleClientComm(object? client) // CHUYỂN THÀNH ASYNC VOID
        {
            TcpClient tcpClient = (TcpClient)client!;
            NetworkStream clientStream = tcpClient.GetStream();

            byte[] message = new byte[4096];
            int bytesRead;

            while (true)
            {
                bytesRead = 0;

                try
                {
                    // Đọc dữ liệu
                    bytesRead = clientStream.Read(message, 0, 4096);
                }
                catch
                {
                    break;
                }

                if (bytesRead == 0)
                {
                    break;
                }

                string dataReceived = Encoding.ASCII.GetString(message, 0, bytesRead);
                string[] requestParts = dataReceived.Split('|');
                string command = requestParts[0];

                switch (command)
                {
                    case "DANGKI":
                        // Xử lý yêu cầu đăng kí (Dùng await)
                        await HandleRegistration(requestParts, clientStream);
                        break;
                    case "DANGNHAP":
                        // Xử lý yêu cầu đăng nhập (Dùng await)
                        await HandleLogin(requestParts, clientStream);
                        break;
                    case "QUENMK":
                        // Xử lý yêu cầu quên mật khẩu (Dùng await)
                        await HandleQMK(requestParts, clientStream);
                        break;
                    default:
                        SendResponse(clientStream, "INVALID_REQUEST");
                        break;
                }
            }
            tcpClient.Close();
        }

        // --- HÀM XỬ LÝ ĐĂNG KÍ (HandleRegistration) ---
        // Bắt buộc là async Task
        private async Task HandleRegistration(string[] requestParts, NetworkStream clientStream)
        {
            string taiKhoan = requestParts[1];
            string matKhau = requestParts[2];
            string email = requestParts[3];

            // Convert base64 image string to byte[]
            byte[] fileAnh = Convert.FromBase64String(requestParts[4]);

            // Dùng FirestoreDatabase mới
            if (await Database.KiemTraTonTaiTaiKhoan(taiKhoan))
            {
                SendResponse(clientStream, "TAIKHOAN_EXIST");
                return;
            }

            if (await Database.KiemTraTonTaiEmail(email))
            {
                SendResponse(clientStream, "EMAIL_EXIST");
                return;
            }

            // Gọi hàm ThemTaiKhoan đã là async Task<bool> và sử dụng await
            bool themThanhCong = await Database.ThemTaiKhoan(taiKhoan, matKhau, email, fileAnh);

            if (themThanhCong)
            {
                SendResponse(clientStream, "DANGKI_SUCCESS");
            }
            else
            {
                SendResponse(clientStream, "DANGKI_FAILED_DB");
            }
        }

        // --- HÀM XỬ LÝ ĐĂNG NHẬP (HandleLogin) ---
        // Chuyển đổi sang async Task
        private async Task HandleLogin(string[] requestParts, NetworkStream clientStream)
        {
            string taiKhoan = requestParts[1];
            string matKhau = requestParts[2];

            // Gọi hàm KiemTraDangNhap đã là async Task<bool>
            bool ketQuaDangNhap = await Database.KiemTraDangNhap(taiKhoan, matKhau);

            if (ketQuaDangNhap)
            {
                // Lấy ID Document (string) và gửi về client (rất quan trọng cho các request sau)
                string userID = await Database.LayIDNguoiDung(taiKhoan);

                // Phản hồi: SUCCESS|ID_DOCUMENT
                SendResponse(clientStream, "DANGNHAP_SUCCESS|" + userID);

                // Hiển thị thông tin đăng nhập trên label (dùng Invoke)
                UpdateUILabel($"Tài khoản: {taiKhoan} - Đăng nhập thành công. ID: {userID}");
            }
            else
            {
                SendResponse(clientStream, "DANGNHAP_FAILED");
                UpdateUILabel($"Tài khoản: {taiKhoan} - Đăng nhập thất bại.");
            }
        }

        // --- HÀM XỬ LÝ QUÊN MẬT KHẨU (HandleQMK) ---
        // Chuyển đổi sang async Task
        private async Task HandleQMK(string[] requestParts, NetworkStream clientStream)
        {
            string email = requestParts[1];

            // Lưu ý: LayMatKhauQuenMatKhau giờ là async Task<string>
            // Hàm này (trong server TCP) KHÔNG NÊN gửi mật khẩu về, mà chỉ kiểm tra email và phản hồi.
            // Nếu bạn muốn gửi OTP, bạn cần triển khai logic gửi email ở đây.

            // Giả sử client đã gửi OTP trước đó và yêu cầu đặt lại mật khẩu ở đây:

            // Bước 1: Kiểm tra email có tồn tại
            if (!await Database.KiemTraTonTaiEmail(email))
            {
                SendResponse(clientStream, "EMAIL_NOT_FOUND");
                return;
            }

            // Bước 2: Lấy mật khẩu đã băm (cho mục đích xác nhận nội bộ, nhưng không gửi về client)
            string matKhauDaLuu = await Database.LayMatKhauQuenMatKhau(email);

            if (!string.IsNullOrEmpty(matKhauDaLuu))
            {
                // ***************************************************************
                // ** CẢNH BÁO: KHÔNG GỬI MẬT KHẨU ĐÃ BĂM (HOẶC MẬT KHẨU GỐC) QUA TCP/IP **
                // ***************************************************************

                // Tốt nhất là phản hồi thành công và để client tự xử lý bước tiếp theo
                SendResponse(clientStream, "QUENMK_SUCCESS");
            }
            else
            {
                SendResponse(clientStream, "QUENMK_FAILED");
            }
        }

        // --- HÀM CẬP NHẬT UI AN TOÀN ---
        private void UpdateUILabel(string message)
        {
            // Kiểm tra xem có cần Invoke không
            if (label1.InvokeRequired)
            {
                // Nếu đang ở luồng khác, gọi lại chính nó trên luồng UI
                label1.Invoke(new Action(() => label1.Text = message));
            }
            else
            {
                // Nếu đang ở luồng UI, cập nhật trực tiếp
                label1.Text = message;
            }
        }

        // --- HÀM GỬI PHẢN HỒI ---
        private void SendResponse(NetworkStream clientStream, string response)
        {
            byte[] responseData = Encoding.ASCII.GetBytes(response);
            try
            {
                clientStream.Write(responseData, 0, responseData.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi gửi phản hồi: " + ex.Message);
            }
        }

    }
}