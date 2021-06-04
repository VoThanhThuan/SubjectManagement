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
            var value = _subjectService.GetKnowledgeGroup();
            return value;
        }

        public Subject FindSubject(string codeSubject)
        {
            var subject = _subjectService.FindSubject(codeSubject, _Class);
            if (subject.IsSuccessed != false) return subject.ResultObj;
            MyCommonDialog.MessageDialog("Không tìm thấy môn học", $"{subject.Message}");

            return null;
        }

        public List<Subject> GetSubjectDifferentSemester(int semester = 0)
        {
 
            return _subjectService.GetSubjectDifferentSemester(semester, _Class.ID);
        }

        public List<Subject> GetSubjectOfClass()
        {
            return _subjectService.GetSubjectOfClass(_Class.ID);
        }
        public List<Subject> GetSubjectOfSemester(int semester)
        {
            return _subjectService.GetSubjectOfSemester(_Class.ID, semester);
        }
        public List<Subject> GetSubjectWithGroup(Guid idGroup)
        {
            return _subjectService.GetSubjectWithGroup(idGroup, _Class.ID);
        }

        public Result<Class> FindClassWithIdSubject(Guid idSubject)
        {
            return _subjectService.FindClassWithIdSubject(idSubject);
        }

        public Result<string> AddSubject(SubjectRequest request)
        {
             var result = _subjectService.AddSubject(request);
             if (result.IsSuccessed) return new ResultSuccess<string>();
             MyCommonDialog.MessageDialog($"{result.Message}");
             return new ResultError<string>($"{result.Message}");
        }

        public void EditWindow(string coursesCode)
        {
            var result = _subjectService.FindSubject(coursesCode, _Class);
            if (!result.IsSuccessed)
            {
                MyCommonDialog.MessageDialog($"{result.Message}", $"{result.Message}");
                return;
            }

            var knowledge = _subjectService.FindKnowledgeGroup(result.ResultObj.ID);
            var idKnowLedge = Guid.Empty;
            if (knowledge.ResultObj != null)
            {
                idKnowLedge = knowledge.ResultObj.ID;
            }
            var oldValue = new Hashtable
            {
                { "ID", result.ResultObj.ID },
                { "IDKnowledgeGroupOld",idKnowLedge }
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

                IDKnowledgeGroup = idKnowLedge,
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
            MyCommonDialog.MessageDialog($"{result.Message}", $"{result.Message}");

        }


        public void RemoveSubject(string coursesCode)
        {
            //Tìm kiếm môn học
            var subject = _subjectService.FindSubject(coursesCode, _Class);
            if (subject.ResultObj is null)
            {

                MyCommonDialog.MessageDialog($"{subject.Message}", $"{subject.Message}");

                return;
            }

            //Hỏi xem muốn xóa thật không

            var confirm = MyCommonDialog.MessageDialog("Bạn muốn xóa môn học này ?", $"{subject.ResultObj.Name}");

            if (confirm != MyDialogResult.Result.Ok)
                return;

            //Tìm kiếm nhóm môn
            var group = _subjectService.FindKnowledgeGroup(subject.ResultObj.ID);
            if (!group.IsSuccessed)
            {
                MyCommonDialog.MessageDialog($"Lỗi tìm kiếm nhóm môn học", $"Đã có lỗi gì đó nhưng bạn yên tâm, phần mềm vẫn sẽ xử lý và xóa cho bạn môn học {subject.ResultObj.Name}");
            }

            //Tiến hành xóa
            var request = new SubjectRequest()
            {
                ID = subject.ResultObj.ID,
                IDClass = subject.ResultObj.IDClass
            };
            if (group.ResultObj != null)
                request.IDKnowledgeGroup = group.ResultObj.ID;

            var result = _subjectService.RemoveSubject(request);
            if (result.ResultObj is not null)
            {
                MyCommonDialog.MessageDialog($"{result.Message}", $"{result.Message}");

                return;
            }

            MyCommonDialog.MessageDialog($"{result.Message}", $"{result.Message}", Colors.DeepSkyBlue);

        }

        public void CopyListSubject(int idClassOld, int idClassNew)
        {
            var result = _subjectService.CopyListSubject(idClassOld, idClassNew);
            MyCommonDialog.MessageDialog($"{result.Message}", $"{result.Message}", Colors.DeepSkyBlue);

        }

    }
}
