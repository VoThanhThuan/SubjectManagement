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
            if(ConstantInfor.InforUser != null)
                tbl_HelloName.Text =$"Xin Chào {ConstantInfor.InforUser.Name}";
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

            }

        }

        private void OnStart()
        {
            InitialTabablzControl.NewItemFactory = () =>
            {
                var tabItem = new TabItem() { Header = "NewTab" };
                tabItem.Content = new NewTabUC(tabItem, this);
                return tabItem;
            };
            var welcome = new TabItem() { Header = "Welcome", Content = new WelcomeTabUC() };
            InitialTabablzControl.Items.Add(welcome);
            var load = new FacultyController();
            load.LoadFacultyAndClass();
        }

        //Hàm new tab nhưng giờ không còn dùng nữa.
        private void NewTab()
        {
            var newItem = new TabItem() { Header = "NewTab" };
            newItem.Content = new NewTabUC(newItem, this);
            InitialTabablzControl.Items.Add(newItem);
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
            var newItem = new TabItem() { Header = "NewTab" };
            newItem.Content = new UserManagerUC();

            InitialTabablzControl.Items.Add(newItem);
        }

        private void Li_NewTab(object sender, MouseButtonEventArgs e)
        {
            NewTab();
        }

        private void Li_Settings(object sender, MouseButtonEventArgs e)
        {
            var setting = new SettingWindow(true);
            setting.ShowDialog();
            if(setting.DialogResult != MyDialogResult.Result.Ok) return;
            _IsReset = true;
            System.Windows.Application.Current.Shutdown();
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            if (!_IsReset) return;
            var path =System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
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
    }
}
