namespace Login
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new DangNhap());
            //Application.Run(new TimKiemNguoiDung());
           //Application.Run(new DanhSachBanBe());
            //Application.Run(new ThongTinNguoiDung());
        }
    }
}