using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SubjectManagement.Common.Result;
using SubjectManagement.Data;
using SubjectManagement.Data.Entities;

namespace SubjectManagement.Application.FacultyApp
{
    public class FacultyService : IFacultyService
    {
        public List<Faculty> GetFaculty()
        {
            var faculty = Db.Context.Faculties.Select(x => x).ToList();
            return faculty;
        }

        public List<Class> GetClass(int IDFaculty)
        {
            var faculty = (from c in Db.Context.Classes
                           join cif in Db.Context.ClassInFaculties on c.ID equals cif.IDClass
                           join f in Db.Context.Faculties on cif.IDFaculty equals f.ID
                           select c).ToList();
            return faculty;
        }

    }
}
