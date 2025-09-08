using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warehouse_manager.dto.o
{
    internal class PhieuNhapDto
    {
        public long Id { get; set; }
        public string LoaiVatLieu { get; set; }
        public string TenHang { get; set; }
        public string DonViTinh { get; set; }
        public long SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public string NhaCungCap { get; set; }
        //public string Ke { get; set; }
        public DateTime NgayNhap { get; set; }
        public string MaVatLieu { get; set; }
    }
}
