//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using Microsoft.EntityFrameworkCore;

//namespace warehouse_manager.Models;

//[Table("thong_so_san_pham")]
//[Index("SanPhamId", Name = "UQ__thong_so__4D30BD4A0F8A6F5B", IsUnique = true)]
//[Index("SanPhamId", Name = "idx_ts_san_pham_id", IsUnique = true)]
//public partial class ThongSoSanPham
//{
//    [Key]
//    [Column("id")]
//    public long Id { get; set; }

//    [Column("san_pham_id")]
//    public long? SanPhamId { get; set; }

//    [Column("cpu")]
//    [StringLength(100)]
//    [Unicode(false)]
//    public string? Cpu { get; set; }

//    [Column("ram")]
//    [StringLength(50)]
//    [Unicode(false)]
//    public string? Ram { get; set; }

//    [Column("o_cung")]
//    [StringLength(100)]
//    [Unicode(false)]
//    public string? OCung { get; set; }

//    [Column("man_hinh")]
//    [StringLength(100)]
//    [Unicode(false)]
//    public string? ManHinh { get; set; }

//    [Column("card_do_hoa")]
//    [StringLength(100)]
//    [Unicode(false)]
//    public string? CardDoHoa { get; set; }

//    [Column("he_dieu_hanh")]
//    [StringLength(50)]
//    [Unicode(false)]
//    public string? HeDieuHanh { get; set; }

//    [Column("trong_luong", TypeName = "decimal(5, 2)")]
//    public decimal? TrongLuong { get; set; }

//    [Column("bao_hanh")]
//    public int? BaoHanh { get; set; }

//    [ForeignKey("SanPhamId")]
//    [InverseProperty("ThongSoSanPham")]
//    public virtual SanPham? SanPham { get; set; }
//}
