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
using SubjectManagement.Data.Entities;
using SubjectManagement.GUI.Controller;

namespace SubjectManagement.GUI.Main.Children.Compare
{
    /// <summary>
    /// Interaction logic for SubjectCompareUC.xaml
    /// </summary>
    public partial class SubjectCompareUC : UserControl
    {
        public SubjectCompareUC(Class _class1, Class _class2)
        {
            InitializeComponent();
            _ClassCurent = _class1;
            _ClassCompare = _class2;
            tbl_nameClass.Text = $"{_class1.CodeClass} vs {_class2.CodeClass}";
            LoadCompare();
        }

        private Class _ClassCurent { get; init; }
        private Class _ClassCompare { get; init; }

        private void LoadCompare()
        {
            var compare = new CompareController();
            compare.CompareClass(_ClassCurent, _ClassCompare, dg_ListCourses);

        }

    }
}
