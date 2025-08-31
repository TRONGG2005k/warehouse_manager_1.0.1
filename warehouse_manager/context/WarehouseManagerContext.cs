using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using warehouse_manager.configuration;
using warehouse_manager.Models;

namespace warehouse_manager.context;

public partial class WarehouseManagerContext : DbContext
{
    public WarehouseManagerContext()
    {
    }

    public WarehouseManagerContext(DbContextOptions<WarehouseManagerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; }

    public virtual DbSet<ChiTietPhieuXuat> ChiTietPhieuXuats { get; set; }

    public virtual DbSet<Ke> Kes { get; set; }

    //public virtual DbSet<LoaiSanPham> LoaiSanPhams { get; set; }

    public virtual DbSet<LoaiVatLieu> LoaiVatLieus { get; set; }

    public virtual DbSet<NguoiDung> NguoiDungs { get; set; }

    public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }

    public virtual DbSet<PhieuNhap> PhieuNhaps { get; set; }

    public virtual DbSet<PhieuXuat> PhieuXuats { get; set; }

    //public virtual DbSet<SanPham> SanPhams { get; set; }

    //public virtual DbSet<ThongSoSanPham> ThongSoSanPhams { get; set; }

    public virtual DbSet<PhieuKiemKe> PhieuKiemKes { get; set; }
    public virtual DbSet<ChiTietPhieuKiemKe> ChiTietPhieuKiemKes { get; set; }
    public virtual DbSet<CoSoSanXuat> CoSoSanXuats { get; set; }
    public virtual DbSet<VatLieu> VatLieus { get; set; }

    public virtual DbSet<VaiTro> VaiTros { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(Config.GetConnectionString("MyDb"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChiTietPhieuNhap>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__chi_tiet__3213E83FB53AD9BE");

            entity.Property(e => e.DonViTinh).HasDefaultValue("thùng");

            entity.HasOne(d => d.PhieuNhap).WithMany(p => p.ChiTietPhieuNhaps).HasConstraintName("fk_ctpn_pn");

            entity.HasOne(d => d.VatLieu).WithMany(p => p.ChiTietPhieuNhaps).HasConstraintName("fk_ctpn_vl");
        });

        modelBuilder.Entity<ChiTietPhieuXuat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__chi_tiet__3213E83F19F23F9F");

            entity.Property(e => e.DonViTinh).HasDefaultValue("máy");

            entity.HasOne(d => d.PhieuXuat).WithMany(p => p.ChiTietPhieuXuats).HasConstraintName("fk_ctpx_px");

            entity.HasOne(d => d.VatLieu).WithMany(p => p.ChiTietPhieuXuats).HasConstraintName("fk_ctpx_vl");
        });

        modelBuilder.Entity<Ke>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ke__3213E83FFEE94E32");
        });
        modelBuilder.Entity<CoSoSanXuat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__co_so_sa__3213E83F1C1D8D7E");
        });
        //modelBuilder.Entity<LoaiSanPham>(entity =>
        //{
        //    entity.HasKey(e => e.Id).HasName("PK__loai_san__3213E83F1302A9C6");
        //});

        modelBuilder.Entity<LoaiVatLieu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__loai_vat__3213E83F4FA35F6B");
        });

        modelBuilder.Entity<NguoiDung>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__nguoi_du__3213E83F34795468");
           
            entity.Property(e => e.HoatDong).HasDefaultValue(true);
        });

        modelBuilder.Entity<NhaCungCap>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__nha_cung__3213E83F8EE1BB3E");
        });

        modelBuilder.Entity<PhieuNhap>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__phieu_nh__3213E83FACF203CD");

            entity.Property(e => e.NgayNhap).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.MaNhaCungCapNavigation).WithMany(p => p.PhieuNhaps).HasConstraintName("fk_pn_ncc");

            entity.HasOne(d => d.NguoiDung).WithMany(p => p.PhieuNhaps).HasConstraintName("fk_pn_nd");
        });

        modelBuilder.Entity<PhieuXuat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__phieu_xu__3213E83FBEA461AE");

            entity.Property(e => e.NgayXuat).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.CoSoSanXuat).WithMany(p => p.PhieuXuats).HasConstraintName("fk_px_csx");

            entity.HasOne(d => d.NguoiDung).WithMany(p => p.PhieuXuats).HasConstraintName("fk_px_nd");
            entity.Property(e => e.TrangThai).HasDefaultValue("CHO_DUYET");
        });

        //modelBuilder.Entity<SanPham>(entity =>
        //{
        //    entity.HasKey(e => e.Id).HasName("PK__san_pham__3213E83F9A86DB77");

        //    entity.Property(e => e.DonViTinh).HasDefaultValue("máy");
        //    entity.Property(e => e.SoLuongTon).HasDefaultValue(0);

        //    entity.HasOne(d => d.MaLoaiNavigation).WithMany(p => p.SanPhams).HasConstraintName("fk_sp_loai");
        //});

        //modelBuilder.Entity<ThongSoSanPham>(entity =>
        //{
        //    entity.HasKey(e => e.Id).HasName("PK__thong_so__3213E83FC28ADEDB");

        //    entity.HasOne(d => d.SanPham).WithOne(p => p.ThongSoSanPham).HasConstraintName("fk_ts_sp");
        //});
        modelBuilder.Entity<PhieuKiemKe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__phieu_ki__3213E83F4D8F1C2E");
            entity.Property(e => e.MaPhieu).IsUnicode(false);
            entity.Property(e => e.NgayKiemKe).HasDefaultValueSql("(getdate())");
        });
        modelBuilder.Entity<ChiTietPhieuKiemKe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__chi_tiet__3213E83F1D8E3C2E");
            entity.HasOne(d => d.PhieuKiemKe).WithMany(p => p.ChiTietPhieuKiemKes).HasConstraintName("fk_ctpkk_pkk");
            entity.HasOne(d => d.VatLieu).WithMany(p => p.ChiTietPhieuKiemKes).HasConstraintName("fk_ctpkk_vl");
        });
        modelBuilder.Entity<VatLieu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__vat_lieu__3213E83F5455BCC9");

            entity.Property(e => e.DonViTinh).HasDefaultValue("thùng").IsUnicode();
            entity.Property(e => e.SoLuongTon).HasDefaultValue(0);

            entity.HasOne(d => d.MaLoaiNavigation).WithMany(p => p.VatLieus).HasConstraintName("fk_vl_loai");

            entity.HasOne(d => d.MaNhaCungCapNavigation).WithMany(p => p.VatLieus).HasConstraintName("fk_vl_ncc");
            
            entity.HasMany(d => d.Kes).WithMany(p => p.VatLieus)
                .UsingEntity<Dictionary<string, object>>(
                    "KeVatLieu",
                    r => r.HasOne<Ke>().WithMany()
                        .HasForeignKey("KeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_k"),
                    l => l.HasOne<VatLieu>().WithMany()
                        .HasForeignKey("VatLieuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_vl"),
                    j =>
                    {
                        j.HasKey("VatLieuId", "KeId").HasName("pk_ke_vat_lieu");
                        j.ToTable("ke_vat_lieu");
                        j.IndexerProperty<long>("VatLieuId").HasColumnName("vat_lieu_id");
                        j.IndexerProperty<long>("KeId").HasColumnName("ke_id");
                    });
        });

        modelBuilder.Entity<VaiTro>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__vai_tro__3213E83F2D7E2C8E");
            entity.HasMany(d => d.NguoiDungs).WithMany(p => p.VaiTros)
                .UsingEntity<Dictionary<string, object>>(
                    "NguoiDungVaiTro",
                    r => r.HasOne<NguoiDung>().WithMany()
                        .HasForeignKey("NguoiDungId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_nd"),
                    l => l.HasOne<VaiTro>().WithMany()
                        .HasForeignKey("VaiTroId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_vt"),
                    j =>
                    {
                        j.HasKey("VaiTroId", "NguoiDungId").HasName("pk_nguoi_dung_vai_tro");
                        j.ToTable("nguoi_dung_vai_tro");
                        j.IndexerProperty<long>("VaiTroId").HasColumnName("vai_tro_id");
                        j.IndexerProperty<long>("NguoiDungId").HasColumnName("nguoi_dung_id");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
