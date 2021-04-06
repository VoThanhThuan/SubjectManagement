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
using SubjectManagement.Common.Dialog;
using SubjectManagement.Data.Entities;
using SubjectManagement.GUI.Controller;
using SubjectManagement.ViewModels.SubjectOfClass;

namespace SubjectManagement.GUI.Main.Children.Semester
{
    /// <summary>
    /// Interaction logic for AddSemesterUC.xaml
    /// </summary>
    public partial class AddSemesterUC : UserControl
    {
        public AddSemesterUC(Class _class)
        {
            InitializeComponent();
            _Class = _class;
            tbl_NameClass.Text = _class.Name;
            LoadListSubject();

        }

        public Class _Class { get; init; }

        private void LoadListSubject(int? semester = null)
        {
            var load = new SubjectController(_Class);
            dg_ListAllSubject.ItemsSource = load.GetSubjectSemester(semester);
        }

        private void LoadSubjectInSemester(int semester)
        {
            if (semester < 0) return;
            var load = new SemesterController(_Class);
            dg_SubjectOfSemester.ItemsSource = load.LoadSubject(semester);
        }

        private void Cbb_Semester_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var semester = int.Parse($"{((ComboBoxItem)cbb_Semester.SelectedItem).Content}");

            //Load lại data grid
            LoadListSubject(semester);
            LoadSubjectInSemester(semester);
        }

        private void Btn_Add_OnClick(object sender, RoutedEventArgs e)
        {

            if (cbb_Semester.SelectedIndex < 0)
            {
                var mess = new MessageDialog()
                {
                    tbl_Title = { Text = $"Chưa chọn học kỳ" },
                    tbl_Message = { Text = $"Bạn vẫn chưa chọn học kỳ, vui lòng chọn học kỳ cần thêm môn học." },
                    title_color = { Background = new SolidColorBrush(Color.FromRgb(255, 0, 0)) },
                    Topmost = true
                };
                mess.ShowDialog();
                return;
            }

            if (dg_ListAllSubject.SelectedIndex < 0) return;
            var subject = (Subject)dg_ListAllSubject.SelectedValue;
            var add = new SemesterController(_Class);

            var semester = int.Parse($"{((ComboBoxItem)cbb_Semester.SelectedItem).Content}");


            add.AddSubject(subject, semester);

            //Load lại data grid
            LoadListSubject(semester);
            LoadSubjectInSemester(semester);
        }

        private void Btn_remove_OnClick(object sender, RoutedEventArgs e)
        {
            if (dg_SubjectOfSemester.SelectedIndex < 0) return;

            var remove = new SemesterController(_Class);

            var subject = (Subject)dg_SubjectOfSemester.SelectedValue;
            var semester = int.Parse($"{((ComboBoxItem)cbb_Semester.SelectedItem).Content}");

            remove.RemoveSubject(subject.ID, semester);

            //Load lại data grid
            LoadListSubject(semester);
            LoadSubjectInSemester(semester);
        }

    }
}
