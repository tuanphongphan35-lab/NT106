using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Agora.Rtc;

namespace Login
{
    public partial class Call : Form
    {
        // Khai báo AgoraService (Class bên file Agora.cs)
        private AgoraService agoraService;
        private string currentChannelId;
        private bool isMuted = false;

        // 1. Sửa Constructor để nhận ChannelID (Tên phòng) từ ChatForm truyền sang
        // Lưu ý: Bạn cần sửa chỗ gọi form này ở ChatForm thành: new Call("TenPhong")
        public Call(string channelId)
        {
            InitializeComponent();

            this.currentChannelId = channelId;
            this.agoraService = new AgoraService();
        }
        private void Call_Load(object sender, EventArgs e)
        {
            // Khởi tạo Engine và truyền vào bộ xử lý sự kiện (EventHandler)
            agoraService.InitEngine(new AgoraEventHandler(this));

            // Vào phòng ngay lập tức
            agoraService.JoinChannel(currentChannelId);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // xử lý tắt micro ở đây
            isMuted = !isMuted; // Đảo ngược trạng thái (Bật -> Tắt, Tắt -> Bật)

            agoraService.MuteLocalAudio(isMuted); // Gọi hàm bên AgoraService

            // Đổi text nút bấm cho dễ nhìn
            if (isMuted) button1.Text = "Bật Mic";
            else button1.Text = "Tắt Mic";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Xử lý ngắt cuộc gọi ở đây
            // Rời phòng Agora
            if (agoraService != null)
            {
                agoraService.LeaveChannel();
            }

            // --- PHẦN GỬI TÍN HIỆU TCP ---
            // Bạn bỏ comment và thay bằng hàm gửi tin nhắn TCP thật của bạn
            // Ví dụ: Client.Send("END_CALL"); 

            // Đóng form
            this.Close();
        }
        internal class AgoraEventHandler : IRtcEngineEventHandler
        {
            private Call parent;

            public AgoraEventHandler(Call p)
            {
                this.parent = p;
            }

            // Khi mình vào phòng thành công
            public override void OnJoinChannelSuccess(RtcConnection connection, int elapsed)
            {
                // Dùng Invoke để cập nhật giao diện từ luồng khác
                parent.Invoke((MethodInvoker)(() =>
                {
                    // Có thể hiện thông báo nhỏ hoặc đổi tiêu đề Form
                    parent.Text = "Đang đợi người kia...";
                }));
            }

            // Khi người kia vào phòng (Hai bên bắt đầu nghe thấy nhau)
            public override void OnUserJoined(RtcConnection connection, uint remoteUid, int elapsed)
            {
                parent.Invoke((MethodInvoker)(() =>
                {
                    parent.Text = "Đã kết nối cuộc gọi!";
                    MessageBox.Show("Người bên kia đã bắt máy.");
                }));
            }

            // Khi người kia tắt máy hoặc mất mạng
            public override void OnUserOffline(RtcConnection connection, uint remoteUid, USER_OFFLINE_REASON_TYPE reason)
            {
                parent.Invoke((MethodInvoker)(() =>
                {
                    MessageBox.Show("Cuộc gọi đã kết thúc.");
                    parent.Close(); // Tự động đóng form
                }));
            }
        }
    }
}
