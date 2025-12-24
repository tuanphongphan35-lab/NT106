using System;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Login
{
    public partial class PhoneCall : Form
    {
        private NetworkStream _stream;
        private string _callerName;
        private string _channelID;

        public PhoneCall(NetworkStream stream, string callerName, string channelID)
        {
            InitializeComponent();
            _stream = stream;
            _callerName = callerName;
            _channelID = channelID;

            // Hiển thị tên người gọi
            // labelTenNguoiGoi.Text = callerName + " đang gọi..."; 
        }

        private void SendSignal(string msg)
        {
            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes(msg + "\n");
                _stream.Write(buffer, 0, buffer.Length);
            }
            catch { }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            // 1. Gửi tín hiệu CHẤP NHẬN về Server
            SendSignal($"RESPONSE_CALL|{_callerName}|ACCEPT");

            // 2. Mở form Gọi Video (Call) lên
            Call videoForm = new Call(_stream, _channelID, _callerName);
            videoForm.Show();

            // 3. Đóng form thông báo này
            this.Close();
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            // 1. Gửi tín hiệu TỪ CHỐI
            SendSignal($"RESPONSE_CALL|{_callerName}|REJECT");
            this.Close();
        }
    }
}