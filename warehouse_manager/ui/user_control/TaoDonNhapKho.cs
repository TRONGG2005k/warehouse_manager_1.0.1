using Microsoft.IdentityModel.Tokens;
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
using warehouse_manager.dto.o;
using warehouse_manager.Models;
using warehouse_manager.service;

namespace warehouse_manager.ui.user_control
{
    public partial class TaoDonNhapKho : UserControl
    {
        public TaoDonNhapKho()
        {
            InitializeComponent();
        }

        private void TaoDonNhapKho_Load(object sender, EventArgs e)
        {
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
        }

        private void button11_Click(object sender, EventArgs e)
        {
            PhieuService phieuService = new PhieuService();
            try
            {
                if (string.IsNullOrEmpty(comboBox1.SelectedItem.ToString()) ||
                  string.IsNullOrEmpty(textBox1.Text) ||
                  string.IsNullOrEmpty(comboBox2.SelectedItem.ToString()) ||
                  string.IsNullOrEmpty(comboBox3.SelectedItem.ToString()) ||
                  string.IsNullOrEmpty(textBox2.Text) ||
                  string.IsNullOrEmpty(comboBox4.SelectedItem.ToString() )||
                  (int)numericUpDown2.Value == 0 
                )
                {
                    //MessageBox.Show("vui lòng nhập đủ thông tìn");
                    throw new Exception("vui lòng nhập đủ thông tìn");
                }
                phieuService.themPhieuNhap(new TaoPhieuNhapKhoDto
                {
                    LoaiVatLieu = comboBox1.SelectedItem.ToString(),
                    TenVatLieu = textBox1.Text,
                    DonViTinh = comboBox2.SelectedItem.ToString(),
                    DonGia = numericUpDown1.Value,
                    NhaCungCap = comboBox3.SelectedItem.ToString(),
                    MaVatLieu = textBox2.Text,
                    SoLuong = (int)numericUpDown2.Value,
                    Make = comboBox4.SelectedItem.ToString(),
                });
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                //throw new Exception("lỗi " + ex.Message);
            }
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
            MainForm mainForm = (MainForm)this.Parent!.Parent!;
            mainForm.LoadPage(new XoaPhieuNhap());
        }
    }
}
