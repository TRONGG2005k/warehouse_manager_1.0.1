using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using warehouse_manager.service;
using warehouse_manager.ui.user_control.tonKho;

namespace warehouse_manager.ui.user_control
{
    public partial class DanhSachDonNhapKho : UserControl
    {
        public DanhSachDonNhapKho()
        {
            InitializeComponent();
            this.BackColor = Color.AliceBlue;
        }

        public void Dashboard_Load(object sender, EventArgs e)
        {
            var service = new service.PhieuNhapService();
            var serviceXuat = new service.PhieuXuatService();
            var phieuNhapDtos = service.phieuNhapDtos();
            dataGridView1.DataSource = phieuNhapDtos;
            var phieuXuatDtos = serviceXuat.LayTatCaPhieu();
            dataGridView2.DataSource = phieuXuatDtos;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MainForm mainForm = (MainForm)this.Parent!.Parent!;
            mainForm.LoadPage(new DanhSachNhapKho());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MainForm mainForm = (MainForm)this.Parent!.Parent!;
            mainForm.LoadPage(new DanhSachXuatKho());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var nguoiDungService = new NguoiDungService();
            nguoiDungService.logout();
            MainForm mainForm = (MainForm)this.Parent!.Parent!;
            mainForm.LoadPage(new Login());
        }

        private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView2.Columns[e.ColumnIndex].Name == "DonGia" && e.Value != null)
            {
                decimal donGia = (decimal)e.Value;
                e.Value = donGia.ToString("N0") + " ₫";
                e.FormattingApplied = true;
            }
            if (dataGridView2.Columns[e.ColumnIndex].Name == "TongGiaTri" && e.Value != null)
            {
                decimal donGia = (decimal)e.Value;
                e.Value = donGia.ToString("N0") + " ₫";
                e.FormattingApplied = true;
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
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MainForm mainForm = (MainForm)this.Parent!.Parent!;
            mainForm.LoadPage(new BaoCaoNXT());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MainForm mainForm = (MainForm)this.Parent!.Parent!;
            mainForm.LoadPage(new KiemKe());
        }
    }
}
