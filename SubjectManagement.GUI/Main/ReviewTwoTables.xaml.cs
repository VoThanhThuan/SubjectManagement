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
using SubjectManagement.Data;
using SubjectManagement.Data.Entities;
using SubjectManagement.GUI.Controller;

namespace SubjectManagement.GUI.Main
{
    /// <summary>
    /// Interaction logic for ReviewTwoTables.xaml
    /// </summary>
    public partial class ReviewTwoTables : Window
    {
        public ReviewTwoTables(Class ClassOne, Class ClassTwo)
        {
            InitializeComponent();
            _Class_1 = ClassOne;
            _Class_2 = ClassTwo;
            tbl_ClassCurrent.Text = _Class_1.CodeClass;
            tbl_ClassCompare.Text = _Class_2.CodeClass;
            LoadClassCurrent();
            LoadClassCompare();
        }


        private Class _Class_1 { get; init; }
        private Class _Class_2 { get; init; }


        private void LoadClassCurrent()
        {
            var load = new SubjectController(_Class_1);
            dg_ListCurrentSubject.ItemsSource =  load.GetSubjectClass();
        }

        private void LoadClassCompare()
        {
            var load = new SubjectController(_Class_2);
            dg_ListCompareSubjects.ItemsSource = load.GetSubjectClass();
        }

    }
}
