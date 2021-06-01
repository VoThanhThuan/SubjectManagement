using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using DocumentFormat.OpenXml.Wordprocessing;
using SubjectManagement.Application.SubjectApp;
using SubjectManagement.Data;
using SubjectManagement.Data.Entities;
using SubjectManagement.GUI.Dialog;
using SubjectManagement.GUI.Main.Children.ViewListCourses;
using SubjectManagement.ViewModels.Subject;

namespace SubjectManagement.GUI.Controller
{
    public class LoadListController
    {
        public LoadListController(Class _class, int iFaculty)
        {
            _subjectService = new SubjectService();
            _IdFaculty = iFaculty;
            _Class = _class;
        }

        private readonly ISubjectService _subjectService;
        public int _IdFaculty { get; init; }
        public Class _Class { get; init; }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //new Thread(DoSomething).Start();
        }

        private List<SubjectRequest> SubjectToRequests(List<Subject> subjects)
        {
            var index = 0;
            return subjects.Select(item => new SubjectRequest()
                {
                    //Index = ++index,
                    ID = item.ID,
                    CourseCode = item.CourseCode,
                    Name = item.Name,
                    Credit = item.Credit,
                    TypeCourse = item.TypeCourse,
                    NumberOfTheory = item.NumberOfTheory,
                    NumberOfPractice = item.NumberOfPractice,
                    Prerequisite = item.Prerequisite,
                    LearnFirst = item.LearnFirst,
                    Parallel = item.Parallel,
                    Details = item.Details,
                    Semester = item.Semester,
                    IDClass = item.IDClass,
                    IDElectiveGroup = item.IDElectiveGroup
                })
                .ToList();
        }

        public async void LoadList(Grid renderBody, Grid g_loading)
        {
            g_loading.Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(
                () =>
                {
                    g_loading.Visibility = Visibility.Visible;
                })).Wait();

            var group = _subjectService.GetKnowledgeGroup();
            var viewList = new ListExpanderCoursesUC(renderBody, _IdFaculty, _Class) {_g_loading = g_loading, _isViewSemester = false};
            if (_Class.CanEdit == false)
            {
                viewList.btn_Add.IsEnabled = false;
                viewList.btn_Edit.IsEnabled = false;
                viewList.btn_Remove.IsEnabled = false;
                MyCommonDialog.MessageDialog("Đã khóa", "Lớp này đã bị khóa, không thể sửa đổi dữ liệu");

            }

            foreach (var item in group)
            {
                var subjectInGroup = _subjectService.GetSubjectWithGroup(item.ID, _Class.ID);
                var obligatory = subjectInGroup.Where(x => x.TypeCourse == true).Sum(x => x.Credit);
                var listElective = subjectInGroup.Where(x => x.TypeCourse == false).Select(x => x.Credit).ToList();
                var tc = new List<int>(){0};
                foreach (var itc in listElective.Where(itc => !tc.Contains(itc)))
                {
                    tc.Add(itc);
                }

                var expander = new ExpanderCoursesUC(_Class, subjectInGroup)
                {
                    exp_courses =
                    {
                        Header = $"{item.Name}" +
                                 $" - {obligatory+tc.Sum()} TC " +
                                 $" - {subjectInGroup.Count(x => x.TypeCourse == true)} Bắt buộc"
                    },
                    _IdGroup = item.ID
                };
                expander.SetVisible(Visibility.Collapsed);
                viewList.renderExpander.Children.Add(expander);
            }

            renderBody.Children.Clear();
            renderBody.Children.Add(viewList);

            g_loading.Visibility = Visibility.Hidden;
            g_loading.UpdateLayout();
            
        }

        public void LoadListInSemester(Grid renderBody, Grid g_loading)
        {
            g_loading.Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(
                () => { g_loading.Visibility = Visibility.Visible; })).Wait();

            var viewList = new ListExpanderCoursesUC(renderBody, _IdFaculty, _Class) { _g_loading = g_loading, _isViewSemester = true };
            if (_Class.CanEdit == false)
            {
                viewList.btn_Add.IsEnabled = false;
                viewList.btn_Edit.IsEnabled = false;
                viewList.btn_Remove.IsEnabled = false;
                MyCommonDialog.MessageDialog("Đã khóa", "Lớp này đã bị khóa, không thể sửa đổi dữ liệu");
            }

            var listSubjects = _subjectService.GetSubject(_Class.ID);

            for (var i = 1; i < 9; i++)
            {
                var subjects = listSubjects.Where(x => x.Semester == i).OrderByDescending(x => x.TypeCourse).ToList();
                
                var expander = new ExpanderCoursesUC(_Class, subjects)
                {
                    exp_courses = {Header = $"Học kỳ {i}"},
                    _Semester = i
                };
                expander.SetVisible(Visibility.Visible);
                viewList.renderExpander.Children.Add(expander);
            }


            renderBody.Children.Clear();
            renderBody.Children.Add(viewList);

            g_loading.Visibility = Visibility.Hidden;
        }


    }
}
