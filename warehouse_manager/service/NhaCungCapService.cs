using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using warehouse_manager.context;

namespace warehouse_manager.service
{
    internal class NhaCungCapService
    {
        WarehouseManagerContext context;
        public NhaCungCapService()
        {
            context = new WarehouseManagerContext();
        }
        public List<String> danhSachNhaCungCap()
        {
            return context.NhaCungCaps.Select(n => n.TenNhaCungCap!).ToList();
        }
    }
}
