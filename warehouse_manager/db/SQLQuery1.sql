create database warehouse_manager;
go
use warehouse_manager;
go

-- bảng loại vật liệu
create table loai_vat_lieu (
    id bigint primary key identity(1,1),
    ten_loai varchar(255) not null,
    mo_ta nvarchar(max)
);

-- bảng loại sản phẩm
create table loai_san_pham(
    id bigint primary key identity(1,1),
    ten_loai varchar(255) not null,
    mo_ta nvarchar(max)
);

-- bảng nhà cung cấp
create table nha_cung_cap(
    id bigint primary key identity(1,1),
    ten_nha_cung_cap varchar(250),
    dia_chi varchar(255),
    so_dien_thoai varchar(20),
    email varchar(100),
    mo_ta nvarchar(max)
);

-- vật liệu
create table vat_lieu(
    id bigint primary key identity(1,1),
    ma_vat_lieu varchar(500) unique,
    don_gia decimal(18,2),
    don_vi_tinh varchar(50) default 'thùng',
    mo_ta nvarchar(max),
    ten varchar(500),
    trang_thai varchar(50),
    so_luong_ton int default 0,
    ma_nha_cung_cap bigint,
    ma_loai bigint,
    constraint fk_vl_ncc foreign key (ma_nha_cung_cap) references nha_cung_cap(id),
    constraint fk_vl_loai foreign key (ma_loai) references loai_vat_lieu(id)
);

-- bảng người dùng
create table nguoi_dung(
    id bigint primary key identity(1,1),
    ten_dang_nhap varchar(100) unique,
    mat_khau varchar(250),
    hoat_dong bit not null default 1
);

-- phiếu nhập
create table phieu_nhap(
    id bigint primary key identity(1,1),
    ngay_nhap datetime not null default getdate(),
    tong_tien decimal(18,2),
    nguoi_dung_id bigint,
    ma_nha_cung_cap bigint,
    constraint fk_pn_ncc foreign key (ma_nha_cung_cap) references nha_cung_cap(id),
    constraint fk_pn_nd foreign key (nguoi_dung_id) references nguoi_dung(id)
);

-- phiếu xuất
create table phieu_xuat(
    id bigint primary key identity(1,1),
    ngay_xuat datetime not null default getdate(),
    tong_tien decimal(18,2),
    noi_nhan varchar(500),
    nguoi_dung_id bigint,
    constraint fk_px_nd foreign key (nguoi_dung_id) references nguoi_dung(id)
);

-- sản phẩm
create table san_pham(
    id bigint primary key identity(1,1),
    ma_san_pham varchar(10) unique,
    gia_ban decimal(18,2),
    mau varchar(20),
    ten varchar(250),
    so_luong_ton int default 0,
    don_vi_tinh varchar(50) default 'máy',
    mo_ta nvarchar(max),
    ma_loai bigint,
    constraint fk_sp_loai foreign key (ma_loai) references loai_san_pham(id) 
);

-- thông số sản phẩm
create table thong_so_san_pham (
    id bigint primary key identity(1,1),
    san_pham_id bigint unique,
    cpu varchar(100),
    ram varchar(50),
    o_cung varchar(100),
    man_hinh varchar(100),
    card_do_hoa varchar(100),
    he_dieu_hanh varchar(50),
    trong_luong decimal(5,2),
    bao_hanh int,
    constraint fk_ts_sp foreign key (san_pham_id) references san_pham(id) on delete cascade
);

-- chi tiết phiếu nhập
create table chi_tiet_phieu_nhap (
    id bigint primary key identity(1,1),
    so_luong bigint,
    don_gia decimal(18,2),
    thanh_tien decimal(18,2),
    don_vi_tinh varchar(50) default 'thùng',
    phieu_nhap_id bigint,
    vat_lieu_id bigint,
    constraint fk_ctpn_pn foreign key (phieu_nhap_id) references phieu_nhap(id) on delete cascade,
    constraint fk_ctpn_vl foreign key (vat_lieu_id) references vat_lieu(id)
);

-- chi tiết phiếu xuất
create table chi_tiet_phieu_xuat (
    id bigint primary key identity(1,1),
    so_luong bigint,
    don_gia decimal(18,2),
    thanh_tien decimal(18,2),
    don_vi_tinh varchar(50) default 'máy',
    phieu_xuat_id bigint,
    san_pham_id bigint,
    constraint fk_ctpx_px foreign key (phieu_xuat_id) references phieu_xuat(id) on delete cascade,
    constraint fk_ctpx_sp foreign key (san_pham_id) references san_pham(id)
);

create table ke(
	id bigint primary key identity(1,1),
	ma_ke varchar(20) ,
	vi_tri varchar(255),
	mo_ta text
)

create table ke_vat_lieu(
	vat_lieu_id bigint,
	ke_id bigint,
	constraint pk_ke_vat_lieu primary key (vat_lieu_id, ke_id),
	constraint fk_vl foreign key (vat_lieu_id) references vat_lieu(id),
	constraint fk_k foreign key (ke_id) references ke(id)
)

-- tạo index tối ưu hiệu năng
create index idx_vat_lieu_loai on vat_lieu(ma_loai);
create index idx_vat_lieu_ncc on vat_lieu(ma_nha_cung_cap);
create index idx_san_pham_loai on san_pham(ma_loai);
create unique index idx_ts_san_pham_id on thong_so_san_pham(san_pham_id);
create index idx_pn_ncc on phieu_nhap(ma_nha_cung_cap);
create index idx_ctpn_pn on chi_tiet_phieu_nhap(phieu_nhap_id);
create index idx_ctpn_vl on chi_tiet_phieu_nhap(vat_lieu_id);
create index idx_px_user on phieu_xuat(nguoi_dung_id);
create index idx_ctpx_px on chi_tiet_phieu_xuat(phieu_xuat_id);
create index idx_ctpx_sp on chi_tiet_phieu_xuat(san_pham_id);

--sửa 
--ALTER TABLE nguoi_dung
--DROP CONSTRAINT UQ__nguoi_du__6781B7B8B70964EB;

--alter table [dbo].[nguoi_dung] drop column [ma_nhan_vien]

alter table ke 
add constraint unique_ma_ke unique (ma_ke)

EXEC sp_rename '[dbo].[ke].[vi_tri]', 'khu', 'COLUMN';