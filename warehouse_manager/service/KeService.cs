using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using warehouse_manager.context;
using warehouse_manager.Models;

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
        public List<Ke> layDanhSachKe()
        {
            return context.Kes.ToList();
        }


        public void taoKes(String ma, String khu, String mt) {
            context.Kes.Add(new Ke
            {
                MaKe = ma,
                Khu = khu,
                MoTa = mt
            });
            context.SaveChanges();
        }

        public void suaKe(String ma, String khu, String mt, int id)
        {
            var ke = context.Kes.FirstOrDefault(k => k.Id == id)
                ?? throw new Exception("ke không tồn tại");

            ke.MaKe = ma;
            ke.Khu = khu;
            ke.MoTa = mt;
            context.SaveChanges();
        }
    }
}
