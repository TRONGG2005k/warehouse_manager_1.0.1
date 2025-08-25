using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using warehouse_manager.Models;

namespace warehouse_manager.Models
{
    public class VaiTro
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("ten_vai_tro")]
        public string TenVaiTro { get; set; }
        [Column("mo_ta")]
        public string MoTa { get; set; }

        // Quan hệ N-N với Người dùng
        [InverseProperty("VaiTros")]
        public virtual ICollection<NguoiDung> NguoiDungs { get; set; } = new List<NguoiDung>();
    }

}
