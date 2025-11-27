namespace Quản_lý_xe
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            btnQuanLyXe = new Button();
            btnQuanLyLaiXe = new Button();
            btnQuanLyChuyenDi = new Button();
            btnThoat = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(250, 38);
            label1.Name = "label1";
            label1.Size = new Size(297, 38);
            label1.TabIndex = 0;
            label1.Text = "MENU QUẢN LÝ ";
            // 
            // btnQuanLyXe
            // 
            btnQuanLyXe.Location = new Point(274, 110);
            btnQuanLyXe.Name = "btnQuanLyXe";
            btnQuanLyXe.Size = new Size(256, 62);
            btnQuanLyXe.TabIndex = 1;
            btnQuanLyXe.Text = "Quản lý xe";
            btnQuanLyXe.UseVisualStyleBackColor = true;
            // 
            // btnQuanLyLaiXe
            // 
            btnQuanLyLaiXe.Location = new Point(274, 197);
            btnQuanLyLaiXe.Name = "btnQuanLyLaiXe";
            btnQuanLyLaiXe.Size = new Size(256, 62);
            btnQuanLyLaiXe.TabIndex = 2;
            btnQuanLyLaiXe.Text = "Quản lý lái xe";
            btnQuanLyLaiXe.UseVisualStyleBackColor = true;
            // 
            // btnQuanLyChuyenDi
            // 
            btnQuanLyChuyenDi.Location = new Point(274, 279);
            btnQuanLyChuyenDi.Name = "btnQuanLyChuyenDi";
            btnQuanLyChuyenDi.Size = new Size(256, 62);
            btnQuanLyChuyenDi.TabIndex = 3;
            btnQuanLyChuyenDi.Text = "Quản lý chuyến đi";
            btnQuanLyChuyenDi.UseVisualStyleBackColor = true;
            // 
            // btnThoat
            // 
            btnThoat.Location = new Point(274, 382);
            btnThoat.Name = "btnThoat";
            btnThoat.Size = new Size(256, 42);
            btnThoat.TabIndex = 4;
            btnThoat.Text = "Thoát";
            btnThoat.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(870, 450);
            Controls.Add(btnThoat);
            Controls.Add(btnQuanLyChuyenDi);
            Controls.Add(btnQuanLyLaiXe);
            Controls.Add(btnQuanLyXe);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Main";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button btnQuanLyXe;
        private Button btnQuanLyLaiXe;
        private Button btnQuanLyChuyenDi;
        private Button btnThoat;
    }
}
