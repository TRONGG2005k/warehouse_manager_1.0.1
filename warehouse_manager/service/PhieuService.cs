using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using warehouse_manager.context;
using warehouse_manager.dto;
using warehouse_manager.Models;

namespace warehouse_manager.service
{
    internal class PhieuService
    {
        private WarehouseManagerContext context;
        public PhieuService()
        {
            context = new WarehouseManagerContext();
        }
        public List<PhieuDto> danhSachPhieu()
        {
            
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

        public List<PhieuNhapDto> phieuNhapDtos()
        {
            return context.PhieuNhaps
                .Include(pn => pn.MaNhaCungCapNavigation)
                .Include(pn => pn.ChiTietPhieuNhaps)
                    .ThenInclude(ct => ct.VatLieu)
                .SelectMany(pn => pn.ChiTietPhieuNhaps.Select(ct => new PhieuNhapDto
                {
                    Id = pn.Id,
                    NgayNhap = pn.NgayNhap,
                    MaNguoiLap = pn.NguoiDung!.TenDangNhap!,
                    TenNhaCungCap = pn.MaNhaCungCapNavigation!.TenNhaCungCap!,
                    MaVatLieu = ct.VatLieu!.MaVatLieu!,
                    SoLuong = ct.SoLuong ?? 0,
                    DonGia = ct.DonGia ?? 0,
                    DonViTinh = ct.DonViTinh ?? "",
                    
                })).ToList();
            
        }

        public void themPhieuNhap(TaoPhieuNhapKhoDto taoPhieuNhap)
        {

            try
            {
                var loaiVatLieuTonTai = context.LoaiVatLieus
                .FirstOrDefault(l => l.TenLoai == taoPhieuNhap.LoaiVatLieu)
                ?? throw new Exception("Loại vật liệu không tồn tại");

                var nhaCungCapTonTai = context.NhaCungCaps
                    .FirstOrDefault(ncc => ncc.TenNhaCungCap == taoPhieuNhap.NhaCungCap)
                    ?? throw new Exception("Nhà cung cấp không tồn tại");
                var keTonTai = context.Kes
                  .FirstOrDefault(k => k.MaKe == taoPhieuNhap.Make)
                  ?? throw new Exception("kệ không tồn tại");

                var vatLieuTonTai = context.VatLieus
                    .FirstOrDefault(v => v.MaVatLieu == taoPhieuNhap.MaVatLieu);


                if (vatLieuTonTai == null)
                {
                    var vatLieu = context.VatLieus.Add(new Models.VatLieu
                    {
                        MaVatLieu = taoPhieuNhap.MaVatLieu,
                        Ten = taoPhieuNhap.TenVatLieu,
                        MaLoai = loaiVatLieuTonTai.Id,
                        SoLuongTon = taoPhieuNhap.SoLuong,
                        DonGia = taoPhieuNhap.DonGia,
                        DonViTinh = taoPhieuNhap.DonViTinh,
                        TrangThai = "Còn hàng",
                        MaNhaCungCap = nhaCungCapTonTai.Id

                    });

                    context.SaveChanges();

                    var vatLieu1 = context.VatLieus.First(v => v.MaVatLieu == taoPhieuNhap.MaVatLieu);
                    if (!vatLieu1.Kes.Contains(keTonTai))
                    {
                        vatLieu1.Kes.Add(keTonTai);
                        context.SaveChanges();
                    }

                }
                else
                {
                    vatLieuTonTai.SoLuongTon += taoPhieuNhap.SoLuong;
                    context.SaveChanges();
                }

                // 3. Đọc người dùng từ file
                string content = File.Exists("user.txt") ? File.ReadAllText("user.txt") : "";
                var nguoiDung = context.NguoiDungs.FirstOrDefault(n => n.TenDangNhap == content)
                    ?? throw new Exception("Người dùng không tồn tại");

                // 4. Thêm phiếu nhập
                var phieuNhap = context.PhieuNhaps.Add(new Models.PhieuNhap
                {
                    NgayNhap = DateTime.Now,
                    NguoiDungId = nguoiDung.Id,
                    MaNhaCungCap = nhaCungCapTonTai.Id,
                    TongTien = taoPhieuNhap.SoLuong * taoPhieuNhap.DonGia
                });
                context.SaveChanges();

                // 5. Thêm chi tiết phiếu nhập
                var chiTietPhieuNhap = context.ChiTietPhieuNhaps.Add(new Models.ChiTietPhieuNhap
                {
                    SoLuong = taoPhieuNhap.SoLuong,
                    DonGia = taoPhieuNhap.DonGia,
                    DonViTinh = taoPhieuNhap.DonViTinh,
                    ThanhTien = taoPhieuNhap.SoLuong * taoPhieuNhap.DonGia,
                    PhieuNhapId = phieuNhap.Entity.Id,
                    VatLieuId = context.VatLieus.First(v => v.MaVatLieu == taoPhieuNhap.MaVatLieu).Id
                });
                context.SaveChanges();

                MessageBox.Show("Thêm phiếu nhập thành công");
            }
            catch (Exception ex)
            {
                throw new Exception("Thêm phiếu nhập thất bại: " + ex.Message);
            }
        }
    }
}
