using System;
using SubjectManagement.GUI.Main.Children.Common;
using System.Windows;
using System.Windows.Controls;
using System.Threading;
using System.Windows.Input;
using System.Windows.Threading;
using MaterialDesignThemes.Wpf;
using SubjectManagement.GUI.Controller;

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
            InitialTabablzControl.NewItemFactory = () =>
            {
                var tabItem = new TabItem() {Header = "NewTab"};
                tabItem.Content = new NewTabUC(tabItem);
                return tabItem;
            };
            var load = new FacultyController();
            load.LoadFacultyAndClass();
            var welcome = new TabItem() {Header = "Welcome", Content = new WelcomeTabUC()};
            InitialTabablzControl.Items.Add(welcome);


        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var newItem = new TabItem() { Header = "NewTab" };
            newItem.Content = new NewTabUC(newItem);
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
            System.Windows.Application.Current.Shutdown();
        }

        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }


    }
}
