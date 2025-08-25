using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warehouse_manager.dto.i
{
    internal class TaoPhieuNhapKhoDto
    {
        public string LoaiVatLieu { get; set; } = string.Empty;
        public string TenVatLieu { get; set; } = string.Empty;
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public string DonViTinh { get; set; } = string.Empty;
        public string NhaCungCap { get; set; } = string.Empty;
        public string MaNguoiLap { get; set; } = string.Empty;
        public string MaVatLieu { get; set; } = string.Empty;
        public string Make { get; set; } = string.Empty;

        public bool HasNullOrEmptyProperties()
        {
            return string.IsNullOrEmpty(LoaiVatLieu) ||
                   string.IsNullOrEmpty(TenVatLieu) ||
                   string.IsNullOrEmpty(DonViTinh) ||
                   string.IsNullOrEmpty(NhaCungCap) ||
                   string.IsNullOrEmpty(MaNguoiLap) ||
                   string.IsNullOrEmpty(MaVatLieu) ||
                   string.IsNullOrEmpty(Make);
        }
    }
}
