using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLXevaLaiXe
{
    public partial class QuanLyBaoDuong : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\TK\DA.NET\QLXevaLaiXe\QLXevaLaiXe\QLXVLX.mdf;Integrated Security=True");
        SqlDataAdapter da;
        DataTable dt;

        // Giả định tên DataGridView là dgrdsbaoduong
        void Load_data()
        {
            // Lấy dữ liệu từ bảng Baoduong
            da = new SqlDataAdapter("select * from Baoduong", conn);
            dt = new DataTable();
            da.Fill(dt);
            dgrbd.DataSource = dt;
        }

        private string currentAction = "DEFAULT";
        private void SetInputControlsEnabled(bool isEnabled)
        {
            txtMaBaoTri.Enabled = isEnabled; // Ma bao tri (mabt)
            cboMaXe.Enabled = isEnabled; // Ma xe (maxe)
            cboLoaiDichVu.Enabled = isEnabled; // Dich vu (dichvu)
            dtpNgayBatDau.Enabled = isEnabled; // Ngay bat dau (ngaybd)
            dtpNgayKetThuc.Enabled = isEnabled; // Ngay ket thuc (ngaykt)
            txtChiPhi.Enabled = isEnabled; // Chi phi (chiphi)
            txtGhiChu.Enabled = isEnabled; // Ghi chu (ghichu)
        }
        private void SetButtonStates(bool isEditing)
        {
            // isEditing = TRUE khi đang ở trạng thái Thêm/Sửa (Lưu/Hủy được bật)
            // isEditing = FALSE khi ở trạng thái Mặc định (Thêm/Sửa/Xóa được bật)

            btnThem.Enabled = !isEditing;
            btnSua.Enabled = !isEditing;
            btnXoa.Enabled = !isEditing;
            btnTimKiem.Enabled = !isEditing;

            btnLuu.Enabled = isEditing;
            btnHuy.Enabled = isEditing;
            btnReset.Enabled = !isEditing;
        }

        private void ClearInputFields()
        {
            txtMaBaoTri.Clear();
            cboLoaiDichVu.SelectedIndex = -1;
            cboMaXe.SelectedIndex = -1;
            dtpNgayBatDau.Value = DateTime.Now;
            dtpNgayKetThuc.Value = DateTime.Now;
            txtChiPhi.Clear();
            txtGhiChu.Clear();
        }
        private void LoadMaXe()
        {
            // Giả định tên ComboBox mới là cboMaXe
            cboMaXe.Items.Clear();

            string sql = "SELECT maxe FROM Xe";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = null;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    // Thêm từng Mã xe vào ComboBox
                    cboMaXe.Items.Add(reader["maxe"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách Mã xe: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
        public QuanLyBaoDuong()
        {
            InitializeComponent();
            SetInputControlsEnabled(false);
            SetButtonStates(false);
            txtMaBaoTri.Enabled = true;
        }

        private void dgvBaoDuong_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            if (i >= 0 && i < dgrbd.Rows.Count - 1) // Trừ hàng trống cuối cùng
            {
                // Giả định tên cột trong DataGridView khớp với tên cột trong CSDL
                txtMaBaoTri.Text = dgrbd.Rows[i].Cells["mabt"].Value.ToString();
                // Cập nhật: Gán giá trị Mã xe từ DB vào ComboBox
                cboMaXe.Text = dgrbd.Rows[i].Cells["maxe"].Value.ToString();
                cboLoaiDichVu.Text = dgrbd.Rows[i].Cells["loaidichvu"].Value.ToString();

                // Chuyển đổi ngày tháng
                if (dgrbd.Rows[i].Cells["ngaybatdau"].Value != DBNull.Value)
                    dtpNgayBatDau.Value = Convert.ToDateTime(dgrbd.Rows[i].Cells["ngaybatdau"].Value);
                if (dgrbd.Rows[i].Cells["ngayketthuc"].Value != DBNull.Value)
                    dtpNgayKetThuc.Value = Convert.ToDateTime(dgrbd.Rows[i].Cells["ngayketthuc"].Value);

                txtChiPhi.Text = dgrbd.Rows[i].Cells["chiphi"].Value.ToString();
                txtGhiChu.Text = dgrbd.Rows[i].Cells["ghichu"].Value.ToString();
            }
            // Khóa controls sau khi click để người dùng phải bấm Sửa nếu muốn thay đổi
            SetInputControlsEnabled(false);
            SetButtonStates(false);
        }

        private void QuanLyBaoDuong_Load(object sender, EventArgs e)
        {
            Load_data();
            LoadMaXe();

            // Khởi tạo ComboBox LoaiDichVu (ví dụ)
            cboLoaiDichVu.Items.Clear();
            cboLoaiDichVu.Items.Add("Bảo dưỡng định kỳ");
            cboLoaiDichVu.Items.Add("Sửa chữa động cơ");
            cboLoaiDichVu.Items.Add("Thay lốp");
            cboLoaiDichVu.Items.Add("Kiểm tra phanh");
            cboLoaiDichVu.Items.Add("Bảo hiểm");
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            currentAction = "THEM";
            ClearInputFields();
            SetInputControlsEnabled(true);
            SetButtonStates(true);
            txtMaBaoTri.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgrbd.SelectedRows.Count == 0 && txtMaBaoTri.Text == "")
            {
                MessageBox.Show("Vui lòng chọn một bản bảo dưỡng từ danh sách hoặc nhập Mã bảo trì để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            currentAction = "SUA";
            SetInputControlsEnabled(true);
            SetButtonStates(true);
            // Mã bảo trì thường là khóa chính, không cho sửa
            txtMaBaoTri.Enabled = false;
            
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaBaoTri.Text))
            {
                MessageBox.Show("Vui lòng chọn một bản ghi bảo dưỡng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa bản ghi bảo dưỡng " + txtMaBaoTri.Text + " không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            string sql = "DELETE FROM BaoDuong WHERE mabt=@mabt";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@mabt", txtMaBaoTri.Text);

            try
            {
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Đã xóa bản ghi bảo dưỡng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy Mã bảo trì để xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa bản ghi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
                Load_data();
                ClearInputFields();
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaBaoTri.Text) && string.IsNullOrEmpty(cboMaXe.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã bảo trì để tìm kiếm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                string sql = "SELECT * FROM BaoDuong WHERE mabt LIKE @mabt";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@mabt", "%" + txtMaBaoTri.Text + "%");

                SqlDataAdapter daTK = new SqlDataAdapter(cmd);
                DataTable dtTK = new DataTable();
                daTK.Fill(dtTK);
                dgrbd.DataSource = dtTK;
                if (dtTK.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy bản ghi bảo dưỡng nào với Mã bảo trì đã nhập.", "Kết quả tìm kiếm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                currentAction = "DEFAULT";
                SetInputControlsEnabled(false);
                SetButtonStates(false);
                txtMaBaoTri.Enabled = true;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra ràng buộc
            if (string.IsNullOrEmpty(txtMaBaoTri.Text) || string.IsNullOrEmpty(cboMaXe.Text) || string.IsNullOrEmpty(cboLoaiDichVu.Text)) // Đã đổi txtMaXeBD thành cboMaXe
            {
                MessageBox.Show("Mã bảo trì, Mã xe và Loại dịch vụ không được để trống.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Kiểm tra ngày tháng
            if (dtpNgayBatDau.Value > dtpNgayKetThuc.Value)
            {
                MessageBox.Show("Ngày kết thúc không được nhỏ hơn Ngày bắt đầu.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Kiểm tra Chi phí phải là số
            decimal chiPhi;
            if (!decimal.TryParse(txtChiPhi.Text, out chiPhi))
            {
                MessageBox.Show("Chi phí phải là số.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2. Chuẩn bị câu lệnh SQL
            string sql = "";
            if (currentAction == "THEM")
            {
                sql = "INSERT INTO BaoDuong (mabt, maxe, loaidichvu, ngaybatdau, ngayketthuc, chiphi, ghichu) "
                   + "VALUES (@mabt, @maxe, @loaidichvu, @ngaybatdau, @ngayketthuc, @chiphi, @ghichu)";
            }
            else if (currentAction == "SUA")
            {
                sql = "UPDATE BaoDuong SET maxe = @maxe, loaidichvu = @loaidichvu, ngaybatdau = @ngaybatdau, " +
                      "ngayketthuc = @ngayketthuc, chiphi = @chiphi, ghichu = @ghichu WHERE mabt = @mabt";
            }
            else
            {
                MessageBox.Show("Không thể thực hiện Lưu. Vui lòng nhấn Thêm hoặc Sửa trước.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlCommand cmd = new SqlCommand(sql, conn);
            // 3. Gán tham số
            cmd.Parameters.AddWithValue("@mabt", txtMaBaoTri.Text);
            cmd.Parameters.AddWithValue("@maxe", cboMaXe.Text);
            cmd.Parameters.AddWithValue("@loaidichvu", cboLoaiDichVu.Text);
            cmd.Parameters.AddWithValue("@ngaybatdau", dtpNgayBatDau.Value.Date); // Lấy phần ngày
            cmd.Parameters.AddWithValue("@ngayketthuc", dtpNgayKetThuc.Value.Date); // Lấy phần ngày
            cmd.Parameters.AddWithValue("@chiphi", chiPhi); // Sử dụng giá trị đã parse
            cmd.Parameters.AddWithValue("@ghichu", txtGhiChu.Text);

            // 4. Thực thi
            try
            {
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    if (currentAction == "THEM")
                        MessageBox.Show("Thêm bản ghi bảo dưỡng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else if (currentAction == "SUA")
                        MessageBox.Show("Cập nhật thông tin bảo dưỡng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không có bản ghi nào được thay đổi. Kiểm tra Mã bảo trì.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
                Load_data();

                // Trở về trạng thái mặc định sau khi lưu thành công
                currentAction = "DEFAULT";
                SetInputControlsEnabled(false);
                SetButtonStates(false);
                ClearInputFields();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ClearInputFields();
            currentAction = "DEFAULT";
            SetInputControlsEnabled(false);
            SetButtonStates(false);
            txtMaBaoTri.Enabled = true;
            Load_data(); // Tải lại dữ liệu gốc
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Load_data();
            ClearInputFields();
            currentAction = "DEFAULT";
            SetInputControlsEnabled(false);
            SetButtonStates(false);
            txtMaBaoTri.Enabled = true;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát không?", "Xác nhận",
                  MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
