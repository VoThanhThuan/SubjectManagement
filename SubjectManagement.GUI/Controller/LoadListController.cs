using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using SubjectManagement.Application.SubjectApp;
using SubjectManagement.GUI.Main.Children.ViewListCourses;

namespace SubjectManagement.GUI.Controller
{
    public class LoadListController
    {
        public LoadListController()
        {
            _subjectService = new SubjectService();
        }

        private readonly ISubjectService _subjectService;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //new Thread(DoSomething).Start();
        }

        public async void LoadList(Grid renderBody, Grid g_loading)
        {
            g_loading.Visibility = Visibility.Visible;

            var group = await _subjectService.LoadKnowledgeGroup();
            var viewList = new ListExpanderCoursesUC() {_g_loading = g_loading};
            foreach (var item in group)
            {
                var subjectInGroup = await _subjectService.LoadSubjectWithGroup(item.ID);
                var expander = new ExpanderCoursesUC()
                {
                    exp_courses =
                    {
                        Header = $"{item.Name}" +
                                 $" - {subjectInGroup.Sum(x => x.Credit)} TC " +
                                 $" - {subjectInGroup.Count(x => x.TypeCourse == true)} Bắt buộc"
                    },
                    dg_ListCourses = {ItemsSource = subjectInGroup}
                };
                viewList.renderExpander.Children.Add(expander);
            }

            renderBody.Children.Clear();
            renderBody.Children.Add(viewList);

            g_loading.Visibility = Visibility.Hidden;
        }

    }
}
