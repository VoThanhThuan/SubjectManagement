using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using DocumentFormat.OpenXml.InkML;
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
        public AlternativeSubjectUC(Class _class, int idFaculty)
        {
            InitializeComponent();
            _IdFaculty = idFaculty;
            _Class = _class;
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tbx_SearchCurrent, $"Tìm Kiếm Môn học lớp {_class.CodeClass}");
            DataContext = this;
            LoadSubjectClass();
        }

        private int _IdFaculty { get; init; }

        private Class _ClassOld { get; set; }

        private Class _Class { get; init; }

        public List<Subject> _CurrentSubjects { get; set; }
        public List<Subject> _AltertiveSubjects { get; set; }

        private void LoadSubjectClass()
        {
            var clss = new FacultyController();
            clss.GetDifferentClassOlder(cbb_Class, _IdFaculty, _Class);

            var subAlter = new SubjectController(_Class);
            _CurrentSubjects = subAlter.GetSubjectOfClass();
            dg_ListAllSubject.ItemsSource = _CurrentSubjects;
            //var alter = new AlternativeController(_Class);
            //alter.LoadSubjectClass(dg_ListAllSubject);
        }

        private void loadListAlter()
        {
            if (_ClassOld == null) return;
            var loadAlter = new AlternativeController(_Class);
            var subject = (Subject)dg_ListAllSubject.SelectedValue;
            _AltertiveSubjects = new List<Subject>();
            _AltertiveSubjects = loadAlter.GetAlternative(subject.ID, _ClassOld.ID);
            dg_ListSubjectAlternative.ItemsSource = null;
            dg_ListSubjectAlternative.ItemsSource = _AltertiveSubjects;

        }

        private void Btn_Add_OnClick(object sender, RoutedEventArgs e)
        {
            if ((dg_ListAllSubject.SelectedIndex < 0) || (dg_SubjectOfClassOther.SelectedIndex < 0)) return;
            var subject = (Subject)dg_ListAllSubject.SelectedValue;
            var subjectAlter = (Subject)dg_ListAllSubject.SelectedValue;

            var add = new AlternativeController(_ClassOld);
            add.AddAlternative(subject.ID, subjectAlter.ID);
            loadListAlter();
        }

        private void Cbb_Class_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _ClassOld = (Class)cbb_Class.SelectedValue;
            var subAlter = new SubjectController(_ClassOld);
            _AltertiveSubjects = subAlter.GetSubjectOfClass();
            dg_SubjectOfClassOther.ItemsSource = _AltertiveSubjects;
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tbx_SearchAlternative, $"Tìm Kiếm Môn học lớp {_ClassOld.CodeClass}");
        }

        private void Btn_Remove_OnClick(object sender, RoutedEventArgs e)
        {
            if (dg_ListSubjectAlternative.SelectedIndex < 0) return;
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

            var btn = (Button)(sender);

            if ((dg_ListAllSubject.SelectedIndex < 0)) return;
            var subject = (Subject)dg_ListAllSubject.SelectedValue;
            //var subjectAlter = (Subject)dg_ListAllSubject.SelectedValue;

            var add = new AlternativeController(_ClassOld);
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
            var subject = (Button)sender;
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
            var export = new ExportController(_Class){ _ClassOld = _ClassOld};

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
        public string convertToUnSign(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
        private void Tbx_SearchCurrent_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var text = tbx_SearchCurrent.Text;
            var value = _CurrentSubjects.Where(x => x.CourseCode.ToLower().Contains(text.ToLower())).ToList();
            if(value.Count < 1 )
                value = _CurrentSubjects.Where(x => convertToUnSign(x.Name).ToLower().Contains(convertToUnSign(text.ToLower()))).ToList();

            dg_ListAllSubject.ItemsSource = null;
            dg_ListAllSubject.ItemsSource = value;
        }

        private void Tbx_SearchAlternative_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if(_ClassOld == null || _AltertiveSubjects == null) return;
            var text = tbx_SearchAlternative.Text;
            var value = _AltertiveSubjects.Where(x => x.CourseCode.ToLower().Contains(text.ToLower())).ToList();
            if (value.Count < 1)
                value = _AltertiveSubjects.Where(x => convertToUnSign(x.Name).ToLower().Contains(convertToUnSign(text.ToLower()))).ToList();
            dg_SubjectOfClassOther.ItemsSource = null;
            dg_SubjectOfClassOther.ItemsSource = value;
        }
    }
}
