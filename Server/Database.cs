using Microsoft.Data.SqlClient;
using System;
using System.Drawing;
using BCrypt.Net;
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
        public static bool KiemTraDangNhap(string taiKhoan, string matKhau)
        {
              bool ketQua = false;

             using (SqlConnection connection = Connection.getSQLConnection())
             {
                try
                {
                        string query = "SELECT COUNT(*) FROM Users WHERE Username = @TaiKhoan AND Password = @MatKhau";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TaiKhoan", taiKhoan);
                        command.Parameters.AddWithValue("@MatKhau", matKhau);
                        connection.Open();

                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            int count = Convert.ToInt32(result);
                            ketQua = (count > 0);
                            ketQua = true; // Tài khoản và mật khẩu đúng
                        }
                        else
                        {
                            ketQua = false; // Không tìm thấy tài khoản hoặc mật khẩu không đúng
                        }
                    }
                }
                catch (Exception ex)
                { 
                    Console.WriteLine("Lỗi KiemTraDangNhap: " + ex.Message); 
                    ketQua = false; // Nếu lỗi thì xem như đăng nhập thất bại
                }
                return ketQua;
             }
        }
        public static bool KiemTraTonTaiTaiKhoan(string taiKhoan)
        {
            bool tonTai = false;

            using (SqlConnection connection = Connection.getSQLConnection())
            {
                string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", taiKhoan);

                    connection.Open();

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
        public static bool KiemTraTonTaiEmail(string email)
        {
            bool tonTai = false;

            using (SqlConnection connection = Connection.getSQLConnection())
            {
                string query = "SELECT COUNT(*) FROM Users WHERE Email = @Email";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);

                    connection.Open();

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

        public static async Task<bool>  ThemTaiKhoan(string taiKhoan, string matKhau, string email, byte[] fileAnh)
        {
            string matKhauDaBam = BCrypt.Net.BCrypt.HashPassword(matKhau);
            using (SqlConnection connection = Connection.getSQLConnection())
            {
                try
                {
                    string query = "INSERT INTO Users (Username, Email, Password, Avatar) VALUES(@user, @email, @pass, @avatar)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@user", taiKhoan);
                        command.Parameters.AddWithValue("@pass", matKhauDaBam);
                        command.Parameters.AddWithValue("@email", email);

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
        

        public static bool KiemTraTonTaiMatKhau(string matKhau)
        {
            bool tonTai = false;

            using (SqlConnection connection = Connection.getSQLConnection())
            {
                string query = "SELECT COUNT(*) FROM Users WHERE MatKhau = @MatKhau";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MatKhau", matKhau);

                    connection.Open();

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
    }
}
