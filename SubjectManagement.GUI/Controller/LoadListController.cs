using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SubjectManagement.Application.SubjectApp;
using SubjectManagement.Data;
using SubjectManagement.Data.Entities;
using SubjectManagement.GUI.Dialog;
using SubjectManagement.GUI.Main.Children.ViewListCourses;

namespace SubjectManagement.GUI.Controller
{
    public class LoadListController
    {
        public LoadListController(Class _class)
        {
            _subjectService = new SubjectService();
            _Class = _class;
        }

        private readonly ISubjectService _subjectService;
        public Class _Class { get; init; }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //new Thread(DoSomething).Start();
        }



        public void LoadList(Grid renderBody, Grid g_loading)
        {
            g_loading.Visibility = Visibility.Visible;

            var group = _subjectService.LoadKnowledgeGroup();
            var viewList = new ListExpanderCoursesUC(renderBody) {_g_loading = g_loading, _Class = _Class, _isViewSemester = false};
            if (_Class.CanEdit == false)
            {
                viewList.btn_Add.IsEnabled = false;
                viewList.btn_Edit.IsEnabled = false;
                viewList.btn_Remove.IsEnabled = false;
                var result = new MessageDialog()
                {
                    tbl_Title = { Text = $"Đã khóa" },
                    tbl_Message = { Text = $"Lớp này đã bị khóa, không thể sửa đổi dữ liệu" },
                    title_color = { Background = new SolidColorBrush(Color.FromRgb(255, 0, 0)) },
                    Topmost = true
                };
                result.ShowDialog();
            }

            foreach (var item in group)
            {
                var subjectInGroup = _subjectService.LoadSubjectWithGroup(item.ID, _Class.ID);
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
                    }
                };
                expander.SetVisible(Visibility.Collapsed);
                viewList.renderExpander.Children.Add(expander);
            }

            renderBody.Children.Clear();
            renderBody.Children.Add(viewList);

            g_loading.Visibility = Visibility.Hidden;
        }

        public void LoadListInSemester(Grid renderBody, Grid g_loading)
        {
            g_loading.Visibility = Visibility.Visible;

            var viewList = new ListExpanderCoursesUC(renderBody) { _g_loading = g_loading, _Class = _Class, _isViewSemester = true };
            if (_Class.CanEdit == false)
            {
                viewList.btn_Add.IsEnabled = false;
                viewList.btn_Edit.IsEnabled = false;
                viewList.btn_Remove.IsEnabled = false;
                var result = new MessageDialog()
                {
                    tbl_Title = { Text = $"Đã khóa" },
                    tbl_Message = { Text = $"Lớp này đã bị khóa, không thể sửa đổi dữ liệu" },
                    title_color = { Background = new SolidColorBrush(Color.FromRgb(255, 0, 0)) },
                    Topmost = true
                };
                result.ShowDialog();
            }

            var listSubjects = _subjectService.GetSubject(_Class.ID);

            for (var i = 1; i < 9; i++)
            {
                var subject = listSubjects.Where(x => x.Semester == i).ToList();
                var expander = new ExpanderCoursesUC(_Class, subject)
                {
                    exp_courses =
                    {
                        Header = $"Học kỳ {i}"
                    }
                    
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
