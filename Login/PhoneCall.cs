using System;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using Agora.Rtc; // Đảm bảo đã cài NuGet agora_rtc_sdk

namespace Login
{
    public partial class PhoneCall : Form
    {
        // Các biến lưu trữ thông tin kết nối
        private NetworkStream _serverStream;
        private string _myUsername; // Tên mình
        private string _targetUser; // Tên đối phương
        private string _channelId;  // ID phòng
        private bool _isCaller;     // True nếu mình là người gọi, False nếu là người nhận

        // Class xử lý Agora (Sử dụng lại class AgoraService bạn đã có)
        private AgoraService agoraService;
        private bool isMuted = false;

        // Constructor nhận đủ thông tin cần thiết
        public PhoneCall(NetworkStream stream, string myName, string targetName, string channelId, bool isCaller)
        {
            InitializeComponent();

            _serverStream = stream;
            _myUsername = myName;
            _targetUser = targetName;
            _channelId = channelId;
            _isCaller = isCaller;

            // Khởi tạo Agora Service
            agoraService = new AgoraService();
        }

        private void PhoneCall_Load(object sender, EventArgs e)
        {
            // 1. Khởi tạo Engine và lắng nghe sự kiện
            agoraService.InitEngine(new AgoraEventHandler(this));

            // 2. Xử lý giao diện dựa trên vai trò
            if (_isCaller)
            {
                // -- NGƯỜI GỌI --
                lblStatus.Text = $"Đang gọi cho {_targetUser}...";
                btnAccept.Visible = false; // Ẩn nút Nghe
                btnReject.Text = "Hủy";    // Nút từ chối thành nút Hủy

                // Người gọi vào phòng luôn để đợi
                agoraService.JoinChannel(_channelId);
            }
            else
            {
                // -- NGƯỜI NHẬN --
                lblStatus.Text = $"{_targetUser} đang gọi cho bạn...";
                btnAccept.Visible = true;
                btnAccept.Text = "Nghe";
                btnReject.Text = "Từ chối";

                // Người nhận CHƯA vào phòng Agora vội, đợi bấm nút Nghe
            }
        }

        // --- XỬ LÝ NÚT BẤM ---

        // Nút 1: ACCEPT (Nghe) - Chỉ dành cho người nhận
        private void btnAccept_Click(object sender, EventArgs e)
        {
            // 1. Gửi tín hiệu đồng ý về Server
            // Gói tin: RESPONSE_CALL | CallerName | ACCEPT
            SendTcpPacket($"RESPONSE_CALL|{_targetUser}|ACCEPT");

            // 2. Vào phòng Agora để bắt đầu nói chuyện
            agoraService.JoinChannel(_channelId);

            // 3. Cập nhật giao diện
            btnAccept.Visible = false; // Ẩn nút nghe đi
            btnReject.Text = "Kết thúc"; // Nút từ chối giờ thành nút tắt máy
            lblStatus.Text = "Đang kết nối...";
        }

        // Nút 2: REJECT (Từ chối / Hủy / Kết thúc)
        private void btnReject_Click(object sender, EventArgs e)
        {
            // Logic: Dù là đang gọi, đang nghe, hay từ chối thì bấm nút này đều là KẾT THÚC

            // 1. Nếu chưa nghe máy mà bấm Từ chối -> Gửi REJECT
            // 2. Nếu đang nói chuyện mà bấm Kết thúc -> Gửi END_CALL

            if (_isCaller)
            {
                // Người gọi bấm Hủy -> Gửi END_CALL
                SendTcpPacket($"END_CALL|{_targetUser}");
            }
            else
            {
                // Người nhận bấm
                if (btnAccept.Visible == true) // Chưa bấm nghe -> Gửi REJECT
                {
                    SendTcpPacket($"RESPONSE_CALL|{_targetUser}|REJECT");
                }
                else // Đã bấm nghe rồi -> Gửi END_CALL
                {
                    SendTcpPacket($"END_CALL|{_targetUser}");
                }
            }

            CloseFormAndLeave();
        }

        // Hàm chung để thoát và đóng form
        public void CloseFormAndLeave()
        {
            if (agoraService != null)
            {
                agoraService.LeaveChannel();
            }

            // Dùng Invoke để đảm bảo thread-safe nếu gọi từ luồng khác
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)(() => this.Close()));
            }
            else
            {
                this.Close();
            }
        }

        // Hàm gửi tin nhắn TCP (Bọc try-catch để không crash)
        private void SendTcpPacket(string message)
        {
            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes(message);
                if (_serverStream != null && _serverStream.CanWrite)
                {
                    _serverStream.Write(buffer, 0, buffer.Length);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Mất kết nối tới Server: " + ex.Message);
            }
        }

        // --- Class xử lý sự kiện Agora (Callback) ---
        internal class AgoraEventHandler : IRtcEngineEventHandler
        {
            private PhoneCall parent;
            public AgoraEventHandler(PhoneCall p) { parent = p; }

            public override void OnJoinChannelSuccess(RtcConnection connection, int elapsed)
            {
                parent.Invoke((MethodInvoker)(() => parent.lblStatus.Text = "Đang đợi đối phương..."));
            }

            public override void OnUserJoined(RtcConnection connection, uint remoteUid, int elapsed)
            {
                parent.Invoke((MethodInvoker)(() => {
                    parent.lblStatus.Text = "Đã kết nối cuộc gọi";
                    // Nếu là người gọi, khi đối phương vào thì nút Hủy thành Kết thúc
                    if (parent._isCaller) parent.btnReject.Text = "Kết thúc";
                }));
            }

            public override void OnUserOffline(RtcConnection connection, uint remoteUid, USER_OFFLINE_REASON_TYPE reason)
            {
                parent.Invoke((MethodInvoker)(() => {
                    MessageBox.Show("Cuộc gọi đã kết thúc.");
                    parent.CloseFormAndLeave();
                }));
            }
        }
    }
}