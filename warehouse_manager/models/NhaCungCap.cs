using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace warehouse_manager.Models;

[Table("nha_cung_cap")]
public partial class NhaCungCap
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("ten_nha_cung_cap")]
    [StringLength(250)]
    [Unicode(false)]
    public string? TenNhaCungCap { get; set; }

    [Column("dia_chi")]
    [StringLength(255)]
    [Unicode(false)]
    public string? DiaChi { get; set; }

    [Column("so_dien_thoai")]
    [StringLength(20)]
    [Unicode(false)]
    public string? SoDienThoai { get; set; }

    [Column("email")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Email { get; set; }

    [Column("mo_ta")]
    public string? MoTa { get; set; }

    [InverseProperty("MaNhaCungCapNavigation")]
    public virtual ICollection<PhieuNhap> PhieuNhaps { get; set; } = new List<PhieuNhap>();

    [InverseProperty("MaNhaCungCapNavigation")]
    public virtual ICollection<VatLieu> VatLieus { get; set; } = new List<VatLieu>();
}
