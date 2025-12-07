using BCrypt.Net;
using Google.Cloud.Firestore; // Dùng cho Firestore
using Firebase.Storage;       // Dùng cho Firebase Storage
using Server;          // Giả sử các Model (Users, Account) ở đây
using System.IO;              // Dùng cho MemoryStream
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;            // Dùng cho FirstOrDefault()
using System.Windows.Forms;


namespace Server
{
    public class Database
    {

        private static async Task<string> UploadAvatar(byte[] fileData, string userName)
        {
            // CẦN THAY THẾ bằng URL của bạn!
            var storageBucketUrl = "YOUR_FIREBASE_STORAGE_BUCKET_URL";

            var fileName = $"avatars/{userName}_{DateTime.Now.Ticks}.jpg";

            var storage = new FirebaseStorage(storageBucketUrl);

            // Bắt đầu Upload và trả về URL
            string downloadUrl = await storage
                .Child(fileName)
                .PutAsync(new MemoryStream(fileData));

            return downloadUrl;
        }

        public static async Task<List<Account>> ReadListAccount()
        {
            FirestoreDb db = FirestoreHelper.GetDatabase();
            List<Account> accounts = new List<Account>();

            try
            {
                // Lấy tất cả Documents trong Collection "Users"
                QuerySnapshot snapshot = await db.Collection("Users").GetSnapshotAsync();

                foreach (DocumentSnapshot document in snapshot.Documents)
                {
                    if (document.Exists)
                    {
                        // Ánh xạ Document sang Model Users (cần có [FirestoreData])
                        var user = document.ConvertTo<Users>();

                        // Chuyển đổi sang Account Model cũ (nếu cần)
                        accounts.Add(new Account(user.Username, user.Password));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi ReadListAccount: " + ex.Message);
            }
            return accounts;
        }

        public static async Task<bool> LuuThongTinNguoiDung(string userID, string tenNguoiDung, DateTime ngaySinh, string gioiTinh, byte[]? fileAnh)
        {
            FirestoreDb db = FirestoreHelper.GetDatabase();
            string newAvatarUrl = null;

            if (fileAnh != null && fileAnh.Length > 0)
            {
                try
                {
                    // 1. Upload ảnh mới lên Firebase Storage
                    newAvatarUrl = await UploadAvatar(fileAnh, userID);
                }
                catch (Exception uploadEx)
                {
                    Console.WriteLine("Lỗi Upload Avatar: " + uploadEx.Message);
                }
            }

            // 2. Chuẩn bị Dictionary cập nhật
            Dictionary<string, object> updates = new Dictionary<string, object>
            {
                {"tenNguoiDung", tenNguoiDung}, 
                // Chuyển DateTime sang Firestore Timestamp (quan trọng!)
                {"ngaysinh", Timestamp.FromDateTime(ngaySinh.ToUniversalTime())},
                {"gioitinh", gioiTinh}
            };

            if (!string.IsNullOrEmpty(newAvatarUrl))
            {
                updates.Add("avatar", newAvatarUrl); // Cập nhật URL mới vào Firestore
            }

            try
            {
                // 3. Sử dụng SetAsync(MergeAll) để ghi đè các trường đã tồn tại
                DocumentReference userDocRef = db.Collection("Users").Document(userID);
                await userDocRef.SetAsync(updates, SetOptions.MergeAll);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi LuuThongTinNguoiDung Firestore: " + ex.Message);
                return false;
            }
        }

        public static async Task<bool> KiemTraDangNhap(string taiKhoan, string matKhau)
        {
            FirestoreDb db = FirestoreHelper.GetDatabase();

            try
            {
                // 1. Tìm Document theo Username
                Query query = db.Collection("Users").WhereEqualTo("username", taiKhoan).Limit(1);
                QuerySnapshot snapshot = await query.GetSnapshotAsync();

                if (snapshot.Documents.Count > 0)
                {
                    // 2. Lấy mật khẩu đã băm từ Document
                    var user = snapshot.Documents.First().ConvertTo<Users>();
                    string matKhauDaLuu = user.Password;

                    // 3. So sánh BCrypt
                    return BCrypt.Net.BCrypt.Verify(matKhau, matKhauDaLuu);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi KiemTraDangNhap: " + ex.Message);
            }
            return false;
        }

        public static async Task<bool> KiemTraTonTaiTaiKhoan(string taiKhoan)
        {
            FirestoreDb db = FirestoreHelper.GetDatabase();

            Query query = db.Collection("Users")
                            .WhereEqualTo("username", taiKhoan)
                            .Limit(1);

            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            // Nếu có document nào trả về, tức là tài khoản tồn tại
            return snapshot.Documents.Count > 0;
        }
        public static async Task<bool> KiemTraTonTaiEmail(string email)
        {
            FirestoreDb db = FirestoreHelper.GetDatabase();

            Query query = db.Collection("Users")
                            .WhereEqualTo("email", email)
                            .Limit(1);

            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            return snapshot.Documents.Count > 0;
        }

        public static async Task<bool> ThemTaiKhoan(string taiKhoan, string matKhau, string email, byte[] fileAnh)
        {
            string matKhauDaBam = await Task.Run(() => BCrypt.Net.BCrypt.HashPassword(matKhau));
            string avatarUrl = string.Empty;

            if (fileAnh != null && fileAnh.Length > 0)
            {
                // Upload ảnh và lấy URL
                avatarUrl = await UploadAvatar(fileAnh, taiKhoan);
            }

            FirestoreDb db = FirestoreHelper.GetDatabase();

            var newUser = new Users
            {
                Username = taiKhoan,
                Password = matKhauDaBam,
                Email = email,
                AvatarUrl = avatarUrl,
                // Đảm bảo cung cấp các trường mặc định cho Firestore
                DisplayName = "Null",
                DateOfBirth = Timestamp.FromDateTime(DateTime.Now.ToUniversalTime()),
                Gender = "Null",
            };

            try
            {
                // Thêm Document mới. Firestore tự động tạo ID.
                await db.Collection("Users").AddAsync(newUser);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi ThemTaiKhoan: " + ex.Message);
                return false;
            }
        }

        public static async Task<string> LayAvatarUrl(string taiKhoan)
        {
            FirestoreDb db = FirestoreHelper.GetDatabase();

            try
            {
                Query query = db.Collection("Users").WhereEqualTo("username", taiKhoan).Limit(1);
                QuerySnapshot snapshot = await query.GetSnapshotAsync();

                if (snapshot.Documents.Count > 0)
                {
                    var user = snapshot.Documents.First().ConvertTo<Users>();
                    return user.AvatarUrl;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi LayAvatarUrl: " + ex.Message);
            }
            return null;
        }

        public static async Task<UserInfo> LayThongTinNguoiDung(string userID)
        {
            FirestoreDb db = FirestoreHelper.GetDatabase();

            try
            {
                DocumentReference docRef = db.Collection("Users").Document(userID);
                DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

                if (snapshot.Exists)
                {
                    var user = snapshot.ConvertTo<Users>();

                    // Chuyển đổi từ Users Model (FireStore) sang UserInfo Model (WinForms cũ)
                    return new UserInfo
                    {
                        TenNguoiDung = user.DisplayName,
                        Email = user.Email,
                        NgaySinh = user.DateOfBirth.ToDateTime(),
                        GioiTinh = user.Gender,
                        // Avatar giờ là URL (không phải byte[])
                        Avatar = null
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi LayThongTinNguoiDung: " + ex.Message);
            }
            return null;
        }

        public static async Task<string> LayMatKhauQuenMatKhau(string email)
        {
            FirestoreDb db = FirestoreHelper.GetDatabase();

            try
            {
                Query query = db.Collection("Users").WhereEqualTo("email", email).Limit(1);
                QuerySnapshot snapshot = await query.GetSnapshotAsync();

                if (snapshot.Documents.Count > 0)
                {
                    var user = snapshot.Documents.First().ConvertTo<Users>();
                    return user.Password;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy email trong hệ thống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return string.Empty;
        }

        public static async Task<string> LayIDNguoiDung(string username)
        {
            FirestoreDb db = FirestoreHelper.GetDatabase();

            try
            {
                Query query = db.Collection("Users").WhereEqualTo("username", username).Limit(1);
                QuerySnapshot snapshot = await query.GetSnapshotAsync();

                if (snapshot.Documents.Count > 0)
                {
                    return snapshot.Documents.First().Id;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi LayIDNguoiDung: " + ex.Message);
            }
            return null;
        }

        public static async Task<string?> LayUsernameTuEmail(string email)
        {
            FirestoreDb db = FirestoreHelper.GetDatabase();

            try
            {
                // 1. Tạo Query: Lọc Documents có trường "email" bằng giá trị input
                Query query = db.Collection("Users")
                                .WhereEqualTo("email", email)
                                .Limit(1);

                QuerySnapshot snapshot = await query.GetSnapshotAsync();

                if (snapshot.Documents.Count > 0)
                {
                    // 2. Lấy Document đầu tiên và trả về trường Username
                    // Lưu ý: Tên trường trong Firestore là "username"
                    return snapshot.Documents.First().GetValue<string>("username");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi LayUsernameTuEmail: " + ex.Message);
            }
            return null; // Trả về null nếu không tìm thấy hoặc có lỗi
        }

        public static async Task<bool> UpdatePasswordAsync(string userID, string matKhauDaBam)
        {
            FirestoreDb db = FirestoreHelper.GetDatabase();
            // 1. Chuẩn bị dữ liệu cập nhật
            Dictionary<string, object> updates = new Dictionary<string, object>
            {
                { "Password", matKhauDaBam }
            };
            try
            {
                // 2. Chỉ định Document cần cập nhật bằng userID (ID Document Firestore)
                DocumentReference userDocRef = db.Collection("Users").Document(userID);

                // 3. Thực hiện cập nhật bằng SetAsync với tùy chọn MergeAll
                await userDocRef.SetAsync(updates, SetOptions.MergeAll);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi UpdatePasswordAsync: " + ex.Message);
                return false;
            }
        }

        public class UserInfo
        {
            public string? TenNguoiDung { get; set; }
            public string? Email { get; set; }
            public DateTime NgaySinh { get; set; }
            public string? GioiTinh { get; set; }
            public byte[]? Avatar { get; set; } // Giữ lại cho UI cũ, nhưng giá trị sẽ luôn là NULL
        }
    }
}
