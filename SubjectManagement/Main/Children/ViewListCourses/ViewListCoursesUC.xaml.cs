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

namespace SubjectManagement.Main.Children.ViewListCourses
{
    /// <summary>
    /// Interaction logic for ViewListCoursesUC.xaml
    /// </summary>
    public partial class ViewListCoursesUC : UserControl
    {
        public ViewListCoursesUC()
        {
            InitializeComponent();
            _subjectService = new SubjectService();
            LoadListSubject();
        }

        private ISubjectService _subjectService;

        private async void LoadListSubject()
        {
            var listSubject = await _subjectService.LoadSubject();
            dg_ListCourses.ItemsSource = listSubject.ResultObj.ToList();
        }

    }
}
