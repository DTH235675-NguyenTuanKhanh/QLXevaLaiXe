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
    public partial class Xe : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\TK\DA.NET\QLXevaLaiXe\QLXevaLaiXe\QLXVLX.mdf;Integrated Security=True");
        SqlDataAdapter da;
        DataTable dt;
        void Load_data()
        {
            da= new SqlDataAdapter("select * from Xe", conn);
            dt= new DataTable();
            da.Fill(dt);
            dgrdsxe.DataSource = dt;
        }

        private string currentAction = "DEFAULT";
        private void SetInputControlsEnabled(bool isEnabled)
        {
            txtMaXe.Enabled = isEnabled;
            cboHangXe.Enabled = isEnabled;
            txtBienSo.Enabled = isEnabled;
            txtNamSX.Enabled = isEnabled;
            cboTinhTrang.Enabled = isEnabled;
            txtSoGhe.Enabled = isEnabled;
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
            // btnReset thường chỉ có ý nghĩa khi đang nhập, nhưng ở đây ta dùng btnHuy
            btnReset.Enabled = !isEditing;
        }


        private void ClearInputFields()
        {
            txtMaXe.Clear();
            cboHangXe.SelectedIndex = -1;
            txtBienSo.Clear();
            txtNamSX.Clear();
            cboTinhTrang.SelectedIndex = -1;
            txtSoGhe.Clear();
        }
        public Xe()
        {
            InitializeComponent();
            SetInputControlsEnabled(false);
            SetButtonStates(false);
            txtMaXe.Enabled = true;
        }

        private void Xe_Load(object sender, EventArgs e)
        {
            Load_data();

            cboTinhTrang.Items.Clear();
            cboTinhTrang.Items.Add("Đang hoạt động");
            cboTinhTrang.Items.Add("Cần bảo dưỡng");
            cboTinhTrang.Items.Add("Hỏng");
            cboTinhTrang.Items.Add("Đang bảo dưỡng");

            cboHangXe.Items.Clear();
            cboHangXe.Items.Add("Toyota");
            cboHangXe.Items.Add("Ford");
            cboHangXe.Items.Add("Hyundai");
            cboHangXe.Items.Add("Kia");
            cboHangXe.Items.Add("Mazda");
            cboHangXe.Items.Add("Honda");
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            currentAction = "THEM";

            // 2. Xóa trắng các trường nhập liệu
            ClearInputFields();

            // 3. Bật các trường nhập liệu
            SetInputControlsEnabled(true);

            // 4. Bật nút Lưu/Hủy và Tắt nút Thêm/Sửa/Xóa/Tìm kiếm
            SetButtonStates(true);

            // 5. Đặt con trỏ vào Mã xe
            txtMaXe.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgrdsxe.SelectedRows.Count == 0 && txtMaXe.Text == "")
            {
                MessageBox.Show("Vui lòng chọn một xe từ danh sách hoặc nhập Mã xe để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Chuyển sang trạng thái chỉnh sửa
            currentAction = "SUA";
            SetInputControlsEnabled(true);
            SetButtonStates(true);

            // Mã xe thường là khóa chính, không cho sửa trong chế độ sửa.
            txtMaXe.Enabled = false;

            txtMaXe.Focus();

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql = "DELETE FROM Xe WHERE maxe=@maxe";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@maxe", txtMaXe.Text);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            Load_data();
            MessageBox.Show("Đã xóa xe!");
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaXe.Text) && string.IsNullOrWhiteSpace(txtBienSo.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã xe để tìm kiếm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string sql = "SELECT * FROM Xe WHERE maxe LIKE @maxe ";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@maxe", "%" + txtMaXe.Text + "%");

                SqlDataAdapter daTK = new SqlDataAdapter(cmd);
                DataTable dtTK = new DataTable();
                daTK.Fill(dtTK);
                dgrdsxe.DataSource = dtTK;

                // Thông báo nếu không tìm thấy
                if (dtTK.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy kết quả phù hợp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // 1. Đảm bảo trạng thái form không phải là THÊM/SỬA
                currentAction = "DEFAULT";
                SetButtonStates(false);
                SetInputControlsEnabled(false); // Khóa tất cả các controls khác

                // 2. Mở lại ô Mã xe để tiếp tục nhập từ khóa tìm kiếm
                txtMaXe.Enabled = true;

                // 3. Đặt con trỏ chuột vào ô Mã xe
                txtMaXe.Focus();

                // 4. Xóa trống ô Biển số để chuẩn bị cho tìm kiếm mới
                txtBienSo.Clear();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            
            if (MessageBox.Show("Bạn có chắc muốn thoát không?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }
        

        private void btnReset_Click(object sender, EventArgs e)
        {
            Load_data();
            ClearInputFields();
            currentAction = "DEFAULT";
            SetInputControlsEnabled(false);
            SetButtonStates(false);
            txtMaXe.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaXe.Text) || string.IsNullOrEmpty(cboHangXe.Text) || string.IsNullOrEmpty(txtBienSo.Text))
            {
                MessageBox.Show("Mã xe, Hãng xe và Biển số không được để trống.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!int.TryParse(txtNamSX.Text, out _) || !int.TryParse(txtSoGhe.Text, out _))
            {
                MessageBox.Show("Năm sản xuất và Số ghế phải là số nguyên.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string sql = "";
            if (currentAction == "THEM")
            {
                // Sửa tên cột và tham số thành chữ thường
                sql = "INSERT INTO Xe (maxe, hangxe, bienso, namsx, tinhtrang, soghe) "
               + "VALUES (@maxe, @hangxe, @bienso, @namsx, @tinhtrang, @soghe)";
            }
            else if (currentAction == "SUA")
            {
                // Sửa lỗi cú pháp và tên cột/tham số thành chữ thường
                sql = "UPDATE Xe SET hangxe = @hangxe, bienso = @bienso, namsx = @namsx, " +
                                "tinhtrang=@tinhtrang, soghe=@soghe WHERE maxe=@maxe";
            }
            else // Trường hợp không xác định được hành động (chỉ để an toàn)
            {
                MessageBox.Show("Không thể thực hiện Lưu. Vui lòng nhấn Thêm hoặc Sửa trước.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlCommand cmd = new SqlCommand(sql, conn);
            // Chuyển tên tham số thành chữ thường
            cmd.Parameters.AddWithValue("@maxe", txtMaXe.Text);
            cmd.Parameters.AddWithValue("@hangxe", cboHangXe.Text);
            cmd.Parameters.AddWithValue("@bienso", txtBienSo.Text);
            cmd.Parameters.AddWithValue("@namsx", int.Parse(txtNamSX.Text));
            cmd.Parameters.AddWithValue("@tinhtrang", cboTinhTrang.Text);
            cmd.Parameters.AddWithValue("@soghe", int.Parse(txtSoGhe.Text));

            try
            {
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    if (currentAction == "THEM")
                        MessageBox.Show("Thêm xe thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else if (currentAction == "SUA")
                        MessageBox.Show("Cập nhật thông tin xe thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
                // Giả định Load_data() tồn tại và tải lại DataGridView
                 Load_data();
                currentAction = "DEFAULT";
                 SetInputControlsEnabled(true); // Giả định hàm này tồn tại
                 SetButtonStates(false); // Giả định hàm này tồn tại
                 ClearInputFields(); // Giả định hàm này tồn tại
            }

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            // Giữ nguyên: Xóa trắng các trường
            ClearInputFields();

            // Bổ sung: Trở về trạng thái mặc định
            currentAction = "DEFAULT";
            SetInputControlsEnabled(false);
            SetButtonStates(false);
            txtMaXe.Enabled = true;

        }

        private void dgrdsxe_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            if (i >= 0)
            {
                txtMaXe.Text = dgrdsxe.Rows[i].Cells["MaXe"].Value.ToString();
                cboHangXe.Text = dgrdsxe.Rows[i].Cells["HangXe"].Value.ToString();
                txtBienSo.Text = dgrdsxe.Rows[i].Cells["BienSo"].Value.ToString();
                txtNamSX.Text = dgrdsxe.Rows[i].Cells["NamSX"].Value.ToString();
                cboTinhTrang.Text = dgrdsxe.Rows[i].Cells["TinhTrang"].Value.ToString();
                txtSoGhe.Text = dgrdsxe.Rows[i].Cells["SoGhe"].Value.ToString();
            }
            currentAction = "DEFAULT";
            SetInputControlsEnabled(false);
            SetButtonStates(false);

        }
    }
}
