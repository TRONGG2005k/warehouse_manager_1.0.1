using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using warehouse_manager.context;

namespace warehouse_manager.service
{
    internal class ChuyenSanXuatService
    {
        private WarehouseManagerContext context;
        public ChuyenSanXuatService()
        {
            context = new WarehouseManagerContext();
        }

        public List<String> danhSachCoSoSanXuat()
        {
            return context.CoSoSanXuats.Select(cs => cs.TenCoSo).ToList();
        }
    }
}
