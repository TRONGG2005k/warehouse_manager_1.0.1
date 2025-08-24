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
    public partial class DanhSachNhapKho : UserControl
    {
        public DanhSachNhapKho()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // TODO: xử lý khi nhấn button5
        }
        private void button2_Click(object sender, EventArgs e)
        {
            // TODO: xử lý khi nhấn button5
            MainForm mainForm = (MainForm)this.Parent!.Parent!;
            mainForm.LoadPage(new Dashboard());
        }

        private void DanhSachNhapKho_Load(object sender, EventArgs e)
        {
            var service = new service.PhieuService();
            var phieuNhapDtos = service.phieuNhapDtos();
            dataGridView1.DataSource = phieuNhapDtos;

            dataGridView1.Columns["NgayNhap"].HeaderText = "Ngày Nhập";
            dataGridView1.Columns["MaNguoiLap"].HeaderText = "Người Lập";
            dataGridView1.Columns["TenNhaCungCap"].HeaderText = "Tên Nhà Cung Cấp";
            dataGridView1.Columns["MaVatLieu"].HeaderText = "Mã Vật Liệu";
            dataGridView1.Columns["SoLuong"].HeaderText = "Số Lượng";
            dataGridView1.Columns["DonGia"].HeaderText = "Đơn Giá";
            dataGridView1.Columns["DonViTinh"].HeaderText = "Đơn Vị Tính";
            dataGridView1.Columns["ThanhTien"].HeaderText = "Thành Tiền";

        }

        private void button7_Click(object sender, EventArgs e)
        {
            MainForm mainForm = (MainForm)this.Parent!.Parent!;
            mainForm.LoadPage(new TaoDonNhapKho());
        }
    }
}
