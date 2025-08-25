using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using warehouse_manager.context;
using warehouse_manager.dto.o;
using warehouse_manager.dto.i;
using warehouse_manager.Models;
using warehouse_manager.ui.user_control;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

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

        public List<dto.i.PhieuNhapDto> phieuNhapDtos()
        {
            return context.PhieuNhaps
                .Include(pn => pn.MaNhaCungCapNavigation)
                .Include(pn => pn.ChiTietPhieuNhaps)
                    .ThenInclude(ct => ct.VatLieu)
                .SelectMany(pn => pn.ChiTietPhieuNhaps.Select(ct => new dto.i.PhieuNhapDto
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

        public void themPhieuNhap(dto.i.TaoPhieuNhapKhoDto taoPhieuNhap)
        {

            try
            {
                if(taoPhieuNhap.HasNullOrEmptyProperties())
                {
                    MessageBox.Show("vui lòng nhập đủ thông tìn");
                    throw new Exception("vui lòng nhập đủ thông tìn");
                }
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

               
                var phieuNhap = context.PhieuNhaps.Add(new Models.PhieuNhap
                {
                    NgayNhap = DateTime.Now,
                    NguoiDungId = nguoiDung.Id,
                    MaNhaCungCap = nhaCungCapTonTai.Id,
                    TongTien = taoPhieuNhap.SoLuong * taoPhieuNhap.DonGia
                });
                context.SaveChanges();

                
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

        private void suaPhieuNhap(SuaPhieuNhapDto suaPhieuNhap)
        {
            try
            {
                var phieuNhap = context.PhieuNhaps
                    .Include(pn => pn.ChiTietPhieuNhaps)
                    .First(pn => pn.Id == suaPhieuNhap.Id)
                    ?? throw new Exception("Phiếu nhập không tồn tại");
                var chiTietPhieuNhap = phieuNhap.ChiTietPhieuNhaps.First();
                var vatLieu = chiTietPhieuNhap!.VatLieu;

                string content = File.Exists("user.txt") ? File.ReadAllText("user.txt") : "";
                var nguoiDung = context.NguoiDungs.FirstOrDefault(n => n.TenDangNhap == content)
                    ?? throw new Exception("Người dùng không tồn tại");

                phieuNhap.NguoiDungId = nguoiDung.Id;
                phieuNhap.MaNhaCungCap = context.NhaCungCaps
                    .First(ncc => ncc.TenNhaCungCap == suaPhieuNhap.NhaCungCap).Id;
                phieuNhap.TongTien = suaPhieuNhap.SoLuong * suaPhieuNhap.DonGia;

                if (suaPhieuNhap.MaVatLieu != vatLieu!.MaVatLieu)
                {
                   
                    var vatLieuTonTai2 = context.VatLieus
                        .First(v => v.MaVatLieu == suaPhieuNhap.MaVatLieu);

                    
                    if(vatLieuTonTai2 != null)
                    {
                        vatLieuTonTai2.SoLuongTon += suaPhieuNhap.SoLuong;
                        chiTietPhieuNhap.VatLieuId = vatLieuTonTai2.Id;
                    }
                    else
                    {
                        vatLieu.DonGia = suaPhieuNhap.DonGia;
                        vatLieu.DonViTinh = suaPhieuNhap.DonViTinh;
                        vatLieu.MaVatLieu = suaPhieuNhap.MaVatLieu;
                        vatLieu.Ten = suaPhieuNhap.TenVatLieu;
                        vatLieu.SoLuongTon = suaPhieuNhap.SoLuong;
                        vatLieu.TrangThai = "Còn hàng";
                        vatLieu.MaNhaCungCap = context.NhaCungCaps
                            .First(ncc => ncc.TenNhaCungCap == suaPhieuNhap.NhaCungCap).Id;
                        vatLieu.MaLoai = context.LoaiVatLieus
                            .First(l => l.TenLoai == suaPhieuNhap.LoaiVatLieu).Id;
                    }
                }
                else
                {

                    int chenhLech = (int)chiTietPhieuNhap.SoLuong - suaPhieuNhap.SoLuong;
                    vatLieu.SoLuongTon += chenhLech;
                }
                chiTietPhieuNhap.SoLuong = suaPhieuNhap.SoLuong;
                chiTietPhieuNhap.DonGia = suaPhieuNhap.DonGia;
                chiTietPhieuNhap.DonViTinh = suaPhieuNhap.DonViTinh;
                chiTietPhieuNhap.ThanhTien = suaPhieuNhap.SoLuong * suaPhieuNhap.DonGia;



                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Sửa phiếu nhập thất bại: " + ex.Message);
            }
        }
    }
}
