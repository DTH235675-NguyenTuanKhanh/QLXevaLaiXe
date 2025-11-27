using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLXevaLaiXe
{
    public partial class DangNhap : Form
    {
        public DangNhap()
        {
            InitializeComponent();
            txtMatKhau.PasswordChar = '*';
            this.AcceptButton = btnDangNhap;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void DangNhap_Load(object sender, EventArgs e)
        {

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string tenDangNhap = txtTaiKhoan.Text;
            string matKhau = txtMatKhau.Text;

            // Gọi hàm kiểm tra (sẽ viết ở dưới)
            if (KiemTraDangNhap(tenDangNhap, matKhau))
            {
                // Nếu đăng nhập thành công:

                // 1. Tạo một đối tượng Form Main Menu
                MenuChinh frmMain = new MenuChinh();

                // 2. Thêm sự kiện: Khi Form Main bị đóng, Form Đăng nhập (this) cũng đóng.
                // Điều này đảm bảo ứng dụng thoát hoàn toàn.
                frmMain.FormClosed += (s, args) => this.Close();

                // 3. Hiển thị Form Main
                frmMain.Show();

                // 4. Ẩn Form Đăng nhập hiện tại
                this.Hide();
            }
            else
            {
                // Nếu đăng nhập thất bại:
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Lỗi Đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        } // <-- DẤU NGOẶC ĐÓNG CỦA btnDangNhap_Click ĐƯỢC DỜI LÊN ĐÂY

        // HÀM NÀY PHẢI NẰM NGOÀI HÀM btnDangNhap_Click, NHƯNG VẪN TRONG CLASS DangNhap
        private bool KiemTraDangNhap(string user, string pass)
        {
           

            if ((user == "Bùi Thành Nhơn" && pass == "12345") || (user == "NTK" && pass == "12345"))
            {
                return true;
            }
            return false;
        }
    }
}


