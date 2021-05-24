using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using SubjectManagement.Application.SemesterApp;
using SubjectManagement.Data;
using SubjectManagement.Data.Entities;
using SubjectManagement.GUI.Dialog;
using SubjectManagement.ViewModels.SubjectOfClass;

namespace SubjectManagement.GUI.Controller
{
    public class SemesterController
    {

        public SemesterController(Class _class)
        {
            _Class = _class;
            _semesterServicel = new SemesterService();
        }

        private readonly ISemesterService _semesterServicel;
        private Class _Class { get; init; }
        public void AddSubject(Subject request, int semester)
        {
            var result = _semesterServicel.AddSubject(request, semester);
            MyCommonDialog.MessageDialog("Lỗi thêm", $"{result.Message}");
        }
        public void RemoveSubject(Guid idSubject, int term)
        {
            var result = _semesterServicel.RemoveSubject(idSubject, term);

            MyCommonDialog.MessageDialog("Lỗi xóa", $"{result.Message}");

        }
        public List<Subject> LoadSubject(int idSemester)
        {
            return _semesterServicel.LoadSubject(idSemester, _Class.ID);

        }
    }
}
