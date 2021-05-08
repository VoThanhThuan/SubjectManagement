using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using DocumentFormat.OpenXml.VariantTypes;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SubjectManagement.Application.SubjectApp;
using SubjectManagement.Common.Result;
using SubjectManagement.Data;
using SubjectManagement.Data.Entities;
using SubjectManagement.GUI.Dialog;
using SubjectManagement.GUI.Main;
using SubjectManagement.ViewModels.Subject;

namespace SubjectManagement.GUI.Controller
{
    public class SubjectController
    {
        public SubjectController(Class _class)
        {
            _subjectService = new SubjectService();
            _Class = _class;
        }

        private readonly ISubjectService _subjectService;

        public Class _Class { get; init; }

        public List<KnowledgeGroup> LoadCombobox()
        {
            var value = _subjectService.LoadKnowledgeGroup();
            return value;
        }

        public Subject FindSubject(string codeSubject)
        {
            var subject = _subjectService.FindSubject(codeSubject);
            if (subject.IsSuccessed != false) return subject.ResultObj;
            var mess = new MessageDialog()
            {
                tbl_Title = { Text = $"Không tìm thấy môn học" },
                tbl_Message = { Text = $"{subject.Message}" },
                title_color = { Background = new SolidColorBrush(Color.FromRgb(255, 0, 0)) },
                Topmost = true
            };
            mess.ShowDialog();
            return null;
        }

        public List<Subject> GetSubjectSemester(int semester = 0)
        {
 
            return _subjectService.LoadSubjectDifferentSemester(semester, _Class.ID);
        }

        public List<Subject> GetSubjectClass()
        {
            return _subjectService.LoadSubjectOfClass(_Class.ID);
        }

        public void AddSubject(SubjectRequest request)
        {
             var result = _subjectService.AddSubject(request);
             if (result.IsSuccessed) return;
             var mess = new MessageDialog()
             {
                 tbl_Title = { Text = $"{result.Message}" },
                 tbl_Message = { Text = $"{result.Message}" },
                 title_color = { Background = new SolidColorBrush(Color.FromRgb(255, 0, 0)) },
                 Topmost = true
             };
             mess.ShowDialog();
        }

        public void EditWindow(string coursesCode)
        {
            var result = _subjectService.FindSubject(coursesCode);
            if (!result.IsSuccessed)
            {
                var mess = new MessageDialog()
                {
                    tbl_Title = { Text = $"{result.Message}" },
                    tbl_Message = { Text = $"{result.Message}" },
                    title_color = { Background = new SolidColorBrush(Color.FromRgb(255, 0, 0)) },
                    Topmost = true
                };
                mess.ShowDialog();
                return;
            }

            var knowledge = _subjectService.FindKnowledgeGroup(result.ResultObj.ID);

            var oldValue = new Hashtable
            {
                { "ID", result.ResultObj.ID },
                { "IDKnowledgeGroupOld", knowledge.ResultObj.ID }
            };

            var subject = new SubjectRequest()
            {
                ID = result.ResultObj.ID,
                CourseCode = result.ResultObj.CourseCode,
                Name = result.ResultObj.Name,
                Credit = result.ResultObj.Credit,
                TypeCourse = result.ResultObj.TypeCourse,
                NumberOfTheory = result.ResultObj.NumberOfTheory,
                NumberOfPractice = result.ResultObj.NumberOfPractice,
                Prerequisite = result.ResultObj.Prerequisite,
                LearnFirst = result.ResultObj.LearnFirst,
                Parallel = result.ResultObj.Parallel,
                Semester = result.ResultObj.Semester,
                Details = result.ResultObj.Details,

                IDKnowledgeGroup = knowledge.ResultObj.ID,
                IdClass = _Class.ID
            };

            var editWindow = new AddSubjectWindow(subject, _Class)
            {
                btn_AddProduct = { Content = "Sửa" },
                tbx_CourseCode = { Text = result.ResultObj.CourseCode },
                tbx_Name = { Text = result.ResultObj.Name },
                tbx_Credit = { Text = $"{result.ResultObj.Credit}" },
                chk_TypeCourse = { IsChecked = result.ResultObj.TypeCourse },
                tbx_NumberOfTheory = { Text = $"{result.ResultObj.NumberOfTheory}" },
                tbx_NumberOfPractice = { Text = $"{result.ResultObj.NumberOfPractice}" },
                tbx_Prerequisite = { Text = $"{result.ResultObj.Prerequisite}" },
                tbx_LearnFirst = { Text = $"{result.ResultObj.LearnFirst}" },
                tbx_Parallel = { Text = $"{result.ResultObj.Parallel}" },
                tbl_Semeter = {Text = $"{result.ResultObj.Semester}"},
                tbx_Details = { Text = result.ResultObj.Details },
                OldValue = oldValue,
                IsEdit = true,
            };

            //editWindow.cbb_CoursesGroup.SelectedValue = knowledge.ResultObj;
            editWindow.SetValueCombobox();
            editWindow.Show();
            

        }

