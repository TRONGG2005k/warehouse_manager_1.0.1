using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using warehouse_manager.context;
using warehouse_manager.Models;

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

        public VatLieu layVatLieuTheoMa(string mavatLieu)
        {
            return context.VatLieus.FirstOrDefault(vl => vl.MaVatLieu == mavatLieu);
        }

        public List<string > danhSachTrangThaiVatLieu()
        {
            return context.VatLieus.Select(vl => vl.TrangThai).ToList();
        }
        public List<VatLieu> layVatLieuTheoMa()
        {
            return context.VatLieus
                .Include(vl => vl.Kes)
                .ToList();
        }
    }
}
