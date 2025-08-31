using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warehouse_manager.dto.o
{
    internal class BCNXT
    {
        public string MaVatLieu { get; set; } = string.Empty;
        public string TenVatLieu { get; set; } = string.Empty;
        public string DonViTinh { get; set; } = string.Empty;
        public long TonDauKy { get; set; }
        public long NhapTrongKy { get; set; }
        public long XuatTrongKy { get; set; }
        public long TonCuoiKy { get; set; }
    }
}
