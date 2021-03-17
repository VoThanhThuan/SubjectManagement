using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Dragablz;
using SubjectManagement.Login;
using SubjectManagement.Main.Children.Common;

namespace SubjectManagement
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
                var tabItem = new TabItem()
                {
                    Header = "NewTab",
                };
                tabItem.Content = new NewTabUC(tabItem);
                return tabItem;
            };
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var newItem = new TabItem() {Header = "NewTab" };
            newItem.Content = new NewTabUC(newItem);
            InitialTabablzControl.Items.Add(newItem);
        }
    }
}
