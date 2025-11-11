-- Dòng này sẽ xóa bảng Users cũ (và tất cả dữ liệu bên trong)
IF OBJECT_ID('dbo.Users', 'U') IS NOT NULL
    DROP TABLE dbo.Users;

-- Dán code CREATE TABLE MỚI VÀ ĐÚNG CỦA BẠN vào đây:
CREATE TABLE [dbo].[Users] (
    [UserID] INT IDENTITY(1, 1) NOT NULL,
    [Username] NVARCHAR(100) NOT NULL,
    [Email] NVARCHAR(55) NOT NULL,
    [Password] NVARCHAR(60) NOT NULL,  -- Khuyến nghị 60 cho BCrypt
    [Avatar] VARBINARY(MAX) NULL,
    [TenNguoiDung] NVARCHAR(255) NULL,
    [NgaySinh] DATE NULL,
    [GioiTinh] NVARCHAR(50) NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([UserID] ASC),
    CONSTRAINT [UQ_Email] UNIQUE NONCLUSTERED ([Email] ASC),
    CONSTRAINT [UQ_Username] UNIQUE NONCLUSTERED ([Username] ASC)
);