using SubjectManagement.Common.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SubjectManagement.Data.EF;
using SubjectManagement.Data.Entities;

namespace SubjectManagement.Application.SemesterApp
{
    public class SemesterService : ISemesterService
    {

        public SemesterService(SubjectDbContext db)
        {
            _db = db;
        }

        private readonly SubjectDbContext _db;

        public Result<string> AddSubject(Guid idSubject, int term)
        {
            var checkValid =
                _db.SubjectInSemesters.Find(idSubject, term);
            if (checkValid is not null)
                return new ResultError<string>($"Môn học này thuộc học kỳ {checkValid.IDSemester}");
            var sis = new SubjectInSemester()
            {
                IDSemester = term,
                IDSubject = idSubject
            };
            _db.SubjectInSemesters.Add(sis);
            _db.SaveChanges();
            return new ResultSuccess<string>("Thêm thành công");
        }

        public List<Subject> LoadSubject(int idSemester)
        {
            var subject = (from sis in _db.SubjectInSemesters
                join s in _db.Subjects on sis.IDSubject equals s.ID
                where sis.IDSemester == idSemester
                select s).ToList();
            return subject;
        }

        public Result<string> RemoveSubject(Guid idSubject, int term)
        {
            var sis = _db.SubjectInSemesters.Find(idSubject, term);
            if (sis is null)
                return new ResultError<string>($"Môn học không tồn tại");
            _db.SubjectInSemesters.Remove(sis);
            _db.SaveChanges();
            return new ResultSuccess<string>($"Xóa thành công môn học khỏi học kỳ {term}");
        }
    }
}
