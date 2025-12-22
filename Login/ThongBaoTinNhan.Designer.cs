namespace Login
{
    partial class ThongBaoTinNhan
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlThongBaoContainer = new Panel();
            flowLayoutPanel1 = new FlowLayoutPanel();
            pnlThongBaoContainer.SuspendLayout();
            SuspendLayout();
            // 
            // pnlThongBaoContainer
            // 
            pnlThongBaoContainer.Controls.Add(flowLayoutPanel1);
            pnlThongBaoContainer.Location = new Point(0, 0);
            pnlThongBaoContainer.Name = "pnlThongBaoContainer";
            pnlThongBaoContainer.Size = new Size(923, 572);
            pnlThongBaoContainer.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Location = new Point(0, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(923, 572);
            flowLayoutPanel1.TabIndex = 0;
            flowLayoutPanel1.WrapContents = false;
            flowLayoutPanel1.Paint += flowLayoutPanel1_Paint;
            // 
            // ThongBaoTinNhan
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 68, 75);
            ClientSize = new Size(877, 541);
            Controls.Add(pnlThongBaoContainer);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4);
            Name = "ThongBaoTinNhan";
            Text = "ThongBaoTinNhan";
            Load += ThongBaoTinNhan_Load;
            pnlThongBaoContainer.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlThongBaoContainer;
        private FlowLayoutPanel flowLayoutPanel1;
    }
}