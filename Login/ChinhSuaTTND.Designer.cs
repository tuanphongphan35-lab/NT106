
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
            circularPictureBox1 = new CircularPictureBox();
            textBox1 = new TextBox();
            dateTimePicker1 = new DateTimePicker();
            comboBox1 = new ComboBox();
            button2 = new RoundButton();
            panel1 = new Panel();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            button1 = new RoundButton();
            ((System.ComponentModel.ISupportInitialize)circularPictureBox1).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // circularPictureBox1
            // 
            circularPictureBox1.Location = new Point(173, 36);
            circularPictureBox1.Name = "circularPictureBox1";
            circularPictureBox1.Size = new Size(99, 94);
            circularPictureBox1.TabIndex = 0;
            circularPictureBox1.TabStop = false;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(131, 89);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(241, 27);
            textBox1.TabIndex = 1;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(131, 170);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(241, 27);
            dateTimePicker1.TabIndex = 2;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(131, 241);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(241, 28);
            comboBox1.TabIndex = 3;
            // 
            // button2
            // 
            button2.BackColor = Color.MediumSlateBlue;
            button2.BackgroundImage = Properties.Resources.Colored_Sky;
            button2.BorderColor = Color.Transparent;
            button2.BorderRadius = 20;
            button2.BorderThickness = 0F;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button2.ForeColor = Color.White;
            button2.Location = new Point(173, 136);
            button2.Name = "button2";
            button2.Size = new Size(99, 31);
            button2.TabIndex = 4;
            button2.Text = "Thêm ảnh";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(40, 43, 51);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(textBox1);
            panel1.Controls.Add(comboBox1);
            panel1.Controls.Add(dateTimePicker1);
            panel1.Location = new Point(29, 190);
            panel1.Name = "panel1";
            panel1.Size = new Size(393, 354);
            panel1.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.White;
            label4.Location = new Point(83, 17);
            label4.Name = "label4";
            label4.Size = new Size(227, 31);
            label4.TabIndex = 7;
            label4.Text = "Chỉnh sửa thông tin";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.White;
            label3.Location = new Point(36, 249);
            label3.Name = "label3";
            label3.Size = new Size(33, 20);
            label3.TabIndex = 6;
            label3.Text = "Sex";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.White;
            label2.Location = new Point(20, 175);
            label2.Name = "label2";
            label2.Size = new Size(100, 20);
            label2.TabIndex = 5;
            label2.Text = "Date of Birth";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(20, 96);
            label1.Name = "label1";
            label1.Size = new Size(80, 20);
            label1.TabIndex = 4;
            label1.Text = "Username";
            // 
            // button1
            // 
            button1.BackColor = Color.MediumSlateBlue;
            button1.BackgroundImage = Properties.Resources.Colored_Sky;
            button1.BorderColor = Color.Transparent;
            button1.BorderRadius = 20;
            button1.BorderThickness = 0F;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.White;
            button1.Location = new Point(141, 522);
            button1.Name = "button1";
            button1.Size = new Size(188, 50);
            button1.TabIndex = 6;
            button1.Text = "Xác nhận";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click_1;
            // 
            // ChinhSuaTTND
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(62, 66, 75);
            ClientSize = new Size(449, 615);
            Controls.Add(button1);
            Controls.Add(button2);
            Controls.Add(circularPictureBox1);
            Controls.Add(panel1);
            Margin = new Padding(2);
            Name = "ChinhSuaTTND";
            Text = "ChinhSuaTTND";
            ((System.ComponentModel.ISupportInitialize)circularPictureBox1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private CircularPictureBox circularPictureBox1;
        private TextBox textBox1;
        private DateTimePicker dateTimePicker1;
        private ComboBox comboBox1;
        private RoundButton button2;
        private Panel panel1;
        private Label label3;
        private Label label2;
        private Label label1;
        private Label label4;
        private RoundButton button1;
    }
}