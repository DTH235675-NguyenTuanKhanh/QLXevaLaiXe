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
    public partial class MenuChinh : Form
    {
        public MenuChinh()
        {
            InitializeComponent();
        }

        private void btnx_Click(object sender, EventArgs e)
        {
            Xe f1 = new Xe();
            f1.ShowDialog();
        }

        private void btntx_Click(object sender, EventArgs e)
        {
            Taixe f2 = new Taixe();
            f2.ShowDialog();
        }

        private void btnpc_Click(object sender, EventArgs e)
        {
            Phancong f3 = new Phancong();
            f3.ShowDialog();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MenuChinh_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            QuanLyViPham f1 = new QuanLyViPham(); 
            f1.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            QuanLyBaoDuong f2 = new QuanLyBaoDuong();
            f2.ShowDialog();
        }

        private void Dangkiem_Click(object sender, EventArgs e)
        {
            LichDangKiemcs f3 = new LichDangKiemcs();
            f3.ShowDialog();
        }
    }
}
