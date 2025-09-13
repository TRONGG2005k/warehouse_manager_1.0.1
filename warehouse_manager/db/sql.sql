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

select * from vat_lieu

select * from [nha_cung_cap]


ALTER TABLE vat_lieu
ADD is_deleted BIT NOT NULL CONSTRAINT DF_vat_lieu_is_deleted DEFAULT(0);
