using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using warehouse_manager.context;
using warehouse_manager.dto;

namespace warehouse_manager.service
{
    internal class PhieuService
    {
        
        public List<PhieuDto> danhSachPhieu()
        {
            var context = new WarehouseManagerContext();
            var query = context.PhieuNhaps.Select(
                p => new PhieuDto
                {
                    Id = p.Id,
                    NgayLap = p.NgayNhap,
                    TongTien = p.TongTien,
                    NguoiLap = p.NguoiDung!.TenDangNhap!,
                    LoaiPhieu = "Phiếu nhập ⬅️",
                    TenHang = p.ChiTietPhieuNhaps!.FirstOrDefault()!.VatLieu!.Ten ?? "",
                    SoLuong = p.ChiTietPhieuNhaps!.FirstOrDefault()!.SoLuong ?? 0
                }
            ).Union(
                context.PhieuXuats.Select(
                    p => new PhieuDto
                    {
                        Id = p.Id,
                        NgayLap = p.NgayXuat,
                        TongTien = p.TongTien,
                        NguoiLap = p.NguoiDung!.TenDangNhap!,
                        LoaiPhieu = "Phiếu xuất ➡️",
                        TenHang = p.ChiTietPhieuXuats!.FirstOrDefault()!.SanPham!.Ten ?? "",
                        SoLuong = p.ChiTietPhieuXuats!.FirstOrDefault()!.SoLuong ?? 0
                    }
                )
            ).OrderBy(p => p.NgayLap); ;

           
            return query.ToList();
        }

        public List<PhieuDto> danhSachPhieuNhap()
        {
            var context = new WarehouseManagerContext();
            var query = context.PhieuNhaps.Select(
                p => new PhieuDto
                {
                    Id = p.Id,
                    NgayLap = p.NgayNhap,
                    TongTien = p.TongTien,
                    NguoiLap = p.NguoiDung!.TenDangNhap!,
                    LoaiPhieu = "Phiếu nhập ⬅️",
                    TenHang = p.ChiTietPhieuNhaps!.FirstOrDefault()!.VatLieu!.Ten ?? "",
                    SoLuong = p.ChiTietPhieuNhaps!.FirstOrDefault()!.SoLuong ?? 0
                }
            ).OrderBy(p => p.NgayLap); ;
           
            return query.ToList();
        }

        public List<PhieuDto> danhSachPhieuXuat()
        {
            var context = new WarehouseManagerContext();
            var query = context.PhieuXuats.Select(
                p => new PhieuDto
                {
                    Id = p.Id,
                    NgayLap = p.NgayXuat,
                    TongTien = p.TongTien,
                    NguoiLap = p.NguoiDung!.TenDangNhap!,
                    LoaiPhieu = "Phiếu xuất ➡️",
                    TenHang = p.ChiTietPhieuXuats!.FirstOrDefault()!.SanPham!.Ten ?? "",
                    SoLuong = p.ChiTietPhieuXuats!.FirstOrDefault()!.SoLuong ?? 0
                }
            ).OrderBy(p => p.NgayLap); ;
           
            return query.ToList();
        }
    }
}
