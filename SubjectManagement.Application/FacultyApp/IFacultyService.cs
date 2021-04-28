using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SubjectManagement.Common.Result;
using SubjectManagement.Data.Entities;

namespace SubjectManagement.Application.FacultyApp
{
    public interface IFacultyService
    {
        Task<List<Faculty>> GetFaculty();
        Task<List<Class>> GetClass(int? idClass = null);
        Result<string> AddFaculty(string name);
        Result<string> RemoveFaculty(int id);

        Result<string> AddClass(Class c, int idFaculty);
        Result<string> RemoveClass(int id);

        Result<string> UnlockClass(int id, bool isLock);

    }
}
