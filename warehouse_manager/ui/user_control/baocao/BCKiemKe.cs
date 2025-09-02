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
using warehouse_manager.dto.o;
using warehouse_manager.service;
using warehouse_manager.ui.user_control.tonKho;

namespace warehouse_manager.ui.user_control.baocao
{
    public partial class BCKiemKe : UserControl
    {
        private WarehouseManagerContext context = new WarehouseManagerContext();
        private BaoCaoService baoCao = new BaoCaoService();
        public BCKiemKe()
        {
            InitializeComponent();
        }
        private string tam;
        private void button10_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = context.PhieuKiemKes
              .Where(kk => kk.NgayKiemKe >= dateTimePicker1.Value && kk.NgayKiemKe <= dateTimePicker2.Value)
              .Select(kk => new PhieuKiemKeDto
              {
                  MaPhieuKiemKe = kk.MaPhieu ?? "",
                  NgayKiemKe = kk.NgayKiemKe,
                  NguoiTao = kk.NguoiKiemKe ?? "",
                  GhiChu = kk.GhiChu ?? ""
              })
              .ToList();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            baoCao.XuatExcel(baoCao.bCKienKe(tam),
                @"C:\Users\Hi ASUS TUF\Documents\BaoCaoKiemKe.xlsx", dateTimePicker1.Value, dateTimePicker2.Value);
        }

        private void BCKiemKe_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = context.PhieuKiemKes
                .Where(kk => kk.NgayKiemKe >= dateTimePicker1.Value && kk.NgayKiemKe <= dateTimePicker2.Value)
                .ToList();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null &&
                 dataGridView1.CurrentRow.Cells["MaPhieuKiemKe"] != null &&
                dataGridView1.CurrentRow.Cells["MaPhieuKiemKe"].Value != null)
            {
                string maPhieu = dataGridView1.CurrentRow.Cells["MaPhieuKiemKe"].Value.ToString();
                tam = maPhieu;
                dataGridView1.DataSource = baoCao.bCKienKe(maPhieu);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn phiếu kiểm kê trước khi xem chi tiết.");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MainForm mainForm = (MainForm)this.Parent.Parent;
            mainForm.LoadPage(new DanhSachDonNhapKho());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MainForm mainForm = (MainForm)this.Parent.Parent;
            mainForm.LoadPage(new KiemKe());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MainForm mainForm = (MainForm)this.Parent.Parent;
            mainForm.LoadPage(new DanhSachXuatKho());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MainForm mainForm = (MainForm)this.Parent.Parent;
            mainForm.LoadPage(new DanhSachNhapKho());
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MainForm mainForm = (MainForm)this.Parent.Parent;
            mainForm.LoadPage(new BaoCaoNXT());
        }
    }
}
