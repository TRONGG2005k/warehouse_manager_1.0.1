using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace warehouse_manager.Models;

[Table("phieu_nhap")]
[Index("MaNhaCungCap", Name = "idx_pn_ncc")]
public partial class PhieuNhap
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("ngay_nhap", TypeName = "datetime")]
    public DateTime NgayNhap { get; set; }

    [Column("tong_tien", TypeName = "decimal(18, 2)")]
    public decimal? TongTien { get; set; }

    [Column("nguoi_dung_id")]
    public long? NguoiDungId { get; set; }

    [Column("ma_nha_cung_cap")]
    public long? MaNhaCungCap { get; set; }

    [InverseProperty("PhieuNhap")]
    public virtual ICollection<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; } = new List<ChiTietPhieuNhap>();

    [ForeignKey("MaNhaCungCap")]
    [InverseProperty("PhieuNhaps")]
    public virtual NhaCungCap? MaNhaCungCapNavigation { get; set; }

    [ForeignKey("NguoiDungId")]
    [InverseProperty("PhieuNhaps")]
    public virtual NguoiDung? NguoiDung { get; set; }
}
