using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace warehouse_manager.Migrations
{
    /// <inheritdoc />
    public partial class TenMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoSoSanXuat",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenCoSo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SoDienThoai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__co_so_sa__3213E83F1C1D8D7E", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ke",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ma_ke = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    khu = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    mo_ta = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ke__3213E83FFEE94E32", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "loai_vat_lieu",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ten_loai = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    mo_ta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__loai_vat__3213E83F4FA35F6B", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "nguoi_dung",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ten_dang_nhap = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    mat_khau = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    hoat_dong = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__nguoi_du__3213E83F34795468", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "nha_cung_cap",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ten_nha_cung_cap = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    dia_chi = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    so_dien_thoai = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    mo_ta = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__nha_cung__3213E83F8EE1BB3E", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "phieu_kiem_ke",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ma_phieu = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ngay_kiem_ke = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    nguoi_kiem_ke = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ghi_chu = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__phieu_ki__3213E83F4D8F1C2E", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "vai_tro",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ten_vai_tro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mo_ta = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__vai_tro__3213E83F2D7E2C8E", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "phieu_xuat",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ngay_xuat = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    tong_tien = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    nguoi_dung_id = table.Column<long>(type: "bigint", nullable: true),
                    co_so_san_xuat_id = table.Column<long>(type: "bigint", nullable: true),
                    ghi_chu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    trang_thai = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "CHO_DUYET")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__phieu_xu__3213E83FBEA461AE", x => x.id);
                    table.ForeignKey(
                        name: "fk_px_csx",
                        column: x => x.co_so_san_xuat_id,
                        principalTable: "CoSoSanXuat",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "fk_px_nd",
                        column: x => x.nguoi_dung_id,
                        principalTable: "nguoi_dung",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "phieu_nhap",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ngay_nhap = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    tong_tien = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    nguoi_dung_id = table.Column<long>(type: "bigint", nullable: true),
                    ma_nha_cung_cap = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__phieu_nh__3213E83FACF203CD", x => x.id);
                    table.ForeignKey(
                        name: "fk_pn_ncc",
                        column: x => x.ma_nha_cung_cap,
                        principalTable: "nha_cung_cap",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_pn_nd",
                        column: x => x.nguoi_dung_id,
                        principalTable: "nguoi_dung",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "vat_lieu",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ma_vat_lieu = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    don_gia = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    don_vi_tinh = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValue: "thùng"),
                    mo_ta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ten = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    trang_thai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    so_luong_ton = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    ma_nha_cung_cap = table.Column<long>(type: "bigint", nullable: true),
                    ma_loai = table.Column<long>(type: "bigint", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__vat_lieu__3213E83F5455BCC9", x => x.id);
                    table.ForeignKey(
                        name: "fk_vl_loai",
                        column: x => x.ma_loai,
                        principalTable: "loai_vat_lieu",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_vl_ncc",
                        column: x => x.ma_nha_cung_cap,
                        principalTable: "nha_cung_cap",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "nguoi_dung_vai_tro",
                columns: table => new
                {
                    vai_tro_id = table.Column<long>(type: "bigint", nullable: false),
                    nguoi_dung_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_nguoi_dung_vai_tro", x => new { x.vai_tro_id, x.nguoi_dung_id });
                    table.ForeignKey(
                        name: "fk_nd",
                        column: x => x.nguoi_dung_id,
                        principalTable: "nguoi_dung",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_vt",
                        column: x => x.vai_tro_id,
                        principalTable: "vai_tro",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "chi_tiet_kiem_ke",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    phieu_kiem_ke_id = table.Column<long>(type: "bigint", nullable: false),
                    vat_lieu_id = table.Column<long>(type: "bigint", nullable: false),
                    ton_he_thong = table.Column<int>(type: "int", nullable: false),
                    ton_thuc_te = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__chi_tiet__3213E83F1D8E3C2E", x => x.id);
                    table.ForeignKey(
                        name: "fk_ctpkk_pkk",
                        column: x => x.phieu_kiem_ke_id,
                        principalTable: "phieu_kiem_ke",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_ctpkk_vl",
                        column: x => x.vat_lieu_id,
                        principalTable: "vat_lieu",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "chi_tiet_phieu_nhap",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    so_luong = table.Column<long>(type: "bigint", nullable: true),
                    don_gia = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    thanh_tien = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    don_vi_tinh = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValue: "thùng"),
                    phieu_nhap_id = table.Column<long>(type: "bigint", nullable: true),
                    vat_lieu_id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__chi_tiet__3213E83FB53AD9BE", x => x.id);
                    table.ForeignKey(
                        name: "fk_ctpn_pn",
                        column: x => x.phieu_nhap_id,
                        principalTable: "phieu_nhap",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_ctpn_vl",
                        column: x => x.vat_lieu_id,
                        principalTable: "vat_lieu",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "chi_tiet_phieu_xuat",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    so_luong_yeu_cau = table.Column<long>(type: "bigint", nullable: false),
                    so_luong_thuc_xuat = table.Column<long>(type: "bigint", nullable: false),
                    don_gia = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    thanh_tien = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    don_vi_tinh = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false, defaultValue: "máy"),
                    phieu_xuat_id = table.Column<long>(type: "bigint", nullable: false),
                    vat_lieu_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__chi_tiet__3213E83F19F23F9F", x => x.id);
                    table.ForeignKey(
                        name: "fk_ctpx_px",
                        column: x => x.phieu_xuat_id,
                        principalTable: "phieu_xuat",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_ctpx_vl",
                        column: x => x.vat_lieu_id,
                        principalTable: "vat_lieu",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ke_vat_lieu",
                columns: table => new
                {
                    vat_lieu_id = table.Column<long>(type: "bigint", nullable: false),
                    ke_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ke_vat_lieu", x => new { x.vat_lieu_id, x.ke_id });
                    table.ForeignKey(
                        name: "fk_k",
                        column: x => x.ke_id,
                        principalTable: "ke",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_vl",
                        column: x => x.vat_lieu_id,
                        principalTable: "vat_lieu",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_chi_tiet_kiem_ke_phieu_kiem_ke_id",
                table: "chi_tiet_kiem_ke",
                column: "phieu_kiem_ke_id");

            migrationBuilder.CreateIndex(
                name: "IX_chi_tiet_kiem_ke_vat_lieu_id",
                table: "chi_tiet_kiem_ke",
                column: "vat_lieu_id");

            migrationBuilder.CreateIndex(
                name: "idx_ctpn_pn",
                table: "chi_tiet_phieu_nhap",
                column: "phieu_nhap_id");

            migrationBuilder.CreateIndex(
                name: "idx_ctpn_vl",
                table: "chi_tiet_phieu_nhap",
                column: "vat_lieu_id");

            migrationBuilder.CreateIndex(
                name: "idx_ctpx_px",
                table: "chi_tiet_phieu_xuat",
                column: "phieu_xuat_id");

            migrationBuilder.CreateIndex(
                name: "IX_chi_tiet_phieu_xuat_vat_lieu_id",
                table: "chi_tiet_phieu_xuat",
                column: "vat_lieu_id");

            migrationBuilder.CreateIndex(
                name: "unique_ma_ke",
                table: "ke",
                column: "ma_ke",
                unique: true,
                filter: "[ma_ke] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "uq_ma_ke",
                table: "ke",
                column: "ma_ke",
                unique: true,
                filter: "[ma_ke] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ke_vat_lieu_ke_id",
                table: "ke_vat_lieu",
                column: "ke_id");

            migrationBuilder.CreateIndex(
                name: "IX_nguoi_dung_vai_tro_nguoi_dung_id",
                table: "nguoi_dung_vai_tro",
                column: "nguoi_dung_id");

            migrationBuilder.CreateIndex(
                name: "idx_pn_ncc",
                table: "phieu_nhap",
                column: "ma_nha_cung_cap");

            migrationBuilder.CreateIndex(
                name: "IX_phieu_nhap_nguoi_dung_id",
                table: "phieu_nhap",
                column: "nguoi_dung_id");

            migrationBuilder.CreateIndex(
                name: "idx_px_user",
                table: "phieu_xuat",
                column: "nguoi_dung_id");

            migrationBuilder.CreateIndex(
                name: "IX_phieu_xuat_co_so_san_xuat_id",
                table: "phieu_xuat",
                column: "co_so_san_xuat_id");

            migrationBuilder.CreateIndex(
                name: "idx_vat_lieu_loai",
                table: "vat_lieu",
                column: "ma_loai");

            migrationBuilder.CreateIndex(
                name: "idx_vat_lieu_ncc",
                table: "vat_lieu",
                column: "ma_nha_cung_cap");

            migrationBuilder.CreateIndex(
                name: "UQ__vat_lieu__5DB2F7BDE532D564",
                table: "vat_lieu",
                column: "ma_vat_lieu",
                unique: true,
                filter: "[ma_vat_lieu] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "chi_tiet_kiem_ke");

            migrationBuilder.DropTable(
                name: "chi_tiet_phieu_nhap");

            migrationBuilder.DropTable(
                name: "chi_tiet_phieu_xuat");

            migrationBuilder.DropTable(
                name: "ke_vat_lieu");

            migrationBuilder.DropTable(
                name: "nguoi_dung_vai_tro");

            migrationBuilder.DropTable(
                name: "phieu_kiem_ke");

            migrationBuilder.DropTable(
                name: "phieu_nhap");

            migrationBuilder.DropTable(
                name: "phieu_xuat");

            migrationBuilder.DropTable(
                name: "ke");

            migrationBuilder.DropTable(
                name: "vat_lieu");

            migrationBuilder.DropTable(
                name: "vai_tro");

            migrationBuilder.DropTable(
                name: "CoSoSanXuat");

            migrationBuilder.DropTable(
                name: "nguoi_dung");

            migrationBuilder.DropTable(
                name: "loai_vat_lieu");

            migrationBuilder.DropTable(
                name: "nha_cung_cap");
        }
    }
}
