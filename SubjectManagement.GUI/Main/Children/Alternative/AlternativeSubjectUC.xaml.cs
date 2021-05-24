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
using Microsoft.Win32;
using SubjectManagement.Data.Entities;
using SubjectManagement.GUI.Controller;
using SubjectManagement.GUI.Dialog;

namespace SubjectManagement.GUI.Main.Children.Alternative
{
    /// <summary>
    /// Interaction logic for AlternativeSubjectUC.xaml
    /// </summary>
    public partial class AlternativeSubjectUC : UserControl
    {
        public AlternativeSubjectUC(Class _class)
        {
            InitializeComponent();
            _Class = _class;
            LoadSubjectClass();
        }

        private Class _Class { get; init; }

        private void LoadSubjectClass()
        {
            var clss = new FacultyController();
            clss.GetClass(cbb_Class, _Class.ID);

            var subAlter = new SubjectController(_Class);
            dg_ListAllSubject.ItemsSource = subAlter.GetSubjectClass();
            //var alter = new AlternativeController(_Class);
            //alter.LoadSubjectClass(dg_ListAllSubject);
        }

        private void loadListAlter()
        {
            var loadAlter = new AlternativeController(_Class);
            var subject = (Subject)dg_ListAllSubject.SelectedValue;

            dg_ListSubjectAlternative.ItemsSource = loadAlter.GetAlternative(subject.ID);
        }

        private void Btn_Add_OnClick(object sender, RoutedEventArgs e)
        {
            if((dg_ListAllSubject.SelectedIndex < 0) || (dg_SubjectOfClassOther.SelectedIndex < 0)) return;
            var subject = (Subject)dg_ListAllSubject.SelectedValue;
            var subjectAlter = (Subject)dg_ListAllSubject.SelectedValue;

            var add = new AlternativeController(_Class);
            add.AddAlternative(subject.ID, subjectAlter.ID);
            loadListAlter();
        }

        private void Cbb_Class_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var clss = (Class) cbb_Class.SelectedValue;
            var subAlter = new SubjectController(clss);
            dg_SubjectOfClassOther.ItemsSource = subAlter.GetSubjectClass();
        }

        private void Btn_Remove_OnClick(object sender, RoutedEventArgs e)
        {
            if(dg_ListSubjectAlternative.SelectedIndex < 0) return;
            var remove = new AlternativeController(_Class);
            var subject = (Subject)dg_ListSubjectAlternative.SelectedValue;
            remove.RemoveAlternative(subject.ID);
            loadListAlter();
        }


        private void Dg_ListAllSubject_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            loadListAlter();
        }

        private void Btn_AddInCell_OnClick(object sender, RoutedEventArgs e)
        {
            if (_Class.CanEdit == false)
            {
                var mess = new MessageDialog()
                {
                    tbl_Title = { Text = $"Lỗi" },
                    tbl_Message = { Text = $"Lớp học này đã bị khóa, bạn không thể thực hiện hành động này" },
                    title_color = { Background = new SolidColorBrush(Color.FromRgb(255, 0, 0)) },
                    Topmost = true
                };
                mess.ShowDialog();
                return;
            }

            var btn = (Button) (sender);
            
            if ((dg_ListAllSubject.SelectedIndex < 0)) return;
            var subject = (Subject)dg_ListAllSubject.SelectedValue;
            //var subjectAlter = (Subject)dg_ListAllSubject.SelectedValue;

            var add = new AlternativeController(_Class);
            add.AddAlternative(subject.ID, Guid.Parse($"{btn.Tag}"));
            loadListAlter();
        }

        private void Btn_RemoveInCell_OnClick(object sender, RoutedEventArgs e)
        {
            if (_Class.CanEdit == false)
            {
                var mess = new MessageDialog()
                {
                    tbl_Title = { Text = $"Lỗi" },
                    tbl_Message = { Text = $"Lớp học này đã bị khóa, bạn không thể thực hiện hành động này" },
                    title_color = { Background = new SolidColorBrush(Color.FromRgb(255, 0, 0)) },
                    Topmost = true
                };
                mess.ShowDialog();
                return;
            }

            var remove = new AlternativeController(_Class);
            var subject = (Button) sender;
            remove.RemoveAlternative(Guid.Parse($"{subject.Tag}"));
            loadListAlter();
        }

        private void Btn_Export_OnClick(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel (*.xlsx)|*.xlsx|JSON (*.json)|*.json";
            saveFileDialog.Title = "Xuất file";
            saveFileDialog.FileName = DateTime.Now.ToString("yyyyMMddTHHmmss");
            if (saveFileDialog.ShowDialog() != true) return;
            var filename = saveFileDialog.FileName;
            var export = new ExportController(_Class);

            switch (saveFileDialog.FilterIndex)
            {
                case 1:
                    export.ExportAlternativeForExcel(filename);
                    break;
                case 2:
                    export.ExportAlternativeForJson(filename);
                    break;
            }

            MyCommonDialog.MessageDialog($"Đã lưu vào {saveFileDialog.FileName}", Colors.DeepSkyBlue);
        }
    }
}
