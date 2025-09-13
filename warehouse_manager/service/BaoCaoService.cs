using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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

        public List<BCKienKeDto> bCKienKe()
        {
            return context.PhieuKiemKes
                .SelectMany(kk => kk.ChiTietPhieuKiemKes, (kk, ct) => new BCKienKeDto
                {
         
                    MaVatLieu = ct.VatLieu.MaVatLieu ?? "",
                    TenVatLieu = ct.VatLieu.Ten ?? "",
                    DonViTinh = ct.VatLieu.DonViTinh ?? "",
                    TonHeThong = ct.TonHeThong,
                    TonThucTe = ct.TonThucTe,
                    GhiChu = kk.GhiChu ?? ""
                })
                .ToList();
        }

        public List<BCKienKeDto> bCKienKe(string maphieu)
        {
            return context.PhieuKiemKes
                .Where(kk => kk.MaPhieu == maphieu)
                .SelectMany(kk => kk.ChiTietPhieuKiemKes, (kk, ct) => new BCKienKeDto
                {

                    MaVatLieu = ct.VatLieu.MaVatLieu ?? "",
                    TenVatLieu = ct.VatLieu.Ten ?? "",
                    DonViTinh = ct.VatLieu.DonViTinh ?? "",
                    TonHeThong = ct.TonHeThong,
                    TonThucTe = ct.TonThucTe,
                    GhiChu = kk.GhiChu ?? ""
                })
                .ToList();
        }
        

        public void XuatExcel<T>(
            List<T> danhSach,
            string filePath,
            DateTime tuNgay,
            DateTime denNgay,
            string tieuDeBaoCao,
            string tienToSheet,
            string[] headers,
            Action<IXLWorksheet, T, int> mapRow // hàm ánh xạ dữ liệu từng dòng
        )
        {
            try
            {
                XLWorkbook workbook;
                if (System.IO.File.Exists(filePath))
                    workbook = new XLWorkbook(filePath);
                else
                    workbook = new XLWorkbook();

                string sheetName = $"{tienToSheet}_{tuNgay:ddMMyyyy}_{denNgay:ddMMyyyy}";
                int count = 1;
                while (workbook.Worksheets.Any(ws => ws.Name == sheetName))
                {
                    sheetName = $"{tienToSheet}_{tuNgay:ddMMyyyy}_{denNgay:ddMMyyyy}_{count++}";
                }

                var ws = workbook.Worksheets.Add(sheetName);

                ws.Cell(1, 1).Value = tieuDeBaoCao;
                ws.Range(1, 1, 1, headers.Length).Merge();
                ws.Cell(1, 1).Style.Font.Bold = true;
                ws.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                
                ws.Cell(2, 1).Value = $"Từ ngày: {tuNgay:dd/MM/yyyy}  đến ngày: {denNgay:dd/MM/yyyy}";
                ws.Range(2, 1, 2, headers.Length).Merge();
                ws.Cell(2, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                
                for (int i = 0; i < headers.Length; i++)
                {
                    ws.Cell(4, i + 1).Value = headers[i];
                    ws.Cell(4, i + 1).Style.Font.Bold = true;
                    ws.Cell(4, i + 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                }

                
                int row = 5;
                foreach (var item in danhSach)
                {
                    mapRow(ws, item, row);
                    row++;
                }

                ws.Columns().AdjustToContents();
                workbook.SaveAs(filePath);

                MessageBox.Show("Xuất Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất Excel: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
