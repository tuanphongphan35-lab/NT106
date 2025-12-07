namespace Login
{
    partial class ChatForm
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
            panel1 = new Panel();
            roundTextBox1 = new RoundTextBox();
            roundFlowLayoutPanel2 = new RoundFlowLayoutPanel();
            panel2 = new Panel();
            roundButton1 = new RoundButton();
            roundTextBox3 = new RoundTextBox();
            roundFlowLayoutPanel1 = new RoundFlowLayoutPanel();
            roundTextBox2 = new RoundTextBox();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)circularPictureBox1).BeginInit();
            panel2.SuspendLayout();
            roundFlowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackgroundImage = Properties.Resources.BackList;
            panel1.Controls.Add(roundButton8);
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(roundTextBox1);
            panel1.Controls.Add(roundFlowLayoutPanel2);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(4);
            panel1.Name = "panel1";
            panel1.Size = new Size(434, 832);
            panel1.TabIndex = 0;
            panel1.Paint += panel1_Paint;
            // 
            // roundTextBox1
            // 
            roundTextBox1.BackColor = Color.FromArgb(64, 68, 75);
            roundTextBox1.BorderColor = Color.Gray;
            roundTextBox1.BorderFocusColor = Color.Gray;
            roundTextBox1.BorderRadius = 15;
            roundTextBox1.BorderSize = 0;
            roundTextBox1.ForeColor = Color.Gray;
            roundTextBox1.Location = new Point(12, 30);
            roundTextBox1.Multiline = false;
            roundTextBox1.Name = "roundTextBox1";
            roundTextBox1.Padding = new Padding(10, 7, 10, 7);
            roundTextBox1.PasswordChar = false;
            roundTextBox1.Size = new Size(253, 35);
            roundTextBox1.TabIndex = 1;
            roundTextBox1.Texts = "Tìm bạn bè...";
            roundTextBox1.UnderlinedStyle = false;
            roundTextBox1._TextChanged += roundTextBox1__TextChanged;
            // 
            // roundFlowLayoutPanel2
            // 
            roundFlowLayoutPanel2.BackColor = Color.FromArgb(64, 68, 75);
            roundFlowLayoutPanel2.BorderColor = Color.Transparent;
            roundFlowLayoutPanel2.BorderRadius = 20;
            roundFlowLayoutPanel2.BorderThickness = 0F;
            roundFlowLayoutPanel2.Location = new Point(12, 80);
            roundFlowLayoutPanel2.Name = "roundFlowLayoutPanel2";
            roundFlowLayoutPanel2.Size = new Size(253, 567);
            roundFlowLayoutPanel2.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.BackgroundImage = Properties.Resources.BackGroundChat1;
            panel2.Controls.Add(roundButton1);
            panel2.Controls.Add(roundTextBox3);
            panel2.Controls.Add(roundFlowLayoutPanel1);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(280, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(725, 666);
            panel2.TabIndex = 1;
            panel2.Paint += panel2_Paint;
            // 
            // roundButton1
            // 
            roundButton1.BackColor = Color.MediumSlateBlue;
            roundButton1.BorderColor = Color.Transparent;
            roundButton1.BorderRadius = 20;
            roundButton1.BorderThickness = 0F;
            roundButton1.FlatAppearance.BorderSize = 0;
            roundButton1.FlatStyle = FlatStyle.Flat;
            roundButton1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            roundButton1.ForeColor = Color.White;
            roundButton1.Location = new Point(577, 583);
            roundButton1.Name = "roundButton1";
            roundButton1.Size = new Size(119, 35);
            roundButton1.TabIndex = 2;
            roundButton1.Text = "Gửi";
            roundButton1.UseVisualStyleBackColor = false;
            // 
            // roundTextBox3
            // 
            roundTextBox3.BackColor = Color.FromArgb(64, 68, 75);
            roundTextBox3.BorderColor = Color.MediumSlateBlue;
            roundTextBox3.BorderFocusColor = Color.HotPink;
            roundTextBox3.BorderRadius = 15;
            roundTextBox3.BorderSize = 0;
            roundTextBox3.Location = new Point(39, 583);
            roundTextBox3.Multiline = false;
            roundTextBox3.Name = "roundTextBox3";
            roundTextBox3.Padding = new Padding(10, 7, 10, 7);
            roundTextBox3.PasswordChar = false;
            roundTextBox3.Size = new Size(532, 35);
            roundTextBox3.TabIndex = 1;
            roundTextBox3.Texts = "";
            roundTextBox3.UnderlinedStyle = false;
            // 
            // panel3
            // 
            panel3.BackColor = Color.Black;
            panel3.Controls.Add(roundButton6);
            panel3.Controls.Add(roundButton5);
            panel3.Controls.Add(roundButton4);
            panel3.Controls.Add(roundButton3);
            panel3.Controls.Add(roundButton2);
            panel3.Controls.Add(circularPictureBox1);
            panel3.Dock = DockStyle.Left;
            panel3.Location = new Point(0, 0);
            panel3.Margin = new Padding(4);
            panel3.Name = "panel3";
            panel3.Size = new Size(102, 832);
            panel3.TabIndex = 2;
            // 
            // roundTextBox2
            // 
            roundFlowLayoutPanel2.BackColor = Color.FromArgb(64, 68, 75);
            roundFlowLayoutPanel2.BorderColor = Color.Transparent;
            roundFlowLayoutPanel2.BorderRadius = 20;
            roundFlowLayoutPanel2.BorderThickness = 0F;
            roundFlowLayoutPanel2.Location = new Point(139, 92);
            roundFlowLayoutPanel2.Margin = new Padding(4);
            roundFlowLayoutPanel2.Name = "roundFlowLayoutPanel2";
            roundFlowLayoutPanel2.Size = new Size(252, 709);
            roundFlowLayoutPanel2.TabIndex = 0;
            // 
            // panel2
            // 
            // ChatForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1256, 832);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Margin = new Padding(4);
            Name = "ChatForm";
            Text = "ChatForm";
            Load += ChatForm_Load;
            panel1.ResumeLayout(false);
            panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)circularPictureBox1).EndInit();
            panel2.ResumeLayout(false);
            roundFlowLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private RoundFlowLayoutPanel roundFlowLayoutPanel1;
        private RoundTextBox roundTextBox1;
        private RoundFlowLayoutPanel roundFlowLayoutPanel2;
        private RoundTextBox roundTextBox2;
        private RoundButton roundButton1;
        private RoundTextBox txtNoiDungTinNhan;
        private Panel panel3;
        private CircularPictureBox circularPictureBox1;
        private RoundButton roundButton4;
        private RoundButton roundButton3;
        private RoundButton roundButton2;
        private RoundButton roundButton5;
        private RoundButton roundButton6;
        private RoundButton roundButton7;
        private RoundButton roundButton8;
    }
}