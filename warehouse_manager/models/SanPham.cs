//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using Microsoft.EntityFrameworkCore;

//namespace warehouse_manager.Models;

//[Table("san_pham")]
//[Index("MaSanPham", Name = "UQ__san_pham__9D25990D9DE1134A", IsUnique = true)]
//[Index("MaLoai", Name = "idx_san_pham_loai")]
//public partial class SanPham
//{
//    [Key]
//    [Column("id")]
//    public long Id { get; set; }

//    [Column("ma_san_pham")]
//    [StringLength(10)]
//    [Unicode(false)]
//    public string? MaSanPham { get; set; }

//    [Column("gia_ban", TypeName = "decimal(18, 2)")]
//    public decimal? GiaBan { get; set; }

//    [Column("mau")]
//    [StringLength(20)]
//    [Unicode(false)]
//    public string? Mau { get; set; }

//    [Column("ten")]
//    [StringLength(250)]
//    [Unicode(false)]
//    public string? Ten { get; set; }

//    [Column("so_luong_ton")]
//    public int? SoLuongTon { get; set; }

//    [Column("don_vi_tinh")]
//    [StringLength(50)]
//    [Unicode(false)]
//    public string? DonViTinh { get; set; }

//    [Column("mo_ta")]
//    public string? MoTa { get; set; }

//    [Column("ma_loai")]
//    public long? MaLoai { get; set; }

//    [InverseProperty("SanPham")]
//    public virtual ICollection<ChiTietPhieuXuat> ChiTietPhieuXuats { get; set; } = new List<ChiTietPhieuXuat>();

//    [ForeignKey("MaLoai")]
//    [InverseProperty("SanPhams")]
//    public virtual LoaiSanPham? MaLoaiNavigation { get; set; }

//    [InverseProperty("SanPham")]
//    public virtual ThongSoSanPham? ThongSoSanPham { get; set; }
//}
