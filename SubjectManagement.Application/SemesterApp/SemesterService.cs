using SubjectManagement.Common.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SubjectManagement.Data;
using SubjectManagement.Data.EF;
using SubjectManagement.Data.Entities;
using SubjectManagement.ViewModels.SubjectOfClass;

namespace SubjectManagement.Application.SemesterApp
{
    public class SemesterService : ISemesterService
    {

        public SemesterService()
        {
            var connect = new Db();
            _db = connect.Context;
        }

        private readonly SubjectDbContext _db;

        public Result<string> AddSubject(Subject request, int semester)
        {
            request.Semester = semester;

            _db.SaveChanges();
            return new ResultSuccess<string>("Thêm thành công");
        }

        public List<Subject> LoadSubject(int idSemester, int idClass)
        {
            //var subject = (from sis in _db.SubjectInSemesters
            //    join s in _db.Subjects on sis.IDSubject equals s.ID
            //    where sis.IDSemester == idSemester
            //    select s).ToList();

            var subject = _db.Subjects.Where(x => x.Semester == idSemester && x.IDClass == idClass).Select(x => x).ToList();

            return subject;
        }

        public Result<string> RemoveSubject(Guid idSubject, int term)
        {
            var s = _db.Subjects.Find(idSubject);
            if (s is null)
                return new ResultError<string>($"Môn học không tồn tại");

            s.Semester = null;

            _db.SaveChanges();
            return new ResultSuccess<string>($"Xóa thành công môn học khỏi học kỳ {term}");
        }
    }
}
