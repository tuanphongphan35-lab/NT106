/* Đảm bảo bạn đang dùng đúng database */
USE [ChatApp];
GO

/* Lệnh này sẽ THÊM cột Avatar vào bảng Users của bạn */
ALTER TABLE Users
ADD Avatar VARBINARY(MAX) NULL; 
/* - VARBINARY(MAX) là kiểu dữ liệu để lưu file/ảnh
   - NULL nghĩa là cột này được phép để trống (nếu người dùng không upload ảnh)
*/
GO