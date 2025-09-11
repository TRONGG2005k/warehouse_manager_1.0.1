using Microsoft.EntityFrameworkCore;
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
    internal class PhieuXuatService
    {
        private WarehouseManagerContext context;
        private NguoiDungService nguoiDungService;
        public PhieuXuatService()
        {
            nguoiDungService = new NguoiDungService();
            context = new WarehouseManagerContext();
        }
        //các hàm select
        public List<PhieuXuatDto> LayTatCaPhieu()
        {
            string trangThai ="";

            return context.PhieuXuats
            .Include(p => p.CoSoSanXuat)
            .Include(p => p.NguoiDung)
            .Include(p => p.ChiTietPhieuXuats)
                .ThenInclude(ct => ct.VatLieu)
            .Select(px => new PhieuXuatDto
            {
                Id = px.Id,
                NgayLap = px.NgayXuat,
                TongTien = px.TongTien,
                NguoiLap = px.NguoiDung != null ? px.NguoiDung.TenDangNhap : "N/A",
                MaTruyenSanXuat = px.CoSoSanXuat != null ? px.CoSoSanXuat.TenCoSo : "N/A",
                SoLuongYeuCau = (long)px.ChiTietPhieuXuats.FirstOrDefault().SoLuongYeuCau,
                SoLuongThucTe = (long)px.ChiTietPhieuXuats.FirstOrDefault().SoLuongThucXuat,
                DonViTinh = px.ChiTietPhieuXuats.FirstOrDefault() != null
                                ? px.ChiTietPhieuXuats.FirstOrDefault().DonViTinh
                                : "N/A",
                DonGia = px.ChiTietPhieuXuats.FirstOrDefault() != null
                                ? px.ChiTietPhieuXuats.FirstOrDefault().DonGia ?? 0
                                : 0,
                TongGiaTri = px.ChiTietPhieuXuats.Sum(ct => ct.SoLuongThucXuat * (ct.DonGia ?? 0)),
                TenVatLieu = px.ChiTietPhieuXuats.FirstOrDefault() != null
                             && px.ChiTietPhieuXuats.FirstOrDefault().VatLieu != null
                                ? px.ChiTietPhieuXuats.FirstOrDefault().VatLieu.Ten
                                : "N/A",
                MaVatLieu = px.ChiTietPhieuXuats.FirstOrDefault() != null
                            && px.ChiTietPhieuXuats.FirstOrDefault().VatLieu != null
                                ? px.ChiTietPhieuXuats.FirstOrDefault().VatLieu.MaVatLieu
                                : "N/A",
                TrangThai = px.TrangThai == "CHO_DUYET" ? "Chờ duyệt"
                           : px.TrangThai == "DA_DUYET" ? "Đã duyệt"
                           : px.TrangThai == "DA_HUY" ? "Đã hủy"
                           : "N/A",
                GhiChu = px.GhiChu
            })
            .OrderByDescending(px => px.NgayLap)
            .ToList();

        }

        public List<PhieuXuatDto> SelectPhieuThieu()
        {
            return context.PhieuXuats
             .Where(P => P.GhiChu.Contains("Thiếu") && P.TrangThai != "DA_HUY")
            .Include(p => p.CoSoSanXuat)
            .Include(p => p.NguoiDung)
            .Include(p => p.ChiTietPhieuXuats)
                .ThenInclude(ct => ct.VatLieu)
            .Select(px => new PhieuXuatDto
            {
                Id = px.Id,
                NgayLap = px.NgayXuat,
                TongTien = px.TongTien,
                NguoiLap = px.NguoiDung != null ? px.NguoiDung.TenDangNhap : "N/A",
                MaTruyenSanXuat = px.CoSoSanXuat != null ? px.CoSoSanXuat.TenCoSo : "N/A",
                SoLuongYeuCau = (long)px.ChiTietPhieuXuats.FirstOrDefault().SoLuongYeuCau,
                SoLuongThucTe = (long)px.ChiTietPhieuXuats.FirstOrDefault().SoLuongThucXuat,
                DonViTinh = px.ChiTietPhieuXuats.FirstOrDefault() != null
                                ? px.ChiTietPhieuXuats.FirstOrDefault().DonViTinh
                                : "N/A",
                DonGia = px.ChiTietPhieuXuats.FirstOrDefault() != null
                                ? px.ChiTietPhieuXuats.FirstOrDefault().DonGia ?? 0
                                : 0,
                TongGiaTri = px.ChiTietPhieuXuats.Sum(ct => ct.SoLuongThucXuat * (ct.DonGia ?? 0)),
                TenVatLieu = px.ChiTietPhieuXuats.FirstOrDefault() != null
                             && px.ChiTietPhieuXuats.FirstOrDefault().VatLieu != null
                                ? px.ChiTietPhieuXuats.FirstOrDefault().VatLieu.Ten
                                : "N/A",
                MaVatLieu = px.ChiTietPhieuXuats.FirstOrDefault() != null
                            && px.ChiTietPhieuXuats.FirstOrDefault().VatLieu != null
                                ? px.ChiTietPhieuXuats.FirstOrDefault().VatLieu.MaVatLieu
                                : "N/A",
                TrangThai = px.TrangThai == "CHO_DUYET" ? "Chờ duyệt"
                           : px.TrangThai == "DA_DUYET" ? "Đã duyệt"
                           : px.TrangThai == "DA_HUY" ? "Đã hủy"
                           : "N/A",
                GhiChu = px.GhiChu
            })
            .OrderByDescending(px => px.NgayLap)
            .ToList();

        }
        public List<PhieuXuatDto> TimTheoTrangThai(string trangThai)
        {
           

            return context.PhieuXuats
             .Where(P => P.TrangThai == trangThai)
            .Include(p => p.CoSoSanXuat)
            .Include(p => p.NguoiDung)
            .Include(p => p.ChiTietPhieuXuats)
                .ThenInclude(ct => ct.VatLieu)
            .Select(px => new PhieuXuatDto
            {
                Id = px.Id,
                NgayLap = px.NgayXuat,
                TongTien = px.TongTien,
                NguoiLap = px.NguoiDung != null ? px.NguoiDung.TenDangNhap : "N/A",
                MaTruyenSanXuat = px.CoSoSanXuat != null ? px.CoSoSanXuat.TenCoSo : "N/A",
                SoLuongYeuCau = (long)px.ChiTietPhieuXuats.FirstOrDefault().SoLuongYeuCau,
                SoLuongThucTe = (long)px.ChiTietPhieuXuats.FirstOrDefault().SoLuongThucXuat,
                DonViTinh = px.ChiTietPhieuXuats.FirstOrDefault() != null
                                ? px.ChiTietPhieuXuats.FirstOrDefault().DonViTinh
                                : "N/A",
                DonGia = px.ChiTietPhieuXuats.FirstOrDefault() != null
                                ? px.ChiTietPhieuXuats.FirstOrDefault().DonGia ?? 0
                                : 0,
                TongGiaTri = px.ChiTietPhieuXuats.Sum(ct => ct.SoLuongThucXuat * (ct.DonGia ?? 0)),
                TenVatLieu = px.ChiTietPhieuXuats.FirstOrDefault() != null
                             && px.ChiTietPhieuXuats.FirstOrDefault().VatLieu != null
                                ? px.ChiTietPhieuXuats.FirstOrDefault().VatLieu.Ten
                                : "N/A",
                MaVatLieu = px.ChiTietPhieuXuats.FirstOrDefault() != null
                            && px.ChiTietPhieuXuats.FirstOrDefault().VatLieu != null
                                ? px.ChiTietPhieuXuats.FirstOrDefault().VatLieu.MaVatLieu
                                : "N/A",
                TrangThai = px.TrangThai == "CHO_DUYET" ? "Chờ duyệt"
                           : px.TrangThai == "DA_DUYET" ? "Đã duyệt"
                           : px.TrangThai == "DA_HUY" ? "Đã hủy"
                           : "N/A",
                GhiChu = px.GhiChu
            })
            .OrderByDescending(px => px.NgayLap)
            .ToList();

        }

        public List<PhieuXuatDto> TimTheoNgay(LocTheoNgayDto dto)
        {


            return context.PhieuXuats
             .Where(P => P.NgayXuat >= dto.Start && P.NgayXuat <= dto.End)
            .Include(p => p.CoSoSanXuat)
            .Include(p => p.NguoiDung)
            .Include(p => p.ChiTietPhieuXuats)
                .ThenInclude(ct => ct.VatLieu)
            .Select(px => new PhieuXuatDto
            {
                Id = px.Id,
                NgayLap = px.NgayXuat,
                TongTien = px.TongTien,
                NguoiLap = px.NguoiDung != null ? px.NguoiDung.TenDangNhap : "N/A",
                MaTruyenSanXuat = px.CoSoSanXuat != null ? px.CoSoSanXuat.TenCoSo : "N/A",
                SoLuongYeuCau = (long)px.ChiTietPhieuXuats.FirstOrDefault().SoLuongYeuCau,
                SoLuongThucTe = (long)px.ChiTietPhieuXuats.FirstOrDefault().SoLuongThucXuat,
                DonViTinh = px.ChiTietPhieuXuats.FirstOrDefault() != null
                                ? px.ChiTietPhieuXuats.FirstOrDefault().DonViTinh
                                : "N/A",
                DonGia = px.ChiTietPhieuXuats.FirstOrDefault() != null
                                ? px.ChiTietPhieuXuats.FirstOrDefault().DonGia ?? 0
                                : 0,
                TongGiaTri = px.ChiTietPhieuXuats.Sum(ct => ct.SoLuongThucXuat * (ct.DonGia ?? 0)),
                TenVatLieu = px.ChiTietPhieuXuats.FirstOrDefault() != null
                             && px.ChiTietPhieuXuats.FirstOrDefault().VatLieu != null
                                ? px.ChiTietPhieuXuats.FirstOrDefault().VatLieu.Ten
                                : "N/A",
                MaVatLieu = px.ChiTietPhieuXuats.FirstOrDefault() != null
                            && px.ChiTietPhieuXuats.FirstOrDefault().VatLieu != null
                                ? px.ChiTietPhieuXuats.FirstOrDefault().VatLieu.MaVatLieu
                                : "N/A",
                TrangThai = px.TrangThai == "CHO_DUYET" ? "Chờ duyệt"
                           : px.TrangThai == "DA_DUYET" ? "Đã duyệt"
                           : px.TrangThai == "DA_HUY" ? "Đã hủy"
                           : "N/A",
                GhiChu = px.GhiChu
            })
            .OrderByDescending(px => px.NgayLap)
            .ToList();

        }


        public List<PhieuXuatDto> TimTheoMa(int id)
        {


            return context.PhieuXuats
             .Where(P => P.Id == id)
            .Include(p => p.CoSoSanXuat)
            .Include(p => p.NguoiDung)
            .Include(p => p.ChiTietPhieuXuats)
                .ThenInclude(ct => ct.VatLieu)
            .Select(px => new PhieuXuatDto
            {
                Id = px.Id,
                NgayLap = px.NgayXuat,
                TongTien = px.TongTien,
                NguoiLap = px.NguoiDung != null ? px.NguoiDung.TenDangNhap : "N/A",
                MaTruyenSanXuat = px.CoSoSanXuat != null ? px.CoSoSanXuat.TenCoSo : "N/A",
                SoLuongYeuCau = (long)px.ChiTietPhieuXuats.FirstOrDefault().SoLuongYeuCau,
                SoLuongThucTe = (long)px.ChiTietPhieuXuats.FirstOrDefault().SoLuongThucXuat,
                DonViTinh = px.ChiTietPhieuXuats.FirstOrDefault() != null
                                ? px.ChiTietPhieuXuats.FirstOrDefault().DonViTinh
                                : "N/A",
                DonGia = px.ChiTietPhieuXuats.FirstOrDefault() != null
                                ? px.ChiTietPhieuXuats.FirstOrDefault().DonGia ?? 0
                                : 0,
                TongGiaTri = px.ChiTietPhieuXuats.Sum(ct => ct.SoLuongThucXuat * (ct.DonGia ?? 0)),
                TenVatLieu = px.ChiTietPhieuXuats.FirstOrDefault() != null
                             && px.ChiTietPhieuXuats.FirstOrDefault().VatLieu != null
                                ? px.ChiTietPhieuXuats.FirstOrDefault().VatLieu.Ten
                                : "N/A",
                MaVatLieu = px.ChiTietPhieuXuats.FirstOrDefault() != null
                            && px.ChiTietPhieuXuats.FirstOrDefault().VatLieu != null
                                ? px.ChiTietPhieuXuats.FirstOrDefault().VatLieu.MaVatLieu
                                : "N/A",
                TrangThai = px.TrangThai == "CHO_DUYET" ? "Chờ duyệt"
                           : px.TrangThai == "DA_DUYET" ? "Đã duyệt"
                           : px.TrangThai == "DA_HUY" ? "Đã hủy"
                           : "N/A",
                GhiChu = px.GhiChu
            })
            .OrderByDescending(px => px.NgayLap)
            .ToList();

        }
        // các hàm insert, update, delete

        public bool themPhieuXuat(ThemPhieuXuatDto dto)
        {
            try
            {
                MessageBox.Show(dto.MaVatLieu);
                var vatLieu = context.VatLieus.FirstOrDefault(vl => vl.MaVatLieu == dto.MaVatLieu)
                    ?? throw new Exception("Vật liệu không tồn tại");

                var chuyenSanXuat = context.CoSoSanXuats
                    .FirstOrDefault(csx => csx.TenCoSo == dto.TenChuyenSanXuat)
                    ?? throw new Exception("Chuyền sản xuất không tồn tại");

                if (vatLieu.SoLuongTon == 0)
                    throw new Exception("Vật liệu đã hết hàng, vui lòng báo với quản lý");

                long soLuongThucXuat;

                if (dto.SoLuongYeuCau > vatLieu.SoLuongTon)
                {
                    DialogResult result = MessageBox.Show(
                        $"Vật liệu yêu cầu vượt quá tồn kho. Cần thêm {dto.SoLuongYeuCau - vatLieu.SoLuongTon} để đủ.\nBạn có chắc muốn tạo phiếu xuất này không?",
                        "Xác nhận",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (result == DialogResult.Yes)
                    {
                        soLuongThucXuat = (int)vatLieu.SoLuongTon;
                        dto.GhiChu = (dto.GhiChu ?? "") + $" (Yêu cầu {dto.SoLuongYeuCau}, thực xuất {soLuongThucXuat}) cần bù thêm {dto.SoLuongYeuCau - vatLieu.SoLuongTon}";
                        vatLieu.SoLuongTon = 0;
                        
                    }
                    else
                    {
                        MessageBox.Show("Đã hủy tạo phiếu xuất.");
                        return false;
                    }
                }
                else
                {
                    soLuongThucXuat = dto.SoLuongYeuCau;
                    vatLieu.SoLuongTon -= (int)soLuongThucXuat;
                }

                string content = File.Exists("user.txt") ? File.ReadAllText("user.txt") : "";
                var nguoiDung = context.NguoiDungs.FirstOrDefault(n => n.TenDangNhap == content)
                    ?? throw new Exception("Người dùng không tồn tại");


                var phieuXuatMoi = new Models.PhieuXuat
                {
                    CoSoSanXuatId = chuyenSanXuat.Id,
                    NgayXuat = DateTime.Now,
                    TongTien = soLuongThucXuat * vatLieu.DonGia,
                    GhiChu = dto.GhiChu,
                    NguoiDungId = nguoiDung.Id,
                    TrangThai = "CHO_DUYET"
                };

                var chiTiet = new Models.ChiTietPhieuXuat
                {
                    VatLieuId = vatLieu.Id,
                    SoLuongYeuCau = dto.SoLuongYeuCau,
                    SoLuongThucXuat = soLuongThucXuat,
                    DonGia = vatLieu.DonGia,
                    DonViTinh = vatLieu.DonViTinh,
                    PhieuXuat = phieuXuatMoi   
                };

                context.PhieuXuats.Add(phieuXuatMoi);
                context.ChiTietPhieuXuats.Add(chiTiet);
                context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool YeuCauthemPhieuXuat(ThemPhieuXuatDto dto)
        {
            try
            {
                var nguoiDung = nguoiDungService.layThongTinNguoiDung();
                 
                var vatLieu = context.VatLieus.FirstOrDefault(vl => vl.MaVatLieu == dto.MaVatLieu)
                    ?? throw new Exception("Vật liệu không tồn tại");

     
                var chuyenSanXuat = context.CoSoSanXuats
                    .FirstOrDefault(csx => csx.TenCoSo == dto.TenChuyenSanXuat)
                    ?? throw new Exception("Chuyền sản xuất không tồn tại");

              
                long soLuongThucXuat = dto.SoLuongYeuCau > vatLieu.SoLuongTon
                    ? (long)vatLieu.SoLuongTon
                    : dto.SoLuongYeuCau;

                if(vatLieu.SoLuongTon == 0)
                {
                    throw new Exception("Vật liệu đã hết hàng, vui lòng báo với quản lý");
                }
                
                if (dto.SoLuongYeuCau > vatLieu.SoLuongTon)
                {
                    
                    DialogResult result = MessageBox.Show(
                        $"Vật liệu yêu cầu vượt quá tồn kho. Cần thêm {dto.SoLuongYeuCau - vatLieu.SoLuongTon} để đủ.\nBạn có chắc muốn tạo phiếu xuất này không?",
                        "Xác nhận",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (result == DialogResult.Yes)
                    {
                        dto.GhiChu = (dto.GhiChu ?? "") +
                                 $" |(Yêu cầu {dto.SoLuongYeuCau}, thực xuất {soLuongThucXuat}) " +
                                 $"Thiếu {dto.SoLuongYeuCau - vatLieu.SoLuongTon}";
                    }
                    else
                    {
                        MessageBox.Show("Đã hủy tạo phiếu xuất.");
                        return false;
                    }
                }

                
                

                var phieuXuatMoi = new Models.PhieuXuat
                {
                    CoSoSanXuatId = chuyenSanXuat.Id,
                    NgayXuat = DateTime.Now,
                    TongTien = soLuongThucXuat * vatLieu.DonGia,
                    GhiChu = dto.GhiChu,
                    NguoiDungId = nguoiDung.Id,
                    TrangThai = "CHO_DUYET"
                };

                var chiTiet = new Models.ChiTietPhieuXuat
                {
                    VatLieuId = vatLieu.Id,
                    SoLuongYeuCau = dto.SoLuongYeuCau,
                    SoLuongThucXuat = soLuongThucXuat,
                    DonGia = vatLieu.DonGia,
                    DonViTinh = vatLieu.DonViTinh,
                    PhieuXuat = phieuXuatMoi
                };

                context.PhieuXuats.Add(phieuXuatMoi);
                context.ChiTietPhieuXuats.Add(chiTiet);
                context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool duyetPhieuXuat(int phieuXuatId)
        {
            try
            {
                DialogResult result = MessageBox.Show(
                   "Bạn có chắc muốn duyệt có id: "+ phieuXuatId +" phiếu nhập này không?",
                   "Xác nhận",
                   MessageBoxButtons.YesNo,
                   MessageBoxIcon.Question
               );

                if (result == DialogResult.Yes)
                {
                    var phieuXuat = context.PhieuXuats
                    .Include(px => px.ChiTietPhieuXuats)
                    .FirstOrDefault(px => px.Id == phieuXuatId)
                    ?? throw new Exception("Phiếu xuất không tồn tại");

                    if (phieuXuat.TrangThai != "CHO_DUYET")
                        throw new Exception("Phiếu xuất đã được xử lý");

                    // lấy chi tiết duy nhất
                    var chiTiet = phieuXuat.ChiTietPhieuXuats.FirstOrDefault()
                        ?? throw new Exception("Phiếu xuất không có chi tiết");

                    var vatLieu = context.VatLieus
                        .FirstOrDefault(vl => vl.Id == chiTiet.VatLieuId)
                        ?? throw new Exception("Vật liệu trong phiếu xuất không tồn tại");
                    if(vatLieu.SoLuongTon == 0)
                    {
                        throw new Exception("Vật liệu đã hết hàng không thể duyệt, vui lòng đặt thêm hàng");
                    }   
                    if (chiTiet.SoLuongThucXuat > vatLieu.SoLuongTon)
                    {
                        int soThieu = (int)chiTiet.SoLuongThucXuat - (int)vatLieu.SoLuongTon;


                        phieuXuat.GhiChu +=
                            $" | Vật liệu {vatLieu.MaVatLieu} thiếu {soThieu}, đã xuất {vatLieu.SoLuongTon}";

                        chiTiet.SoLuongThucXuat = (long)vatLieu.SoLuongTon;
                        vatLieu.SoLuongTon = 0;
                        vatLieu.TrangThai = "HET_HANG";
                    }
                    else
                    {
                        vatLieu.SoLuongTon -= (int)chiTiet.SoLuongThucXuat;
                    }

                    phieuXuat.TrangThai = "DA_DUYET";
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    // Người dùng chọn No → bỏ qua
                    MessageBox.Show("Đã hủy xác nhận.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool huyPhieuXuat(HuyPhieuDto dto)
        {
            try
            {   
                DialogResult result = MessageBox.Show(
                   "Bạn có chắc muốn hủy có id: " + dto.PhieuXuatId + " phiếu nhập này không?",
                   "Xác nhận",
                   MessageBoxButtons.YesNo,
                   MessageBoxIcon.Question
                );
                if (result == DialogResult.Yes)
                {
                    var phieuXuat = context.PhieuXuats
                        .Include(px => px.ChiTietPhieuXuats)
                            .ThenInclude(ct => ct.VatLieu)
                        .FirstOrDefault(px => px.Id == dto.PhieuXuatId)
                        ?? throw new Exception("Phiếu xuất không tồn tại");
                    var chiTiet = phieuXuat.ChiTietPhieuXuats.FirstOrDefault()
                        ?? throw new Exception("Phiếu xuất không có chi tiết");

                    var vatLieu = context.VatLieus
                        .FirstOrDefault(vl => vl.Id == chiTiet.VatLieuId)
                        ?? throw new Exception("Vật liệu trong phiếu xuất không tồn tại");
                    if (phieuXuat.TrangThai == "CHO_DUYET")
                    {
                        phieuXuat.TrangThai = "DA_HUY";
                        phieuXuat.GhiChu = (phieuXuat.GhiChu ?? "") + " | Lý do hủy: " + dto.LyDoHuy + "\n |Người huỷ: " +
                            nguoiDungService.layThongTinNguoiDung().TenDangNhap; ;
                    }
                    else if( phieuXuat.TrangThai == "DA_DUYET")
                    {
                        vatLieu.SoLuongTon += (int)phieuXuat.ChiTietPhieuXuats.FirstOrDefault().SoLuongThucXuat;
                        phieuXuat.TrangThai = "DA_HUY";
                        phieuXuat.GhiChu = (phieuXuat.GhiChu ?? "") 
                            + " | Lý do hủy: " + dto.LyDoHuy +"\n |Người huỷ: "+
                            nguoiDungService.layThongTinNguoiDung().TenDangNhap;
                    } else
                    {
                        MessageBox.Show("Đơn Đã bị huỷ rồi");
                        return false;
                    }
                        context.SaveChanges();
                    return true;
                }
                else
                {
                    
                    MessageBox.Show("Đã hủy xác nhận.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}
