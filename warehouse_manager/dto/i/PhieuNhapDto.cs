using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warehouse_manager.dto.i
{
    internal class PhieuNhapDto
    {
        [Display(Name = "ID Phiếu Nhập")]
        public long Id { get; set; }
        [Display(Name = "Ngày Nhập")]
        public DateTime NgayNhap { get; set; }

        [Display(Name = "Người Dùng ID")]
        public string MaNguoiLap{ get; set; } = string.Empty;

        [Display(Name = "Tên Nhà Cung Cấp")]
        public string NhaCungCap { get; set; } = string.Empty;

        // Danh sách chi tiết
        [Display(Name = "Mã Vật Liệu")]
        public string MaVatLieu { get; set; } = string.Empty;

        [Display(Name = "Số Lượng")]
        public long SoLuong { get; set; }

        [Display(Name = "Đơn Giá")]
        public decimal DonGia { get; set; }

        [Display(Name = "Đơn Vị Tính")]
        public string DonViTinh { get; set; } = string.Empty;
        [Display(Name = "Loại vật liệu")]
        public string LoaiVatLieu { get; set; } = string.Empty ;

        public string TenHang { get; set; } = string.Empty;
        [Display(Name = "Thành Tiền")]
        public decimal ThanhTien => SoLuong * DonGia;

        public string Ke { get; set; } = string.Empty;
    }
}
