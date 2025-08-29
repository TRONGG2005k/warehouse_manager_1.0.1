using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace warehouse_manager.ui.user_control
{
    public partial class DuyetPhieuXuat : UserControl
    {
        private service.NguoiDungService nguoiDungService;
        public DuyetPhieuXuat()
        {
            nguoiDungService = new service.NguoiDungService();
            InitializeComponent();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

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
        private void DuyetPhieuXuat_Load(object sender, EventArgs e)
        {
            LoadData();
            List<String> trangThais = new List<string>
            {
                "CHO_DUYET",
                "DA_DUYET",
                "DA_HUY"
            };
            foreach (var item in trangThais)
            {
                comboBox1.Items.Add(item);
            }
        }

        private void LoadData()
        {
            var service = new service.PhieuXuatService();
            var phieuXuatDtos = service.LayTatCaPhieu();
            dataGridView1.DataSource = phieuXuatDtos;

        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Visible = radioButton1.Checked;
            button13.Visible = radioButton1.Checked;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.SelectedItem == null)
                {
                    throw new Exception("Vui lòng chọn trạng thái để lọc");

                }
                var service = new service.PhieuXuatService();
                var trangThai = comboBox1.SelectedItem != null ? comboBox1.SelectedItem.ToString() : "";
                var phieuXuatDtos = service.TimTheoTrangThai(trangThai);
                dataGridView1.DataSource = phieuXuatDtos;
                comboBox1.SelectedItem = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            try
            {

                var service = new service.PhieuXuatService();
                var startDate = dateTimePicker2.Value;
                var endDate = dateTimePicker1.Value;
                if (startDate > endDate)
                {
                    throw new Exception("Ngày bắt đầu phải nhỏ hơn ngày kết thúc");
                }
                var phieuXuatDtos = service.TimTheoNgay(new dto.i.LocTheoNgayDto
                {
                    Start = startDate,
                    End = endDate
                });
                dataGridView1.DataSource = phieuXuatDtos;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

            dateTimePicker1.Visible = radioButton2.Checked;
            dateTimePicker2.Visible = radioButton2.Checked;
            button14.Visible = radioButton2.Checked;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            MainForm mainForm = (MainForm)this.Parent!.Parent!;
            mainForm.LoadPage(new DanhSachXuatKho());
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MainForm mainForm = (MainForm)this.Parent!.Parent!;
            mainForm.LoadPage(new TaoDonXuatKho());
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var service = new service.PhieuXuatService();
            try
            {
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    throw new Exception("Vui lòng chọn phiếu xuất để duyệt");
                }
                var phieuXuatId = int.Parse(textBox1.Text);
                if (service.duyetPhieuXuat(phieuXuatId))
                {
                    LoadData();
                    textBox1.Text = "";
                    MessageBox.Show("Duyệt phiếu xuất kho thành công");
                }
                else
                {
                    MessageBox.Show("Duyệt phiếu xuất kho thất bại");
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Mã phiếu xuất không hợp lệ");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                MessageBox.Show("bạn chọn đơn có id: " + dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString());
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (nguoiDungService.kiemTraVaiTroAdmin())
            {
                MainForm mainForm = (MainForm)this.Parent!.Parent!;
                mainForm.LoadPage(new HuyDonXuatcs());
            }
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

        private void button3_Click(object sender, EventArgs e)
        {
            MainForm mainForm = (MainForm)this.Parent!.Parent!;
            mainForm.LoadPage(new DanhSachDonNhapKho());
        }

        private void dataGridView1_CellFormatting_1(object sender, DataGridViewCellFormattingEventArgs e)
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
    }
}
