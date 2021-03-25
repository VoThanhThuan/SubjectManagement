using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using SubjectManagement.GUI.Controller;
using SubjectManagement.GUI.Main.Children.ViewListCourses;

namespace SubjectManagement.GUI.Main.Children.Common
{
    /// <summary>
    /// Interaction logic for NewTabUC.xaml
    /// </summary>
    public partial class NewTabUC : UserControl
    {
        public NewTabUC(TabItem titleTab)
        {
            InitializeComponent();
            _titleTab = titleTab;
        }

        private TabItem _titleTab;

        private void Btn_ViewList_OnClick(object sender, RoutedEventArgs e)
        {
            _titleTab.Header = "View List";
            
            //RenderBody.Dispatcher.Invoke(new Action(() =>
            //{
                
            //}));
            var listCourses = new LoadListController();
            listCourses.LoadList(RenderBody, g_loading);
        }


        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

    }
}
