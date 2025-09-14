using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using warehouse_manager.context;
using warehouse_manager.dto.i;
using warehouse_manager.dto.o;
using warehouse_manager.Models;


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
                    //Ke = p.ChiTietPhieuNhaps.First().VatLieu.Kes.First()!.MaKe,
                    NgayNhap = p.NgayNhap,
                    MaVatLieu = p.ChiTietPhieuNhaps!.FirstOrDefault()!.VatLieu!.MaVatLieu,

                }
            ).OrderByDescending(p => p.NgayNhap).ToList();

        }
        public List<dto.o.PhieuNhapDto> phieuNhapDtos()
        {
            return context.PhieuNhaps
                .Include(pn => pn.MaNhaCungCapNavigation)
                .Include(pn => pn.ChiTietPhieuNhaps)
                    .ThenInclude(ct => ct.VatLieu)
                        .ThenInclude(v => v.Kes)
                .SelectMany(pn => pn.ChiTietPhieuNhaps, (pn, ct) => new dto.o.PhieuNhapDto
                {
                    Id = pn.Id,
                    TenHang = ct.VatLieu.Ten,
                    DonViTinh = ct.VatLieu.DonViTinh,
                    SoLuong = (long)ct.SoLuong,
                    DonGia = ct.DonGia ?? 0,
                    NhaCungCap = pn.MaNhaCungCapNavigation.TenNhaCungCap,
                    //Ke = ct.VatLieu.Kes.FirstOrDefault() != null
                    //     ? ct.VatLieu.Kes.FirstOrDefault().MaKe
                    //     : null,
                    NgayNhap = pn.NgayNhap,
                    MaVatLieu = ct.VatLieu.MaVatLieu,
                    LoaiVatLieu = ct.VatLieu.MaLoaiNavigation.TenLoai
                })
                .OrderByDescending(p => p.NgayNhap)
                .ToList();
        }


        public List<dto.o.PhieuNhapDto> TimPhieuTheoMa(int id)
        {
            return context.PhieuNhaps
                .Where(pn => pn.Id == id)
                .Include(pn => pn.MaNhaCungCapNavigation)
                .Include(pn => pn.ChiTietPhieuNhaps)
                    .ThenInclude(ct => ct.VatLieu)
                        .ThenInclude(v => v.Kes)
                .SelectMany(pn => pn.ChiTietPhieuNhaps, (pn, ct) => new dto.o.PhieuNhapDto
                {
                    Id = pn.Id,
                    TenHang = ct.VatLieu.Ten,
                    DonViTinh = ct.VatLieu.DonViTinh,
                    SoLuong = (long)ct.SoLuong,
                    DonGia = ct.DonGia ?? 0,
                    NhaCungCap = pn.MaNhaCungCapNavigation.TenNhaCungCap,
                    //Ke = ct.VatLieu.Kes.FirstOrDefault() != null
                    //     ? ct.VatLieu.Kes.FirstOrDefault().MaKe
                    //     : null,
                    NgayNhap = pn.NgayNhap,
                    MaVatLieu = ct.VatLieu.MaVatLieu,
                    LoaiVatLieu = ct.VatLieu.MaLoaiNavigation.TenLoai
                })
                .OrderByDescending(p => p.NgayNhap)
                .ToList();

        }
        public List<dto.o.PhieuNhapDto> TimPhieuTheoKhoangThoiGian(LocTheoNgayDto locTheoNgay)
        {
            
            return context.PhieuNhaps
                .Where(pn => pn.NgayNhap >= locTheoNgay.Start && pn.NgayNhap <= locTheoNgay.End)
                .Include(pn => pn.MaNhaCungCapNavigation)
                .Include(pn => pn.ChiTietPhieuNhaps)
                    .ThenInclude(ct => ct.VatLieu)
                        .ThenInclude(v => v.Kes)
                .SelectMany(pn => pn.ChiTietPhieuNhaps, (pn, ct) => new dto.o.PhieuNhapDto
                {
                    Id = pn.Id,
                    TenHang = ct.VatLieu.Ten,
                    DonViTinh = ct.VatLieu.DonViTinh,
                    SoLuong = (long)ct.SoLuong,
                    DonGia = ct.DonGia ?? 0,
                    NhaCungCap = pn.MaNhaCungCapNavigation.TenNhaCungCap,
                    //Ke = ct.VatLieu.Kes.FirstOrDefault() != null
                    //     ? ct.VatLieu.Kes.FirstOrDefault().MaKe
                    //     : null,
                    NgayNhap = pn.NgayNhap,
                    MaVatLieu = ct.VatLieu.MaVatLieu,
                    LoaiVatLieu = ct.VatLieu.MaLoaiNavigation.TenLoai
                })
                .OrderByDescending(p => p.NgayNhap)
                .ToList();

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
                    //Ke = k != null ? k.MaKe : null,  // Nếu vật liệu không có kệ, vẫn ra 1 bản ghi với null
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
                    //Ke = k != null ? k.MaKe : null,  // Nếu vật liệu không có kệ, vẫn ra 1 bản ghi với null
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
                    //Ke = k != null ? k.MaKe : null,  // Nếu vật liệu không có kệ, vẫn ra 1 bản ghi với null
                    NgayNhap = x.pn.NgayNhap,
                    MaVatLieu = x.ct.VatLieu.MaVatLieu,
                    LoaiVatLieu = x.ct.VatLieu.MaLoaiNavigation.TenLoai

                }).OrderByDescending(p => p.NgayNhap).ToList();

        }
        // chức năng không liên quan đến select
        public void ThemPhieuNhap(dto.i.TaoPhieuNhapKhoDto taoPhieuNhap)
        {
            try
            {
                
                var loaiVatLieu = context.LoaiVatLieus
                    .FirstOrDefault(l => l.TenLoai == taoPhieuNhap.LoaiVatLieu)
                    ?? throw new Exception("Loại vật liệu không tồn tại");

                var nhaCungCap = context.NhaCungCaps
                    .FirstOrDefault(ncc => ncc.TenNhaCungCap == taoPhieuNhap.NhaCungCap)
                    ?? throw new Exception("Nhà cung cấp không tồn tại");


                var vatLieu = context.VatLieus
                    .FirstOrDefault(v => v.MaVatLieu == taoPhieuNhap.MaVatLieu);

                if (vatLieu == null)
                {
                    vatLieu = new Models.VatLieu
                    {
                        MaVatLieu = taoPhieuNhap.MaVatLieu,
                        Ten = taoPhieuNhap.TenVatLieu,
                        MaLoai = loaiVatLieu.Id,
                        SoLuongTon = taoPhieuNhap.SoLuong,
                        DonGia = taoPhieuNhap.DonGia,
                        DonViTinh = taoPhieuNhap.DonViTinh,
                        TrangThai = "CON_HANG",
                        MaNhaCungCap = nhaCungCap.Id
                    };
                    context.VatLieus.Add(vatLieu);
                }
                else
                {
                    vatLieu.SoLuongTon += taoPhieuNhap.SoLuong;
                }

                // 3. Đọc người dùng hiện tại
                string content = File.Exists("user.txt") ? File.ReadAllText("user.txt") : "";
                var nguoiDung = context.NguoiDungs
                    .FirstOrDefault(n => n.TenDangNhap == content)
                    ?? throw new Exception("Người dùng không tồn tại");

                // 4. Tạo phiếu nhập kèm chi tiết
                var phieuNhap = new Models.PhieuNhap
                {
                    NgayNhap = DateTime.Now,
                    NguoiDungId = nguoiDung.Id,
                    MaNhaCungCap = nhaCungCap.Id,
                    TongTien = taoPhieuNhap.SoLuong * taoPhieuNhap.DonGia,
                    ChiTietPhieuNhaps = new List<Models.ChiTietPhieuNhap>
                    {
                        new Models.ChiTietPhieuNhap
                        {
                            SoLuong = taoPhieuNhap.SoLuong,
                            DonGia = taoPhieuNhap.DonGia,
                            DonViTinh = taoPhieuNhap.DonViTinh,
                            ThanhTien = taoPhieuNhap.SoLuong * taoPhieuNhap.DonGia,
                            VatLieu = vatLieu
                        }
                    }
                };

                context.PhieuNhaps.Add(phieuNhap);

                // 5. Lưu thay đổi
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
                    .FirstOrDefault(pn => pn.Id == suaPhieuNhap.Id)
                    ?? throw new Exception("Phiếu nhập không tồn tại");
                var chiTietPhieuNhap = phieuNhap.ChiTietPhieuNhaps.First();
                var vatLieu = chiTietPhieuNhap!.VatLieu;

                //var ke = context.Kes.FirstOrDefault(k => k.MaKe == suaPhieuNhap.Ke)
                //?? throw new Exception("Kệ không tồn tại");
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
                        vatLieu.TrangThai = "CON_HANG";
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
                        if(vatLieuTonTai2.SoLuongTon > 0)
                        {
                            vatLieuTonTai2.TrangThai = "CON_HANG";
                        }
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
                throw new Exception("Sửa phiếu nhập thất bại: " + ex.Message+ " inner" + ex.InnerException);
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
