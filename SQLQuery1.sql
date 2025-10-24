CREATE TABLE TaiKhoan_Server (
    TaiKhoan nvarchar(50) PRIMARY KEY,
    MatKhau nvarchar(50) NOT NULL,
    Email nvarchar(100) NOT NULL UNIQUE
);