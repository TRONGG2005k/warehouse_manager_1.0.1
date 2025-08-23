using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace warehouse_manager.Models;

[Table("phieu_xuat")]
[Index("NguoiDungId", Name = "idx_px_user")]
public partial class PhieuXuat
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("ngay_xuat", TypeName = "datetime")]
    public DateTime NgayXuat { get; set; }

    [Column("tong_tien", TypeName = "decimal(18, 2)")]
    public decimal? TongTien { get; set; }

    [Column("noi_nhan")]
    [StringLength(500)]
    [Unicode(false)]
    public string? NoiNhan { get; set; }

    [Column("nguoi_dung_id")]
    public long? NguoiDungId { get; set; }

    [InverseProperty("PhieuXuat")]
    public virtual ICollection<ChiTietPhieuXuat> ChiTietPhieuXuats { get; set; } = new List<ChiTietPhieuXuat>();

    [ForeignKey("NguoiDungId")]
    [InverseProperty("PhieuXuats")]
    public virtual NguoiDung? NguoiDung { get; set; }
}
