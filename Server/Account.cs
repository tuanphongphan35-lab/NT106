using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class Account
    {
        private string username;
        private string password;

        public Account()
        {
            // Default constructor
        }
        public  Account(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
        // Properties truy cập và sửa đổi username và password
        public string Username
        {
            get { return username; }
            set { username = value; }
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

    }
}
