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
    public partial class XoaPhieuNhap : UserControl
    {

        public XoaPhieuNhap()
        {
            InitializeComponent();

        }

        private void XoaPhieuNhap_Load(object sender, EventArgs e)
        {
            LoadData();

        }

        private void LoadData()
        {
            List<String> loaiVatlieus = new LoaiVatLieuService().danhSachLoaiVatLieu();
            foreach (var item in loaiVatlieus)
            {
                comboBox1.Items.Add(item);
            }
            var service = new PhieuService();
            var phieuNhapDtos = service.phieuNhapResponse();
            List<String> nhaCungCaps = new NhaCungCapService().danhSachNhaCungCap();
            foreach (var item in nhaCungCaps)
            {
                comboBox2.Items.Add(item);
            }
            dataGridView1.DataSource = phieuNhapDtos;
            List<String> donViTinhs = new List<string>
            {
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
            MainForm mainForm = (MainForm)this.Parent!.Parent!;
            mainForm.LoadPage(new SuaPhieuNhap());
        }

        private void button10_Click(object sender, EventArgs e)
        {

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

        private void button7_Click(object sender, EventArgs e)
        {
            MainForm mainForm = (MainForm)this.Parent!.Parent!;
            mainForm.LoadPage(new TaoDonNhapKho());
        }



        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {
            textBox1.Visible = radioButton1.Checked;
            button11.Visible = radioButton1.Checked;
        }

        private void radioButton3_CheckedChanged_1(object sender, EventArgs e)
        {
            dateTimePicker1.Visible = radioButton3.Checked;
            dateTimePicker2.Visible = radioButton3.Checked;
            button12.Visible = radioButton3.Checked;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.Visible = radioButton2.Checked;
            button13.Visible = radioButton2.Checked;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Visible = radioButton4.Checked;
            button14.Visible = radioButton4.Checked;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            comboBox2.Visible = radioButton5.Checked;
            button15.Visible = radioButton5.Checked;
        }
        private void button11_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tính năng tìm theo mã đang được phát triển");
            PhieuService phieuService = new PhieuService();
            List<dto.o.PhieuNhapDto> list = phieuService.TimPhieuTheoMa(Convert.ToInt32(textBox1.Text));
            dataGridView1.DataSource = list;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tính năng tìm ngày đang được phát triển");
            PhieuService phieuService = new PhieuService();
            List<dto.o.PhieuNhapDto> list = phieuService.TimPhieuTheoKhoangThoiGian(
                    new LocTheoNgayDto
                    {
                        Start = dateTimePicker1.Value,
                        End = dateTimePicker2.Value
                    }
                );
        }
        private void button14_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tính năng tìm theo loại vật liệu đang được phát triển");
            PhieuService phieuService = new PhieuService();
            List<dto.o.PhieuNhapDto> list = phieuService.TimPhieuTheoLoaiVatLieu(comboBox1.SelectedItem.ToString() ?? "");
            dataGridView1.DataSource = list;
        }

        private void button13_Click(object sender, EventArgs e)
        {

            MessageBox.Show("Tính năng tìm theo mã liệu đang được phát triển");
            PhieuService phieuService = new PhieuService();
            List<dto.o.PhieuNhapDto> list = phieuService.TimPhieuTheoMaLieu(textBox2.Text);
            dataGridView1.DataSource = list;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tính năng tìm theo nhà cung cấp đang được phát triển");
            PhieuService phieuService = new PhieuService();
            List<dto.o.PhieuNhapDto> list = phieuService.TimPhieuTheoTenNcc(comboBox2.SelectedItem.ToString() ?? "");
            dataGridView1.DataSource = list;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button16_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox3.Text))
                {
                    MessageBox.Show("Vui lòng chọn phiếu nhập cần xóa");
                    return;
                }
                PhieuService phieuService = new PhieuService();
                phieuService.xoaPhieuNhap(Convert.ToInt32(textBox3.Text));
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xóa phiếu nhập thất bại");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString() ?? "";
        }
    }
}
