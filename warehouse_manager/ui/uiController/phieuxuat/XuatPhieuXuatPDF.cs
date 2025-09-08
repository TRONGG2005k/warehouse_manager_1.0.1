using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using warehouse_manager.service;

namespace warehouse_manager.ui.uiController.phieuxuat
{
    public partial class XuatPhieuXuatPDF : UserControl
    {
        PrintDocument printDocument = new PrintDocument();
        private NguoiDungService nguoiDungService;
        public XuatPhieuXuatPDF()
        {
            nguoiDungService = new NguoiDungService();
            InitializeComponent();
            printDocument.PrintPage += PrintDocument_PrintPage;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var phieuService = new PhieuXuatService();
            var phieu = phieuService.TimTheoMa(int.Parse(dataGridView1.CurrentRow.Cells["Id"].Value.ToString()));
            if (phieu.FirstOrDefault().TrangThai != "Đã duyệt")
            {
                MessageBox.Show("Chỉ có thể in phiếu đã duyệt dong 32");
                return;
            }
            printDocument.DefaultPageSettings.PaperSize = new PaperSize("A4", 827, 1169);

            // Cấu hình lề (trái, phải, trên, dưới)
            printDocument.DefaultPageSettings.Margins = new Margins(50, 50, 50, 50);

            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
                MessageBox.Show("xuất file thành công" +
                    "");
            }
        }

        private void PrintDocument_PrintPage(object? sender, PrintPageEventArgs e)
        {
            Font fontNormal = new Font("Arial", 11);
            Font fontBold = new Font("Arial", 11, FontStyle.Bold);
            Font fontTitle = new Font("Arial", 16, FontStyle.Bold);

            float y = 50;
            float left = 50;
            float pageWidth = e.PageBounds.Width;

            var phieuService = new PhieuXuatService();
            var phieu = phieuService.TimTheoMa(int.Parse(dataGridView1.CurrentRow.Cells["Id"].Value.ToString()));
            if (phieu.FirstOrDefault().TrangThai != "Đã duyệt")
            {
                MessageBox.Show("Chỉ có thể in phiếu đã duyệt dong 63");
                return;
            }
            if (phieu.Count == 0)
            {
                e.Graphics.DrawString("Không tìm thấy phiếu xuất!", fontBold, Brushes.Black, left, y);
                return;
            }

            var header = phieu.First();

            // Tiêu đề căn giữa
            string title = "PHIẾU XUẤT KHO";
            SizeF titleSize = e.Graphics.MeasureString(title, fontTitle);
            float titleX = (pageWidth - titleSize.Width) / 2;
            e.Graphics.DrawString(title, fontTitle, Brushes.Black, titleX, y);
            y += 50;

            // Thông tin chung
            string[] headerInfo = {
                $"Số phiếu: {header.Id}",
                $"Ngày xuất: {header.NgayLap:dd/MM/yyyy}",
                $"Người lập: {header.NguoiLap}",
                $"Cơ sở sản xuất: {header.MaTruyenSanXuat}",
                $"Trạng thái: {header.TrangThai}",
                $"Ghi chú: {header.GhiChu}"
            };

            foreach (var line in headerInfo)
            {
                e.Graphics.DrawString(line, fontNormal, Brushes.Black, left, y);
                y += 25;
            }

            y += 15;

            // Chi tiết phiếu (mỗi thuộc tính 1 dòng)
            decimal tongTien = 0;
            foreach (var item in phieu)
            {
                tongTien += item.TongGiaTri ?? 0;

                e.Graphics.DrawString($"Mã vật liệu: {item.MaVatLieu}", fontNormal, Brushes.Black, left, y); y += 20;
                e.Graphics.DrawString($"Tên vật liệu: {item.TenVatLieu}", fontNormal, Brushes.Black, left, y); y += 20;
                e.Graphics.DrawString($"SL yêu cầu: {item.SoLuongYeuCau}", fontNormal, Brushes.Black, left, y); y += 20;
                e.Graphics.DrawString($"SL thực tế: {item.SoLuongThucTe}", fontNormal, Brushes.Black, left, y); y += 20;
                e.Graphics.DrawString($"Đơn vị tính: {item.DonViTinh}", fontNormal, Brushes.Black, left, y); y += 20;
                e.Graphics.DrawString($"Đơn giá: {item.DonGia:N0}", fontNormal, Brushes.Black, left, y); y += 20;
                e.Graphics.DrawString($"Tổng giá trị: {item.TongGiaTri:N0}", fontNormal, Brushes.Black, left, y); y += 30;

                // khoảng cách giữa 2 vật liệu
                y += 10;
            }

            // Tổng cộng
            e.Graphics.DrawString("TỔNG CỘNG: " + tongTien.ToString("N0") + " ₫",
                new Font("Arial", 12, FontStyle.Bold), Brushes.Black, left, y);

            // chữ ký
            y += 60;
            e.Graphics.DrawString("Người lập phiếu", fontNormal, Brushes.Black, left, y);
            e.Graphics.DrawString("Thủ kho", fontNormal, Brushes.Black, left + 250, y);
            e.Graphics.DrawString("Kế toán", fontNormal, Brushes.Black, pageWidth - 200, y);
        }

        private void XuatPhieuXuatPDF_Load(object sender, EventArgs e)
        {
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
            LoadData();
        }
        void LoadData()
        {
            var service = new service.PhieuXuatService();
            var phieuXuatDtos = service.LayTatCaPhieu();
            dataGridView1.DataSource = phieuXuatDtos;
        }
        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.SelectedItem == null)
                {
                    throw new Exception("Vui lòng chọn trạng thái để lọc");
                }
                if (comboBox1.Items.Contains(comboBox1.Text) == false)
                {
                    throw new Exception("Loại trạng thái này không tồn tại");
                }
                var service = new service.PhieuXuatService();
                var ps = service.TimTheoTrangThai(comboBox1.SelectedItem.ToString());
                dataGridView1.DataSource = ps;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
