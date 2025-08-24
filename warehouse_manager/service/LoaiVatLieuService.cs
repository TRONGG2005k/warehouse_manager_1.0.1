using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using warehouse_manager.context;

namespace warehouse_manager.service
{
    internal class LoaiVatLieuService
    {
        WarehouseManagerContext context; 
        public LoaiVatLieuService()
        {
            context = new WarehouseManagerContext();
        }

        public List<String> danhSachLoaiVatLieu()
        {
            return context.LoaiVatLieus.Select(l => l.TenLoai!).ToList();
        }
    }
}
