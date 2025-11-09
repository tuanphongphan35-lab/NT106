using BCrypt.Net;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace Server
{
    public class Database
    {
        private OpenFileDialog openFileDialog = new OpenFileDialog();
        public static  List<Account> ReadListAccount()
        {
            // Giả sử chúng ta đọc từ một nguồn dữ liệu và trả về danh sách tài khoản
            List<Account> accounts = new List<Account>();
            {
                using (SqlConnection connection = Connection.getSQLConnection())
                {
                    string query = "SELECT Username, Password FROM Users";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string taiKhoan = reader.GetString(0);
                                string matKhau = reader.GetString(1);

                                Account tk = new Account(taiKhoan, matKhau);
                                accounts.Add(tk);
                            }
                        }
                    }
                }

                return accounts;
            }
        }
        public static async Task<bool> LuuThongTinNguoiDung(int  userID, string tenNguoiDung, DateTime ngaySinh, string gioiTinh, byte[]? fileAnh)
        {
            using (SqlConnection connection = Connection.getSQLConnection())
            {
                try
                {
                    // Xây dựng câu query để UPDATE tất cả trong 1 lệnh
                    string query = @"UPDATE Users 
                             SET 
                                 Email = @Email, 
                                 TenNguoiDung = @Ten, 
                                 NgaySinh = @NgaySinh, 
                                 GioiTinh = @GioiTinh";

                    // Chỉ cập nhật Avatar NẾU có ảnh mới (tránh bị NULL)
                    if (fileAnh != null)
                    {
                        query += ", Avatar = @Avatar";
                    }

                    query += " WHERE UserID = @UserID"; // Lọc theo UserID

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", userID);
                        command.Parameters.AddWithValue("@Ten", tenNguoiDung);
                        command.Parameters.AddWithValue("@NgaySinh", ngaySinh);
                        command.Parameters.AddWithValue("@GioiTinh", gioiTinh);

                        if (fileAnh != null)
                        {
                            command.Parameters.AddWithValue("@Avatar", fileAnh);
                        }

                        await connection.OpenAsync();
                        int rowsAffected = await command.ExecuteNonQueryAsync();
                        return rowsAffected > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi LuuThongTinNguoiDung: " + ex.Message);
                    return false;
                }
            }
        }
        public static bool   KiemTraDangNhap(string taiKhoan, string matKhau)
        {
            string matKhauDaLuu = null; // Biến để lưu mật khẩu đã băm từ DB

            try
            {
                string query = "SELECT Password FROM Users WHERE Username = @user";

                using (SqlConnection conn = Connection.getSQLConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@user", taiKhoan);
                        object result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value) 
                        {
                            matKhauDaLuu = result.ToString();
                        }
                    }
                } 

                if (matKhauDaLuu == null)
                {
                    return false;
                }
                return BCrypt.Net.BCrypt.Verify(matKhau, matKhauDaLuu);
            }
            catch (Exception ex)
            {

                Console.WriteLine("Lỗi KiemTraDangNhap: " + ex.Message);
                return false;
            }
        }
        public static async Task<bool> KiemTraTonTaiTaiKhoan(string taiKhoan)
        {
            bool tonTai = false;

            using (SqlConnection connection = Connection.getSQLConnection())
            {
                string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", taiKhoan);

                    await connection.OpenAsync();

                    object? result = await command.ExecuteScalarAsync();

                    if (result != null && result != DBNull.Value)
                    {
                        int count = Convert.ToInt32(result);
                        tonTai = (count > 0);
                    }
                }
            }

            return tonTai;
        }
        public static async Task<bool> KiemTraTonTaiEmail(string email)
        {
            bool tonTai = false;

            using (SqlConnection connection = Connection.getSQLConnection())
            {
                string query = "SELECT COUNT(*) FROM Users WHERE Email = @Email";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);

                    await connection.OpenAsync();

                    object? result = await command.ExecuteScalarAsync();

                    if (result != null && result != DBNull.Value)
                    {
                        int count = Convert.ToInt32(result);
                        tonTai = (count > 0);
                    }
                }
            }

            return tonTai;
        }
        public static async Task<bool>  ThemTaiKhoan(string taiKhoan, string matKhau, string email, byte[] fileAnh)
        {
            string matKhauDaBam = await Task.Run(() => BCrypt.Net.BCrypt.HashPassword(matKhau));
            using (SqlConnection connection = Connection.getSQLConnection())
            {
                try
                {
                    string query = "INSERT INTO Users (Username, Email, Password, Avatar, TenNguoiDung, NgaySinh, GioiTinh)" +
                        "VALUES(@user, @email, @pass, @avatar, @ten, @ngay, @gt)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@user", taiKhoan);
                        command.Parameters.AddWithValue("@pass", matKhauDaBam);
                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@ten", "Null");
                        command.Parameters.AddWithValue("@ngay", DateTime.Now); // Ngày sinh mặc định
                        command.Parameters.AddWithValue("@gt", "Null"); // Giới tính mặc định

                        // Thêm avatar vào
                        if (fileAnh != null)
                        {
                            command.Parameters.AddWithValue("@avatar", fileAnh);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@avatar", DBNull.Value);

                        }
                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi ThemTaiKhoan: " + ex.Message);
                    return false;
                }
            }
        }
        public static byte[]? LayAvatar(string taiKhoan)
        {
            byte[]? avatar = null;
            using (SqlConnection connection = Connection.getSQLConnection())
            {
                try
                {
                    string query = "SELECT Avatar FROM Users WHERE Username = @Username";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", taiKhoan);
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            avatar = (byte[])result;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi Lấy Avatar: " + ex.Message);
                }
            }
            return avatar;
        }
        public static async Task<UserInfo> LayThongTinNguoiDung(int userID)
        {
            UserInfo? user = null;
            using (SqlConnection connection = Connection.getSQLConnection())
            {
                string query = @"
            SELECT TenNguoiDung, Email, NgaySinh, GioiTinh, Avatar 
            FROM Users 
            WHERE UserID = @ID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", userID);
                    await connection.OpenAsync();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            user = new UserInfo
                            {
                                // Kiểm tra DBNull trước khi đọc
                                TenNguoiDung = reader["TenNguoiDung"] != DBNull.Value ? reader["TenNguoiDung"].ToString() : string.Empty,
                                Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : string.Empty,
                                NgaySinh = reader["NgaySinh"] != DBNull.Value ? Convert.ToDateTime(reader["NgaySinh"]) : new DateTime(2000, 1, 1),
                                GioiTinh = reader["GioiTinh"] != DBNull.Value ? reader["GioiTinh"].ToString() : string.Empty,
                                Avatar = reader["Avatar"] != DBNull.Value ? (byte[])reader["Avatar"] : null
                            };
                        }
                    }
                }
            }
            return user;
        }
        public static string LayMatKhauQuenMatKhau(string email)
        {
            string matKhau = string.Empty;

            using (SqlConnection connection = Connection.getSQLConnection())
            {
                try
                {
                    string query = "SELECT MatKhau FROM Users  WHERE Email = @Email";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);

                        connection.Open();

                        object result = command.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            matKhau = result.ToString();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy email trong hệ thống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Xử lý ngoại lệ và hiển thị thông báo cụ thể về lỗi
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return matKhau;
        }
        public static int LayIDNguoiDung(string username)
        {
            try
            {
                string query = "SELECT UserID FROM dbo.Users WHERE Username = @user";
                using (SqlConnection conn = Connection.getSQLConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@user", username);

                        // Dùng ExecuteScalar vì chỉ lấy 1 giá trị
                        object result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            return Convert.ToInt32(result); // Trả về UserID
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi LayIDNguoiDung: " + ex.Message);
            }
            return 0; // Trả về 0 nếu không tìm thấy hoặc có lỗi
        }

        public static async Task<bool> KiemTraTonTaiMatKhau(string matKhau)
        {
            bool tonTai = false;

            using (SqlConnection connection = Connection.getSQLConnection())
            {
                string query = "SELECT COUNT(*) FROM Users WHERE MatKhau = @MatKhau";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MatKhau", matKhau);

                    await connection.OpenAsync();

                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        int count = Convert.ToInt32(result);
                        tonTai = (count > 0);
                    }
                }
            }

            return tonTai;
        }
        public class UserInfo
        {
            public string? TenNguoiDung { get; set; }
            public string? Email { get; set; }
            public DateTime NgaySinh { get; set; }
            public string? GioiTinh { get; set; }
            public byte[]? Avatar { get; set; }
        }
    }
}
