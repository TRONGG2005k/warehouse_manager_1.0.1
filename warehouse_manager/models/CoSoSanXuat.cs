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
    [Table("CoSoSanXuat")]
    public class CoSoSanXuat
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [MaxLength(250)]
        [Column("TenCoSo")]
        [StringLength(500)]
        [Unicode(true)]
        public string TenCoSo { get; set; } = null!;

        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;

        [MaxLength(500)]
        public string? DiaChi { get; set; }

        [MaxLength(50)]
        public string? SoDienThoai { get; set; }

        [MaxLength(100)]
        public string? Email { get; set; }

        public string? MoTa { get; set; }

        // Quan hệ 1-n với PhieuXuat
        public ICollection<PhieuXuat> PhieuXuats { get; set; } = new List<PhieuXuat>();
    }
}
