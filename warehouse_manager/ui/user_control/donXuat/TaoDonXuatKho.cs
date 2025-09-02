using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using warehouse_manager.dto.i;
using warehouse_manager.service;
using warehouse_manager.ui.user_control.tonKho;

namespace warehouse_manager.ui.user_control
{
    public partial class TaoDonXuatKho : UserControl
    {
        private NguoiDungService nguoiDungService = new NguoiDungService();
        public TaoDonXuatKho()
        {
            InitializeComponent();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Items.Contains(comboBox1.Text) == false)
                {
                    throw new Exception("Tên chuyền sản xuất không tồn tại");
                }
                else if (comboBox2.Items.Contains(comboBox2.Text) == false)
                {
                    throw new Exception("Mã vật liệu không tồn tại vui lòng báo cáo quản lý");
                }
                var service = new service.PhieuXuatService();
                var phieuXuat = new ThemPhieuXuatDto
                {
                    GhiChu = textBox1.Text,
                    MaVatLieu = comboBox2.SelectedItem != null ? comboBox2.SelectedItem.ToString() : "",
                    SoLuongYeuCau = (long)numericUpDown1.Value,
                    TenChuyenSanXuat = comboBox1.SelectedItem != null ? comboBox1.SelectedItem.ToString() : ""
                };

                var context = new ValidationContext(phieuXuat);


                Validator.ValidateObject(phieuXuat, context, validateAllProperties: true);
                if (service.YeuCauthemPhieuXuat(phieuXuat))
                {
                    textBox1.Text = "";
                    comboBox1.SelectedItem = null;
                    comboBox2.SelectedItem = null;
                    numericUpDown1.Value = 0;
                    MessageBox.Show("Tạo phiếu xuất kho thành công");
                }
                //MessageBox.Show("Tạo phiếu xuất kho thành công");
            }
            catch (ValidationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TaoDonXuatKho_Load(object sender, EventArgs e)
        {
            LoadData();
            var service = new service.PhieuXuatService();
            var phieuXuatDtos = service.SelectPhieuThieu();
            dataGridView1.DataSource = phieuXuatDtos;
        }
        private void LoadData()
        {
            var vatLieuService = new service.VatLieuService();
            List<String> vatLieus = vatLieuService.danhSachVatLieu();
            foreach (var item in vatLieus)
            {
                comboBox2.Items.Add(item);
            }
            var chuyenSanXuatService = new service.ChuyenSanXuatService();
            List<String> chuyenSanXuats = chuyenSanXuatService.danhSachCoSoSanXuat();
            foreach (var item in chuyenSanXuats)
            {
                comboBox1.Items.Add(item);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            MainForm mainForm = (MainForm)this.Parent!.Parent!;
            mainForm.LoadPage(new DanhSachXuatKho());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MainForm mainForm = (MainForm)this.Parent!.Parent!;
            mainForm.LoadPage(new DanhSachDonNhapKho());
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (nguoiDungService.kiemTraVaiTroAdmin())
            {
                MainForm mainForm = (MainForm)this.Parent!.Parent!;
                mainForm.LoadPage(new HuyDonXuatcs());
            }
            else
            {
                MessageBox.Show("Bạn không có quyền truy cập chức năng này");
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            MainForm mainForm = (MainForm)this.Parent!.Parent!;
            mainForm.LoadPage(new DuyetPhieuXuat());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            nguoiDungService.logout();
            MainForm mainForm = (MainForm)this.Parent!.Parent!;
            mainForm.LoadPage(new Login());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MainForm mainForm = (MainForm)this.Parent!.Parent!;
            mainForm.LoadPage(new DanhSachNhapKho());
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if ((e.RowIndex >= 0))
                {
                    long maPhieu = (long)dataGridView1.Rows[e.RowIndex].Cells["Id"].Value;
                    textBox1.Text = $"|Bù cho phiếu {maPhieu}";

                    comboBox1.SelectedItem = dataGridView1.Rows[e.RowIndex].Cells["MaTruyenSanXuat"].Value.ToString();

                    comboBox2.SelectedItem = dataGridView1.Rows[e.RowIndex].Cells["MaVatLieu"].Value.ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "DonGia" && e.Value != null)
            {
                decimal donGia = (decimal)e.Value;
                e.Value = donGia.ToString("N0") + " ₫";
                e.FormattingApplied = true;
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "TongGiaTri" && e.Value != null)
            {
                decimal donGia = (decimal)e.Value;
                e.Value = donGia.ToString("N0") + " ₫";
                e.FormattingApplied = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MainForm mainForm = (MainForm)this.Parent.Parent;
            mainForm.LoadPage(new KiemKe());
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MainForm mainForm = (MainForm)this.Parent.Parent;
            mainForm.LoadPage(new BaoCaoNXT());
        }
    }
}
