using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warehouse_manager.dto.o
{
    internal class PhieuXuatDto
    {
        public long Id { get; set; }
        public DateTime NgayLap { get; set; }
        public decimal? TongTien { get; set; }
        public string NguoiLap { get; set; }
        public string MaTruyenSanXuat { get; set; }
        public long SoLuongYeuCau { get; set; }
        public long SoLuongThucTe { get; set; }
        public string DonViTinh { get; set; }
        public decimal? DonGia { get; set; }
        public decimal? TongGiaTri { get; set; }
        public string TenVatLieu { get; set; }
        public string MaVatLieu { get; set; }
        public string TrangThai { get; set; }
        public string GhiChu { get; set; }
    }
}
