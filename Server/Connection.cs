using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class Connection
    {
        // Ở đây bạn có thể thêm các phương thức và thuộc tính liên quan đến kết nối cơ sở dữ liệu lay tu D:\Database
        private static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ChatApp;Integrated Security=True;TrustServerCertificate=True";
        private static SqlConnection? connection;

        internal static SqlConnection getSQLConnection()
        {
            throw new NotImplementedException();
        }
    }
}
