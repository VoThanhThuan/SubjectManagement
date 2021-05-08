using SubjectManagement.Application.System.Users;
using SubjectManagement.GUI.Member;
using SubjectManagement.ViewModels.System.Users;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using DocumentFormat.OpenXml.VariantTypes;
using MaterialDesignThemes.Wpf;
using SubjectManagement.GUI.Controller;
using SubjectManagement.GUI.Dialog;
using SubjectManagement.GUI.Main;

namespace SubjectManagement.GUI.Login
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            SetBackground();
            ConnectDatabase();

        }

        private bool IsConnected { get; set; } = false;

        private bool IsShowPassword { get; set; } = true;
        private void ConnectDatabase()
        {
            var connect = new SettingController();
            IsConnected = connect.ReadConnectString().IsSuccessed;
            if (!IsConnected) return;
            connect.ReadConnectString();
        }

        private async void Login()
        {
            if(IsConnected == false) return;
            var open = new OpenWindowController();
            var loginInfo = new LoginRequest()
            {
                Username = tbx_UserName.Text,
                Password = tbx_Password.Password
                //ListWindows = new Hashtable()
            };

            //loginInfo.ListWindows.Add("admin", new MainWindow());
            //loginInfo.ListWindows.Add("guest", new MemberWindow());
            var result = await open.OpenWindow(loginInfo, Grid_Loading);
            if (result)
                this.Close();
        }

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
            Login();
        }


        private void btn_Close_Checked(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Hyperlink_OnRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Login();
            }
        }

        private void Btn_setting_OnClick(object sender, RoutedEventArgs e)
        {
            var setting = new SettingWindow()
            {
                tab_Class = {Visibility = Visibility.Collapsed}
            };
            setting.ShowDialog();
            ConnectDatabase();
        }


        private void tbx_Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            btn_ShowPass.Visibility = tbx_Password.Password.Length switch
            {
                > 0 => Visibility.Visible,
                0 when IsShowPassword == false => Visibility.Hidden,
                _ => btn_ShowPass.Visibility
            };
        }

        private string _passShow = "";

        private void Btn_ShowPass_OnClick(object sender, RoutedEventArgs e)
        {
            if (IsShowPassword)
            {
                MaterialDesignThemes.Wpf.HintAssist.SetHint(tbx_Password, tbx_Password.Password);
                _passShow = tbx_Password.Password;
                tbx_Password.Password = "";
                tbx_Password.IsEnabled = false;
                IsShowPassword = false;
                icon_btnShowPass.Kind = PackIconKind.EyeOff;
            }
            else
            {
                MaterialDesignThemes.Wpf.HintAssist.SetHint(tbx_Password, "Password");
                tbx_Password.Password = _passShow;
                IsShowPassword = true;
                tbx_Password.IsEnabled = true;
                icon_btnShowPass.Kind = PackIconKind.Eye;
            }

        }
    }
}
