using Agora.Rtc;
using System;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Login
{
    public partial class Call : Form
    {
        // --- CẤU HÌNH AGORA ---
        private string _appID = "c505b6fe6e6549509d0c735b2335fe85"; // <--- ĐIỀN APP ID
        private IRtcEngine rtcEngine;

        private NetworkStream _serverStream;
        private string _channelName;
        private string _otherPersonName;
        private bool _isMicOn = true;

        public Call(NetworkStream stream, string channelName, string otherPersonName)
        {
            InitializeComponent();
            _serverStream = stream;
            _channelName = channelName;
            _otherPersonName = otherPersonName;

            // Tự động tắt khi Form đóng
            this.FormClosing += Call_FormClosing;
        }

        private void Call_Load(object sender, EventArgs e)
        {
            this.Text = "Đang gọi video với: " + _otherPersonName;
            InitAgoraEngine();
            JoinChannel();
            this.Text = "Đang gọi tại phòng: " + this._channelName;
        }

        private void InitAgoraEngine()
        {
            try
            {
                rtcEngine = RtcEngine.CreateAgoraRtcEngine();
                RtcEngineContext context = new RtcEngineContext(_appID, 0,
                                            CHANNEL_PROFILE_TYPE.CHANNEL_PROFILE_COMMUNICATION,
                                            AUDIO_SCENARIO_TYPE.AUDIO_SCENARIO_DEFAULT);

                // --- BẮT LỖI TẠI ĐÂY ---
                int ret = rtcEngine.Initialize(context);
                if (ret != 0)
                {
                    MessageBox.Show($"AGORA KHỞI TẠO THẤT BẠI! Mã lỗi: {ret}\n" +
                                    $"(Lỗi -1: Thiếu Visual C++ hoặc DLL)\n" +
                                    $"(Lỗi -7: Sai App ID)");
                    rtcEngine = null; // Đánh dấu là hỏng
                    return;
                }

                // Nếu thành công thì mới làm tiếp
                rtcEngine.InitEventHandler(new UserEventHandler(this));
                rtcEngine.EnableVideo();
                rtcEngine.EnableAudio();

                // Join Channel
                rtcEngine.JoinChannel("", _channelName, "", 0);

                // Setup Camera Local
                // (Đảm bảo tên circularPictureBox2 là đúng tên trên giao diện của bạn)
                if (circularPictureBox2 != null)
                {
                    VideoCanvas localVideo = new VideoCanvas();
                    localVideo.view = (long)circularPictureBox2.Handle;
                    localVideo.renderMode = RENDER_MODE_TYPE.RENDER_MODE_HIDDEN;
                    localVideo.uid = 0;
                    rtcEngine.SetupLocalVideo(localVideo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Code Init: " + ex.Message);
            }
        }

        private void JoinChannel()
        {
            if (rtcEngine != null)
            {
                rtcEngine.JoinChannel("", _channelName, "", 0);
            }
        }

        // --- XỬ LÝ SỰ KIỆN TỪ AGORA (Người kia vào/ra) ---
        public void OnUserJoined(uint uid)
        {
            this.Invoke(new Action(() =>
            {
                // Setup Video người kia (Remote)
                // Lưu ý: Bạn cần tạo pbRemote trên giao diện Design
                if (circularPictureBox1 != null)
                {
                    VideoCanvas remoteVideo = new VideoCanvas();
                    remoteVideo.view = (long)circularPictureBox1.Handle;
                    remoteVideo.renderMode = RENDER_MODE_TYPE.RENDER_MODE_HIDDEN;
                    remoteVideo.uid = uid;

                    rtcEngine.SetupRemoteVideo(remoteVideo);
                }
            }));
        }

        public void OnUserOffline(uint uid)
        {
            this.Invoke(new Action(() =>
            {
                MessageBox.Show("Cuộc gọi đã kết thúc.");
                this.Close();
            }));
        }

        // --- NÚT BẤM ---
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // QUAN TRỌNG: Thêm + "\n" vào cuối cùng
                string msg = $"END_CALL|{_otherPersonName}|{_channelName}\n";

                byte[] buffer = Encoding.UTF8.GetBytes(msg);
                _serverStream.Write(buffer, 0, buffer.Length);
            }
            catch { } // Kệ lỗi mạng, cứ tắt form

            // Hủy Agora
            if (rtcEngine != null)
            {
                rtcEngine.LeaveChannel();
                rtcEngine.Dispose();
                rtcEngine = null;
            }
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (rtcEngine == null)
            {
                MessageBox.Show("Agora Engine chưa khởi động được! Kiểm tra lại file DLL hoặc AppID.");
                return;
            }
            _isMicOn = !_isMicOn;
            rtcEngine.EnableLocalAudio(_isMicOn);
            button1.Text = _isMicOn ? "Mic: ON" : "Mic: OFF";
        }

        private void Call_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (rtcEngine != null)
            {
                rtcEngine.LeaveChannel();
                rtcEngine.Dispose();
                rtcEngine = null;
            }
        }
    }

    // Class lắng nghe sự kiện
    internal class UserEventHandler : IRtcEngineEventHandler
    {
        private Call _parent;
        public UserEventHandler(Call parent) { _parent = parent; }

        public override void OnUserJoined(RtcConnection connection, uint remoteUid, int elapsed)
        {
            _parent.OnUserJoined(remoteUid);
        }

        public override void OnUserOffline(RtcConnection connection, uint remoteUid, USER_OFFLINE_REASON_TYPE reason)
        {
            _parent.OnUserOffline(remoteUid);
        }
    }
}