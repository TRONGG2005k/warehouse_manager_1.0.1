using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using warehouse_manager.context;

namespace warehouse_manager.service
{
    internal class VatLieuService
    {
        private WarehouseManagerContext context;
        public VatLieuService()
        {
            context = new WarehouseManagerContext();
        }
        public List<String> danhSachVatLieu()
        {
            return context.VatLieus.Select(vl => vl.MaVatLieu).ToList();
        }   
    }
}
