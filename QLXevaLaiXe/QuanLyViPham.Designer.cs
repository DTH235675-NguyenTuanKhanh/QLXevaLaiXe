namespace QLXevaLaiXe
{
    partial class QuanLyViPham
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
            this.dgrViPham = new System.Windows.Forms.DataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.dtpNgayXayRa = new System.Windows.Forms.DateTimePicker();
            this.cboMaXe = new System.Windows.Forms.ComboBox();
            this.cboMaTaiXe = new System.Windows.Forms.ComboBox();
            this.txtMucPhat = new System.Windows.Forms.TextBox();
            this.txtMaViPham = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cboLoaiViPham = new System.Windows.Forms.ComboBox();
            this.cboTrangThai = new System.Windows.Forms.ComboBox();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgrViPham)).BeginInit();
            this.SuspendLayout();
            // 
            // dgrViPham
            // 
            this.dgrViPham.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrViPham.Location = new System.Drawing.Point(26, 225);
            this.dgrViPham.Name = "dgrViPham";
            this.dgrViPham.RowHeadersWidth = 51;
            this.dgrViPham.RowTemplate.Height = 24;
            this.dgrViPham.Size = new System.Drawing.Size(838, 209);
            this.dgrViPham.TabIndex = 173;
            this.dgrViPham.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgrDanhSachViPham_CellContentClick);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(23, 206);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(116, 16);
            this.label8.TabIndex = 172;
            this.label8.Text = "Hiển thị danh sách";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(356, 17);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(193, 25);
            this.label9.TabIndex = 163;
            this.label9.Text = "VI PHẠM SỰ CỐ";
            // 
            // dtpNgayXayRa
            // 
            this.dtpNgayXayRa.Location = new System.Drawing.Point(408, 58);
            this.dtpNgayXayRa.Name = "dtpNgayXayRa";
            this.dtpNgayXayRa.Size = new System.Drawing.Size(235, 22);
            this.dtpNgayXayRa.TabIndex = 161;
            // 
            // cboMaXe
            // 
            this.cboMaXe.FormattingEnabled = true;
            this.cboMaXe.Location = new System.Drawing.Point(112, 124);
            this.cboMaXe.Name = "cboMaXe";
            this.cboMaXe.Size = new System.Drawing.Size(177, 24);
            this.cboMaXe.TabIndex = 160;
            // 
            // cboMaTaiXe
            // 
            this.cboMaTaiXe.FormattingEnabled = true;
            this.cboMaTaiXe.Location = new System.Drawing.Point(112, 94);
            this.cboMaTaiXe.Name = "cboMaTaiXe";
            this.cboMaTaiXe.Size = new System.Drawing.Size(177, 24);
            this.cboMaTaiXe.TabIndex = 159;
            // 
            // txtMucPhat
            // 
            this.txtMucPhat.Location = new System.Drawing.Point(408, 126);
            this.txtMucPhat.Name = "txtMucPhat";
            this.txtMucPhat.Size = new System.Drawing.Size(235, 22);
            this.txtMucPhat.TabIndex = 157;
            // 
            // txtMaViPham
            // 
            this.txtMaViPham.Location = new System.Drawing.Point(112, 61);
            this.txtMaViPham.Name = "txtMaViPham";
            this.txtMaViPham.Size = new System.Drawing.Size(177, 22);
            this.txtMaViPham.TabIndex = 156;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(305, 98);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(99, 16);
            this.label7.TabIndex = 155;
            this.label7.Text = "Trạng thái xử lý:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 162);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 16);
            this.label6.TabIndex = 154;
            this.label6.Text = "Loại vi phạm:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(305, 132);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 16);
            this.label5.TabIndex = 153;
            this.label5.Text = "Mức phạt:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(305, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 16);
            this.label4.TabIndex = 152;
            this.label4.Text = "Ngày xảy ra:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 16);
            this.label3.TabIndex = 151;
            this.label3.Text = "Mã xe:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 16);
            this.label2.TabIndex = 150;
            this.label2.Text = "Mã tài xế:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 16);
            this.label1.TabIndex = 149;
            this.label1.Text = "Mã vi phạm:";
            // 
            // cboLoaiViPham
            // 
            this.cboLoaiViPham.FormattingEnabled = true;
            this.cboLoaiViPham.Location = new System.Drawing.Point(112, 154);
            this.cboLoaiViPham.Name = "cboLoaiViPham";
            this.cboLoaiViPham.Size = new System.Drawing.Size(177, 24);
            this.cboLoaiViPham.TabIndex = 174;
            // 
            // cboTrangThai
            // 
            this.cboTrangThai.FormattingEnabled = true;
            this.cboTrangThai.Location = new System.Drawing.Point(408, 90);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new System.Drawing.Size(235, 24);
            this.cboTrangThai.TabIndex = 175;
            // 
            // btnLuu
            // 
            this.btnLuu.Location = new System.Drawing.Point(777, 91);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(87, 30);
            this.btnLuu.TabIndex = 183;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = true;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(777, 54);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(87, 33);
            this.btnReset.TabIndex = 182;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(777, 130);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(87, 29);
            this.btnThoat.TabIndex = 181;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.Location = new System.Drawing.Point(777, 170);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(87, 32);
            this.btnHuy.TabIndex = 180;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Location = new System.Drawing.Point(677, 169);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(87, 33);
            this.btnTimKiem.TabIndex = 179;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = true;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(677, 128);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(87, 32);
            this.btnXoa.TabIndex = 178;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(677, 92);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(87, 29);
            this.btnSua.TabIndex = 177;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(677, 56);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(87, 31);
            this.btnThem.TabIndex = 176;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // QuanLyViPham
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(887, 450);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnTimKiem);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.cboTrangThai);
            this.Controls.Add(this.cboLoaiViPham);
            this.Controls.Add(this.dgrViPham);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.dtpNgayXayRa);
            this.Controls.Add(this.cboMaXe);
            this.Controls.Add(this.cboMaTaiXe);
            this.Controls.Add(this.txtMucPhat);
            this.Controls.Add(this.txtMaViPham);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "QuanLyViPham";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QuanLyViPham";
            this.Load += new System.EventHandler(this.QuanLyViPham_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgrViPham)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.DataGridView dgrViPham;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.DateTimePicker dtpNgayXayRa;
    private System.Windows.Forms.ComboBox cboMaXe;
    private System.Windows.Forms.ComboBox cboMaTaiXe;
    private System.Windows.Forms.TextBox txtMucPhat;
    private System.Windows.Forms.TextBox txtMaViPham;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox cboLoaiViPham;
    private System.Windows.Forms.ComboBox cboTrangThai;
    private System.Windows.Forms.Button btnLuu;
    private System.Windows.Forms.Button btnReset;
    private System.Windows.Forms.Button btnThoat;
    private System.Windows.Forms.Button btnHuy;
    private System.Windows.Forms.Button btnTimKiem;
    private System.Windows.Forms.Button btnXoa;
    private System.Windows.Forms.Button btnSua;
    private System.Windows.Forms.Button btnThem;
}
}

