using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SubjectManagement.GUI.Main.Children.Common
{
    /// <summary>
    /// Interaction logic for WelcomeTabUC.xaml
    /// </summary>
    public partial class WelcomeTabUC : UserControl
    {
        public WelcomeTabUC()
        {
            InitializeComponent();
        }

        private void EmailButton_OnClick(object sender, RoutedEventArgs e)
        {
            Process.Start("cmd", @"/c start mailto:vothuan1407@gmail.com?subject=Xin%20ch%C3%A0o%20Thu%E1%BA%ADn&body=Hello");
        }

        private void DonateButton_OnClick(object sender, RoutedEventArgs e)
        {
            Process.Start("cmd", "/c start https://www.facebook.com/anome69/");
        }

        private void ChatButton_OnClick(object sender, RoutedEventArgs e)
        {
            Process.Start("cmd", "/c start https://zalo.me/anome69");
        }

        private void GitHubButton_OnClick(object sender, RoutedEventArgs e)
        {
            Process.Start("cmd", "/c start https://vothanhthuan.github.io/vtt");
        }

        private void FacebookButton_OnClick(object sender, RoutedEventArgs e)
        {
            Process.Start("cmd", "/c start https://www.facebook.com/anome69/");
        }
    }
}
