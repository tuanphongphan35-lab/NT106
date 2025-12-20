using System;
using System.Windows.Forms;

namespace Login
{
    public partial class UC_ThongBaoKetBan : UserControl
    {
        private string _nguoiGui;
        private Action<string, string> _callback;

        // --- 2. Constructor nhận dữ liệu và gán vào biến ---
        public UC_ThongBaoKetBan(string nguoiGui, Action<string, string> callback)
        {
            InitializeComponent();

            // QUAN TRỌNG: Gán dữ liệu từ bên ngoài vào biến nội bộ
            this._nguoiGui = nguoiGui;
            this._callback = callback;
        }

        // --- 3. Nút ĐỒNG Ý ---
        private void roundButton1_Click(object sender, EventArgs e)
        {
            if (_callback != null)
            {
                _callback(_nguoiGui, "DONG_Y");
            }

            this.Parent.Controls.Remove(this);
        }

        // --- 4. Nút TỪ CHỐI ---
        private void roundButton2_Click(object sender, EventArgs e)
        {
            if (_callback != null)
            {
                _callback(_nguoiGui, "TU_CHOI");
            }

            this.Parent.Controls.Remove(this);
        }
    }
}