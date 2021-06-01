using MaterialDesignThemes.Wpf;
using SubjectManagement.Common.Result;
using SubjectManagement.GUI.Constant;
using SubjectManagement.GUI.Controller;
using SubjectManagement.GUI.Dialog;
using SubjectManagement.GUI.Main.Children.Common;
using SubjectManagement.GUI.Main.Children.User;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using SubjectManagement.GUI.Login;

namespace SubjectManagement.GUI.Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetColor();
            OnStart();
            if (ConstantInfor.InforUser != null)
                tbl_HelloName.Text = $"Xin Chào {ConstantInfor.InforUser.Name}!";
        }

        private bool _IsReset = false;
        private void SetColor()
        {
            try
            {
                var setting = new SettingController();
                var result = setting.ReadSetting();
                if (result == null) return;

                var style = this.FindResource("OptionColor") as LinearGradientBrush;
                var c = new GradientStopCollection();
                var colorStart = (Color)ColorConverter.ConvertFromString(result.ColorStart);
                var colorEnd = (Color)ColorConverter.ConvertFromString(result.ColorEnd);

                c.Add(new GradientStop(colorStart, 0.0));
                c.Add(new GradientStop(colorEnd, 1.0));
                if (style != null) style.GradientStops = c;



                var textColor = this.FindResource("OptionTextColor") as SolidColorBrush;

                var tc = (Color)ColorConverter.ConvertFromString(result.TextColor);
                textColor.Color = tc;
            }
            catch (Exception e)
            {
                MessageBox.Show($"{e}");
            }

        }

        private void OnStart()
        {
            InitialTabablzControl.NewItemFactory = () =>
            {
                var tabItem = new TabItem() { Header = "NewTab", Tag = "New Tab" };
                tabItem.Content = new NewTabUC(tabItem, this, g_loading, tbl_HelloName);
                return tabItem;
            };
            var welcome = new TabItem() { Header = "Welcome", Content = new WelcomeTabUC(), Tag = $"Xin chào {ConstantInfor.InforUser.Name}!" };
            InitialTabablzControl.Items.Add(welcome);
            var load = new FacultyController();
        }

        //Hàm new tab nhưng giờ không còn dùng nữa.
        private void NewTab()
        {
            g_loading.Visibility = Visibility.Visible;
            var newItem = new TabItem() { Header = "NewTab", Tag = "New Tab" };
            newItem.Content = new NewTabUC(newItem, this, g_loading, tbl_HelloName);
            InitialTabablzControl.Items.Add(newItem);
            InitialTabablzControl.SelectedItem = newItem;
            g_loading.Visibility = Visibility.Hidden;
        }

        private void OpenSetting()
        {
            g_loading.Visibility = Visibility.Visible;

            var setting = new SettingController().OpenSetting();

            g_loading.Visibility = Visibility.Hidden;
            if (setting.DialogResult != MyDialogResult.Result.Ok) return;
            _IsReset = true;
            System.Windows.Application.Current.Shutdown();
        }

        private void Btn_Hide_OnClickHide(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Btn_Zoom_OnClickZoom(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
            Icon_Zoom.Kind = PackIconKind.CheckboxMultipleBlankOutline;

        }

        private void Btn_Exit_OnClickExit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }


        private void li_Shutdown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void li_Account(object sender, MouseButtonEventArgs e)
        {
            var newItem = new TabItem() { Header = "NewTab"};
            newItem.Content = new UserManagerUC();

            InitialTabablzControl.Items.Add(newItem);
        }

        private void Li_NewTab(object sender, MouseButtonEventArgs e)
        {
            NewTab();
        }

        private void Li_Settings(object sender, MouseButtonEventArgs e)
        {
            OpenSetting();
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            if (!_IsReset) return;
            var path = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            Process.Start("cmd", $"/c start {path}/SubjectManagement.GUI.exe");
        }

        private void Li_Class(object sender, MouseButtonEventArgs e)
        {
            var setting = new SettingWindow(true);
            setting.tab_Class.IsSelected = true;
            setting.ShowDialog();
            if (setting.DialogResult != MyDialogResult.Result.Ok) return;
            _IsReset = true;
            System.Windows.Application.Current.Shutdown();
        }

        private void BtnNewTab_OnClick(object sender, RoutedEventArgs e)
        {
            NewTab();
        }

        private void Li_AboutMe(object sender, MouseButtonEventArgs e)
        {
            Process.Start("cmd", "/c start https://vothanhthuan.github.io/vtt");
        }

        private void li_Logout(object sender, MouseButtonEventArgs e)
        {
            new LoginWindow().Show();
            this.Close();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            OpenSetting();
        }

        private void InitialTabablzControl_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is not TabControl) return;
            var tab = ((e.Source as Dragablz.TabablzControl)?.SelectedValue as TabItem);
            if(tab != null)
                tbl_HelloName.Text = $"{tab.Tag}";
        }
    }
}
