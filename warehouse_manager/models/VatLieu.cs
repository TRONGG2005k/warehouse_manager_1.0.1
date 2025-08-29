using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace warehouse_manager.Models;

[Table("vat_lieu")]
[Index("MaVatLieu", Name = "UQ__vat_lieu__5DB2F7BDE532D564", IsUnique = true)]
[Index("MaLoai", Name = "idx_vat_lieu_loai")]
[Index("MaNhaCungCap", Name = "idx_vat_lieu_ncc")]
public partial class VatLieu
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("ma_vat_lieu")]
    [StringLength(500)]
    [Unicode(false)]
    public string? MaVatLieu { get; set; }

    [Column("don_gia", TypeName = "decimal(18, 2)")]
    public decimal? DonGia { get; set; }

    [Column("don_vi_tinh")]
    [StringLength(50)]
    [Unicode(true)]
    public string? DonViTinh { get; set; }

    [Column("mo_ta")]
    public string? MoTa { get; set; }

    [Column("ten")]
    [StringLength(500)]
    [Unicode(true)]
    public string? Ten { get; set; }

    [Column("trang_thai")]
    [StringLength(50)]
    [Unicode(true)]
    public string? TrangThai { get; set; }

    [Column("so_luong_ton")]
    public int? SoLuongTon { get; set; }

    [Column("ma_nha_cung_cap")]
    public long? MaNhaCungCap { get; set; }

    [Column("ma_loai")]
    public long? MaLoai { get; set; }

    [InverseProperty("VatLieu")]
    public virtual ICollection<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; } = new List<ChiTietPhieuNhap>();

    [ForeignKey("MaLoai")]
    [InverseProperty("VatLieus")]
    public virtual LoaiVatLieu? MaLoaiNavigation { get; set; }

    [ForeignKey("MaNhaCungCap")]
    [InverseProperty("VatLieus")]
    public virtual NhaCungCap? MaNhaCungCapNavigation { get; set; }

    [InverseProperty("VatLieus")]
    public virtual ICollection<Ke> Kes { get; set; } = new List<Ke>();

    public ICollection<ChiTietPhieuXuat> ChiTietPhieuXuats { get; set; } = new List<ChiTietPhieuXuat>();
}
