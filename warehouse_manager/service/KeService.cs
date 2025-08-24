using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using warehouse_manager.context;

namespace warehouse_manager.service
{
    internal class KeService
    {
        WarehouseManagerContext context;
        public KeService()
        {
            context = new WarehouseManagerContext();
        }
        public List<String> danhSachKe()
        {
            return context.Kes.Select(k => k.MaKe!).ToList();
        }
    }
}
