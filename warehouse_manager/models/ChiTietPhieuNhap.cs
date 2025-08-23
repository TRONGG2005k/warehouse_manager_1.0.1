using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace warehouse_manager.Models;

[Table("chi_tiet_phieu_nhap")]
[Index("PhieuNhapId", Name = "idx_ctpn_pn")]
[Index("VatLieuId", Name = "idx_ctpn_vl")]
public partial class ChiTietPhieuNhap
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("so_luong")]
    public long? SoLuong { get; set; }

    [Column("don_gia", TypeName = "decimal(18, 2)")]
    public decimal? DonGia { get; set; }

    [Column("thanh_tien", TypeName = "decimal(18, 2)")]
    public decimal? ThanhTien { get; set; }

    [Column("don_vi_tinh")]
    [StringLength(50)]
    [Unicode(false)]
    public string? DonViTinh { get; set; }

    [Column("phieu_nhap_id")]
    public long? PhieuNhapId { get; set; }

    [Column("vat_lieu_id")]
    public long? VatLieuId { get; set; }

    [ForeignKey("PhieuNhapId")]
    [InverseProperty("ChiTietPhieuNhaps")]
    public virtual PhieuNhap? PhieuNhap { get; set; }

    [ForeignKey("VatLieuId")]
    [InverseProperty("ChiTietPhieuNhaps")]
    public virtual VatLieu? VatLieu { get; set; }
}
