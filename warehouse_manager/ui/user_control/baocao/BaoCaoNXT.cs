using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
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
using warehouse_manager.ui.user_control;
using warehouse_manager.ui.user_control.baocao;
using warehouse_manager.ui.user_control.tonKho;

namespace warehouse_manager.ui
{
    public partial class BaoCaoNXT : UserControl
    {
        BaoCaoService baoCao = new BaoCaoService();
        public BaoCaoNXT()
        {
            InitializeComponent();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = baoCao.baoCaoNXT(dateTimePicker1.Value, dateTimePicker2.Value);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            baoCao.XuatExcel(baoCao.baoCaoNXT(dateTimePicker1.Value, dateTimePicker2.Value),
                @"C:\Users\Hi ASUS TUF\Documents\BaoCaoNXT.xlsx", dateTimePicker1.Value, dateTimePicker2.Value);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            MainForm mainForm = (MainForm)this.Parent.Parent;
            mainForm.LoadPage(new BCKiemKe());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MainForm mainForm = (MainForm)this.Parent.Parent;
            mainForm.LoadPage(new KiemKe());
        }

        private void button4_Click(object sender, EventArgs e)
        {

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

        private void button6_Click(object sender, EventArgs e)
        {
            MainForm mainForm = (MainForm)this.Parent.Parent;
            mainForm.LoadPage(new DanhSachDonNhapKho());
        }
    }
}
