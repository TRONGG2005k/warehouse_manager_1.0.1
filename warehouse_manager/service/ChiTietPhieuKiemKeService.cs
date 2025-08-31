using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using warehouse_manager.dto.i;
using warehouse_manager.Models;

namespace warehouse_manager.service
{

    internal class ChiTietPhieuKiemKeService
    {
        private context.WarehouseManagerContext context;
        public ChiTietPhieuKiemKeService()
        {
            context = new context.WarehouseManagerContext();
        }

        public ChiTietPhieuKiemKe taoChiTietPhieuKiemKe(ChiTietPhieuKiemKeCreateDto dto)
        {
            
            var entity = new Models.ChiTietPhieuKiemKe
            {
                PhieuKiemKeId = dto.PhieuKiemKeId,
                VatLieuId = dto.VatLieuId,
                TonHeThong = dto.TonHeThong,
                TonThucTe = dto.TonThucTe
            };

            // Thêm vào DbContext
            context.Add(entity);
            context.SaveChanges();
            return entity;
        }
    }
}
