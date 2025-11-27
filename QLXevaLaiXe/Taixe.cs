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
    public partial class Taixe : Form
    {
        string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\TK\\DA.NET\\QLXevaLaiXe\\QLXevaLaiXe\\QLXVLX.mdf";
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
            txtMaSo.Clear();
            txtBanglai.Clear();
            txtTen.Clear();
            txtHoLot.Clear();
            txtSDT.Clear();
            radNam.Checked = false;
            radNu.Checked = false;
            dtpNgaySinh.Value = DateTime.Now;
        }
        private void SetInputControlsEnabled(bool isEnabled)
        {
            txtMaSo.Enabled = isEnabled;
            txtBanglai.Enabled = isEnabled;
            txtTen.Enabled = isEnabled;
            txtHoLot.Enabled = isEnabled;
            txtSDT.Enabled = isEnabled;
            radNam.Enabled = isEnabled;
            radNu.Enabled = isEnabled;
            dtpNgaySinh.Enabled = isEnabled;
        }
        public Taixe()
        {
            InitializeComponent();
            conn = new SqlConnection(connectionString);
            SetInputControlsEnabled(false);
            SetButtonStates(false);
            txtMaSo.Enabled = true;
            txtTen.Enabled = true;
            txtHoLot.Enabled = true;
        }

        // Load dữ liệu chính
        private void LoadData()
        {
            try
            {
                da = new SqlDataAdapter("SELECT * FROM Taixe", conn);
                dt = new DataTable();
                da.Fill(dt);
                dgrTaixe.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }
    

        // Xóa trắng ô nhập
        private void ClearInputs()
        {
            txtMaSo.Clear();
            txtHoLot.Clear();
            txtTen.Clear();
            radNam.Checked = true;
            dtpNgaySinh.Value = DateTime.Now;
            txtSDT.Clear();
            txtBanglai.Clear();

            // Đảm bảo ô luôn mở
            txtMaSo.ReadOnly = false;
            txtHoLot.ReadOnly = false;
            txtTen.ReadOnly = false;
            txtSDT.ReadOnly = false;
            txtBanglai.ReadOnly = false;
            dtpNgaySinh.Enabled = true;
            radNam.Enabled = true;
            radNu.Enabled = true;
        }
        private void Taixe_Load(object sender, EventArgs e)
        {
                LoadData();

        }

        // Vô hiệu hóa CellClick
        private void dgrTaixe_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Không bắt buộc click để thao tác
        }

        // CÁC NÚT CHỨC NĂNG CHÍNH
        
        // Nút THÊM
        private void btnThem_Click(object sender, EventArgs e)
        {
            currentAction = "THEM";

            // 2. Xóa trắng các trường và bật nhập liệu
            ClearInputFields();
            SetInputControlsEnabled(true);

            // 3. Bật nút Lưu/Hủy và tắt các nút khác
            SetButtonStates(true);

            // 4. Đặt con trỏ vào Mã số
            txtMaSo.Focus();

            // Đảm bảo Mã số được phép nhập khi thêm
            txtMaSo.Enabled = true;
        }

        // Nút SỬA
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgrTaixe.SelectedRows.Count == 0 && txtMaSo.Text == "")
            {
                MessageBox.Show("Vui lòng chọn một tài xế từ danh sách hoặc nhập Mã số để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Chuyển sang trạng thái chỉnh sửa
            currentAction = "SUA";
            SetInputControlsEnabled(true);

            // Gọi hàm thiết lập trạng thái nút (nếu có)
            SetButtonStates(true);

            // Mã số (khóa chính) không cho sửa trong chế độ sửa.
            txtMaSo.Enabled = false;

            // Di chuyển con trỏ chuột đến trường đầu tiên có thể chỉnh sửa
            txtHoLot.Focus();
        }

        // Nút XÓA
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaSo.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã số tài xế cần xóa!");
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa tài xế này?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    string sql = "DELETE FROM Taixe WHERE maso = @maso";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@maso", txtMaSo.Text.Trim());

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    LoadData();
                    ClearInputs();
                    MessageBox.Show("🗑️ Xóa tài xế thành công!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Lỗi khi xóa: " + ex.Message);
                    if (conn.State == ConnectionState.Open) conn.Close();
                }
            }
        }

        // Nút TÌM
        private void btnTim_Click(object sender, EventArgs e)
        {
            try
            {
                string keyword = txtTen.Text.Trim(); // Có thể thay bằng txtMaSo.Text nếu bạn muốn tìm theo mã
                if (string.IsNullOrWhiteSpace(keyword))
                {
                    MessageBox.Show("Vui lòng nhập Tên hoặc Mã tài xế cần tìm!");
                    return;
                }

                string sql = @"SELECT * FROM Taixe 
                               WHERE ten LIKE @keyword OR maso LIKE @keyword OR holot LIKE @keyword";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.SelectCommand.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                DataTable dtSearch = new DataTable();
                da.Fill(dtSearch);
                dgrTaixe.DataSource = dtSearch;

                if (dtSearch.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy kết quả phù hợp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
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
                txtTen.Enabled = true;
                txtHoLot.Enabled = true;

                // 3. Đặt con trỏ chuột vào ô Mã xe
                txtMaSo.Focus();

                // 4. Xóa trống ô Biển số để chuẩn bị cho tìm kiếm mới
                txtHoLot.Clear();
                txtTen.Clear();
            }
        }

        // Nút THOÁT
        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát không?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        // NÚT HỦY
        private void btnHuy_Click(object sender, EventArgs e)
        {
            // Giữ nguyên: Xóa trắng các trường
            ClearInputFields();

            // Bổ sung: Trở về trạng thái mặc định
            currentAction = "DEFAULT";
            SetInputControlsEnabled(false);
            SetButtonStates(false);
            txtMaSo.Enabled = true;
            txtTen.Enabled = true;
            txtHoLot.Enabled = true;
        }
        // NÚT RESET
        private void btnReset_Click(object sender, EventArgs e)
        {
            LoadData();
            ClearInputFields();
            currentAction = "DEFAULT";
            SetInputControlsEnabled(false);
            SetButtonStates(false);
            txtMaSo.Enabled = true;
            txtTen.Enabled = true;
            txtHoLot.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtMaSo.Text) || string.IsNullOrWhiteSpace(txtTen.Text))
            {
                MessageBox.Show("Mã số và Tên tài xế không được để trống!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string sql = "";
            string successMessage = "";

            if (currentAction == "THEM")
            {
                // Câu lệnh INSERT
                sql = @"INSERT INTO Taixe (maso, holot, ten, phai, ngaysinh, sdt, banglai)
                VALUES (@maso, @holot, @ten, @phai, @ngaysinh, @sdt, @banglai)";
                successMessage = "✅ Thêm tài xế thành công!";
            }
            else if (currentAction == "SUA")
            {
                // Câu lệnh UPDATE
                sql = @"UPDATE Taixe 
                SET holot = @holot, ten = @ten, phai = @phai,
                    ngaysinh = @ngaysinh, sdt = @sdt, banglai = @banglai
                WHERE maso = @maso";
                successMessage = "✅ Cập nhật thông tin tài xế thành công!";
            }
            else
            {
                MessageBox.Show("Không thể thực hiện Lưu. Vui lòng nhấn Thêm hoặc Sửa trước.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);

                // Gán tham số chung cho cả INSERT và UPDATE
                cmd.Parameters.AddWithValue("@maso", txtMaSo.Text.Trim());
                cmd.Parameters.AddWithValue("@holot", txtHoLot.Text.Trim());
                cmd.Parameters.AddWithValue("@ten", txtTen.Text.Trim());
                cmd.Parameters.AddWithValue("@phai", radNam.Checked ? "Nam" : "Nữ");
                cmd.Parameters.AddWithValue("@ngaysinh", dtpNgaySinh.Value.Date);
                cmd.Parameters.AddWithValue("@sdt", txtSDT.Text.Trim());
                cmd.Parameters.AddWithValue("@banglai", txtBanglai.Text.Trim());

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                conn.Close();

                if (rowsAffected > 0)
                {
                    MessageBox.Show(successMessage);
                }
                else
                {
                    MessageBox.Show("Không có bản ghi nào được thay đổi. Vui lòng kiểm tra lại Mã số.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                SetInputControlsEnabled(false);
                SetButtonStates(false);
                ClearInputFields();
            }
        }

        private void dgrTaixe_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 1. Kiểm tra xem có dòng nào được chọn không (tuyệt đối cần thiết)
            int i = e.RowIndex;
            if (i >= 0)
            {
                // Lấy dữ liệu từ dòng có chỉ mục 'i' của lưới dgrdtaixe

                // 1. Gán các trường Text
                txtMaSo.Text = dgrTaixe.Rows[i].Cells["maso"].Value.ToString();
                txtHoLot.Text = dgrTaixe.Rows[i].Cells["holot"].Value.ToString();
                txtTen.Text = dgrTaixe.Rows[i].Cells["ten"].Value.ToString();
                txtSDT.Text = dgrTaixe.Rows[i].Cells["sdt"].Value.ToString();
                txtBanglai.Text = dgrTaixe.Rows[i].Cells["banglai"].Value.ToString();

                // 2. Xử lý trường Ngày Sinh (DateTimePicker)
                if (dgrTaixe.Rows[i].Cells["ngaysinh"].Value != DBNull.Value)
                {
                    dtpNgaySinh.Value = DateTime.Parse(dgrTaixe.Rows[i].Cells["ngaysinh"].Value.ToString());
                }

                // 3. Xử lý trường Phái (Radio Button)
                string gioiTinh = dgrTaixe.Rows[i].Cells["phai"].Value.ToString().Trim().ToUpper();

                // Kiểm tra giá trị Phái được lưu trong CSDL
                if (gioiTinh == "NAM" || gioiTinh == "M")
                {
                    radNam.Checked = true;
                    radNu.Checked = false;
                }
                else // Giả định là Nữ nếu không phải Nam (hoặc kiểm tra giá trị "NU", "F")
                {
                    radNam.Checked = false;
                    radNu.Checked = true;
                }
            }
            currentAction = "DEFAULT"; // Rất quan trọng!
            SetInputControlsEnabled(false);
            SetButtonStates(false);
            txtMaSo.Enabled = false;
        }
    }

}


