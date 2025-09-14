using DocumentFormat.OpenXml.Office2010.Excel;
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
                MessageBox.Show("Đơn giá không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                MessageBox.Show("Tên nhà cung cấp chưa được chọn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBox1.Focus();
                return false;
            }

            // ComboBox2: Nhà cung cấp
            if (comboBox2.SelectedIndex == -1)
            {
                MessageBox.Show("Tên loại chưa được chọn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                if (context.VatLieus.FirstOrDefault(vl => vl.MaVatLieu == textBox1.Text) != null)
                {
                    throw new Exception("mã vật liệu đã tồn tại");
                }
                if (!KiemTraNhapLieu()) return;
                var loai = context.LoaiVatLieus.FirstOrDefault(l => l.TenLoai == comboBox2.SelectedItem.ToString());
                var ncc = context.NhaCungCaps.FirstOrDefault(n => n.TenNhaCungCap == comboBox1.SelectedItem.ToString());

                if (loai == null || ncc == null)
                {
                    MessageBox.Show("Không tìm thấy loại vật liệu hoặc nhà cung cấp trong CSDL!");
                    return;
                }

                var selectedKes = checkedListBox1.CheckedItems
                                   .Cast<Ke>()
                                   .ToList();

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
                    TrangThai = sl > 0 ? "CON_HANG" : "HET_HANG",
                    Kes = selectedKes
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
            foreach (var item in ncc.danhSachNhaCungCap())
            {
                comboBox1.Items.Add(item);
                comboBox4.Items.Add(item);
            }
            var lvl = new LoaiVatLieuService();
            foreach (var item in lvl.danhSachLoaiVatLieu())
            {
                comboBox2.Items.Add(item);
                comboBox3.Items.Add(item);
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
            foreach (var item in donViTinhs)
            {
                comboBox5.Items.Add(item);
            }
            checkedListBox1.DataSource = context.Kes.ToList();
            checkedListBox1.DisplayMember = "MaKe";
            checkedListBox1.ValueMember = "Id";
        }
        private void LoadData()
        {
            
            dataGridView1.DataSource = context.VatLieus
                .Include(vl => vl.MaNhaCungCapNavigation)
                .Include(vl => vl.MaLoaiNavigation)
                .Where(vl => vl.IsDeleted != true)
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
            if (e.RowIndex < 0) return;

            var ma = dataGridView1.CurrentRow.Cells["MaVatLieu"].Value;
            var vatLieu = context.VatLieus
            .Include(vl => vl.MaLoaiNavigation)
            .Include(vl => vl.MaNhaCungCapNavigation)
            .Include(vl => vl.Kes) // load luôn Kes nếu cần
            .FirstOrDefault(vl => vl.MaVatLieu == ma);

            if (vatLieu == null) return;
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            textBox1.Text = row.Cells["MaVatLieu"].Value?.ToString();
            textBox2.Text = row.Cells["DonGia"].Value?.ToString();
            textBox3.Text = row.Cells["Ten"].Value?.ToString();
            textBox4.Text = row.Cells["SoLuongTon"].Value?.ToString();
            comboBox1.SelectedItem = row.Cells["TenNhaCungCap"].Value?.ToString();
            comboBox2.SelectedItem = row.Cells["LoaiVatLieu"].Value?.ToString();
            comboBox5.SelectedItem = row.Cells["DonViTinh"].Value?.ToString();
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                var ke = (Ke)checkedListBox1.Items[i];
                checkedListBox1.SetItemChecked(i, vatLieu.Kes.Any(k => k.Id == ke.Id));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                if (!KiemTraNhapLieu()) return;

                // Tìm bản ghi theo Mã vật liệu
                var vl = context.VatLieus.FirstOrDefault(v => v.MaVatLieu == textBox1.Text);
                if (vl == null)
                {
                    MessageBox.Show("Không tìm thấy vật liệu cần sửa!");
                    return;
                }
                var selectedKes = checkedListBox1.CheckedItems
                                   .Cast<Ke>()
                                   .ToList();
                // Lấy loại vật liệu và nhà cung cấp
                var loai = context.LoaiVatLieus.FirstOrDefault(l => l.TenLoai == comboBox2.SelectedItem.ToString());
                var ncc = context.NhaCungCaps.FirstOrDefault(n => n.TenNhaCungCap == comboBox1.SelectedItem.ToString());
                if (loai == null || ncc == null)
                {
                    MessageBox.Show("Không tìm thấy loại vật liệu hoặc nhà cung cấp!");
                    return;
                }

                // Cập nhật thông tin
                vl.Ten = textBox3.Text;
                vl.DonGia = decimal.TryParse(textBox2.Text, out decimal dg) ? dg : 0;
                vl.SoLuongTon = int.TryParse(textBox4.Text, out int sl) ? sl : 0;
                vl.DonViTinh = comboBox5.SelectedItem?.ToString();
                vl.MaLoai = loai.Id;
                vl.MaNhaCungCap = ncc.Id;
                vl.TrangThai = vl.SoLuongTon > 0 ? "Còn hàng" : "Hết hàng";
                vl.Kes = selectedKes;
                context.SaveChanges();
                MessageBox.Show("Cập nhật vật liệu thành công!");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi sửa: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show(
                     "Bạn có chắc chắn muốn xoá vật liệu này không?",
                     "Xác nhận xoá",
                     MessageBoxButtons.YesNo,
                     MessageBoxIcon.Warning
                 );

                if (result != DialogResult.Yes)
                    return;


                // Tìm bản ghi theo Mã vật liệu
                var vl = context.VatLieus.FirstOrDefault(v => v.MaVatLieu == textBox1.Text);
                if (vl == null)
                {
                    MessageBox.Show("Không tìm thấy vật liệu cần xoá!");
                    return;
                }

                vl.IsDeleted = true;
                context.SaveChanges();
                MessageBox.Show("Xoá vật liệu thành công!");
                LoadData();

                // Xoá dữ liệu trên form
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                comboBox1.SelectedIndex = -1;
                comboBox2.SelectedIndex = -1;
                comboBox5.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Có lỗi khi xoá: " + ex.Message);
                throw new Exception("Có lỗi khi xoá: " + ex.Message);

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {


            if (comboBox3.SelectedIndex != -1 && comboBox4.SelectedIndex != -1)
            {
                var loai = context.LoaiVatLieus.FirstOrDefault(
                l => l.TenLoai == comboBox3.SelectedItem.ToString());

                var ncc = context.NhaCungCaps.FirstOrDefault(
                    n => n.TenNhaCungCap == comboBox4.SelectedItem.ToString());
                dataGridView1.DataSource = context.VatLieus
                .Where(v => v.MaLoai == loai.Id && v.MaNhaCungCap == ncc.Id && v.IsDeleted != true)
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
            else if (comboBox3.SelectedIndex != -1)
            {
                var loai = context.LoaiVatLieus.FirstOrDefault(
                l => l.TenLoai == comboBox3.SelectedItem.ToString());

                dataGridView1.DataSource = context.VatLieus
                .Where(v => v.MaLoai == loai.Id && v.IsDeleted != true)
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
            else if (comboBox4.SelectedIndex != -1)
            {
                var ncc = context.NhaCungCaps.FirstOrDefault(
                    n => n.TenNhaCungCap == comboBox4.SelectedItem.ToString());
                dataGridView1.DataSource = context.VatLieus
                .Where(v => v.MaNhaCungCap == ncc.Id &&  v.IsDeleted != true)
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
            else
            {
                MessageBox.Show("vui lòng chọn giá trị");
            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
