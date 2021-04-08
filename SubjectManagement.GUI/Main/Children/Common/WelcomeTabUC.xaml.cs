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
            try
            {
                Process.Start(@"mailto:vothuan1407@gmail.com?subject=Test&body=Hello");
            }
            catch (Exception exception)
            {

            }
        }

        private void DonateButton_OnClick(object sender, RoutedEventArgs e)
        {
            Process.Start("https://www.facebook.com/anome69/");
        }

        private void ChatButton_OnClick(object sender, RoutedEventArgs e)
        {
            Process.Start("https://zalo.me/anome69");
        }

        private void GitHubButton_OnClick(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/VoThanhThuan");
        }

        private void FacebookButton_OnClick(object sender, RoutedEventArgs e)
        {
            Process.Start("https://www.facebook.com/anome69/");
        }
    }
}
