using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore.Internal;
using SubjectManagement.Data.Entities;
using SubjectManagement.GUI.Controller;
using SubjectManagement.ViewModels.Subject;

namespace SubjectManagement.GUI.Main.Children.ViewListCourses 
{
    /// <summary>
    /// Interaction logic for ListCoursesUC.xaml
    /// </summary>
    public partial class ExpanderCoursesUC : UserControl
    {
        public ExpanderCoursesUC(Class _class, List<Subject> subjects)
        {
            InitializeComponent();
            _Class = _class;
            _subjects = subjects;
            dg_ListCourses.ItemsSource = _subjects;
        }

        private Class _Class { get; set; }
        
        private List<Subject> _subjects { get; set; }
        public void SetVisible(Visibility vis)
        {
            tbl_Vis_itemCM.Visibility = vis;
        }

        private SubjectRequest InfoSubject(Subject currentSubject, Guid idKnowledgeGroup)
        {
            try
            {
                var subject = new SubjectRequest()
                {
                    ID = currentSubject.ID,
                    CourseCode = currentSubject.CourseCode,
                    Name = currentSubject.Name,
                    Credit = currentSubject.Credit,
                    TypeCourse = currentSubject.TypeCourse,
                    NumberOfTheory = currentSubject.NumberOfTheory,
                    NumberOfPractice = currentSubject.NumberOfPractice,
                    Prerequisite = currentSubject.Prerequisite,
                    LearnFirst = currentSubject.LearnFirst,
                    Parallel = currentSubject.Parallel,
                    Semester = currentSubject.Semester,
                    Details = currentSubject.Details,

                    IDKnowledgeGroup = idKnowledgeGroup,
                    IdClass = _Class.ID
                };
                return subject;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }

        }

        private void mi_Edit_Click(object sender, RoutedEventArgs e)
        {
            var currentSubject = (Subject)dg_ListCourses.SelectedValue;
            var edit = new SubjectController(_Class);
            edit.EditWindow(currentSubject.CourseCode);
        }

        private void mi_Delete_Click(object sender, RoutedEventArgs e)
        {
            var currentSubject = (Subject)dg_ListCourses.SelectedValue;
            var edit = new SubjectController(_Class);
            edit.RemoveSubject(currentSubject.CourseCode);
        }

        private void mi_Group_Click(object sender, RoutedEventArgs e)
        {
            var data = dg_ListCourses.ItemsSource;
            var subjects = dg_ListCourses.SelectedItems;
            var group = new ElectiveGroupController(_Class);
            foreach (var item in subjects)
            {
                var subject = (Subject)item;
                if (subject.IDElectiveGroup != null)
                {
                    if (!group.RemoveGroup(((Subject)item).ID)) continue;
                    foreach (var s in _subjects.Where(x => x.ID == subject.ID))
                        s.IDElectiveGroup = null;
                }
                else
                {
                    if (!group.AddGroup((Subject)item)) continue;
                    foreach (var s in _subjects.Where(x => x.ID == subject.ID))
                        s.IDElectiveGroup = Guid.NewGuid();
                }

            }

            dg_ListCourses.ItemsSource = null;
            dg_ListCourses.ItemsSource = _subjects;
            dg_ListCourses.UpdateLayout();
            
        }

    }


}
