using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SubjectManagement.Common.Result;
using SubjectManagement.Data;
using SubjectManagement.Data.EF;
using SubjectManagement.Data.Entities;

namespace SubjectManagement.Application.FacultyApp
{
    public class FacultyService : IFacultyService
    {
        public FacultyService()
        {
            var connect = new Db();
            _db = connect.Context;
        }

        private readonly SubjectDbContext _db;

        public async Task<List<Faculty>> GetFaculty()
        {
            var faculty = _db.Faculties.Select(x => x).ToList();
            return faculty;
        }

        public async Task<List<Class>> GetClass(int? idClass = null)
        {
            var faculty =  (from c in _db.Classes
                           join cif in _db.ClassInFaculties on c.ID equals cif.IDClass
                           join f in _db.Faculties on cif.IDFaculty equals f.ID
                           where c.ID != idClass
                           select c).ToList();
            return faculty;
        }

        public Result<string> AddFaculty(string name)
        {
            var faculty = new Faculty()
            {
                Name = name
            };
            _db.Faculties.Add(faculty);
            _db.SaveChanges();
            return new ResultSuccess<string>("Thêm thành công");
        }

        public Result<string> AddClass(Class c, int idFaculty)
        {
            var cls = _db.Classes.FirstOrDefault(x => x.CodeClass == c.CodeClass);
            if (cls != null) return new ResultError<string>("Mã lớp đã tồn tại");
            _db.Classes.Add(c);
            _db.SaveChanges();
            var idClass = _db.Classes.FirstOrDefault(x => x.CodeClass == c.CodeClass);
            if (idClass == null) return new ResultError<string>("Lỗi thêm qua hệ lớp khoa");
            var cif = new ClassInFaculty()
            {
                IDClass = idClass.ID,
                IDFaculty = idFaculty
            };
            _db.ClassInFaculties.Add(cif);
            _db.SaveChanges();
            return new ResultSuccess<string>("Thêm thành công");
        }

        public Result<string> RemoveFaculty(int id)
        {
            var faculty = _db.Faculties.Find(id);
            if (faculty == null) return new ResultError<string>("Không tìm thấy mã khoa");
            _db.Faculties.Remove(faculty);
            _db.SaveChanges();
            return new ResultSuccess<string>("Xóa thành công");
        }

        public Result<string> RemoveClass(int id)
        {
            var cif = _db.ClassInFaculties.FirstOrDefault(x => x.IDClass == id);
            if (cif == null) return new ResultError<string>("Không tìm thấy mã quan hệ của lớp");
            _db.ClassInFaculties.Remove(cif);

            var clss = _db.Classes.Find(id);
            if (clss == null) return new ResultError<string>("Không tìm thấy mã lớp");
            _db.Classes.Remove(clss);

            _db.SaveChanges();
            return new ResultSuccess<string>("Xóa thành công");
        }
    }
    
}