        public void EditSubject(SubjectRequest request)
        {
            var result = _subjectService.EditSubject(request);
            if (result.IsSuccessed) return;
            var mess = new MessageDialog()
            {
                tbl_Title = { Text = $"{result.Message}" },
                tbl_Message = { Text = $"{result.Message}" },
                title_color = { Background = new SolidColorBrush(Color.FromRgb(255, 0, 0)) },
                Topmost = true
            };
            mess.ShowDialog();
        }


        public void RemoveSubject(string coursesCode)
        {
            //Tìm kiếm môn học
            var subject = _subjectService.FindSubject(coursesCode);
            if (subject.ResultObj is null)
            {

                var mess = new MessageDialog()
                {
                    tbl_Title = { Text = $"{subject.Message}" },
                    tbl_Message = { Text = $"{subject.Message}" },
                    title_color = { Background = new SolidColorBrush(Color.FromRgb(255, 0, 0)) },
                    Topmost = true
                };
                mess.ShowDialog();
                return;
            }

            //Hỏi xem muốn xóa thật không
            var confirm = new MessageDialog()
            {
                tbl_Title = { Text = $"Bạn muốn xóa môn học này ?" },
                tbl_Message = { Text = $"{subject.ResultObj.Name}" },
                title_color = { Background = new SolidColorBrush(Color.FromRgb(255, 0, 0)) },
                Topmost = true
            };
            confirm.ShowDialog();

            if (confirm.DialogResult != MyDialogResult.Result.Ok)
                return;


            //Tìm kiếm nhóm môn
            var group = _subjectService.FindKnowledgeGroup(subject.ResultObj.ID);
            if (!group.IsSuccessed)
            {
                var mess = new MessageDialog()
                {
                    tbl_Title = { Text = $"Lỗi tìm kiếm nhóm môn học" },
                    tbl_Message = { Text = $"Đã có lỗi gì đó nhưng bạn yên tâm, phần mềm vẫn sẽ xử lý và xóa cho bạn môn học {subject.ResultObj.Name}" },
                    title_color = { Background = new SolidColorBrush(Color.FromRgb(255, 0, 0)) },
                    Topmost = true
                };
                mess.ShowDialog();
                return;
            }

            //Tiến hành xóa
            var request = new SubjectRequest()
            {
                ID = subject.ResultObj.ID,
                IDKnowledgeGroup = group.ResultObj.ID
            };

            var result = _subjectService.RemoveSubject(request);
            if (result.ResultObj is not null)
            {
                var mess = new MessageDialog()
                {
                    tbl_Title = { Text = $"{result.Message}" },
                    tbl_Message = { Text = $"{result.Message}" },
                    title_color = { Background = new SolidColorBrush(Color.FromRgb(255, 0, 0)) },
                    Topmost = true
                };
                mess.ShowDialog();
                return;
            }

            var sus = new MessageDialog()
            {
                tbl_Title = { Text = $"{result.Message}" },
                tbl_Message = { Text = $"{result.Message}" },
                title_color = { Background = new SolidColorBrush(Color.FromRgb(255, 0, 0)) },
                Topmost = true
            };
            sus.ShowDialog();
        }

        public void CopyListSubject(int idClassOld, int idClassNew)
        {
            var result = _subjectService.CopyListSubject(idClassOld, idClassNew);
            var mess = new MessageDialog()
            {
                tbl_Title = { Text = $"Thông puma" },
                tbl_Message = { Text = $"{result.Message}" },
                title_color = { Background = new SolidColorBrush(Color.FromRgb(255, 0, 0)) },
                Topmost = true
            };
            mess.ShowDialog();
        }

    }
}
