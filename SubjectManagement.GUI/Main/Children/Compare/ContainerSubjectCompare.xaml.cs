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
using MaterialDesignThemes.Wpf;
using SubjectManagement.Data.Entities;
using SubjectManagement.GUI.Dialog;

namespace SubjectManagement.GUI.Main.Children.Compare
{
    /// <summary>
    /// Interaction logic for ContainerSubjectCompare.xaml
    /// </summary>
    public partial class ContainerSubjectCompare : UserControl
    {
        public ContainerSubjectCompare(Class _class, Class _classCompare)
        {
            InitializeComponent();
            _Class = _class;
            _ClassCompare = _classCompare;
            LoadTable();
        }

        private Class _Class { get; init; }
        private Class _ClassCompare { get; init; }

        private bool _mode = false;

        private void LoadTable()
        {
            RenderBody.Children.Clear();
            if (_mode)
            {
                var compareUC = new SubjectCompare2TableUC(_Class, _ClassCompare);
                RenderBody.Children.Add(compareUC);
                _mode = false;
                icon_btnChangeView.Kind = PackIconKind.ArrowCollapseHorizontal;
            }
            else
            {
                var compareUC = new SubjectCompareUC(_Class, _ClassCompare);
                RenderBody.Children.Add(compareUC);
                _mode = true;
                icon_btnChangeView.Kind = PackIconKind.ArrowSplitVertical;
            }
        }

        private void Btn_Export_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Btn_ChangeView_OnClick(object sender, RoutedEventArgs e)
        {
            LoadTable();
        }
    }
}
