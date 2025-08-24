    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.EntityFrameworkCore;

    namespace warehouse_manager.Models;

    [Table("ke")]
    [Index("MaKe", Name = "unique_ma_ke", IsUnique = true)]
    [Index("MaKe", Name = "uq_ma_ke", IsUnique = true)]
    public partial class Ke
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("ma_ke")]
        [StringLength(20)]
        [Unicode(false)]
        public string? MaKe { get; set; }

        [Column("khu")]
        [StringLength(255)]
        [Unicode(false)]
        public string? Khu { get; set; }

        [Column("mo_ta", TypeName = "text")]
        public string? MoTa { get; set; }

        [ForeignKey("KeId")]
        [InverseProperty("Kes")]
        public virtual ICollection<VatLieu> VatLieus { get; set; } = new List<VatLieu>();
    }
