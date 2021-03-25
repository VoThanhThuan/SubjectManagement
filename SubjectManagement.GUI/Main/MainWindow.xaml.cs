using System;
using SubjectManagement.GUI.Main.Children.Common;
using System.Windows;
using System.Windows.Controls;
using System.Threading;
using System.Windows.Threading;
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
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var newItem = new TabItem() { Header = "NewTab" };
            newItem.Content = new NewTabUC(newItem);
            InitialTabablzControl.Items.Add(newItem);
        }
    }
}
