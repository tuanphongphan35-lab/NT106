namespace Login
{
    partial class ThongTinNguoiDung
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
            circularPictureBox1 = new CircularPictureBox();
            roundPanel1 = new RoundPanel();
            textBox3 = new TextBox();
            textBox2 = new TextBox();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            textBox4 = new TextBox();
            textBox1 = new TextBox();
            roundPanel2 = new RoundPanel();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)circularPictureBox1).BeginInit();
            roundPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // circularPictureBox1
            // 
            circularPictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            circularPictureBox1.Location = new Point(192, 33);
            circularPictureBox1.Name = "circularPictureBox1";
            circularPictureBox1.Size = new Size(133, 129);
            circularPictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            circularPictureBox1.TabIndex = 0;
            circularPictureBox1.TabStop = false;
            circularPictureBox1.Click += circularPictureBox1_Click;
            // 
            // roundPanel1
            // 
            roundPanel1.BorderColor = Color.Gray;
            roundPanel1.BorderRadius = 10;
            roundPanel1.BorderThickness = 1F;
            roundPanel1.Controls.Add(textBox3);
            roundPanel1.Controls.Add(textBox2);
            roundPanel1.Controls.Add(label4);
            roundPanel1.Controls.Add(label3);
            roundPanel1.Controls.Add(label2);
            roundPanel1.Controls.Add(label1);
            roundPanel1.Controls.Add(textBox4);
            roundPanel1.Controls.Add(textBox1);
            roundPanel1.Location = new Point(37, 203);
            roundPanel1.Name = "roundPanel1";
            roundPanel1.Size = new Size(443, 349);
            roundPanel1.TabIndex = 1;
            roundPanel1.Paint += roundPanel1_Paint;
            // 
            // textBox3
            // 
            textBox3.BackColor = SystemColors.GradientActiveCaption;
            textBox3.Location = new Point(170, 284);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(258, 31);
            textBox3.TabIndex = 11;
            // 
            // textBox2
            // 
            textBox2.BackColor = SystemColors.GradientActiveCaption;
            textBox2.Location = new Point(170, 202);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(258, 31);
            textBox2.TabIndex = 10;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label4.Location = new Point(16, 287);
            label4.Name = "label4";
            label4.Size = new Size(92, 25);
            label4.TabIndex = 7;
            label4.Text = "Giới Tính";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label3.Location = new Point(16, 205);
            label3.Name = "label3";
            label3.Size = new Size(101, 25);
            label3.TabIndex = 6;
            label3.Text = "Ngày Sinh";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label2.Location = new Point(16, 124);
            label2.Name = "label2";
            label2.Size = new Size(61, 25);
            label2.TabIndex = 5;
            label2.Text = "Email";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.Location = new Point(16, 44);
            label1.Name = "label1";
            label1.Size = new Size(153, 25);
            label1.TabIndex = 4;
            label1.Text = "Tên Người Dùng";
            // 
            // textBox4
            // 
            textBox4.BackColor = SystemColors.GradientActiveCaption;
            textBox4.Location = new Point(170, 121);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(258, 31);
            textBox4.TabIndex = 3;
            // 
            // textBox1
            // 
            textBox1.BackColor = SystemColors.GradientActiveCaption;
            textBox1.Location = new Point(170, 41);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(258, 31);
            textBox1.TabIndex = 0;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // roundPanel2
            // 
            roundPanel2.BackgroundImage = Properties.Resources.logo1;
            roundPanel2.BackgroundImageLayout = ImageLayout.Stretch;
            roundPanel2.BorderColor = Color.Gray;
            roundPanel2.BorderRadius = 10;
            roundPanel2.BorderThickness = 1F;
            roundPanel2.Location = new Point(12, 12);
            roundPanel2.Name = "roundPanel2";
            roundPanel2.Size = new Size(101, 97);
            roundPanel2.TabIndex = 2;
            // 
            // button1
            // 
            button1.BackColor = SystemColors.MenuHighlight;
            button1.Font = new Font("Candara", 10F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            button1.ForeColor = SystemColors.Desktop;
            button1.Location = new Point(131, 575);
            button1.Name = "button1";
            button1.Size = new Size(254, 45);
            button1.TabIndex = 3;
            button1.Text = "Chỉnh Sửa Thông Tin";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // ThongTinNguoiDung
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientActiveCaption;
            ClientSize = new Size(509, 646);
            Controls.Add(button1);
            Controls.Add(roundPanel2);
            Controls.Add(roundPanel1);
            Controls.Add(circularPictureBox1);
            Name = "ThongTinNguoiDung";
            Text = "ThongTinNguoiDung";
            TransparencyKey = SystemColors.ButtonHighlight;
            Load += ThongTinNguoiDung_Load;
            MouseDown += ThongTinNguoiDungForm_MouseDown;
            MouseMove += ThongTinNguoiDungForm_MouseMove;
            MouseUp += ThongTinNguoiDungForm_MouseUp;
            ((System.ComponentModel.ISupportInitialize)circularPictureBox1).EndInit();
            roundPanel1.ResumeLayout(false);
            roundPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private CircularPictureBox circularPictureBox1;
        private RoundPanel roundPanel1;
        private RoundPanel roundPanel2;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private TextBox textBox4;
        private TextBox textBox1;
        private Button button1;
        private TextBox textBox3;
        private TextBox textBox2;
    }
}