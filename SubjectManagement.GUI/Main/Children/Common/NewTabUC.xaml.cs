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
using SubjectManagement.Data.Entities;
using SubjectManagement.GUI.Controller;
using SubjectManagement.GUI.Main.Children.Alternative;
using SubjectManagement.GUI.Main.Children.Compare;
using SubjectManagement.GUI.Main.Children.Semester;
using SubjectManagement.GUI.Main.Children.ViewListCourses;
using SubjectManagement.GUI.Dialog;

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
            var faculty = new FacultyDialog {Owner = System.Windows.Application.Current.MainWindow};
            faculty.ShowDialog();
            _Class = faculty._Class;
            tbl_Class.Text = $"{_Class.Name} - {_Class.CodeClass}";
        }

        private TabItem _titleTab;
        public Class _Class { get; init; }

        private void Btn_ViewList_OnClick(object sender, RoutedEventArgs e)
        {
            _titleTab.Header = "View List";
            
            var listCourses = new LoadListController(_Class){_Class = _Class};
            listCourses.LoadList(MainBody, g_loading);
        }

        private void Btn_AddSemester_OnClick(object sender, RoutedEventArgs e)
        {
            var semester = new AddSemesterUC(_Class);
            MainBody.Children.Clear();
            MainBody.Children.Add(semester);
          
        }

        private void Btn_Compare_OnClick(object sender, RoutedEventArgs e)
        {
            var compare = new CompareDialog(_Class){ Owner = System.Windows.Application.Current.MainWindow };
            compare.ShowDialog();
            if (compare.DialogResult != true) return;
            var compareUC = new SubjectCompareUC(_Class, compare._ClassCompare);
            MainBody.Children.Clear();
            MainBody.Children.Add(compareUC);
        }

        private void Btn_AlternativeSubject_OnClick(object sender, RoutedEventArgs e)
        {
            var alter = new AlternativeSubjectUC(_Class);
            MainBody.Children.Clear();
            MainBody.Children.Add(alter);
        }
    }
}
