namespace Login
{
    partial class QuenMK
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
            pictureBox1 = new PictureBox();
            label1 = new Label();
            buttonOTP = new Button();
            textBoxOTP = new TextBox();
            label5 = new Label();
            textBox4 = new TextBox();
            textBox3 = new TextBox();
            textBox2 = new TextBox();
            label3 = new Label();
            label2 = new Label();
            label4 = new Label();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = Properties.Resources.logo3;
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.InitialImage = null;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(133, 123);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.Location = new Point(228, 85);
            label1.Name = "label1";
            label1.Size = new Size(232, 38);
            label1.TabIndex = 1;
            label1.Text = "Quên Mật Khẩu ";
            // 
            // buttonOTP
            // 
            buttonOTP.BackColor = Color.DeepSkyBlue;
            buttonOTP.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold | FontStyle.Italic);
            buttonOTP.ForeColor = SystemColors.ButtonHighlight;
            buttonOTP.Location = new Point(370, 251);
            buttonOTP.Name = "buttonOTP";
            buttonOTP.Size = new Size(115, 37);
            buttonOTP.TabIndex = 29;
            buttonOTP.Text = "Gửi OTP";
            buttonOTP.UseVisualStyleBackColor = false;
            buttonOTP.Click += buttonOTP_Click;
            // 
            // textBoxOTP
            // 
            textBoxOTP.Location = new Point(286, 306);
            textBoxOTP.Name = "textBoxOTP";
            textBoxOTP.Size = new Size(291, 31);
            textBoxOTP.TabIndex = 28;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Times New Roman", 10F, FontStyle.Bold);
            label5.ForeColor = Color.Black;
            label5.Location = new Point(85, 311);
            label5.Name = "label5";
            label5.Size = new Size(136, 23);
            label5.TabIndex = 27;
            label5.Text = "OTP Xác Thực";
            // 
            // textBox4
            // 
            textBox4.Location = new Point(286, 461);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(291, 31);
            textBox4.TabIndex = 26;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(286, 384);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(291, 31);
            textBox3.TabIndex = 25;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(286, 202);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(291, 31);
            textBox2.TabIndex = 24;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Times New Roman", 10F, FontStyle.Bold);
            label3.ForeColor = Color.Black;
            label3.Location = new Point(85, 466);
            label3.Name = "label3";
            label3.Size = new Size(185, 23);
            label3.TabIndex = 23;
            label3.Text = "Xác Nhận Mật Khẩu ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Times New Roman", 10F, FontStyle.Bold);
            label2.ForeColor = Color.Black;
            label2.Location = new Point(85, 389);
            label2.Name = "label2";
            label2.Size = new Size(94, 23);
            label2.TabIndex = 22;
            label2.Text = "Mật Khẩu";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = SystemColors.Control;
            label4.Font = new Font("Times New Roman", 10F, FontStyle.Bold);
            label4.ForeColor = Color.Black;
            label4.Location = new Point(85, 207);
            label4.Name = "label4";
            label4.Size = new Size(58, 23);
            label4.TabIndex = 21;
            label4.Text = "Email";
            // 
            // button1
            // 
            button1.BackColor = Color.Lime;
            button1.Font = new Font("Segoe UI", 10F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            button1.ForeColor = SystemColors.ControlText;
            button1.Location = new Point(208, 550);
            button1.Name = "button1";
            button1.Size = new Size(235, 41);
            button1.TabIndex = 30;
            button1.Text = "Đặt Lại Mật Khẩu";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // QuenMK
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(662, 659);
            Controls.Add(button1);
            Controls.Add(buttonOTP);
            Controls.Add(textBoxOTP);
            Controls.Add(label5);
            Controls.Add(textBox4);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label4);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Name = "QuenMK";
            Text = "Quên Mật Khẩu";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Label label1;
        private Button buttonOTP;
        private TextBox textBoxOTP;
        private Label label5;
        private TextBox textBox4;
        private TextBox textBox3;
        private TextBox textBox2;
        private Label label3;
        private Label label2;
        private Label label4;
        private Button button1;
    }
}