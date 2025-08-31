using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using warehouse_manager.context;
using warehouse_manager.dto.i;
using warehouse_manager.Models;

namespace warehouse_manager.service
{
    internal class PhieuKiemKeService
    {
        private WarehouseManagerContext context;
        public PhieuKiemKeService()
        {
            context = new WarehouseManagerContext();
        }
        private string GenerateMaPhieu()
        {
            string prefix = "KK";
            string datePart = DateTime.Now.ToString("yyyyMMdd");

            // Đếm số phiếu trong ngày hiện tại
            int countToday = context.PhieuKiemKes
                .Count(p => p.NgayKiemKe.Date == DateTime.Now.Date);

            // Tạo số thứ tự tăng dần, bắt đầu từ 1
            string numberPart = (countToday + 1).ToString("D4");

            return $"{prefix}-{datePart}-{numberPart}";
        }

        public PhieuKiemKe taoPhieuKiemKe(PhieuKiemKeCreateDto dto)
        {
            var nguoidungService = new NguoiDungService();
            var entity = new PhieuKiemKe
            {
                MaPhieu = GenerateMaPhieu(),
                NgayKiemKe = dto.NgayKiemKe,
                NguoiKiemKe = nguoidungService.layThongTinNguoiDung().TenDangNhap,
                GhiChu = dto.GhiChu,
                ChiTietPhieuKiemKes = dto.ChiTietPhieuKiemKes.Select(ct => new ChiTietPhieuKiemKe
                {
                    VatLieuId = ct.VatLieuId,
                    TonHeThong = ct.TonHeThong, 
                    TonThucTe = ct.TonThucTe
                }).ToList()
            };

            // Thêm vào DbContext
            context.PhieuKiemKes.Add(entity);
            context.SaveChanges();

            return entity;
        }

        public PhieuKiemKe LayPhieuKiemKeMoiNhat()
        {
            return context.PhieuKiemKes.Include(pk => pk.ChiTietPhieuKiemKes)
                                       .ThenInclude(ct => ct.VatLieu)
                                       .OrderByDescending(pk => pk.NgayKiemKe)
                                       .FirstOrDefault();
        }
    }
}
