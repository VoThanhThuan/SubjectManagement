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
        List<Subject> LoadSubject(int idSemester, int idClass);
        Result<string> AddSubject(Subject request, int semester);
        Result<string> RemoveSubject(Guid idSubject, int term);
    }
}
