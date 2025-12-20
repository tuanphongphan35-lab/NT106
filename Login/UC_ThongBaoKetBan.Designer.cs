
namespace Login
{
    partial class UC_ThongBaoKetBan
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            roundButton1 = new RoundButton();
            roundButton2 = new RoundButton();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(38, 26);
            label1.Name = "label1";
            label1.Size = new Size(65, 28);
            label1.TabIndex = 0;
            label1.Text = "label1";
            // 
            // roundButton1
            // 
            roundButton1.BackColor = Color.CornflowerBlue;
            roundButton1.BorderColor = Color.Transparent;
            roundButton1.BorderRadius = 20;
            roundButton1.BorderThickness = 0F;
            roundButton1.FlatAppearance.BorderSize = 0;
            roundButton1.FlatStyle = FlatStyle.Flat;
            roundButton1.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            roundButton1.ForeColor = Color.White;
            roundButton1.Location = new Point(482, 10);
            roundButton1.Name = "roundButton1";
            roundButton1.Size = new Size(135, 60);
            roundButton1.TabIndex = 1;
            roundButton1.Text = "Đồng ý";
            roundButton1.UseVisualStyleBackColor = false;
            roundButton1.Click += roundButton1_Click;
            // 
            // roundButton2
            // 
            roundButton2.BackColor = SystemColors.ControlDark;
            roundButton2.BorderColor = Color.Transparent;
            roundButton2.BorderRadius = 20;
            roundButton2.BorderThickness = 0F;
            roundButton2.FlatAppearance.BorderSize = 0;
            roundButton2.FlatStyle = FlatStyle.Flat;
            roundButton2.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            roundButton2.ForeColor = Color.White;
            roundButton2.Location = new Point(642, 10);
            roundButton2.Name = "roundButton2";
            roundButton2.Size = new Size(133, 60);
            roundButton2.TabIndex = 2;
            roundButton2.Text = "Từ Chối";
            roundButton2.UseVisualStyleBackColor = false;
            roundButton2.Click += roundButton2_Click;
            // 
            // UC_ThongBaoKetBan
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDarkDark;
            Controls.Add(roundButton2);
            Controls.Add(roundButton1);
            Controls.Add(label1);
            Name = "UC_ThongBaoKetBan";
            Size = new Size(821, 81);
            Load += UC_ThongBaoKetBan_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        private void UC_ThongBaoKetBan_Load(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private Label label1;
        private RoundButton roundButton1;
        private RoundButton roundButton2;
    }
}
