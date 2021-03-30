using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SubjectManagement.Common.Result;
using SubjectManagement.Data.Entities;

namespace SubjectManagement.Application.SemesterApp
{
    public interface ISemesterService
    {
        List<Subject> LoadSubject(int idSemester);
        Result<string> AddSubject(Guid idSubject, int term);
        Result<string> RemoveSubject(Guid idSubject, int term);
    }
}
