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
using System.Windows.Shapes;
using SubjectManagement.Data.Entities;
using SubjectManagement.GUI.Controller;

namespace SubjectManagement.GUI.Member
{
    /// <summary>
    /// Interaction logic for ViewSubjectClass.xaml
    /// </summary>
    public partial class ViewSubjectClass : Window
    {
        public ViewSubjectClass(Class _class)
        {
            InitializeComponent();
            _Class = _class;
            LoadSubject();
        }

        private Class _Class { get; init; }


        private void LoadSubject()
        {
            var subject = new SubjectController(_Class);
            dg_ListCourses.ItemsSource = subject.GetSubjectClass();
        }

    }
}
