using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using SubjectManagement.Application.CompareApp;
using SubjectManagement.Application.FacultyApp;
using SubjectManagement.Application.SubjectApp;
using SubjectManagement.Common.Result;
using SubjectManagement.Data;
using SubjectManagement.Data.Entities;
using SubjectManagement.GUI.Dialog;
using SubjectManagement.GUI.Main.Children.Compare;
using SubjectManagement.ViewModels.Subject;
using static SubjectManagement.ViewModels.Subject.SubjectDifferent;

namespace SubjectManagement.GUI.Controller
{
    public class CompareController
    {
        public CompareController()
        {
            _facultyService = new FacultyService();
            _compareService = new CompareService();
            _subjectService = new SubjectService();
        }

        private readonly IFacultyService _facultyService;
        private readonly ICompareService _compareService;
        private readonly ISubjectService _subjectService;

        public List<SubjectCompareVM> GetListComapare(Class classCurrent, Class classComapre)
        {
            return _compareService.CompareSubject(classCurrent.ID, classComapre.ID).ResultObj;
        }

        public void CompareClass(Class classCurrent, Class classComapre, DataGrid dg)
        {
            var list = _compareService.CompareSubject(classCurrent.ID, classComapre.ID).ResultObj;
            for (var i = 0; i < list.Count; i++)
            {
                if (list[i].Different != Different.SubjectChange) continue;
                list[i].ID = list[i + 1].ID;
                list[i].IdClass = list[i].IdClass;
                list[i].CourseCode = $"({classCurrent.CodeClass}){list[i].CourseCode}{Environment.NewLine}({classComapre.CodeClass}){list[i + 1].CourseCode}";
                list[i].Name = $"{list[i].Name}{Environment.NewLine}{list[i + 1].Name} ";
                list[i].Credit = $"{list[i].Credit}{Environment.NewLine}{list[i + 1].Credit}";
                list[i].TypeCourse = $"{list[i].TypeCourse}{Environment.NewLine}{list[i+1].TypeCourse}";
                list[i].NumberOfTheory = $"{list[i].NumberOfTheory}{Environment.NewLine}{list[i + 1].NumberOfTheory}";
                list[i].NumberOfPractice = $"{list[i].NumberOfPractice}{Environment.NewLine}{list[i + 1].NumberOfPractice}";
                list[i].Prerequisite = $"{list[i].Prerequisite}{Environment.NewLine}{list[i + 1].Prerequisite}";
                list[i].LearnFirst = $"{list[i].LearnFirst}{Environment.NewLine}{list[i + 1].LearnFirst}";
                list[i].Parallel = $"{list[i].Parallel}{Environment.NewLine}{list[i + 1].Parallel}";
                i++;

            }

            dg.ItemsSource = list;
        }

        public void Compare2TableClass(Class classCurrent, Class classComapre, bool isCompareTable, DataGrid dg)
        {
            var list = _compareService.CompareSubject(classCurrent.ID, classComapre.ID).ResultObj;
            var countList = list.Count;
            for (var i = 0; i < countList; i++)
            {
                switch (list[i].Different)
                {
                    case Different.SubjectChange when isCompareTable:
                        list.RemoveAt(i+1);
                        countList--;
                        break;
                    case Different.SubjectChange when !isCompareTable:
                        list[i].Different = Different.SubjectChange;
                        list.RemoveAt(i+1);
                        countList--;
                        break;
                }
            }

            dg.ItemsSource = list;
        }

        public void CompareOnlyClass(Class classCurrent, Class classComapre, StackPanel spl)
        {
            var list = _compareService.CompareSubject(classCurrent.ID, classComapre.ID).ResultObj;
            var listDefault = new List<SubjectCompareVM>();
            var listChange = new List<SubjectCompareVM>();
            var listPlus   = new List<SubjectCompareVM>();
            var listRemove = new List<SubjectCompareVM>();

            foreach (var item in list)
            {
                switch (item.Different)
                {
                    case Different.SubjectDefault :
                        listDefault.Add(item);
                        break;
                    case Different.SubjectChange :
                        listChange.Add(item);
                        break;
                    case Different.SubjectNew :
                        listPlus.Add(item);
                        break;
                    case Different.SubjectRemove:
                        listRemove.Add(item);
                        break;
                    case Different.SubjectOriginal:
                        break;
                    default:
                        MyCommonDialog.MessageDialog("Lỗi ngoài ý muốn khi so sánh");
                        break;
                }
            }

            var expander1 = new ExpanderCompare() { exp_Difference = { Header = "Môn không thay đổi" }, dg_ListCourses = { ItemsSource = listDefault} };
            var expander2 = new ExpanderCompare() { exp_Difference = { Header = "Môn thay đổi" },       dg_ListCourses = { ItemsSource = listChange } };
            var expander3 = new ExpanderCompare() { exp_Difference = { Header = "Môn thêm mới" },       dg_ListCourses = { ItemsSource = listPlus   } };
            var expander4 = new ExpanderCompare() { exp_Difference = { Header = "Môn xóa bỏ" },         dg_ListCourses = { ItemsSource = listRemove } };

            spl.Children.Add(expander1);
            spl.Children.Add(expander2);
            spl.Children.Add(expander3);
            spl.Children.Add(expander4);

        }

    }
}
