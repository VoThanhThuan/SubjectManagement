using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using SubjectManagement.Application.Alternative;
using SubjectManagement.Application.SubjectApp;
using SubjectManagement.Common.Dialog;
using SubjectManagement.Data;
using SubjectManagement.Data.Entities;
using SubjectManagement.ViewModels.Subject;

namespace SubjectManagement.GUI.Controller
{
    public class AlternativeController
    {

        public AlternativeController(Class _class)
        {
            _alternativeService = new AlternativeService();
            _subjectService = new SubjectService();
            _Class = _class;
        }

        private Class _Class { get; init; }
        private IAlternativeService _alternativeService;
        private ISubjectService _subjectService;
        public void LoadSubjectClass(DataGrid dg)
        {
            var subjects = _subjectService.LoadSubjectOfClass(_Class.ID);
            var listItem = new List<SubjectAlternativeMV>();
            foreach (var item in subjects)
            {
                var btn = new Button();
                btn.Click += Btn_Click;
                var subjectOfClass = new SubjectAlternativeMV()
                {
                    Button = btn,
                    ID = item.ID,
                    CourseCode = item.CourseCode,
                    Name = item.Name,
                    Credit = $"{item.Credit}",
                    TypeCourse = item.TypeCourse,
                    NumberOfTheory = $"{item.NumberOfTheory}",
                    NumberOfPractice = $"{item.NumberOfPractice}",
                    Prerequisite = $"{item.Prerequisite}",
                    LearnFirst = $"{item.LearnFirst}",
                    Parallel = $"{item.Parallel}",
                    Details = item.Details,
                    IdClass = item.IDClass
                };
                listItem.Add(subjectOfClass);
            }

            dg.ItemsSource = listItem;
        }

        private void Btn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void AddAlternative(Guid idSubject, Guid idSubjectAlter)
        {
            var add = _alternativeService.AddAlternative(_Class.ID, idSubject, idSubjectAlter);
            if (add.IsSuccessed is false)
            {
                var mess = new MessageDialog()
                {
                    tbl_Title = { Text = "Lỗi thêm" },
                    tbl_Message = { Text = $"{add.Message}" },
                    title_color = { Background = new SolidColorBrush(Color.FromRgb(255, 0, 0)) },
                    Topmost = true
                };
                mess.ShowDialog();
            }

        }

        public void RemoveAlternative(Guid idSubject)
        {
            var remove = _alternativeService.RemoveAlternative(_Class.ID, idSubject);
            if (remove.IsSuccessed is not false)
            {
                var mess = new MessageDialog()
                {
                    tbl_Title = { Text = "Lỗi thêm" },
                    tbl_Message = { Text = $"{remove.Message}" },
                    title_color = { Background = new SolidColorBrush(Color.FromRgb(255, 0, 0)) },
                    Topmost = true
                };
                mess.ShowDialog();
            }
        }

        public List<Subject> GetAlternative( Guid idSubject)
        {
            var alter = _alternativeService.GetAlternative(_Class.ID, idSubject);
            return alter;
        }
    }
}
