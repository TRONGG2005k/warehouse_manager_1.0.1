using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warehouse_manager.dto.i
{
    public class PhieuKiemKeCreateDto
    {
        //public string MaPhieu { get; set; } = string.Empty;
        public DateTime NgayKiemKe { get; set; } = DateTime.Now;
        //public string? NguoiKiemKe { get; set; }    
        public string? GhiChu { get; set; }

        // Danh sách chi tiết (nếu bạn muốn nhập cùng lúc)
        public List<ChiTietPhieuKiemKeCreateDto> ChiTietPhieuKiemKes { get; set; }
            = new List<ChiTietPhieuKiemKeCreateDto>();
    }
}
