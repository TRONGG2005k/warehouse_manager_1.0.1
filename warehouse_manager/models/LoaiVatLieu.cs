using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace warehouse_manager.Models;

[Table("loai_vat_lieu")]
public partial class LoaiVatLieu
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("ten_loai")]
    [StringLength(255)]
    [Unicode(true)]
    public string TenLoai { get; set; } = null!;

    [Column("mo_ta")]
    public string? MoTa { get; set; }

    [Column("is_deleted")]
    public bool IsDeleted { get; set; } = false;

    [InverseProperty("MaLoaiNavigation")]
    public virtual ICollection<VatLieu> VatLieus { get; set; } = new List<VatLieu>();
}
