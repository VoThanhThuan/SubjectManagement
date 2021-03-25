using System;
using System.Collections.Generic;
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
using SubjectManagement.Application.SubjectApp;
using SubjectManagement.GUI.Controller;

namespace SubjectManagement.GUI.Main.Children.ViewListCourses
{
    /// <summary>
    /// Interaction logic for ViewListCoursesUC.xaml
    /// </summary>
    public partial class ListExpanderCoursesUC : UserControl
    {
        public ListExpanderCoursesUC()
        {
            InitializeComponent();
        }

        public Grid _g_loading;

        private async void LoadListSubject()
        {
            var load = new LoadListController();
            load.LoadList(renderBody, _g_loading);
        }

        private void btn_AddOpen_Click(object sender, RoutedEventArgs e)
        {
            var addSubject = new AddSubjectWindow();
            addSubject.Show();
        }

        private void Btn_Reload_OnClick(object sender, RoutedEventArgs e)
        {
            LoadListSubject();
        }

        private void Btn_Edit_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
