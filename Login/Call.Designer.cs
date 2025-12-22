namespace Login
{
    partial class Call
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
            button1 = new Button();
            button2 = new Button();
            circularPictureBox1 = new CircularPictureBox();
            circularPictureBox2 = new CircularPictureBox();
            ((System.ComponentModel.ISupportInitialize)circularPictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)circularPictureBox2).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(90, 558);
            button1.Name = "button1";
            button1.Size = new Size(112, 34);
            button1.TabIndex = 0;
            button1.Text = "mic ";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(267, 558);
            button2.Name = "button2";
            button2.Size = new Size(112, 34);
            button2.TabIndex = 1;
            button2.Text = "tắt";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // circularPictureBox1
            // 
            circularPictureBox1.Location = new Point(30, 12);
            circularPictureBox1.Name = "circularPictureBox1";
            circularPictureBox1.Size = new Size(287, 259);
            circularPictureBox1.TabIndex = 2;
            circularPictureBox1.TabStop = false;
            // 
            // circularPictureBox2
            // 
            circularPictureBox2.Location = new Point(245, 277);
            circularPictureBox2.Name = "circularPictureBox2";
            circularPictureBox2.Size = new Size(253, 239);
            circularPictureBox2.TabIndex = 3;
            circularPictureBox2.TabStop = false;
            // 
            // Call
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(510, 674);
            Controls.Add(circularPictureBox2);
            Controls.Add(circularPictureBox1);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "Call";
            Text = "Call";
            Load += Call_Load;
            ((System.ComponentModel.ISupportInitialize)circularPictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)circularPictureBox2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Button button2;
        private CircularPictureBox circularPictureBox1;
        private CircularPictureBox circularPictureBox2;
    }
}