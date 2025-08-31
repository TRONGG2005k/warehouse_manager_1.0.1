using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warehouse_manager.Models
{
    [Table("chi_tiet_kiem_ke")]
    public class ChiTietPhieuKiemKe
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("phieu_kiem_ke_id")]
        public long PhieuKiemKeId { get; set; }

        [Column("vat_lieu_id")]
        public long VatLieuId { get; set; }

        [Column("ton_he_thong")]
        public int TonHeThong { get; set; }

        [Column("ton_thuc_te")]
        public int TonThucTe { get; set; }

        [NotMapped] // vì computed column bên DB đã tự tính
        public int ChenhLech => TonThucTe - TonHeThong;

        [ForeignKey("PhieuKiemKeId")]
        [InverseProperty("ChiTietPhieuKiemKes")]
        public virtual PhieuKiemKe PhieuKiemKe { get; set; } = null!;

        [ForeignKey("VatLieuId")]
        public virtual VatLieu VatLieu { get; set; } = null!;
    }
}
