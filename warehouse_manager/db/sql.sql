CREATE TABLE [dbo].[phieu_kiem_ke](
    [id] BIGINT IDENTITY(1,1) PRIMARY KEY,
    [ngay_kiem_ke] DATETIME NOT NULL DEFAULT(GETDATE()),
    [nguoi_kiem_ke] NVARCHAR(250) NOT NULL,
    [ghi_chu] NVARCHAR(MAX) NULL
);
CREATE TABLE [dbo].[chi_tiet_kiem_ke](
    [id] BIGINT IDENTITY(1,1) PRIMARY KEY,
    [phieu_kiem_ke_id] BIGINT NOT NULL,
    [vat_lieu_id] BIGINT NOT NULL,
    [ton_he_thong] INT NOT NULL,
    [ton_thuc_te] INT NOT NULL,
    [chenh_lech] AS ([ton_thuc_te] - [ton_he_thong]) PERSISTED,

    CONSTRAINT fk_ctkk_pk FOREIGN KEY([phieu_kiem_ke_id]) REFERENCES [dbo].[phieu_kiem_ke]([id]),
    CONSTRAINT fk_ctkk_vl FOREIGN KEY([vat_lieu_id]) REFERENCES [dbo].[vat_lieu]([id])
);

ALTER TABLE [phieu_kiem_ke] ADD ma_phieu NVARCHAR(50) NOT NULL DEFAULT('');

DECLARE @id BIGINT;
SET @id = 123; -- id phiếu nhập cần lọc

SELECT 
    pn.id AS Id,
    vl.ten AS TenHang,
    vl.don_vi_tinh AS DonViTinh,
    ct.so_luong AS SoLuong,
    ISNULL(ct.don_gia, 0) AS DonGia,
    ncc.ten_nha_cung_cap AS NhaCungCap,
    k.ma_ke AS Ke,  -- có thể NULL nếu vật liệu chưa có kệ
    pn.ngay_nhap AS NgayNhap,
    vl.ma_vat_lieu AS MaVatLieu,
    loai.ten_loai AS LoaiVatLieu
FROM phieu_nhap pn
INNER JOIN nha_cung_cap ncc 
    ON pn.ma_nha_cung_cap = ncc.id
INNER JOIN chi_tiet_phieu_nhap ct 
    ON pn.id = ct.phieu_nhap_id
INNER JOIN vat_lieu vl 
    ON ct.vat_lieu_id = vl.id
INNER JOIN loai_vat_lieu loai
    ON vl.ma_loai = loai.id
LEFT JOIN ke_vat_lieu kvl
    ON vl.id = kvl.vat_lieu_id
LEFT JOIN ke k
    ON kvl.ke_id = k.id
WHERE pn.id = 57
ORDER BY pn.ngay_nhap DESC;

