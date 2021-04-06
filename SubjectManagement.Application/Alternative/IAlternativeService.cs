using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SubjectManagement.Common.Result;
using SubjectManagement.Data.Entities;

namespace SubjectManagement.Application.Alternative
{
    public interface IAlternativeService
    {
        public Result<string> AddAlternative(int idClass, Guid idSubject, Guid idSubjectAlter);
        public Result<string> RemoveAlternative(int idClass, Guid idSubject);
        public List<Subject> GetAlternative(int idClass, Guid idSubject);

    }
}
