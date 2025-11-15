using Google.Cloud.Firestore; // Dùng cho FirestoreDb
using Google.Apis.Auth.OAuth2; // Dùng cho GoogleCredential
using System;
using System.IO;

namespace Server
{
    public static class FirestoreHelper // Hoặc giữ tên Connection nếu muốn
    {
        private static FirestoreDb db;

        // CẬP NHẬT TÊN FILE JSON BẠN ĐÃ LƯU
        private const string JsonPath = "nt106-7d101-firebase-adminsdk-fbsvc-aa1a28b8ca.json";
        // CẬP NHẬT PROJECT ID CỦA BẠN
        private const string ProjectId = "nt106-7d101";

        // Hàm này thay thế cho getSQLConnection()
        public static FirestoreDb GetDatabase()
        {
            if (db == null)
            {
                // **Bước 1: Thiết lập Xác thực bằng Biến Môi trường**
                string credentialsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, JsonPath);

                // Kiểm tra sự tồn tại của file trước khi thiết lập
                if (!File.Exists(credentialsPath))
                {
                    throw new FileNotFoundException("File xác thực Firebase JSON không tìm thấy. Hãy đảm bảo nó được đặt trong thư mục Output.", credentialsPath);
                }

                Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credentialsPath);

                // **Bước 2: Khởi tạo Firestore Client**
                db = FirestoreDb.Create(ProjectId);
            }
            return db;
        }
    }
}    