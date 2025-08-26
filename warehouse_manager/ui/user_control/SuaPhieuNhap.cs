using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using warehouse_manager.dto.i;
using warehouse_manager.Models;
using warehouse_manager.service;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace warehouse_manager.ui.user_control
{
    public partial class SuaPhieuNhap : UserControl
    {
        public SuaPhieuNhap()
        {
            InitializeComponent();
        }

        private void SuaPhieuNhap_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            var service = new PhieuService();
            var phieuNhapDtos = service.phieuNhapResponse();
            dataGridView1.DataSource = phieuNhapDtos;
            List<String> loaiVatlieus = new LoaiVatLieuService().danhSachLoaiVatLieu();
            comboBox1.Items.Add("");
            foreach (var item in loaiVatlieus)
            {
                comboBox1.Items.Add(item);
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
                comboBox2.Items.Add(item);
            }
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            List<String> nhaCungCaps = new NhaCungCapService().danhSachNhaCungCap();
            foreach (var item in nhaCungCaps)
            {
                comboBox3.Items.Add(item);
            }
            List<String> kes = new KeService().danhSachKe();
            foreach (var item in kes)
            {
                comboBox4.Items.Add(item);
            }
            dataGridView1.Columns["Id"].HeaderText = "Mã Phiếu";
            dataGridView1.Columns["LoaiVatLieu"].HeaderText = "Loại Vật Liệu";
            dataGridView1.Columns["TenHang"].HeaderText = "Tên Vật Liệu";
            dataGridView1.Columns["SoLuong"].HeaderText = "Số Lượng";
            dataGridView1.Columns["DonGia"].HeaderText = "Đơn Giá";
            dataGridView1.Columns["DonViTinh"].HeaderText = "Đơn Vị Tính";
            dataGridView1.Columns["NhaCungCap"].HeaderText = "Nhà Cung Cấp";
            dataGridView1.Columns["MaVatLieu"].HeaderText = "Mã Vật Liệu";
            dataGridView1.Columns["Ke"].HeaderText = "Mã kệ";
            dataGridView1.Columns["NgayNhap"].HeaderText = "Ngày nhập";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(comboBox1.SelectedItem.ToString()) ||
                string.IsNullOrEmpty(textBox1.Text) ||
                string.IsNullOrEmpty(comboBox2.SelectedItem.ToString()) ||
                string.IsNullOrEmpty(comboBox3.SelectedItem.ToString()) ||
                string.IsNullOrEmpty(textBox2.Text) ||
                string.IsNullOrEmpty(comboBox4.SelectedItem.ToString()) ||
                (int)numericUpDown2.Value == 0
                )
                {
                    //MessageBox.Show("vui lòng nhập đủ thông tìn");
                    throw new Exception("vui lòng nhập đủ thông tìn");
                }
                PhieuService phieuService = new PhieuService();
                var isSuccess = phieuService.suaPhieuNhap(new SuaPhieuNhapDto
                {
                    Id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value),
                    LoaiVatLieu = comboBox1.SelectedItem.ToString(),
                    TenVatLieu = textBox1.Text,
                    DonViTinh = comboBox2.SelectedItem.ToString(),
                    DonGia = Convert.ToDecimal(numericUpDown1.Value),
                    NhaCungCap = comboBox3.SelectedItem.ToString(),
                    MaVatLieu = textBox2.Text,
                    SoLuong = Convert.ToInt32(numericUpDown2.Value),
                    Ke = comboBox4.SelectedItem.ToString()
                });
                if (isSuccess)
                {
                    MessageBox.Show("Sửa phiếu nhập thành công");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("sửa thất bại " + ex.Message);
            }
        }



        private void TaoDonNhapKho_Load_1(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            MainForm mainForm = (MainForm)this.Parent!.Parent!;
            mainForm.LoadPage(new DanhSachNhapKho());
        }

        private void button9_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm();
            mainForm.LoadPage(new SuaPhieuNhap());
        }

        private void button10_Click(object sender, EventArgs e)
        {
            MainForm mainForm = (MainForm)this.Parent!.Parent!;
            mainForm.LoadPage(new XoaPhieuNhap());
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                var row = dataGridView1.CurrentRow;
                comboBox1.SelectedItem = row.Cells["LoaiVatLieu"].Value.ToString();
                textBox1.Text = row.Cells["TenHang"].Value.ToString();
                comboBox2.SelectedItem = row.Cells["DonViTinh"].Value.ToString();
                numericUpDown1.Value = Convert.ToDecimal(row.Cells["DonGia"].Value);
                comboBox3.SelectedItem = row.Cells["NhaCungCap"].Value.ToString();
                textBox2.Text = row.Cells["MaVatLieu"].Value.ToString();
                numericUpDown2.Value = Convert.ToDecimal(row.Cells["SoLuong"].Value);
                comboBox4.SelectedItem = row.Cells["Ke"].Value.ToString();

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
