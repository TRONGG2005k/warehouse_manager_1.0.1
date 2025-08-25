using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace warehouse_manager.ui.user_control
{
    public partial class DanhSachDonNhapKho : UserControl
    {
        public DanhSachDonNhapKho()
        {
            InitializeComponent();
            this.BackColor = Color.AliceBlue;
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            var service = new service.PhieuService();
            var phieuNhapDtos = service.danhSachPhieuNhap();
            dataGridView1.DataSource = phieuNhapDtos;
            var phieuXuatDtos = service.danhSachPhieuXuat();
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

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainForm mainForm = (MainForm)this.Parent!.Parent!;
            mainForm.LoadPage(new Login());
        }
    }
}
