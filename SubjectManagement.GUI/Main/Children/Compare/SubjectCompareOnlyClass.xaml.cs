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
    /// Interaction logic for SubjectCompareOnlyClass.xaml
    /// </summary>
    public partial class SubjectCompareOnlyClass : UserControl
    {
        public SubjectCompareOnlyClass(Class _class1, Class _class2)
        {
            InitializeComponent();
            _ClassCurent = _class1;
            _ClassCompare = _class2;

            LoadSubjectComapre();
        }
        private Class _ClassCurent { get; init; }
        private Class _ClassCompare { get; init; }

        private void LoadSubjectComapre()
        {
            var compare = new CompareController();
            compare.CompareOnlyClass(_ClassCurent,_ClassCompare, RenderBody);
        }

        private void UIElement_OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (sender is not ScrollViewer scv) return;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;

        }
    }
}
