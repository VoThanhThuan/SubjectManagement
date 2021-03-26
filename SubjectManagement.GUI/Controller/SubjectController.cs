using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using SubjectManagement.Application.SubjectApp;
using SubjectManagement.Common.Dialog;
using SubjectManagement.Common.Result;
using SubjectManagement.Data.Entities;
using SubjectManagement.GUI.Main;
using SubjectManagement.ViewModels.Subject;

namespace SubjectManagement.GUI.Controller
{
    public class SubjectController
    {
        public SubjectController()
        {
            _subjectService = new SubjectService();
        }

        private readonly ISubjectService _subjectService;

        public async void LoadCombobox(ComboBox cbb)
        {
            var value = await _subjectService.LoadKnowledgeGroup();
            cbb.ItemsSource = value;
            cbb.DisplayMemberPath = "Name";
        }

        public async void AddSubject(SubjectRequest request)
        {
            await _subjectService.AddSubject(request);
        }

        public async void EditWindow(string coursesCode)
        {
            var result = await _subjectService.FindSubject(coursesCode);
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

            var oldValue = new Hashtable();
            oldValue.Add("ID", result.ResultObj.ID);
            oldValue.Add("IDKnowledgeGroupOld", knowledge.Result[0].ID);

            var editWindow = new AddSubjectWindow
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
                chk_IsOffical = { IsChecked = result.ResultObj.IsOffical },
                tbx_Details = { Text = result.ResultObj.Details },
                cbb_CoursesGroup = { SelectedValue = knowledge.Result[0] },

                IsEdit = true,
                OldValue = oldValue
            };


            editWindow.Show();
        }

        public async void EditSubject(SubjectRequest request)
        {
            var result = await _subjectService.EditSubject(request);
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


        public async void RemoveSubject(string coursesCode)
        {
            //Tìm kiếm môn học
            var subject = await _subjectService.FindSubject(coursesCode);
            if (subject is null)
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
            var group = await _subjectService.FindKnowledgeGroup(subject.ResultObj.ID);
            if (group.Count == 0)
            {

                var mess = new MessageDialog()
                {
                    tbl_Title = { Text = $"Lỗi tìm kiếm" },
                    tbl_Message = { Text = $"" },
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
                IDKnowledgeGroup = group[0].ID
            };

            var result = await _subjectService.RemoveSubject(request);
            if (result is not null) return;
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
        }

    }
}
