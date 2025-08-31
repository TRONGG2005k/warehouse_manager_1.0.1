using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warehouse_manager.dto.o
{
    internal class BCKienKeDto
    {
        //public int STT { get; set; }                 // Số thứ tự
        public string MaVatLieu { get; set; } = "";  // Mã vật liệu
        public string TenVatLieu { get; set; } = ""; // Tên vật liệu
        public string DonViTinh { get; set; } = "";  // Đơn vị tính
        public decimal TonHeThong { get; set; }      // Tồn kho hệ thống trước khi kiểm kê
        public decimal TonThucTe { get; set; }       // Tồn thực tế khi kiểm kê
        public decimal ChenhLech => TonThucTe - TonHeThong; // Chênh lệch
        public string GhiChu { get; set; } = "";     // Ghi chú (nếu có)
    }
}
