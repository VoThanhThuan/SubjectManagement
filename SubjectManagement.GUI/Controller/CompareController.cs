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
                list[i].CourseCode = $"{list[i].CourseCode} {Environment.NewLine}>> {list[i + 1].CourseCode}";
                list[i].Name = $"{list[i].Name}{Environment.NewLine}{list[i + 1].Name} ";
                list[i].Credit = $"{list[i].Credit} {Environment.NewLine}{list[i + 1].Credit}";
                list[i].TypeCourse = list[i].TypeCourse;
                list[i].NumberOfTheory = $"{list[i].NumberOfTheory} {Environment.NewLine}{list[i + 1].NumberOfTheory}";
                list[i].NumberOfPractice = $"{list[i].NumberOfPractice} {Environment.NewLine}{list[i + 1].NumberOfPractice}";
                list[i].Prerequisite = $"{list[i].Prerequisite} {Environment.NewLine}{list[i + 1].Prerequisite}";
                list[i].LearnFirst = $"{list[i].LearnFirst} {Environment.NewLine}{list[i + 1].LearnFirst}";
                list[i].Parallel = $"{list[i].Parallel} {Environment.NewLine}{list[i + 1].Parallel}";
                i++;

            }

            dg.ItemsSource = list;
        }

        public void Compare2TableClass(Class classCurrent, Class classComapre, bool isCompareTable, DataGrid dg)
        {
            var list = _compareService.CompareSubject(classCurrent.ID, classComapre.ID).ResultObj;
            for (var i = 0; i < list.Count; i++)
            {
                if ((list[i].Different == Different.SubjectNew || list[i].Different == Different.SubjectRemove) && isCompareTable == false)
                {
                    list.RemoveAt(i);
                    continue;
                }
                if (list[i].Different != Different.SubjectChange) continue;
                list[i].ID = list[i + 1].ID;
                list[i].IdClass = list[i].IdClass;
                list[i].CourseCode = isCompareTable == false ? $"{list[i].CourseCode}" : $"{list[i + 1].CourseCode}";
                list[i].Name = isCompareTable == false ? $"{list[i].Name}" : $"{list[i + 1].Name}";
                list[i].Credit = isCompareTable == false ? $"{list[i].Credit}" : $"{list[i + 1].Credit}";
                list[i].TypeCourse = list[i].TypeCourse;
                list[i].NumberOfTheory = isCompareTable == false ? $"{list[i].NumberOfTheory}" : $"{list[i + 1].NumberOfTheory}";
                list[i].NumberOfPractice = isCompareTable == false ? $"{list[i].NumberOfPractice}" : $"{list[i + 1].NumberOfPractice}";
                list[i].Prerequisite = isCompareTable == false ? $"{list[i].Prerequisite}" : $"{list[i + 1].Prerequisite}";
                list[i].LearnFirst = isCompareTable == false ? $"{list[i].LearnFirst}" : $"{list[i + 1].LearnFirst}";
                list[i].Parallel = isCompareTable == false ? $"{list[i].Parallel}" : $"{list[i + 1].Parallel}";
                i++;

            }

            dg.ItemsSource = list;
        }

    }
}
