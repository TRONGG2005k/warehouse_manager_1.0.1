using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using warehouse_manager.context;

namespace warehouse_manager.ui.uiController.nhacungcap
{
    public partial class NhaCungCap : UserControl
    {
        private WarehouseManagerContext context = new WarehouseManagerContext();
        private long selectedId = 0;

        public NhaCungCap()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            try
            {
                dataGridView1.DataSource = context.NhaCungCaps
                    .Select(x => new
                    {
                        x.Id,
                        x.TenNhaCungCap,
                        x.DiaChi,
                        x.SoDienThoai,
                        x.Email,
                        x.MoTa
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrEmpty(txtTenNCC.Text) || txtTenNCC.Text == "")
            {
                MessageBox.Show("Tên nhà cung cấp không được để trống!");
                return false;
            }
            if (string.IsNullOrEmpty(txtDiaChi.Text) || txtDiaChi.Text =="")
            {
                MessageBox.Show("Địa chỉ không được để trống!");
                return false;
            }
            if (!string.IsNullOrEmpty(txtSoDienThoai.Text) && !txtSoDienThoai.Text.All(char.IsDigit) || txtSoDienThoai.Text == "")
            {
                MessageBox.Show("Số điện thoại chỉ được nhập số!");
                return false;
            }
            if (!string.IsNullOrEmpty(txtEmail.Text) && !txtEmail.Text.Contains("@") || txtEmail.Text =="")
            {
                MessageBox.Show("Email không hợp lệ!");
                return false;
            }
            return true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInput()) return;

                var ncc = new Models.NhaCungCap
                {
                    TenNhaCungCap = txtTenNCC.Text.Trim(),
                    DiaChi = txtDiaChi.Text.Trim(),
                    SoDienThoai = txtSoDienThoai.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    MoTa = txtMoTa.Text.Trim()
                };

                context.NhaCungCaps.Add(ncc);
                context.SaveChanges();
                LoadData();
                MessageBox.Show("Thêm nhà cung cấp thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedId == 0)
                {
                    MessageBox.Show("Vui lòng chọn nhà cung cấp để sửa!");
                    return;
                }

                if (!ValidateInput()) return;

                var ncc = context.NhaCungCaps.FirstOrDefault(x => x.Id == selectedId);
                if (ncc != null)
                {
                    ncc.TenNhaCungCap = txtTenNCC.Text.Trim();
                    ncc.DiaChi = txtDiaChi.Text.Trim();
                    ncc.SoDienThoai = txtSoDienThoai.Text.Trim();
                    ncc.Email = txtEmail.Text.Trim();
                    ncc.MoTa = txtMoTa.Text.Trim();

                    context.SaveChanges();
                    LoadData();
                    MessageBox.Show("Sửa thông tin thành công!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedId == 0)
                {
                    MessageBox.Show("Vui lòng chọn nhà cung cấp để xoá!");
                    return;
                }

                var confirm = MessageBox.Show("Bạn có chắc muốn xoá?", "Xác nhận", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.No) return;

                var ncc = context.NhaCungCaps.FirstOrDefault(x => x.Id == selectedId);
                if (ncc != null)
                {
                    context.NhaCungCaps.Remove(ncc);
                    context.SaveChanges();
                    selectedId = 0;
                    LoadData();
                    MessageBox.Show("Xoá nhà cung cấp thành công!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xoá: " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                var keyword = txtSearch.Text.Trim().ToLower();
                dataGridView1.DataSource = context.NhaCungCaps
                    .Where(x => x.TenNhaCungCap.ToLower().Contains(keyword))
                    .Select(x => new
                    {
                        x.Id,
                        x.TenNhaCungCap,
                        x.DiaChi,
                        x.SoDienThoai,
                        x.Email,
                        x.MoTa
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message);
            }
        }

        private void NhaCungCap_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    var row = dataGridView1.Rows[e.RowIndex];
                    selectedId = Convert.ToInt64(row.Cells["Id"].Value);

                    txtTenNCC.Text = row.Cells["TenNhaCungCap"].Value?.ToString();
                    txtDiaChi.Text = row.Cells["DiaChi"].Value?.ToString();
                    txtSoDienThoai.Text = row.Cells["SoDienThoai"].Value?.ToString();
                    txtEmail.Text = row.Cells["Email"].Value?.ToString();
                    txtMoTa.Text = row.Cells["MoTa"].Value?.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chọn dòng: " + ex.Message);
            }
        }
    }
}
