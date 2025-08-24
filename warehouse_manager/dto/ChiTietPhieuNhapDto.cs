using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warehouse_manager.dto
{
    internal class ChiTietPhieuNhapDto
    {
        [Display(Name = "Mã Vật Liệu")]
        public string MaVatLieu { get; set; } = string.Empty;

        [Display(Name = "Số Lượng")]
        public long SoLuong { get; set; }

        [Display(Name = "Đơn Giá")]
        public decimal DonGia { get; set; }

        [Display(Name = "Đơn Vị Tính")]
        public string DonViTinh { get; set; } = string.Empty;

        [Display(Name = "Thành Tiền")]
        public decimal ThanhTien => SoLuong * DonGia;
    }
}
