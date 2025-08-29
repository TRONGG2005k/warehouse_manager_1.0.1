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
    internal class PhieuNhapService
    {
        private WarehouseManagerContext context;
        public PhieuNhapService()
        {
            context = new WarehouseManagerContext();
        }


        // hàm select phiếu nhập và xuất
        

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
            ).OrderByDescending(p => p.NgayLap); ;

            return query.ToList();
        }

        

        public List<dto.o.PhieuNhapDto> phieuNhapResponse()
        {
            return context.PhieuNhaps.Select(
                
                p => new dto.o.PhieuNhapDto
                {
                    Id = p.Id,
                    LoaiVatLieu = p.ChiTietPhieuNhaps.First()!.VatLieu.MaLoaiNavigation.TenLoai,
                    TenHang = p.ChiTietPhieuNhaps!.FirstOrDefault()!.VatLieu!.Ten ,
                    DonViTinh = p.ChiTietPhieuNhaps!.FirstOrDefault()!.DonViTinh ,
                    SoLuong = p.ChiTietPhieuNhaps!.FirstOrDefault()!.SoLuong ?? 0,
                    DonGia = p.ChiTietPhieuNhaps!.FirstOrDefault()!.DonGia ?? 0,
                    NhaCungCap = p.MaNhaCungCapNavigation!.TenNhaCungCap ?? "",
                    Ke = p.ChiTietPhieuNhaps.First().VatLieu.Kes.First()!.MaKe,
                    NgayNhap = p.NgayNhap,
                    MaVatLieu = p.ChiTietPhieuNhaps!.FirstOrDefault()!.VatLieu!.MaVatLieu,

                }
            ).OrderByDescending(p => p.NgayNhap).ToList();

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

                })).OrderByDescending(p=> p.NgayNhap).ToList();

        }

        public List<dto.o.PhieuNhapDto> TimPhieuTheoMa(int id)
        {
            return context.PhieuNhaps
                .Where(pn => pn.Id == id)
                .Include(pn => pn.MaNhaCungCapNavigation)
                .Include(pn => pn.ChiTietPhieuNhaps)
                    .ThenInclude(ct => ct.VatLieu)
                        .ThenInclude(v => v.Kes)
                .SelectMany(pn => pn.ChiTietPhieuNhaps, (pn, ct) => new { pn, ct })
                .SelectMany(x => x.ct.VatLieu.Kes.DefaultIfEmpty(), (x, k) => new dto.o.PhieuNhapDto
                {
                    Id = x.pn.Id,
                    TenHang = x.ct.VatLieu.Ten,
                    DonViTinh = x.ct.VatLieu.DonViTinh,
                    SoLuong = (long)x.ct.SoLuong,
                    DonGia = x.ct.DonGia ?? 0,
                    NhaCungCap = x.pn.MaNhaCungCapNavigation.TenNhaCungCap,
                    Ke = k != null ? k.MaKe : null,  // Nếu vật liệu không có kệ, vẫn ra 1 bản ghi với null
                    NgayNhap = x.pn.NgayNhap,
                    MaVatLieu = x.ct.VatLieu.MaVatLieu,
                    LoaiVatLieu = x.ct.VatLieu.MaLoaiNavigation.TenLoai

                }).OrderByDescending(p => p.NgayNhap).ToList();

        }
        public List<dto.o.PhieuNhapDto> TimPhieuTheoKhoangThoiGian(LocTheoNgayDto locTheoNgay)
        {
            
            return context.PhieuNhaps
                .Where(pn => pn.NgayNhap >= locTheoNgay.Start && pn.NgayNhap <= locTheoNgay.End)
                .Include(pn => pn.MaNhaCungCapNavigation)
                .Include(pn => pn.ChiTietPhieuNhaps)
                    .ThenInclude(ct => ct.VatLieu)
                        .ThenInclude(v => v.Kes)
                .SelectMany(pn => pn.ChiTietPhieuNhaps, (pn, ct) => new { pn, ct })
                .SelectMany(x => x.ct.VatLieu.Kes.DefaultIfEmpty(), (x, k) => new dto.o.PhieuNhapDto
                {
                    Id = x.pn.Id,
                    TenHang = x.ct.VatLieu.Ten,
                    DonViTinh = x.ct.VatLieu.DonViTinh,
                    SoLuong = (long)x.ct.SoLuong,
                    DonGia = x.ct.DonGia ?? 0,
                    NhaCungCap = x.pn.MaNhaCungCapNavigation.TenNhaCungCap,
                    Ke = k != null ? k.MaKe : null,  // Nếu vật liệu không có kệ, vẫn ra 1 bản ghi với null
                    NgayNhap = x.pn.NgayNhap,
                    MaVatLieu = x.ct.VatLieu.MaVatLieu,
                    LoaiVatLieu = x.ct.VatLieu.MaLoaiNavigation.TenLoai

                }).OrderByDescending(p => p.NgayNhap).ToList();

        }

        public List<dto.o.PhieuNhapDto> TimPhieuTheoMaLieu(string ma)
        {
            return context.PhieuNhaps
                
                .Include(pn => pn.MaNhaCungCapNavigation)
                .Include(pn => pn.ChiTietPhieuNhaps)
                    .ThenInclude(ct => ct.VatLieu)
                        .ThenInclude(v => v.Kes)
                .SelectMany(pn => pn.ChiTietPhieuNhaps, (pn, ct) => new { pn, ct })
                .Where(x => x.ct.VatLieu.MaVatLieu == ma)
                .SelectMany(x => x.ct.VatLieu.Kes.DefaultIfEmpty(), (x, k) => new dto.o.PhieuNhapDto
                {
                    Id = x.pn.Id,
                    TenHang = x.ct.VatLieu.Ten,
                    DonViTinh = x.ct.VatLieu.DonViTinh,
                    SoLuong = (long)x.ct.SoLuong,
                    DonGia = x.ct.DonGia ?? 0,
                    NhaCungCap = x.pn.MaNhaCungCapNavigation.TenNhaCungCap,
                    Ke = k != null ? k.MaKe : null,  // Nếu vật liệu không có kệ, vẫn ra 1 bản ghi với null
                    NgayNhap = x.pn.NgayNhap,
                    MaVatLieu = x.ct.VatLieu.MaVatLieu,
                    LoaiVatLieu = x.ct.VatLieu.MaLoaiNavigation.TenLoai

                }).OrderByDescending(p => p.NgayNhap).ToList();

        }

        public List<dto.o.PhieuNhapDto> TimPhieuTheoLoaiVatLieu(string tenLoai)
        {
            return context.PhieuNhaps

                .Include(pn => pn.MaNhaCungCapNavigation)
                .Include(pn => pn.ChiTietPhieuNhaps)
                    .ThenInclude(ct => ct.VatLieu)
                        .ThenInclude(v => v.Kes)
                .SelectMany(pn => pn.ChiTietPhieuNhaps, (pn, ct) => new { pn, ct })
                .Where(x => x.ct.VatLieu.MaLoaiNavigation.TenLoai == tenLoai)
                .SelectMany(x => x.ct.VatLieu.Kes.DefaultIfEmpty(), (x, k) => new dto.o.PhieuNhapDto
                {
                    Id = x.pn.Id,
                    TenHang = x.ct.VatLieu.Ten,
                    DonViTinh = x.ct.VatLieu.DonViTinh,
                    SoLuong = (long)x.ct.SoLuong,
                    DonGia = x.ct.DonGia ?? 0,
                    NhaCungCap = x.pn.MaNhaCungCapNavigation.TenNhaCungCap,
                    Ke = k != null ? k.MaKe : null,  // Nếu vật liệu không có kệ, vẫn ra 1 bản ghi với null
                    NgayNhap = x.pn.NgayNhap,
                    MaVatLieu = x.ct.VatLieu.MaVatLieu,
                    LoaiVatLieu = x.ct.VatLieu.MaLoaiNavigation.TenLoai

                }).OrderByDescending(p => p.NgayNhap).ToList();

        }
        public List<dto.o.PhieuNhapDto> TimPhieuTheoTenNcc(string tenNcc)
        {
            return context.PhieuNhaps

                .Include(pn => pn.MaNhaCungCapNavigation)
                .Include(pn => pn.ChiTietPhieuNhaps)
                    .ThenInclude(ct => ct.VatLieu)
                        .ThenInclude(v => v.Kes)
                .SelectMany(pn => pn.ChiTietPhieuNhaps, (pn, ct) => new { pn, ct })
                .Where(x => x.ct.VatLieu.MaNhaCungCapNavigation.TenNhaCungCap == tenNcc)
                .SelectMany(x => x.ct.VatLieu.Kes.DefaultIfEmpty(), (x, k) => new dto.o.PhieuNhapDto
                {
                    Id = x.pn.Id,
                    TenHang = x.ct.VatLieu.Ten,
                    DonViTinh = x.ct.VatLieu.DonViTinh,
                    SoLuong = (long)x.ct.SoLuong,
                    DonGia = x.ct.DonGia ?? 0,
                    NhaCungCap = x.pn.MaNhaCungCapNavigation.TenNhaCungCap,
                    Ke = k != null ? k.MaKe : null,  // Nếu vật liệu không có kệ, vẫn ra 1 bản ghi với null
                    NgayNhap = x.pn.NgayNhap,
                    MaVatLieu = x.ct.VatLieu.MaVatLieu,
                    LoaiVatLieu = x.ct.VatLieu.MaLoaiNavigation.TenLoai

                }).OrderByDescending(p => p.NgayNhap).ToList();

        }
        // chức năng không liên quan đến select
        public void themPhieuNhap(dto.i.TaoPhieuNhapKhoDto taoPhieuNhap)
        {

            try
            {
               
                var loaiVatLieuTonTai = context.LoaiVatLieus
                .FirstOrDefault(l => l.TenLoai == taoPhieuNhap.LoaiVatLieu)
                ?? throw new Exception("Loại vật liệu không tồn tại");
                //MessageBox.Show("laoi: " + taoPhieuNhap.LoaiVatLieu);
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

                var vatLieuTonTai1 = context.VatLieus.First(v => v.MaVatLieu == taoPhieuNhap.MaVatLieu);

                var chiTietPhieuNhap = context.ChiTietPhieuNhaps.Add(new Models.ChiTietPhieuNhap
                {
                    SoLuong = taoPhieuNhap.SoLuong,
                    DonGia = taoPhieuNhap.DonGia,
                    DonViTinh = taoPhieuNhap.DonViTinh,
                    ThanhTien = taoPhieuNhap.SoLuong * taoPhieuNhap.DonGia,
                    PhieuNhapId = phieuNhap.Entity.Id,
                    VatLieuId = vatLieuTonTai1.Id,
                    VatLieu = vatLieuTonTai1
                });
                context.SaveChanges();

                MessageBox.Show("Thêm phiếu nhập thành công");
            }
            catch (Exception ex)
            {
                throw new Exception("Thêm phiếu nhập thất bại: " + ex.Message);
            }
        }

        public Boolean suaPhieuNhap(SuaPhieuNhapDto suaPhieuNhap)
        {
            try
            {
                var phieuNhap = context.PhieuNhaps
                    .Include(pn => pn.ChiTietPhieuNhaps)
                        .ThenInclude(ct => ct.VatLieu)
                    .First(pn => pn.Id == suaPhieuNhap.Id)
                    ?? throw new Exception("Phiếu nhập không tồn tại");
                var chiTietPhieuNhap = phieuNhap.ChiTietPhieuNhaps.First();
                var vatLieu = chiTietPhieuNhap!.VatLieu;
                  

                //var vatLieu = context.VatLieus
                //    .First(v => v.Id == vatLieu1)
                //    ?? throw new Exception("Vật liệu không tồn tại");

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
                        .FirstOrDefault(v => v.MaVatLieu == suaPhieuNhap.MaVatLieu);


                    if (vatLieuTonTai2 == null)
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
                    else
                    {
                        vatLieuTonTai2.SoLuongTon += suaPhieuNhap.SoLuong;
                        vatLieuTonTai2.DonViTinh = suaPhieuNhap.DonViTinh;
                        chiTietPhieuNhap.VatLieuId = vatLieuTonTai2.Id;

                    }
                }
                else
                {

                    int chenhLech = suaPhieuNhap.SoLuong - (int)chiTietPhieuNhap.SoLuong;
                    vatLieu.SoLuongTon += chenhLech;
                    vatLieu.DonViTinh = suaPhieuNhap.DonViTinh;
                }
                chiTietPhieuNhap.SoLuong = suaPhieuNhap.SoLuong;
                chiTietPhieuNhap.DonGia = suaPhieuNhap.DonGia;
                chiTietPhieuNhap.DonViTinh = suaPhieuNhap.DonViTinh;
                chiTietPhieuNhap.ThanhTien = suaPhieuNhap.SoLuong * suaPhieuNhap.DonGia;



                context.SaveChanges();
                
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Sửa phiếu nhập thất bại: " + ex.Message);
            }
        }

        public void xoaPhieuNhap(int id)
        {
            try
            {
                
                DialogResult result = MessageBox.Show(
                    "Bạn có chắc muốn xóa phiếu nhập này không?", 
                    "Xác nhận",                                  
                    MessageBoxButtons.YesNo,                      
                    MessageBoxIcon.Question                        
                );

                if (result == DialogResult.Yes)
                {
                    var phieuNhap = context.PhieuNhaps
                     .Include(pn => pn.ChiTietPhieuNhaps)
                         .ThenInclude(ct => ct.VatLieu)
                     .First(pn => pn.Id == id)
                     ?? throw new Exception("Phiếu nhập không tồn tại");
                    var chiTietPhieuNhap = phieuNhap.ChiTietPhieuNhaps.First();
                    var vatLieu = chiTietPhieuNhap!.VatLieu;
                    vatLieu!.SoLuongTon -= (int)(chiTietPhieuNhap.SoLuong ?? 0);
                    context.ChiTietPhieuNhaps.Remove(chiTietPhieuNhap);
                    context.PhieuNhaps.Remove(phieuNhap);
                    context.SaveChanges();
                    MessageBox.Show("Đã xóa!");
                }
                else
                {
                    // Người dùng chọn No → bỏ qua
                    MessageBox.Show("Hủy xóa.");

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Xóa phiếu nhập thất bại: " + ex.Message);
            }
        }
    }
}
