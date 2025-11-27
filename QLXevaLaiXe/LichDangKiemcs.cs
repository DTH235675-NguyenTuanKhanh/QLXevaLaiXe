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
    public partial class LichDangKiemcs : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\TK\DA.NET\QLXevaLaiXe\QLXevaLaiXe\QLXVLX.mdf;Integrated Security=True");
        SqlDataAdapter da;
        DataTable dt;

        // Giả định tên DataGridView là dgrdsDangKy
        void Load_data()
        {
            // Lấy dữ liệu từ bảng DangKy
            da = new SqlDataAdapter("select * from DangKiem", conn);
            dt = new DataTable();
            da.Fill(dt);
            dgrDanhSachDangKiem.DataSource = dt;
        }

        // Hàm tải danh sách Mã xe từ bảng Xe
        private void LoadMaXe()
        {
            // Giả định tên ComboBox là cboMaXe
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

        private string currentAction = "DEFAULT";

        // Cập nhật controls cho bảng DangKy
        private void SetInputControlsEnabled(bool isEnabled)
        {
            txtMaDangKy.Enabled = isEnabled;         // madk
            cboMaXe.Enabled = isEnabled;         // maxe 
            dtpNgayDangKiem.Enabled = isEnabled;       // ngaydk
            dtpNgayHetHan.Enabled = isEnabled;   // ngayhethan
            txtGhiChu.Enabled = isEnabled;       // ghichu
        }

        private void SetButtonStates(bool isEditing)
        {
            // isEditing = TRUE khi đang ở trạng thái Thêm/Sửa (Lưu/Hủy được bật)
            // isEditing = FALSE khi ở trạng thái Mặc định 

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
            txtMaDangKy.Clear();
            cboMaXe.SelectedIndex = -1;
            dtpNgayDangKiem.Value = DateTime.Now;
            dtpNgayHetHan.Value = DateTime.Now.AddYears(1); // Mặc định là 1 năm
            txtGhiChu.Clear();
        }
        public LichDangKiemcs()
        {
            InitializeComponent();
            // Khóa controls và đặt trạng thái nút khi form khởi tạo
            SetInputControlsEnabled(false);
            SetButtonStates(false);
            txtMaDangKy.Enabled = true;
            cboMaXe.SelectedIndexChanged += cboXe_SelectedIndexChanged;
        }

        private void LichDangKiemcs_Load(object sender, EventArgs e)
        {
            Load_data();
            LoadMaXe();
        }

        private void cboXe_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaXe.SelectedIndex == -1) return;

            string sql = "SELECT namsx FROM Xe WHERE maxe = @maxe";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@maxe", cboMaXe.Text);

            try
            {
                conn.Open();
                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    int namSX = Convert.ToInt32(result);
                    int soNamSuDung = DateTime.Now.Year - namSX;

                    if (soNamSuDung < 10)
                    {
                        dtpNgayHetHan.Value = dtpNgayDangKiem.Value.AddYears(3);
                    }
                    else
                    {
                        dtpNgayHetHan.Value = dtpNgayDangKiem.Value.AddYears(1);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lấy năm sản xuất: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            currentAction = "THEM";
            ClearInputFields();
            SetInputControlsEnabled(true);
            SetButtonStates(true);
            txtMaDangKy.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgrDanhSachDangKiem.SelectedRows.Count == 0 && string.IsNullOrEmpty(txtMaDangKy.Text))
            {
                MessageBox.Show("Vui lòng chọn một bản ghi hoặc nhập Mã đăng ký để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            currentAction = "SUA";
            SetInputControlsEnabled(true);
            SetButtonStates(true);
            // Mã đăng ký (khóa chính) không cho sửa
            txtMaDangKy.Enabled = false;
            cboMaXe.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaDangKy.Text))
            {
                MessageBox.Show("Vui lòng chọn một bản ghi đăng ký để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa bản ghi đăng ký " + txtMaDangKy.Text + " không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            string sql = "DELETE FROM DangKiem WHERE madk=@madk";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@madk", txtMaDangKy.Text);

            try
            {
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Đã xóa bản ghi đăng ký thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy Mã đăng ký để xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                SetInputControlsEnabled(false);
                SetButtonStates(false);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaDangKy.Text) && string.IsNullOrEmpty(cboMaXe.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã đăng ký hoặc chọn Mã xe để tìm kiếm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                string sql = "SELECT * FROM DangKiem WHERE madk LIKE @madk OR maxe LIKE @maxe";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@madk", "%" + txtMaDangKy.Text + "%");
                cmd.Parameters.AddWithValue("@maxe", "%" + cboMaXe.Text + "%"); // Tìm kiếm theo ComboBox

                SqlDataAdapter daTK = new SqlDataAdapter(cmd);
                DataTable dtTK = new DataTable();
                daTK.Fill(dtTK);
                dgrDanhSachDangKiem.DataSource = dtTK;
                if (dtTK.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy bản ghi nào khớp với tiêu chí tìm kiếm.", "Kết quả tìm kiếm", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                txtMaDangKy.Enabled=true;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Load_data();
            LoadMaXe();
            ClearInputFields();
            currentAction = "DEFAULT";
            SetInputControlsEnabled(false);
            SetButtonStates(false);
            txtMaDangKy.Enabled = true;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát không?", "Xác nhận",
                 MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ClearInputFields();
            currentAction = "DEFAULT";
            SetInputControlsEnabled(false);
            SetButtonStates(false);
            txtMaDangKy.Enabled = true;
            Load_data();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaDangKy.Text) || string.IsNullOrEmpty(cboMaXe.Text))
            {
                MessageBox.Show("Mã đăng ký và Mã xe không được để trống.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dtpNgayDangKiem.Value > dtpNgayHetHan.Value)
            {
                MessageBox.Show("Ngày hết hạn không được nhỏ hơn Ngày đăng ký.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2. Chuẩn bị câu lệnh SQL
            string sql = "";
            if (currentAction == "THEM")
            {
                sql = "INSERT INTO DangKiem (madk, maxe, ngaydk, ngayhethan, ghichu) "
                   + "VALUES (@madk, @maxe, @ngaydk, @ngayhethan, @ghichu)";
            }
            else if (currentAction == "SUA")
            {
                // Cập nhật tất cả các trường ngoại trừ madk
                sql = "UPDATE DangKiem SET maxe = @maxe, ngaydk = @ngaydk, ngayhethan = @ngayhethan, " +
                      "ghichu = @ghichu WHERE madk = @madk";
            }
            else
            {
                MessageBox.Show("Không thể thực hiện Lưu. Vui lòng nhấn Thêm hoặc Sửa trước.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlCommand cmd = new SqlCommand(sql, conn);

            // 3. Gán tham số
            cmd.Parameters.AddWithValue("@madk", txtMaDangKy.Text);
            cmd.Parameters.AddWithValue("@maxe", cboMaXe.Text);
            cmd.Parameters.AddWithValue("@ngaydk", dtpNgayDangKiem.Value.Date);
            cmd.Parameters.AddWithValue("@ngayhethan", dtpNgayHetHan.Value.Date);
            // Cột ghichu là nvarchar nên cần đảm bảo dữ liệu Unicode được truyền đúng
            cmd.Parameters.AddWithValue("@ghichu", txtGhiChu.Text);

            // 4. Thực thi
            try
            {
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    if (currentAction == "THEM")
                        MessageBox.Show("Thêm bản ghi đăng ký thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else if (currentAction == "SUA")
                        MessageBox.Show("Cập nhật thông tin đăng ký thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không có bản ghi nào được thay đổi.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                // Trở về trạng thái mặc định
                currentAction = "DEFAULT";
                SetInputControlsEnabled(false);
                SetButtonStates(false);
                ClearInputFields();
            }
        }

        private void dgrDanhSachDangKiem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            if (i >= 0 && i < dgrDanhSachDangKiem.Rows.Count - 1)
            {
                // Giả định tên cột trong DataGridView khớp với tên cột trong CSDL
                txtMaDangKy.Text = dgrDanhSachDangKiem.Rows[i].Cells["madk"].Value.ToString();
                cboMaXe.Text = dgrDanhSachDangKiem.Rows[i].Cells["maxe"].Value.ToString(); // Lấy giá trị cho ComboBox

                if (dgrDanhSachDangKiem.Rows[i].Cells["ngaydk"].Value != DBNull.Value)
                    dtpNgayDangKiem.Value = Convert.ToDateTime(dgrDanhSachDangKiem.Rows[i].Cells["ngaydk"].Value);

                if (dgrDanhSachDangKiem.Rows[i].Cells["ngayhethan"].Value != DBNull.Value)
                    dtpNgayHetHan.Value = Convert.ToDateTime(dgrDanhSachDangKiem.Rows[i].Cells["ngayhethan"].Value);

                txtGhiChu.Text = dgrDanhSachDangKiem.Rows[i].Cells["ghichu"].Value.ToString();
            }
            SetInputControlsEnabled(false);
            SetButtonStates(false);
        }
    }
}
