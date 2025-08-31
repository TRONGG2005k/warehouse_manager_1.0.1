using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warehouse_manager.dto.o
{
    public class KetQuaKiemKeDto
    {
        public string MaVatTu { get; set; }       // Mã VT
        public string DonViTinh { get; set; }     // ĐVT
        public int TonHeThong { get; set; }       // Tồn hệ thống
        public int TonThucTe { get; set; }        // Tồn thực tế
        public int ChenhLech => TonThucTe - TonHeThong; // Chênh lệch (tự tính)
    }
}
