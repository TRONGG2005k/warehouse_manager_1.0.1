using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace warehouse_manager.Models;

[Table("chi_tiet_phieu_xuat")]
[Index("PhieuXuatId", Name = "idx_ctpx_px")]

public partial class ChiTietPhieuXuat
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("so_luong_yeu_cau")]
    public long SoLuongYeuCau { get; set; }   // bắt buộc nhập

    [Column("so_luong_thuc_xuat")]
    public long SoLuongThucXuat { get; set; } // bắt buộc nhập (<= tồn kho)

    [Column("don_gia", TypeName = "decimal(18, 2)")]
    public decimal? DonGia { get; set; }

    // cột này trong DB là computed column (so_luong_thuc_xuat * don_gia)
    [Column("thanh_tien", TypeName = "decimal(18, 2)")]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public decimal? ThanhTien { get; set; }

    [Column("don_vi_tinh")]
    [StringLength(50)]
    [Unicode(false)]
    public string DonViTinh { get; set; } = null!; // lấy từ bảng vật liệu, không nên để null

    [Column("phieu_xuat_id")]
    public long PhieuXuatId { get; set; }

    [Column("vat_lieu_id")]
    public long VatLieuId { get; set; }

    [ForeignKey("PhieuXuatId")]
    [InverseProperty("ChiTietPhieuXuats")]
    public virtual PhieuXuat PhieuXuat { get; set; } = null!;

    [ForeignKey("VatLieuId")]
    [InverseProperty("ChiTietPhieuXuats")]
    public virtual VatLieu VatLieu { get; set; } = null!;
}
