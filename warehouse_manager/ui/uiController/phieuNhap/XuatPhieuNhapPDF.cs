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

namespace warehouse_manager.ui.uiController.phieuNhap
{
    public partial class XuatPhieuNhapPDF : UserControl
    {
        PrintDocument printDocument = new PrintDocument();
        private NguoiDungService nguoiDungService;
        public XuatPhieuNhapPDF()
        {
            nguoiDungService = new NguoiDungService();
            InitializeComponent();
            printDocument.PrintPage += PrintDocument_PrintPage;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            printDocument.DefaultPageSettings.PaperSize = new PaperSize("A4", 827, 1169);

            // Cấu hình lề (trái, phải, trên, dưới)
            printDocument.DefaultPageSettings.Margins = new Margins(50, 50, 50, 50);

            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }

        private void XuatPhieuNhapPDF_Load(object sender, EventArgs e)
        {
            var service = new service.PhieuNhapService();
            var phieuNhapDtos = service.phieuNhapDtos();


            dataGridView1.DataSource = phieuNhapDtos;

            //dataGridView1.Columns["NgayNhap"].HeaderText = "Ngày Nhập";
            //dataGridView1.Columns["MaNguoiLap"].HeaderText = "Người Lập";
            //dataGridView1.Columns["TenNhaCungCap"].HeaderText = "Tên Nhà Cung Cấp";
            //dataGridView1.Columns["MaVatLieu"].HeaderText = "Mã Vật Liệu";
            //dataGridView1.Columns["SoLuong"].HeaderText = "Số Lượng";
            //dataGridView1.Columns["DonGia"].HeaderText = "Đơn Giá";
            //dataGridView1.Columns["DonViTinh"].HeaderText = "Đơn Vị Tính";
            //dataGridView1.Columns["ThanhTien"].HeaderText = "Thành Tiền";
        }
        private void PrintDocument_PrintPage(object? sender, PrintPageEventArgs e)
        {
            // --- Định nghĩa font ---
            Font fontNormal = new Font("Arial", 11);
            Font fontBold = new Font("Arial", 11, FontStyle.Bold);
            Font fontTitle = new Font("Arial", 16, FontStyle.Bold);

            float y = 50;
            float left = 50;
            float pageWidth = e.PageBounds.Width;

            // --- Lấy dữ liệu phiếu nhập ---
            var phieuNhapService = new PhieuNhapService();
            var phieuNhapDtos = phieuNhapService.TimPhieuTheoMa(int.Parse(
                dataGridView1.CurrentRow.Cells["Id"].Value.ToString()));

            if (phieuNhapDtos.Count == 0)
            {
                e.Graphics.DrawString("Không tìm thấy phiếu nhập!", fontBold, Brushes.Black, left, y);
                return;
            }

            var header = phieuNhapDtos.First();

            // --- Tiêu đề phiếu (căn giữa) ---
            string title = "PHIẾU NHẬP KHO";
            SizeF titleSize = e.Graphics.MeasureString(title, fontTitle);
            float titleX = (pageWidth - titleSize.Width) / 2;
            e.Graphics.DrawString(title, fontTitle, Brushes.Black, titleX, y);
            y += 50;

            // --- Thông tin chung ---
            string[] headerInfo ={
                $"Số phiếu: {header.Id}",
                $"Ngày nhập: {header.NgayNhap:dd/MM/yyyy}",
                $"Nhà cung cấp: {header.NhaCungCap}"
            };

            foreach (var line in headerInfo)
            {
                e.Graphics.DrawString(line, fontNormal, Brushes.Black, left, y);
                y += 25;
            }
            y += 15;

            // --- Bảng chi tiết ---
            int col1 = 50, col2 = 200, col3 = 350, col4 = 500, col5 = 700;
            decimal tongTien = 0;

            foreach (var item in phieuNhapDtos)
            {
                decimal thanhTien = item.SoLuong * item.DonGia;
                tongTien += thanhTien;

                string[] itemInfo =
                {
                    $"Tên hàng: {item.TenHang}",
                    $"Số lượng: {item.SoLuong}",
                    $"Đơn giá: {item.DonGia:N0}",
                    $"Đơn vị tính: {item.DonViTinh}",
                    $"Thành tiền: {thanhTien:N0}"
                };

                foreach (var line in itemInfo)
                {
                    e.Graphics.DrawString(line, fontNormal, Brushes.Black, col1, y);
                    y += 20;
                }

                y += 10;
                e.Graphics.DrawLine(Pens.Black, col1, y, 750, y); // gạch phân cách
                y += 20;
            }

            e.Graphics.DrawString("TỔNG CỘNG: " + tongTien.ToString("N0") + " ₫",
                new Font("Arial", 12, FontStyle.Bold), Brushes.Black, col4, y);


            y += 60;
            e.Graphics.DrawString("Người lập phiếu", fontNormal, Brushes.Black, col1, y);
            e.Graphics.DrawString("Thủ kho", fontNormal, Brushes.Black, col3, y);
            e.Graphics.DrawString("Kế toán", fontNormal, Brushes.Black, col5, y);
        }
    }
}
