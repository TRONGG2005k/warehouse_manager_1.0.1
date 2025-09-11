using Microsoft.EntityFrameworkCore;
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
using warehouse_manager.Models;
using warehouse_manager.service;

namespace warehouse_manager.ui.uiController.vatlieu
{
    public partial class VatLieu : UserControl
    {
        private WarehouseManagerContext context = new WarehouseManagerContext();
        public VatLieu()
        {
            InitializeComponent();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private bool KiemTraNhapLieu()
        {
            // TextBox1: Mã vật liệu
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Mã liệu không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Focus();
                return false;
            }

            // TextBox2: Đơn giá
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("TextBox2 không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Focus();
                return false;
            }
            if (!decimal.TryParse(textBox2.Text, out _))
            {
                MessageBox.Show("Đơn giá phải là số!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Focus();
                return false;
            }

            // TextBox3: Tên vật liệu
            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Tên không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox3.Focus();
                return false;
            }

            // TextBox4: Số lượng tồn
            if (string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("số lượng tồn không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox4.Focus();
                return false;
            }
            if (!int.TryParse(textBox4.Text, out _))
            {
                MessageBox.Show("Số lượng tồn phải là số nguyên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox4.Focus();
                return false;
            }

            // ComboBox1: Loại vật liệu
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Tên loại chưa được chọn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBox1.Focus();
                return false;
            }

            // ComboBox2: Nhà cung cấp
            if (comboBox2.SelectedIndex == -1)
            {
                MessageBox.Show("Tên nhà cung cấp chưa được chọn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBox2.Focus();
                return false;
            }

            // ComboBox5: Đơn vị tính
            if (comboBox5.SelectedIndex == -1)
            {
                MessageBox.Show("Đơn vị tính chưa được chọn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBox5.Focus();
                return false;
            }

            return true; 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var loai = context.LoaiVatLieus.FirstOrDefault(l => l.TenLoai == comboBox1.SelectedItem.ToString());
                var ncc = context.NhaCungCaps.FirstOrDefault(n => n.TenNhaCungCap == comboBox2.SelectedItem.ToString());

                if (loai == null || ncc == null)
                {
                    MessageBox.Show("Không tìm thấy loại vật liệu hoặc nhà cung cấp trong CSDL!");
                    return;
                }
                if (!KiemTraNhapLieu()) return;

                int sl = int.TryParse(textBox4.Text, out int result1) ? result1 : 0;

                var vl = new Models.VatLieu
                {
                    MaVatLieu = textBox1.Text,
                    DonGia = decimal.TryParse(textBox2.Text, out decimal result) ? result : 0,
                    DonViTinh = comboBox5.SelectedItem.ToString(),
                    Ten = textBox3.Text,
                    SoLuongTon = sl,
                    MaLoai = loai.Id,
                    MaNhaCungCap = ncc.Id,
                    TrangThai = sl > 0 ? "Còn hàng" : "Hết hàng"
                };

                context.VatLieus.Add(vl);
                context.SaveChanges();

                MessageBox.Show("Thêm vật liệu thành công!");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi: " + ex.Message);
            }
        }

        private void VatLieu_Load(object sender, EventArgs e)
        {
            LoadData();
            var ncc = new NhaCungCapService();
            foreach(var item in ncc.danhSachNhaCungCap())
            {
                comboBox1.Items.Add(item);
            }
            var lvl = new LoaiVatLieuService();
            foreach(var item in lvl.danhSachLoaiVatLieu())
            {
                comboBox2.Items.Add(item);
            }
            List<String> donViTinhs = new List<string>
            {
                "",
                "Cái",
                "Chiếc",
                "Bộ",
                "Hộp",
                "Thùng",
                "Kg",
                "Gram",
                "Mét",
                "Mét Vuông",
                "Mét Khối",
                "Lít",
                "Chiều"
            };
            foreach (var item in donViTinhs) {
                comboBox5.Items.Add(item);
            }
        }
        private void LoadData()
        {
            dataGridView1.DataSource = context.VatLieus
                .Include(vl => vl.MaNhaCungCapNavigation)
                .Include(vl => vl.MaLoaiNavigation)
                .Select(
                vl => new
                {
                    MaVatLieu = vl.MaVatLieu,
                    DonGia = vl.DonGia,
                    DonViTinh = vl.DonViTinh,
                    Ten = vl.Ten,
                    SoLuongTon = vl.SoLuongTon,
                    LoaiVatLieu = vl.MaLoaiNavigation.TenLoai,
                    TenNhaCungCap = vl.MaNhaCungCapNavigation.TenNhaCungCap,
                    TrangThai = vl.TrangThai
                }
            ).ToList(); ;
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
