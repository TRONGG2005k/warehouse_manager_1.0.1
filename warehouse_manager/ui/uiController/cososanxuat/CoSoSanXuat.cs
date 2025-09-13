using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using warehouse_manager.context;

namespace warehouse_manager.ui.uiController.cososanxuat
{
    public partial class CoSoSanXuat : UserControl
    {
        private readonly WarehouseManagerContext context = new WarehouseManagerContext();
        private long selectedId = 0;
        public CoSoSanXuat()
        {
            InitializeComponent();
        }

        private void CoSoSanXuat_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            dataGridView1.DataSource = context.CoSoSanXuats
                .Where(x => x.IsDeleted != true)
                .Select(x => new
                {
                    x.Id,
                    x.TenCoSo,
                    x.DiaChi,
                    x.SoDienThoai,
                    x.Email,
                    x.MoTa
                })
                .ToList();
        }
        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtTenCoSo.Text) || txtTenCoSo.Text == "")
            {
                MessageBox.Show("Tên cơ sở không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtSoDienThoai.Text) && !Regex.IsMatch(txtSoDienThoai.Text, @"^\d+$") || txtSoDienThoai.Text == "")
            {
                MessageBox.Show("Số điện thoại chỉ được chứa chữ số!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtEmail.Text) &&
                !Regex.IsMatch(txtEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"
                ) || txtEmail.Text == "")
            {
                MessageBox.Show("Email không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    var row = dataGridView1.Rows[e.RowIndex];
                    selectedId = Convert.ToInt64(row.Cells["Id"].Value);

                    txtTenCoSo.Text = row.Cells["TenCoSo"].Value?.ToString();
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInput()) return;

                var csx = new Models.CoSoSanXuat
                {
                    TenCoSo = txtTenCoSo.Text.Trim(),
                    DiaChi = txtDiaChi.Text.Trim(),
                    SoDienThoai = txtSoDienThoai.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    MoTa = txtMoTa.Text.Trim()
                };

                context.CoSoSanXuats.Add(csx);
                context.SaveChanges();
                LoadData();
                MessageBox.Show("Thêm cơ sở thành công!");
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
                    MessageBox.Show("Vui lòng chọn cơ sở để sửa!");
                    return;
                }

                if (!ValidateInput()) return;

                var csx = context.CoSoSanXuats.FirstOrDefault(x => x.Id == selectedId);
                if (csx != null)
                {
                    csx.TenCoSo = txtTenCoSo.Text.Trim();
                    csx.DiaChi = txtDiaChi.Text.Trim();
                    csx.SoDienThoai = txtSoDienThoai.Text.Trim();
                    csx.Email = txtEmail.Text.Trim();
                    csx.MoTa = txtMoTa.Text.Trim();

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
                    MessageBox.Show("Vui lòng chọn cơ sở để xoá!");
                    return;
                }

                var confirm = MessageBox.Show("Bạn có chắc muốn xoá?", "Xác nhận", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.No) return;

                var csx = context.CoSoSanXuats.FirstOrDefault(x => x.Id == selectedId);
                if (csx != null)
                {
                    csx.IsDeleted = true;
                    context.SaveChanges();
                    selectedId = 0;
                    LoadData();
                    MessageBox.Show("Xoá cơ sở thành công!");
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
                dataGridView1.DataSource = context.CoSoSanXuats
                    .Where(x => x.TenCoSo.ToLower().Contains(keyword))
                    .Select(x => new
                    {
                        x.Id,
                        x.TenCoSo,
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

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
