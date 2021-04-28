using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SubjectManagement.Common.Result;
using SubjectManagement.Data.Entities;
using SubjectManagement.ViewModels.SubjectOfClass;

namespace SubjectManagement.Application.SemesterApp
{
    public interface ISemesterService
    {
        List<Subject> LoadSubject(string idSemester, int idClass);
        Result<string> AddSubject(Subject request, string semester);
        Result<string> RemoveSubject(Guid idSubject, string term);
    }
}
