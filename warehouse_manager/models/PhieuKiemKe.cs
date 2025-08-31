using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warehouse_manager.Models
{
    [Table("phieu_kiem_ke")]
    public class PhieuKiemKe
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("ma_phieu")]
        [StringLength(50)]
        [Unicode(true)]
        public string MaPhieu { get; set; } = null!;

        [Column("ngay_kiem_ke")]
        public DateTime NgayKiemKe { get; set; } = DateTime.Now;

        [Column("nguoi_kiem_ke")]
        [StringLength(200)]
        [Unicode(true)]
        public string? NguoiKiemKe { get; set; }

        [Column("ghi_chu")]
        [StringLength(500)]
        [Unicode(true)]
        public string? GhiChu { get; set; }

        [InverseProperty("PhieuKiemKe")]
        public virtual ICollection<ChiTietPhieuKiemKe> ChiTietPhieuKiemKes { get; set; } = new List<ChiTietPhieuKiemKe>();
    }

}
