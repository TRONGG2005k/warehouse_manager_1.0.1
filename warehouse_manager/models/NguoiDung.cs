using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace warehouse_manager.Models;

[Table("nguoi_dung")]
public partial class NguoiDung
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("ten_dang_nhap")]
    [StringLength(100)]
    [Unicode(false)]
    public string? TenDangNhap { get; set; }

    [Column("mat_khau")]
    [StringLength(250)]
    [Unicode(false)]
    public string? MatKhau { get; set; }

    [Column("hoat_dong")]
    public bool HoatDong { get; set; } = true;

    [InverseProperty("NguoiDung")]
    public virtual ICollection<PhieuNhap> PhieuNhaps { get; set; } = new List<PhieuNhap>();

    [InverseProperty("NguoiDung")]
    public virtual ICollection<PhieuXuat> PhieuXuats { get; set; } = new List<PhieuXuat>();
}
