using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SubjectManagement.Common.Result;
using SubjectManagement.Data.Entities;

namespace SubjectManagement.Application.ElectiveGroupApp
{
    public interface IElectiveGroupService
    {
        public Result<string> AddGroup(int idClass, Subject subject, int credit);
        public Result<string> RemoveGroup(int idClass, Guid idSubject);

    }
}
