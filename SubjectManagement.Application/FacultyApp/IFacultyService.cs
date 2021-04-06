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
        List<Faculty> GetFaculty();
        List<Class> GetClass(int? idClass = null);
    }
}
