using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Windows.Forms;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Server;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Server
{
    internal class Database
    {
        public List<Account> ReadListAccount()
        {
            // Giả sử chúng ta đọc từ một nguồn dữ liệu và trả về danh sách tài khoản
            List<Account> accounts = new List<Account>();
            {
                using (SqlConnection connection = Connection.getSQLConnection())
                {
                    string query = "SELECT TaiKhoan, MatKhau FROM TaiKhoan_Server"; // Thay đổi TableName cho phù hợp

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
                        string query = "SELECT COUNT(*) FROM TaiKhoan_Server WHERE TaiKhoan = @TaiKhoan AND MatKhau = @MatKhau";
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
                            }
                        }
                    }
                    catch (Exception ex)
                    { 
                    // Console.WriteLine("Lỗi KiemTraDangNhap: " + ex.Message); 
                        ketQua = false; // Nếu lỗi thì xem như đăng nhập thất bại
                    }
                    return ketQua;
             }
        }
            

        internal static bool KiemTraTonTaiTaiKhoan(string taiKhoan)
        {
            bool tonTai = false;

            using (SqlConnection connection = Connection.getSQLConnection())
            {
                string query = "SELECT COUNT(*) FROM TaiKhoan_Server WHERE TaiKhoan = @TaiKhoan";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TaiKhoan", taiKhoan);

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

        internal static bool KiemTraTonTaiEmail(string email)
        {
            bool tonTai = false;

            using (SqlConnection connection = Connection.getSQLConnection())
            {
                string query = "SELECT COUNT(*) FROM TaiKhoan_Server WHERE Email = @Email";

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

        internal static bool  ThemTaiKhoan(string taiKhoan, string matKhau, string email)
        {
            using (SqlConnection connection = Connection.getSQLConnection())
            {
                try
                {
                    string query = "INSERT INTO TaiKhoan_Server (TaiKhoan, MatKhau, Email) VALUES (@TaiKhoan, @MatKhau, @Email)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TaiKhoan", taiKhoan);
                        command.Parameters.AddWithValue("@MatKhau", matKhau);
                        command.Parameters.AddWithValue("@Email", email);

                        connection.Open();
                        command.ExecuteNonQuery();

                        return true; // Trả về true nếu thành công
                    }
                }
                catch (Exception ex)
                {
                    // Console.WriteLine("Lỗi ThemTaiKhoan: " + ex.Message);
                    return false; // Trả về false nếu có lỗi
                }
            }
        }

        internal static string LayMatKhauQuenMatKhau(string email)
        {
            string matKhau = string.Empty;

            using (SqlConnection connection = Connection.getSQLConnection())
            {
                try
                {
                    string query = "SELECT MatKhau FROM TaiKhoan_Server WHERE Email = @Email";

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

        internal static bool KiemTraTonTaiMatKhau(string matKhau)
        {
            bool tonTai = false;

            using (SqlConnection connection = Connection.getSQLConnection())
            {
                string query = "SELECT COUNT(*) FROM TaiKhoan_Server WHERE MatKhau = @MatKhau";

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
