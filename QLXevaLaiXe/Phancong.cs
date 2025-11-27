using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QLXevaLaiXe
{
    public partial class Phancong : Form
    {
        string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\TK\\DA.NET\\QLXevaLaiXe\\QLXevaLaiXe\\QLXVLX.mdf;Integrated Security=True";
        SqlConnection conn;
        SqlDataAdapter da;
        DataTable dt;

        private string currentAction = "DEFAULT";
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
            // btnReset thường chỉ có ý nghĩa khi đang nhập, nhưng ở đây ta dùng btnHuy
            btnReset.Enabled = !isEditing;
        }


        private void ClearInputFields()
        {
            txtMaPhanCong.Clear();
            txtMaXe.Clear();
            txtMaSo.Clear(); // maso (Mã tài xế)
            dtpNgayPhanCong.Value = DateTime.Now; // ngayphancong
            txtDiaDiemDen.Clear();
            txtGioDi.Clear();
            txtGioDen.Clear();

            // Xử lý trạng thái chuyến (trangthaichuyen)
            rdoDaDi.Checked = false;
            rdoChuaDi.Checked = false;
        }
        private void SetInputControlsEnabled(bool isEnabled)
        {
            txtMaPhanCong.Enabled = isEnabled;         // mapc
            txtMaXe.Enabled = isEnabled;         // maxe
            txtMaSo.Enabled = isEnabled;    // maso (Mã tài xế)
            dtpNgayPhanCong.Enabled = isEnabled; // ngayphancong
            rdoDaDi.Enabled = isEnabled;         // trangthaichuyen
            rdoChuaDi.Enabled = isEnabled;       // trangthaichuyen
            txtDiaDiemDen.Enabled = isEnabled;   // diadiemden
            txtGioDi.Enabled = isEnabled;        // giodi
            txtGioDen.Enabled = isEnabled;       // gioden
        }
        public Phancong()
        {
            InitializeComponent();
            conn = new SqlConnection(connectionString);
            SetInputControlsEnabled(false);
            SetButtonStates(false);
            txtMaSo.Enabled = true;
            txtMaPhanCong.Enabled = true;
            txtMaXe.Enabled = true;

        }

        // ==================== HÀM LOAD DỮ LIỆU ====================
        private void LoadData()
        {
            try
            {
                da = new SqlDataAdapter("SELECT * FROM Phancong", conn);
                dt = new DataTable();
                da.Fill(dt);
                dgrdanhsach.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }

        // ==================== HÀM XÓA TRẮNG Ô NHẬP ====================
        private void ClearInputs()
        {
            txtMaSo.Clear();
            txtMaPhanCong.Clear();
            txtMaXe.Clear();
            txtDiaDiemDen.Clear();
            txtGioDi.Clear();
            txtGioDen.Clear();
            rdoChuaDi.Checked = false;
            rdoDaDi.Checked = false;
            dtpNgayPhanCong.Value = DateTime.Now;
        }

        // ==================== LOAD FORM ====================
        private void Phancong_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        // ==================== NÚT THÊM ====================
        private void btnThem_Click(object sender, EventArgs e)
        {
            // 1.Chuyển sang trạng thái Thêm
            currentAction = "THEM";

            // 2. Xóa trắng các trường nhập liệu
            ClearInputFields();

            // 3. Bật các trường nhập liệu
            SetInputControlsEnabled(true);

            // 4. Bật nút Lưu/Hủy và Tắt các nút khác
            SetButtonStates(true);

            // 5. Đặt con trỏ vào Mã phân công
            txtMaPhanCong.Focus();

            // Đảm bảo tất cả các trường chính được phép nhập
            txtMaPhanCong.Enabled = true;
            txtMaSo.Enabled = true;
            txtMaXe.Enabled = true;
        }

        // ==================== NÚT SỬA ====================
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgrdanhsach.SelectedRows.Count == 0 && txtMaPhanCong.Text == "")
            {
                // Cập nhật thông báo
                MessageBox.Show("Vui lòng chọn một bản **Phân công** từ danh sách hoặc nhập Mã **Phân công (mapc)** để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Chuyển sang trạng thái chỉnh sửa
            currentAction = "SUA";
            SetInputControlsEnabled(true);
            // Gọi hàm thiết lập trạng thái nút (nếu có)
            SetButtonStates(true);
            txtMaPhanCong.Enabled = false;
            // Nếu không cho phép sửa: 
            txtMaSo.Enabled = false;
            txtMaXe.Enabled = false;
            
            txtDiaDiemDen.Focus();
        }

        // ==================== NÚT XÓA ====================
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaSo.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã số phân công cần xóa!");
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa phân công này?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    string sql = "DELETE FROM Phancong WHERE maso = @maso";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@maso", txtMaSo.Text.Trim());

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    LoadData();
                    ClearInputs();
                    MessageBox.Show("🗑️ Xóa phân công thành công!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Lỗi khi xóa: " + ex.Message);
                    if (conn.State == ConnectionState.Open) conn.Close();
                }
            }
        }

        // ==================== NÚT TÌM KIẾM ====================
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                string keyword = txtMaSo.Text.Trim();
                if (string.IsNullOrWhiteSpace(keyword))
                {
                    MessageBox.Show("Vui lòng nhập Mã số hoặc Mã phân công để tìm!");
                    return;
                }

                string sql = @"SELECT * FROM Phancong 
                               WHERE maso LIKE @keyword OR maphancong LIKE @keyword OR diadiemden LIKE @keyword";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.SelectCommand.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                DataTable dtSearch = new DataTable();
                da.Fill(dtSearch);
                dgrdanhsach.DataSource = dtSearch;

                if (dtSearch.Rows.Count == 0)
                    MessageBox.Show("Không tìm thấy phân công nào phù hợp.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi khi tìm kiếm: " + ex.Message);
            }
            finally
            {
                // 1. Đảm bảo trạng thái form không phải là THÊM/SỬA
                currentAction = "DEFAULT";
                SetButtonStates(false);
                SetInputControlsEnabled(false); // Khóa tất cả các controls khác

                // 2. Mở lại ô Mã xe để tiếp tục nhập từ khóa tìm kiếm
                txtMaSo.Enabled = true;
                txtMaPhanCong.Enabled = true;
                txtMaXe.Enabled = true;

                // 3. Đặt con trỏ chuột vào ô Mã xe
                txtMaSo.Focus();

                // 4. Xóa trống ô Biển số để chuẩn bị cho tìm kiếm mới
                txtMaXe.Clear();
                txtMaPhanCong.Clear();
            }
        }

        // ==================== NÚT RESET, HỦY, THOÁT ====================
        private void btnReset_Click(object sender, EventArgs e)
        {
            LoadData();
            ClearInputFields();
            currentAction = "DEFAULT";
            SetInputControlsEnabled(false);
            SetButtonStates(false);
            txtMaSo.Enabled = true;
            txtMaPhanCong.Enabled = true;
            txtMaXe.Enabled = true;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            // Giữ nguyên: Xóa trắng các trường
            ClearInputFields();

            // Bổ sung: Trở về trạng thái mặc định
            currentAction = "DEFAULT";
            SetInputControlsEnabled(false);
            SetButtonStates(false);
            txtMaSo.Enabled = true;
            txtMaPhanCong.Enabled = true;
            txtMaXe.Enabled = true;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát không?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void dgrdstaixe_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Đảm bảo có ít nhất một hàng được chọn trong DataGridView
            // int i = e.RowIndex; // Biến 'i' đã được khai báo sẵn từ sự kiện DataGridView
            int i = dgrdanhsach.CurrentRow.Index;
            if (i >= 0)
            {
                // Lấy dữ liệu từ dòng có chỉ mục 'i' của lưới dgrPhancong (hoặc dgrdanhsach)

                // 1. Gán các trường Text
                txtMaPhanCong.Text = dgrdanhsach.Rows[i].Cells["mapc"].Value.ToString();
                txtMaXe.Text = dgrdanhsach.Rows[i].Cells["maxe"].Value.ToString();
                txtMaSo.Text = dgrdanhsach.Rows[i].Cells["maso"].Value.ToString(); // maso (Mã tài xế)
                txtDiaDiemDen.Text = dgrdanhsach.Rows[i].Cells["diadiemden"].Value.ToString();
                txtGioDi.Text = dgrdanhsach.Rows[i].Cells["giodi"].Value.ToString();
                txtGioDen.Text = dgrdanhsach.Rows[i].Cells["gioden"].Value.ToString();

                // 2. Xử lý trường Ngày Phân công (DateTimePicker)
                if (dgrdanhsach.Rows[i].Cells["ngayphancong"].Value != DBNull.Value)
                {
                    // Chuyển đổi giá trị thành DateTime
                    dtpNgayPhanCong.Value = DateTime.Parse(dgrdanhsach.Rows[i].Cells["ngayphancong"].Value.ToString());
                }

                // 3. Xử lý trường Trạng Thái Chuyến (Radio Button)
                // Tên cột: trangthaichuyen
                string trangthai = dgrdanhsach.Rows[i].Cells["trangthaichuyen"].Value.ToString().Trim().ToLower();
                // Dùng .ToLower() để so sánh không phân biệt chữ hoa/thường

                // Kiểm tra giá trị Trạng Thái được lưu trong CSDL
                if (trangthai == "đã đi")
                {
                    rdoDaDi.Checked = true;
                    rdoChuaDi.Checked = false;
                }
                else // Giả định trường hợp còn lại là "chưa đi"
                {
                    rdoDaDi.Checked = false;
                    rdoChuaDi.Checked = true;
                }
            }
            currentAction = "DEFAULT";
            SetInputControlsEnabled(false);
            SetButtonStates(false);
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaPhanCong.Text) || string.IsNullOrWhiteSpace(txtMaSo.Text) || string.IsNullOrWhiteSpace(txtMaXe.Text) || string.IsNullOrWhiteSpace(txtGioDi.Text))
            {
                MessageBox.Show("Mã phân công, Mã tài xế, Mã xe và Giờ đi không được để trống!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string sql = "";
            string successMessage = "";

            if (currentAction == "THEM")
            {
                // Câu lệnh INSERT
                sql = @"INSERT INTO Phancong (mapc, maso, maxe, ngayphancong, diadiemden, giodi, gioden, trangthaichuyen)
                VALUES (@mapc, @maso, @maxe, @ngayphancong, @diadiemden, @giodi, @gioden, @trangthaichuyen)";
                successMessage = "✅ Thêm phân công thành công!";
            }
            else if (currentAction == "SUA")
            {
                // Câu lệnh UPDATE
                // **Lưu ý: Tôi dùng mapc (Mã phân công) làm WHERE để UPDATE, vì mapc thường là PK.**
                // Nếu bạn dùng maso, logic UPDATE sẽ bị sai nếu 1 maso có nhiều mapc.
                sql = @"UPDATE Phancong 
                SET maso=@maso, maxe=@maxe, ngayphancong=@ngayphancong, 
                    diadiemden=@diadiemden, giodi=@giodi, gioden=@gioden, trangthaichuyen=@trangthaichuyen
                WHERE mapc=@mapc";
                successMessage = "✅ Cập nhật phân công thành công!";
            }
            else
            {
                MessageBox.Show("Vui lòng nhấn Thêm hoặc Sửa trước khi Lưu.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                // Gán tham số
                cmd.Parameters.AddWithValue("@mapc", txtMaPhanCong.Text.Trim());
                cmd.Parameters.AddWithValue("@maso", txtMaSo.Text.Trim());
                cmd.Parameters.AddWithValue("@maxe", txtMaXe.Text.Trim());
                cmd.Parameters.AddWithValue("@ngayphancong", dtpNgayPhanCong.Value.Date);
                cmd.Parameters.AddWithValue("@diadiemden", txtDiaDiemDen.Text.Trim());
                cmd.Parameters.AddWithValue("@giodi", txtGioDi.Text.Trim());
                cmd.Parameters.AddWithValue("@gioden", txtGioDen.Text.Trim());
                cmd.Parameters.AddWithValue("@trangthaichuyen", rdoDaDi.Checked ? "Đã đi" : "Chưa đi");

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                conn.Close();

                if (rowsAffected > 0)
                {
                    MessageBox.Show(successMessage);
                }
                else
                {
                    MessageBox.Show("Không có bản ghi nào được thay đổi. Vui lòng kiểm tra Mã phân công.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi khi lưu dữ liệu: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();

                // Trở về trạng thái mặc định sau khi hoàn tất
                LoadData();
                currentAction = "DEFAULT";
                SetInputControlsEnabled(false); // Khóa controls
                SetButtonStates(false);       // Bật nút Thêm/Sửa/Xóa
                ClearInputFields();           // Xóa trắng trường nhập liệu
            }

        }
    }
}
