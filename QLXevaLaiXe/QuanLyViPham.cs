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
    public partial class QuanLyViPham : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\TK\DA.NET\QLXevaLaiXe\QLXevaLaiXe\QLXVLX.mdf;Integrated Security=True");
        SqlDataAdapter da;
        DataTable dt;

        private string currentAction = "DEFAULT";
        public QuanLyViPham()
        {
            InitializeComponent();
            SetInputControlsEnabled(false);
            SetButtonStates(false);
            txtMaViPham.Enabled = true;
        }
        private void LoadData()
        {
            try
            {
                da = new SqlDataAdapter("SELECT * FROM Vipham", conn);
                dt = new DataTable();
                da.Fill(dt);
                // Giả định tên DataGridView là dgrdsVipham
                dgrViPham.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }
        private void LoadMaSoTaiXe()
        {
            cboMaTaiXe.Items.Clear();
            string sql = "SELECT maso FROM Taixe";
            LoadComboBoxData(sql, cboMaTaiXe, "maso");
        }

        private void LoadMaXe()
        {
            cboMaXe.Items.Clear();
            string sql = "SELECT maxe FROM Xe";
            LoadComboBoxData(sql, cboMaXe, "maxe");
        }

        private void LoadLoaiViPham()
        {
            // Các loại vi phạm có thể là giá trị cố định hoặc lấy từ bảng ThamSo/CauHinh nếu có
            cboLoaiViPham.Items.Clear();
            cboLoaiViPham.Items.Add("Vượt đèn đỏ");
            cboLoaiViPham.Items.Add("Chạy quá tốc độ");
            cboLoaiViPham.Items.Add("Đi sai làn");
            cboLoaiViPham.Items.Add("Không đội mũ bảo hiểm");
            // cboLoaiViPham.Items.Add("Vi phạm khác"); 
        }

        // Hàm hỗ trợ tải dữ liệu chung từ DB vào ComboBox
        private void LoadComboBoxData(string sql, ComboBox cbo, string columnName)
        {
            SqlDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                if (conn.State != ConnectionState.Open) conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    cbo.Items.Add(reader[columnName].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu cho {columnName}: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (reader != null) reader.Close();
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }

        // ==========================================================
        // QUẢN LÝ GIAO DIỆN
        // ==========================================================

        private void SetInputControlsEnabled(bool isEnabled)
        {
            txtMaViPham.Enabled = isEnabled;
            cboMaTaiXe.Enabled = isEnabled;
            cboMaXe.Enabled = isEnabled;
            cboLoaiViPham.Enabled = isEnabled;
            dtpNgayXayRa.Enabled = isEnabled;
            cboTrangThai.Enabled = isEnabled;
            txtMucPhat.Enabled = isEnabled;
        }

        private void SetButtonStates(bool isEditing)
        {
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
            txtMaViPham.Clear();
            cboMaTaiXe.SelectedIndex = -1;
            cboMaXe.SelectedIndex = -1;
            cboLoaiViPham.SelectedIndex = -1;
            dtpNgayXayRa.Value = DateTime.Now;
            cboTrangThai.SelectedIndex = -1;
            txtMucPhat.Clear();
        }
        private void QuanLyViPham_Load(object sender, EventArgs e)
        {
            LoadData();

            // Tải danh sách cho các ComboBox
            LoadMaSoTaiXe();
            LoadMaXe();
            LoadLoaiViPham();

            // Khởi tạo ComboBox Trạng thái
            cboTrangThai.Items.Clear();
            cboTrangThai.Items.Add("Chưa xử lý");
            cboTrangThai.Items.Add("Đã xử lý");

            // Khóa controls khi load form
            SetInputControlsEnabled(false);
            SetButtonStates(false);
            txtMaViPham.Enabled = true;
        }

        // =========================================
        // 3. THÊM
        // =========================================
        private void btnThem_Click(object sender, EventArgs e)
        {

            currentAction = "THEM";
            ClearInputFields();
            SetInputControlsEnabled(true);
            SetButtonStates(true);
            txtMaViPham.Focus();
            txtMaViPham.Enabled = true;
        }

        // =========================================
        // 4. SỬA
        // =========================================
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgrViPham.SelectedRows.Count == 0 && string.IsNullOrEmpty(txtMaViPham.Text))
            {
                MessageBox.Show("Vui lòng chọn một bản ghi vi phạm hoặc nhập Mã vi phạm để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            currentAction = "SUA";
            SetInputControlsEnabled(true);
            SetButtonStates(true);

            // Mã vi phạm (khóa chính) không cho sửa
            txtMaViPham.Enabled = false;

            // Mã tài xế và Mã xe thường cũng không cho sửa
            cboMaTaiXe.Enabled = false;
            cboMaXe.Enabled = false;

            cboLoaiViPham.Focus();
        }

        // =========================================
        // 5. XÓA
        // =========================================
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaViPham.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã vi phạm cần xóa!");
                return;
            }

            if (MessageBox.Show($"Bạn có chắc muốn xóa bản ghi vi phạm {txtMaViPham.Text}?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string sql = "DELETE FROM Vipham WHERE mavipham = @mavipham";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@mavipham", txtMaViPham.Text.Trim());

                try
                {
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("🗑️ Xóa bản ghi vi phạm thành công!");
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy Mã vi phạm để xóa.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Lỗi khi xóa: " + ex.Message);
                }
                finally
                {
                    if (conn.State == ConnectionState.Open) conn.Close();
                    LoadData();
                    ClearInputFields();
                }
            }
        }
        // =========================================
        // 6. TÌM KIẾM
        // =========================================
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                // Tìm kiếm theo Mã vi phạm, Mã tài xế hoặc Mã xe
                string keyword = txtMaViPham.Text.Trim();
                if (string.IsNullOrWhiteSpace(keyword))
                {
                    MessageBox.Show("Vui lòng nhập Mã vi phạm để tìm!");
                    return;
                }

                string sql = @"SELECT * FROM ViPham 
                                WHERE mavipham LIKE @keyword";
                SqlDataAdapter daSearch = new SqlDataAdapter(sql, conn);
                daSearch.SelectCommand.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                DataTable dtSearch = new DataTable();
                daSearch.Fill(dtSearch);
                dgrViPham.DataSource = dtSearch;

                if (dtSearch.Rows.Count == 0)
                    MessageBox.Show("Không tìm thấy bản ghi vi phạm nào phù hợp.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi khi tìm kiếm: " + ex.Message);
            }
            finally
            {
                currentAction = "DEFAULT";  
                SetInputControlsEnabled(false);
                SetButtonStates(false);
                txtMaViPham.Enabled = true;
            }
        }

        // =========================================
        // 7. CLICK DÒNG → ĐỔ DỮ LIỆU LÊN Ô NHẬP
        // =========================================
        private void dgvDanhSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow r = dgrViPham.Rows[e.RowIndex];

            txtMaViPham.Text = r.Cells["maVP"].Value.ToString();
            cboMaTaiXe.Text = r.Cells["maTX"].Value.ToString();
            cboMaXe.Text = r.Cells["maXe"].Value.ToString();
            cboLoaiViPham.Text = r.Cells["loaiVP"].Value.ToString();
            dtpNgayXayRa.Value = Convert.ToDateTime(r.Cells["ngay"].Value);
            cboTrangThai.Text = r.Cells["trangthai"].Value.ToString();
            txtMucPhat.Text = r.Cells["mucphat"].Value.ToString();
        }

        // =========================================
        // 8. RESET
        // =========================================
        private void btnReset_Click(object sender, EventArgs e)
        {
            // Tải lại dữ liệu và reset trạng thái
            LoadData();
            ClearInputFields();
            currentAction = "DEFAULT";
            SetInputControlsEnabled(false);
            SetButtonStates(false);
            txtMaViPham.Enabled = true;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát không?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }



        private void dgrDanhSachViPham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            if (i >= 0 && i < dgrViPham.Rows.Count)
            {
                txtMaViPham.Text = dgrViPham.Rows[i].Cells["mavipham"].Value.ToString();
                cboMaTaiXe.Text = dgrViPham.Rows[i].Cells["maso"].Value.ToString();
                cboMaXe.Text = dgrViPham.Rows[i].Cells["maxe"].Value.ToString();
                cboLoaiViPham.Text = dgrViPham.Rows[i].Cells["loaivipham"].Value.ToString();
                cboTrangThai.Text = dgrViPham.Rows[i].Cells["trangthai"].Value.ToString();
                txtMucPhat.Text = dgrViPham.Rows[i].Cells["mucphat"].Value.ToString();

                // Xử lý Ngày Xảy Ra (DateTimePicker)
                if (dgrViPham.Rows[i].Cells["ngayxayra"].Value != DBNull.Value)
                {
                    dtpNgayXayRa.Value = DateTime.Parse(dgrViPham.Rows[i].Cells["ngayxayra"].Value.ToString());
                }
            }

            currentAction = "DEFAULT";
            SetInputControlsEnabled(false);
            SetButtonStates(false);
            // Đảm bảo Mã vi phạm luôn bị vô hiệu hóa khi ở trạng thái Mặc định/Click
            txtMaViPham.Enabled = false;
        }
        

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ClearInputFields();
            currentAction = "DEFAULT";
            SetInputControlsEnabled(false);
            SetButtonStates(false);
            txtMaViPham.Enabled = true;
            LoadData(); // Tải lại dữ liệu gốc
        }
        
        

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // 1. KIỂM TRA RÀNG BUỘC
            if (string.IsNullOrEmpty(txtMaViPham.Text) || string.IsNullOrEmpty(cboMaTaiXe.Text) || string.IsNullOrEmpty(cboMaXe.Text) || string.IsNullOrEmpty(cboLoaiViPham.Text))
            {
                MessageBox.Show("Mã vi phạm, Mã tài xế, Mã xe và Loại vi phạm không được để trống.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!decimal.TryParse(txtMucPhat.Text, out _))
            {
                MessageBox.Show("Mức phạt phải là số.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2. CHUẨN BỊ SQL
            string sql = "";
            string successMessage = "";

            if (currentAction == "THEM")
            {
                sql = @"INSERT INTO ViPham (mavipham, maso, maxe, loaivipham, ngayxayra, trangthai, mucphat) 
                        VALUES (@mavipham, @maso, @maxe, @loaivipham, @ngayxayra, @trangthai, @mucphat)";
                successMessage = "✅ Thêm bản ghi vi phạm thành công!";
            }
            else if (currentAction == "SUA")
            {
                // Sử dụng mavipham làm khóa chính để UPDATE
                sql = @"UPDATE ViPham 
                        SET loaivipham=@loaivipham, ngayxayra=@ngayxayra, trangthai=@trangthai, mucphat=@mucphat 
                        WHERE mavipham=@mavipham";
                successMessage = "✅ Cập nhật thông tin vi phạm thành công!";
            }
            else
            {
                MessageBox.Show("Vui lòng nhấn Thêm hoặc Sửa trước khi Lưu.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);

                // Gán tham số chung
                cmd.Parameters.AddWithValue("@mavipham", txtMaViPham.Text.Trim());
                cmd.Parameters.AddWithValue("@maso", cboMaTaiXe.Text.Trim());
                cmd.Parameters.AddWithValue("@maxe", cboMaXe.Text.Trim());
                cmd.Parameters.AddWithValue("@loaivipham", cboLoaiViPham.Text.Trim());
                cmd.Parameters.AddWithValue("@ngayxayra", dtpNgayXayRa.Value.Date);
                cmd.Parameters.AddWithValue("@trangthai", cboTrangThai.Text.Trim());
                cmd.Parameters.AddWithValue("@mucphat", decimal.Parse(txtMucPhat.Text)); // Đảm bảo lưu đúng kiểu số

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show(successMessage);
                }
                else
                {
                    MessageBox.Show("Không có bản ghi nào được thay đổi.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi khi lưu dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();

                // Trở về trạng thái mặc định sau khi hoàn tất
                LoadData();
                currentAction = "DEFAULT";
                SetInputControlsEnabled(false);
                SetButtonStates(false);
                ClearInputFields();
            }
        }
    }
}