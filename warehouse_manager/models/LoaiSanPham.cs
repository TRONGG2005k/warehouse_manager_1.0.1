//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using Microsoft.EntityFrameworkCore;

//namespace warehouse_manager.Models;

//[Table("loai_san_pham")]
//public partial class LoaiSanPham
//{
//    [Key]
//    [Column("id")]
//    public long Id { get; set; }

//    [Column("ten_loai")]
//    [StringLength(255)]
//    [Unicode(false)]
//    public string TenLoai { get; set; } = null!;

//    [Column("mo_ta")]
//    public string? MoTa { get; set; }

//    [InverseProperty("MaLoaiNavigation")]
//    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
//}
