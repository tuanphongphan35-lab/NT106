using BCrypt.Net;
using Google.Cloud.Firestore; // Dùng cho Firestore
using Firebase.Storage;       // Dùng cho Firebase Storage
using Server;          // Giả sử các Model (Users, Account) ở đây
using System.IO;              // Dùng cho MemoryStream
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;            // Dùng cho FirstOrDefault()

// Loại bỏ các using cũ: Microsoft.Data.SqlClient, System.Drawing, v.v.
// Nếu muốn giữ MessageBox, bạn cần giữ using System.Windows.Forms; (cần reference tới System.Windows.Forms)
using System.Windows.Forms;


namespace Server
{
    // Đổi tên lớp để tránh nhầm lẫn với Database SQL cũ
    public class Database
    {
        // Loại bỏ: private OpenFileDialog openFileDialog = new OpenFileDialog();

        // --- HELPER 1: UPLOAD AVATAR TO FIREBASE STORAGE ---
        // Hàm này thay thế cho việc lưu byte[] vào SQL Server
        private static async Task<string> UploadAvatar(byte[] fileData, string userName)
        {
            // CẦN THAY THẾ bằng URL của bạn!
            var storageBucketUrl = "YOUR_FIREBASE_STORAGE_BUCKET_URL";

            // Tên file trên Storage: avatars/username_timestamp.jpg
            var fileName = $"avatars/{userName}_{DateTime.Now.Ticks}.jpg";

            var storage = new FirebaseStorage(storageBucketUrl);

            // Bắt đầu Upload và trả về URL
            string downloadUrl = await storage
                .Child(fileName)
                .PutAsync(new MemoryStream(fileData));

            return downloadUrl;
        }

        // --- HÀM CŨ: ReadListAccount() (SELECT ALL) ---
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

        // --- HÀM CŨ: LuuThongTinNguoiDung() (UPDATE) ---
        // Thay đổi int userID thành string userID (ID Document Firestore)
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

        // --- HÀM CŨ: KiemTraDangNhap() (SELECT PASSWORD) ---
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

        // --- HÀM CŨ: KiemTraTonTaiTaiKhoan() (SELECT COUNT) ---
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

        // --- HÀM CŨ: KiemTraTonTaiEmail() (SELECT COUNT) ---
        public static async Task<bool> KiemTraTonTaiEmail(string email)
        {
            FirestoreDb db = FirestoreHelper.GetDatabase();

            Query query = db.Collection("Users")
                            .WhereEqualTo("email", email)
                            .Limit(1);

            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            return snapshot.Documents.Count > 0;
        }

        // --- HÀM CŨ: ThemTaiKhoan() (INSERT) ---
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

        // --- HÀM CŨ: LayAvatar() (SELECT BYTE[]) ---
        // CHUYỂN ĐỔI: Hàm mới trả về URL (string)
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

        // --- HÀM CŨ: LayThongTinNguoiDung() (SELECT USER INFO) ---
        // CHUYỂN ĐỔI: int userID thành string userID
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

        // --- HÀM CŨ: LayMatKhauQuenMatKhau() (SELECT PASSWORD BY EMAIL) ---
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

        // --- HÀM CŨ: LayIDNguoiDung() (SELECT USER ID) ---
        // CHUYỂN ĐỔI: Trả về string (ID Document) thay vì int
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
                { "password", matKhauDaBam }
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

        // Giữ lại UserInfo class, nhưng nhớ Avatar giờ là URL (string) trong code mới
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