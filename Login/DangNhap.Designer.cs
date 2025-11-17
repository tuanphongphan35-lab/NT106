namespace Login
{
    partial class DangNhap
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
            pictureBox1 = new PictureBox();
            panel3 = new Panel();
            button2 = new Button();
            label3 = new Label();
            panel2 = new Panel();
            linkLabel1 = new LinkLabel();
            button1 = new Button();
            textBox2 = new TextBox();
            label2 = new Label();
            textBox1 = new TextBox();
            label1 = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackgroundImage = Properties.Resources.nền_đăng_nhập;
            panel1.BackgroundImageLayout = ImageLayout.Stretch;
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(2);
            panel1.Name = "panel1";
            panel1.Size = new Size(717, 534);
            panel1.TabIndex = 0;
            panel1.Paint += panel1_Paint;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.BackgroundImage = Properties.Resources.logo3;
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Location = new Point(26, 2);
            pictureBox1.Margin = new Padding(2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(122, 121);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(64, 64, 64);
            panel3.Controls.Add(button2);
            panel3.Controls.Add(label3);
            panel3.ForeColor = SystemColors.ActiveCaptionText;
            panel3.Location = new Point(367, 128);
            panel3.Margin = new Padding(2);
            panel3.Name = "panel3";
            panel3.RightToLeft = RightToLeft.No;
            panel3.Size = new Size(293, 314);
            panel3.TabIndex = 1;
            panel3.Paint += panel3_Paint;
            // 
            // button2
            // 
            button2.BackColor = Color.LimeGreen;
            button2.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold | FontStyle.Italic);
            button2.ForeColor = SystemColors.ButtonHighlight;
            button2.Location = new Point(79, 77);
            button2.Margin = new Padding(2);
            button2.Name = "button2";
            button2.Size = new Size(133, 42);
            button2.TabIndex = 3;
            button2.Text = "REGISTER ";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Times New Roman", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = SystemColors.ButtonHighlight;
            label3.Location = new Point(57, 45);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(191, 21);
            label3.TabIndex = 2;
            label3.Text = "NEED AN ACCOUNT?";
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(64, 64, 64);
            panel2.Controls.Add(linkLabel1);
            panel2.Controls.Add(button1);
            panel2.Controls.Add(textBox2);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(textBox1);
            panel2.Controls.Add(label1);
            panel2.Location = new Point(58, 128);
            panel2.Margin = new Padding(2);
            panel2.Name = "panel2";
            panel2.Size = new Size(297, 314);
            panel2.TabIndex = 0;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Font = new Font("Times New Roman", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            linkLabel1.LinkColor = Color.DeepSkyBlue;
            linkLabel1.Location = new Point(29, 279);
            linkLabel1.Margin = new Padding(2, 0, 2, 0);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(161, 19);
            linkLabel1.TabIndex = 4;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Forgot your password ";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // button1
            // 
            button1.BackColor = Color.DeepSkyBlue;
            button1.BackgroundImageLayout = ImageLayout.Stretch;
            button1.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.White;
            button1.Location = new Point(94, 217);
            button1.Margin = new Padding(2);
            button1.Name = "button1";
            button1.Size = new Size(108, 42);
            button1.TabIndex = 3;
            button1.Text = "LOGIN";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(18, 168);
            textBox2.Margin = new Padding(2);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(257, 27);
            textBox2.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Times New Roman", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.ButtonHighlight;
            label2.Location = new Point(18, 130);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(101, 19);
            label2.TabIndex = 2;
            label2.Text = "PASSWORD";
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.White;
            textBox1.Location = new Point(18, 77);
            textBox1.Margin = new Padding(2);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(257, 27);
            textBox1.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(18, 46);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(103, 19);
            label1.TabIndex = 0;
            label1.Text = "USERNAME";
            // 
            // DangNhap
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.nền_đăng_nhập;
            ClientSize = new Size(717, 534);
            Controls.Add(panel1);
            ForeColor = SystemColors.ControlText;
            Margin = new Padding(2);
            Name = "DangNhap";
            Text = "DangNhap";
            Load += DangNhap_Load;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Label label1;
        private TextBox textBox1;
        private Label label2;
        private PictureBox pictureBox1;
        private TextBox textBox2;
        private Button button1;
        private Label label3;
        private Button button2;
        private LinkLabel linkLabel1;
    }
}