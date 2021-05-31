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

        public async Task<List<Class>> GetClass()
        {
            return _db.Classes.Select(x => x).ToList();
        }
        public async Task<List<Class>> GetClassInFaculty(int idFaculty)
        {
            var _class = new List<Class>();
            _class = _db.Classes.Where(x => x.ClassInFaculty.Faculty.ID == idFaculty).ToList();
            return _class;
        }
        //lớp học khác lớp hiện tại
        public async Task<List<Class>> GetClassDifferentClass(int idFaculty, Class currentClass)
        {
            var _class = new List<Class>();
            _class = _db.Classes.Where(x => x.ClassInFaculty.Faculty.ID == idFaculty && x.ID != currentClass.ID).ToList();
            return _class;
        }
        //Lớp học khác có năm học mới hơn
        public async Task<List<Class>> GetDifferentClassNewer(int idFaculty, Class currentClass)
        {
            var _class = new List<Class>();
            _class = _db.Classes.Where(x => x.ClassInFaculty.Faculty.ID == idFaculty && x.ID != currentClass.ID && x.Year > currentClass.Year).ToList();
            return _class;
        }
        //Lớp học khác có năm học cũ hơn
        public async Task<List<Class>> GetDifferentClassOlder(int idFaculty, Class currentClass)
        {
            var _class = new List<Class>();
            _class = _db.Classes.Where(x => x.ClassInFaculty.Faculty.ID == idFaculty && x.ID != currentClass.ID && x.Year < currentClass.Year).ToList();
            return _class;
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
            //Xóa lớp
            var clss = _db.Classes.Where(x => x.ClassInFaculty.IDFaculty == faculty.ID).ToList();
            if (clss.Count > 0)
            {
                foreach (var c in clss)
                {
                    RemoveClass(c.ID);
                }
            }
                //Xóa khoa
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

            //Xóa môn học
            var subjects = new SubjectApp.SubjectService().GetSubject(clss.ID);
            if (subjects.Count > 0)
                _db.Subjects.RemoveRange(subjects);

            //Xóa lớp
            _db.Classes.Remove(clss);

            _db.SaveChanges();
            return new ResultSuccess<string>("Xóa thành công");
        }

        public Result<string> UnlockClass(int id, bool isLock)
        {
            var clss = _db.Classes.FirstOrDefault(x => x.ID == id);
            if (clss == null) return new ResultError<string>("Không tìm thấy lớp");
            clss.CanEdit = isLock;
            _db.SaveChanges();
            var mess = isLock ? "Mở khóa thành công" : "Khóa thành công";
            return new ResultSuccess<string>(mess);

        }
    }

}
