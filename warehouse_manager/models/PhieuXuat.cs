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

    [Column("nguoi_dung_id")]
    public long? NguoiDungId { get; set; }

    [Column("co_so_san_xuat_id")]
    public long? CoSoSanXuatId { get; set; }

    [InverseProperty("PhieuXuat")]
    public virtual ICollection<ChiTietPhieuXuat> ChiTietPhieuXuats { get; set; } = new List<ChiTietPhieuXuat>();

    [ForeignKey("NguoiDungId")]
    [InverseProperty("PhieuXuats")]
    public virtual NguoiDung? NguoiDung { get; set; }

    [ForeignKey("CoSoSanXuatId")]
    [InverseProperty("PhieuXuats")]
    public CoSoSanXuat? CoSoSanXuat { get; set; }

    [Column("ghi_chu")]
    [Unicode(true)]
    public string ? GhiChu { get; set; }
    [Column("trang_thai")]
    [Unicode(true)]
    public string ? TrangThai { get; set; }
}
