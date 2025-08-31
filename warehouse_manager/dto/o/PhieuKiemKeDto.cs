using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warehouse_manager.dto.o
{
    internal class PhieuKiemKeDto
    {
        public string MaPhieuKiemKe { get; set; } = ""; // Mã phiếu kiểm kê
        public DateTime NgayKiemKe { get; set; }       // Ngày kiểm kê
        public string NguoiTao { get; set; } = "";     // Người tạo phiếu
        public string GhiChu { get; set; } = "";       // Ghi chú (nếu có)
        //public List<BCKienKeDto> ChiTietKiemKe { get; set; } = new List<BCKienKeDto>(); // Chi tiết kiểm kê
    }
}
