using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warehouse_manager.dto.i
{
    public class ChiTietPhieuKiemKeCreateDto
    {
        public long PhieuKiemKeId { get; set; }

        public long VatLieuId { get; set; }

        public int TonHeThong { get; set; }  // Nếu muốn lấy từ DB thì có thể bỏ

        public int TonThucTe { get; set; }
    }
}
