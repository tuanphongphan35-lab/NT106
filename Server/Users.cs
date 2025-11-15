using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    [FirestoreData]
    internal class Users
    {
        // ID Document Firestore (string)
        [FirestoreDocumentId]
        public string Id { get; set; }

        // Tên trường: "username"
        [FirestoreProperty("username")]
        public string Username { get; set; }

        // Tên trường: "email"
        [FirestoreProperty("email")]
        public string Email { get; set; }

        // Trường Password (đã băm), thường không hiển thị ở UI nhưng cần cho đăng nhập
        [FirestoreProperty("Password")]
        public string Password { get; set; }

        // Tên trường: "tennguoidung" (Tên hiển thị)
        [FirestoreProperty("tenNguoiDung")]
        public string DisplayName { get; set; }

        // Tên trường: "gioitinh"
        [FirestoreProperty("gioitinh")]
        public string Gender { get; set; }

        // Ngày sinh (Firestore dùng Timestamp)
        [FirestoreProperty("ngaysinh")]
        public Timestamp DateOfBirth { get; set; }

        // Avatar (Lưu URL từ Firebase Storage)
        [FirestoreProperty("avatar")]
        public string? AvatarUrl { get; set; }
    }
}
