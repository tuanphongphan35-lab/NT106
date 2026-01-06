using Agora.Rtc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login
{
    public class AgoraService
    {
        private IRtcEngine rtcEngine;
        // App ID này bạn nên bảo mật, nhưng để test thì cứ để đây
        private string appId = "c505b6fe6e6549509d0c735b2335fe85";
        // Khởi tạo Engine
        // Handler được truyền từ Form PhoneCall vào để Form đó xử lý giao diện
        public void InitEngine(IRtcEngineEventHandler handler)
        {
            if (rtcEngine != null) return;

            rtcEngine = RtcEngine.CreateAgoraRtcEngine();
            var context = new RtcEngineContext(
                appId,
                0,
                CHANNEL_PROFILE_TYPE.CHANNEL_PROFILE_COMMUNICATION,
                AUDIO_SCENARIO_TYPE.AUDIO_SCENARIO_DEFAULT,
                AREA_CODE.AREA_CODE_GLOB,
                null,
                null,
                false
            );

            int result = rtcEngine.Initialize(context);
            if (result != 0)
            {
                // Xử lý lỗi nếu init thất bại (ví dụ: in ra Console)
                Console.WriteLine("Agora Init Failed!");
            }

            // Đăng ký sự kiện từ bên ngoài truyền vào (PhoneCall Form)
            rtcEngine.InitEventHandler(handler);

            // Bắt buộc: Phải enable Audio thì mới nói chuyện được
            rtcEngine.EnableAudio();
        }

        public void JoinChannel(string channelName)
        {
            if (rtcEngine == null) return;

            // Token = "" (nếu chế độ test mode), 
            // channelName = tên phòng, 
            // "" = info (bỏ qua), 
            // 0 = để Agora tự sinh UserID
            rtcEngine.JoinChannel("", channelName, "", 0);
        }

        public void LeaveChannel()
        {
            if (rtcEngine != null)
            {
                rtcEngine.LeaveChannel();
                rtcEngine.Dispose();
                rtcEngine = null;
            }
        }

        // Thêm hàm Mute để nút tắt mic hoạt động
        public void MuteLocalAudio(bool isMuted)
        {
            if (rtcEngine != null)
            {
                // true = tắt mic, false = bật mic
                rtcEngine.MuteLocalAudioStream(isMuted);
            }
        }
    }
}
