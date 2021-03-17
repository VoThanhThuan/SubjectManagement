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
using SubjectManagement.Main.Children.ViewListCourses;

namespace SubjectManagement.Main.Children.Common
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
            var listCourses = new ViewListCoursesUC();
            RenderBody.Children.Clear();
            RenderBody.Children.Add(listCourses);
            _titleTab.Header = "View List";
        }
    }
}
