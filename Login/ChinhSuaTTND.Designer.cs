
namespace Login
{
    partial class ChinhSuaTTND
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
            label1 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            roundPanel1 = new RoundPanel();
            button2 = new Button();
            dateTimePicker1 = new DateTimePicker();
            comboBox1 = new ComboBox();
            textBox1 = new TextBox();
            circularPictureBox1 = new CircularPictureBox();
            label7 = new Label();
            button1 = new Button();
            roundPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)circularPictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Source Han Sans CN Bold", 8F, FontStyle.Bold);
            label1.ForeColor = Color.RoyalBlue;
            label1.Location = new Point(33, 153);
            label1.Name = "label1";
            label1.Size = new Size(134, 24);
            label1.TabIndex = 0;
            label1.Text = "Tên Người Dùng";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Source Han Sans CN Bold", 8F, FontStyle.Bold);
            label3.ForeColor = Color.RoyalBlue;
            label3.Location = new Point(33, 235);
            label3.Name = "label3";
            label3.Size = new Size(93, 24);
            label3.TabIndex = 2;
            label3.Text = "Ngày Sinh ";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Source Han Sans CN Bold", 8F, FontStyle.Bold);
            label4.ForeColor = Color.RoyalBlue;
            label4.Location = new Point(33, 331);
            label4.Name = "label4";
            label4.Size = new Size(80, 24);
            label4.TabIndex = 3;
            label4.Text = "Giới Tính";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Black", 10F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label5.ForeColor = SystemColors.ControlText;
            label5.Location = new Point(170, 50);
            label5.Name = "label5";
            label5.Size = new Size(399, 28);
            label5.TabIndex = 4;
            label5.Text = "CHỈNH SỬA THÔNG TIN NGƯỜI DÙNG";
            label5.Click += label5_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = SystemColors.GradientInactiveCaption;
            label6.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label6.ForeColor = SystemColors.HotTrack;
            label6.Location = new Point(56, 97);
            label6.Name = "label6";
            label6.Size = new Size(213, 25);
            label6.TabIndex = 5;
            label6.Text = "Nhập Các Thông Tin Sau:";
            // 
            // roundPanel1
            // 
            roundPanel1.BorderColor = Color.Gray;
            roundPanel1.BorderRadius = 10;
            roundPanel1.BorderThickness = 1F;
            roundPanel1.Controls.Add(button2);
            roundPanel1.Controls.Add(dateTimePicker1);
            roundPanel1.Controls.Add(comboBox1);
            roundPanel1.Controls.Add(textBox1);
            roundPanel1.Controls.Add(circularPictureBox1);
            roundPanel1.Controls.Add(label7);
            roundPanel1.Controls.Add(label1);
            roundPanel1.Controls.Add(label3);
            roundPanel1.Controls.Add(label4);
            roundPanel1.Location = new Point(46, 125);
            roundPanel1.Name = "roundPanel1";
            roundPanel1.Size = new Size(650, 407);
            roundPanel1.TabIndex = 6;
            roundPanel1.Paint += roundPanel1_Paint;
            // 
            // button2
            // 
            button2.BackColor = Color.PaleGreen;
            button2.Font = new Font("Bell MT", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button2.Location = new Point(263, 54);
            button2.Name = "button2";
            button2.Size = new Size(106, 30);
            button2.TabIndex = 11;
            button2.Text = "Thêm Ảnh";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";
            dateTimePicker1.Location = new Point(203, 229);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(259, 31);
            dateTimePicker1.TabIndex = 10;
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Nam", "", "Nữ", "Không Muốn Tiết Lộ" });
            comboBox1.Location = new Point(203, 322);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(163, 33);
            comboBox1.TabIndex = 9;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(203, 149);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(354, 31);
            textBox1.TabIndex = 6;
            // 
            // circularPictureBox1
            // 
            circularPictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            circularPictureBox1.Location = new Point(413, 14);
            circularPictureBox1.Name = "circularPictureBox1";
            circularPictureBox1.Size = new Size(122, 120);
            circularPictureBox1.TabIndex = 5;
            circularPictureBox1.TabStop = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Source Han Sans CN Bold", 8F, FontStyle.Bold);
            label7.ForeColor = Color.RoyalBlue;
            label7.Location = new Point(33, 55);
            label7.Name = "label7";
            label7.Size = new Size(186, 24);
            label7.TabIndex = 4;
            label7.Text = "Lựa Chọn Ảnh Đại Diện";
            // 
            // button1
            // 
            button1.BackColor = SystemColors.Highlight;
            button1.Font = new Font("Segoe UI", 10F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.FromArgb(128, 255, 255);
            button1.Location = new Point(249, 538);
            button1.Name = "button1";
            button1.Size = new Size(274, 42);
            button1.TabIndex = 7;
            button1.Text = "Xác Nhận Chỉnh Sửa";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // ChinhSuaTTND
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientInactiveCaption;
            ClientSize = new Size(750, 592);
            Controls.Add(button1);
            Controls.Add(roundPanel1);
            Controls.Add(label6);
            Controls.Add(label5);
            Name = "ChinhSuaTTND";
            Text = "ChinhSuaTTND";
            roundPanel1.ResumeLayout(false);
            roundPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)circularPictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private RoundPanel roundPanel1;
        private Label label7;
        private ComboBox comboBox1;
        private TextBox textBox1;
        private CircularPictureBox circularPictureBox1;
        private Button button1;
        private DateTimePicker dateTimePicker1;
        private Button button2;
    }
}