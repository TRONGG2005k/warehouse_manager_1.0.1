using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace warehouse_manager.dto.i
{
    internal class ThemPhieuXuatDto
    {
        [Required(ErrorMessage = "Tên truyền không được bỏ trống")]
        public string TenChuyenSanXuat { get; set; }
        [Range(1, long.MaxValue, ErrorMessage = "Số lượng yêu cầu phải > 0")]
        public long SoLuongYeuCau { get; set; }   // bắt buộc nhập
        [Required(ErrorMessage = "mã vậ liệu không được bỏ trống")]
        public string MaVatLieu { get; set; }
        public string GhiChu { get; set; }

    }
}
