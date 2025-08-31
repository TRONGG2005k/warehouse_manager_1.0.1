using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using warehouse_manager.context;
using warehouse_manager.dto.o;

namespace warehouse_manager.service
{
    internal class BaoCaoService

    {
        WarehouseManagerContext context = new WarehouseManagerContext();
        public List<BCNXT> baoCaoNXT(DateTime tu, DateTime den)
        {
            return context.VatLieus
            .Select(vl => new BCNXT
            {
                MaVatLieu = vl.MaVatLieu ?? "",
                TenVatLieu = vl.Ten ?? "",
                DonViTinh = vl.DonViTinh ?? "",
                TonDauKy = (vl.ChiTietPhieuNhaps
                            .Where(c => c.PhieuNhap!.NgayNhap < tu)
                            .Sum(c => c.SoLuong ?? 0))
                      - (vl.ChiTietPhieuXuats
                            .Where(c => c.PhieuXuat.NgayXuat < tu && c.PhieuXuat.TrangThai == "DA_DUYET")
                            .Sum(c => c.SoLuongThucXuat)),
                NhapTrongKy = vl.ChiTietPhieuNhaps
                                .Where(c => c.PhieuNhap!.NgayNhap >= tu && c.PhieuNhap.NgayNhap <= den)
                                .Sum(c => c.SoLuong ?? 0),
                XuatTrongKy = vl.ChiTietPhieuXuats
                                .Where(c => c.PhieuXuat.NgayXuat >= tu && c.PhieuXuat.NgayXuat <= den 
                                && c.PhieuXuat.TrangThai == "DA_DUYET")
                                .Sum(c => c.SoLuongThucXuat),
            })
            .AsEnumerable()
            .Select(dto =>
            {
                dto.TonCuoiKy = dto.TonDauKy + dto.NhapTrongKy - dto.XuatTrongKy;
                return dto;
            })
            .ToList();
        }

        public void XuatExcel(List<BCNXT> danhSach, string duongDan, DateTime tu, DateTime den)
        {
            using (var workbook = new XLWorkbook())
            {
                var ws = workbook.Worksheets.Add("BaoCaoNXT");

                // Ghi kỳ báo cáo ở dòng đầu
                ws.Cell(1, 1).Value = $"Báo cáo Nhập - Xuất - Tồn từ {tu:dd/MM/yyyy} đến {den:dd/MM/yyyy}";
                ws.Range(1, 1, 1, 7).Merge(); // gộp 7 cột
                ws.Cell(1, 1).Style.Font.Bold = true;
                ws.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                // Tiêu đề cột (dòng 2)
                ws.Cell(2, 1).Value = "Mã vật liệu";
                ws.Cell(2, 2).Value = "Tên vật liệu";
                ws.Cell(2, 3).Value = "Đơn vị tính";
                ws.Cell(2, 4).Value = "Tồn đầu kỳ";
                ws.Cell(2, 5).Value = "Nhập trong kỳ";
                ws.Cell(2, 6).Value = "Xuất trong kỳ";
                ws.Cell(2, 7).Value = "Tồn cuối kỳ";

                int row = 3; // bắt đầu dữ liệu từ dòng 3
                foreach (var item in danhSach)
                {
                    ws.Cell(row, 1).Value = item.MaVatLieu;
                    ws.Cell(row, 2).Value = item.TenVatLieu;
                    ws.Cell(row, 3).Value = item.DonViTinh;
                    ws.Cell(row, 4).Value = item.TonDauKy;
                    ws.Cell(row, 5).Value = item.NhapTrongKy;
                    ws.Cell(row, 6).Value = item.XuatTrongKy;
                    ws.Cell(row, 7).Value = item.TonCuoiKy;
                    row++;
                }

                ws.Columns().AdjustToContents(); // tự động điều chỉnh độ rộng cột
                workbook.SaveAs(duongDan);
            }
        }
    }
}
