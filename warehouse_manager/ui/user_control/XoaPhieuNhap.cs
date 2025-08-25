using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using warehouse_manager.dto;
using warehouse_manager.Models;
using warehouse_manager.service;

namespace warehouse_manager.ui.user_control
{
    public partial class XoaPhieuNhap : UserControl
    {
        public XoaPhieuNhap()
        {
            InitializeComponent();
        }

        private void TaoDonNhapKho_Load(object sender, EventArgs e)
        {
            List<String> loaiVatlieus = new LoaiVatLieuService().danhSachLoaiVatLieu();
            foreach (var item in loaiVatlieus)
            {
                comboBox1.Items.Add(item);
            }

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
            foreach (var item in donViTinhs)
            {
                comboBox2.Items.Add(item);
            }
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
        }

        private void button11_Click(object sender, EventArgs e)
        {
            PhieuService phieuService = new PhieuService();
            phieuService.themPhieuNhap(new TaoPhieuNhapKhoDto
            {
                LoaiVatLieu = comboBox1.SelectedItem.ToString(),
                TenVatLieu = textBox2.Text,
                DonViTinh = comboBox2.SelectedItem.ToString(),
                DonGia = numericUpDown1.Value,
                NhaCungCap = comboBox3.SelectedItem.ToString(),
                MaVatLieu = textBox2.Text,
                SoLuong = (int)numericUpDown2.Value,
                Make = comboBox4.SelectedItem.ToString(),
            });
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
    }
}
