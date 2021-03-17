using SubjectManagement.Application.System.Users;
using SubjectManagement.Dialog;
using SubjectManagement.Member;
using SubjectManagement.ViewModels.System.Users;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace SubjectManagement.Login
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            var userService = new UserService();
            _userService = userService;
        }

        private readonly IUserService _userService;


        /// <summary>
        /// Lấy ảnh nền desktop của người dùng
        /// </summary>
        private const uint SPI_GETDESKWALLPAPER = 0x73;

        private const int MAX_PATH = 260;
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SystemParametersInfo(uint uAction, int uParam, string lpvParam, int fuWinIni);
        public string GetCurrentDesktopWallpaper()
        {
            var currentWallpaper = new string('\0', MAX_PATH);
            SystemParametersInfo(SPI_GETDESKWALLPAPER, currentWallpaper.Length, currentWallpaper, 0);
            return currentWallpaper.Substring(0, currentWallpaper.IndexOf('\0'));
        }

        private void SetBackground()
        {
            var path = GetCurrentDesktopWallpaper();
            if (!File.Exists(path))
                return;
            ImgBackground.ImageSource = new BitmapImage(new Uri(path, UriKind.Relative));
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            dht_Loading.Visibility = Visibility.Visible;

            var info = new LoginRequest() { Username = tbx_UserName.Text, Password = tbx_Password.Password, RememberMe = false };
            var result = _userService.Authentivate(info);
            if (result.IsSuccessed is true)
            {
                if (result.ResultObj.Role is "admin")
                {
                    var main = new MainWindow();
                    main.Show();
                }
                else
                {
                    var member = new MemberWindow();
                    member.Show();
                }
            }
            else
            {
                var mess = new MessageDialog()
                {
                    tbl_Title = { Text = "Lỗi đăng nhập" },
                    tbl_Message = { Text = $"{result.Message}" },
                    title_color = { Background = new SolidColorBrush(Color.FromRgb(255, 0, 0)) },
                    Topmost = true
                };
                mess.ShowDialog();
            }

            dht_Loading.Visibility = Visibility.Hidden;

        }

        private void btn_Close_Checked(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Hyperlink_OnRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
