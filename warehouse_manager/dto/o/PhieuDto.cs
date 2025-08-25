using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warehouse_manager.dto.o
{
    internal class PhieuDto
    {
        public long Id { get; set; }
        [DisplayName("Ngày lập")]
        public DateTime NgayLap { get; set; }

        [DisplayName("Tổng tiền")]
        public decimal? TongTien { get; set; }

        [DisplayName("Người lập")]
        public string NguoiLap { get; set; }

        [DisplayName("Loại phiếu")]
        public string LoaiPhieu { get; set; }

        [DisplayName("Tên hàng")]
        public string TenHang { get; set; }

        [DisplayName("Số lượng")]
        public long SoLuong { get; set; }
    }
}
