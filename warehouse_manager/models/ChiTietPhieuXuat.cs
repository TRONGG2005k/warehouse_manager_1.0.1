using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace warehouse_manager.Models;

[Table("chi_tiet_phieu_xuat")]
[Index("PhieuXuatId", Name = "idx_ctpx_px")]
[Index("SanPhamId", Name = "idx_ctpx_sp")]
public partial class ChiTietPhieuXuat
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

    [Column("phieu_xuat_id")]
    public long? PhieuXuatId { get; set; }

    [Column("san_pham_id")]
    public long? SanPhamId { get; set; }

    [ForeignKey("PhieuXuatId")]
    [InverseProperty("ChiTietPhieuXuats")]
    public virtual PhieuXuat? PhieuXuat { get; set; }

    [ForeignKey("SanPhamId")]
    [InverseProperty("ChiTietPhieuXuats")]
    public virtual SanPham? SanPham { get; set; }
}
