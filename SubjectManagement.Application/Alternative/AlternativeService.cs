using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SubjectManagement.Common.Result;
using SubjectManagement.Data;
using SubjectManagement.Data.EF;
using SubjectManagement.Data.Entities;

namespace SubjectManagement.Application.Alternative
{
    public class AlternativeService : IAlternativeService
    {
        public AlternativeService()
        {
            var connect = new Db();
            _db = connect.Context;
        }

        private readonly SubjectDbContext _db;

        public Result<string> AddAlternative(int idClass, Guid idSubject, Guid idSubjectAlter)
        {
            var result = _db.AlternativeSubjects.FirstOrDefault(x =>
                x.IDClass == idClass && x.IDNew == idSubject && x.IDOld == idSubjectAlter);
            if (result is not null)
                return new ResultError<string>("Môn này thay thế này đã tồn tại, không thể thêm mới");
            var alter = new AlternativeSubject()
            {
                IDNew = idSubjectAlter,
                IDOld = idSubject,
                IDClass = idClass
            };
            _db.AlternativeSubjects.Add(alter);
            _db.SaveChanges();
            return new ResultSuccess<string>("Thêm thành công");
        }

        public Result<string> RemoveAlternative(int idClass, Guid idSubject)
        {
            var alter = _db.AlternativeSubjects.FirstOrDefault(x =>
                x.IDClass == idClass && x.IDNew == idSubject);
            if (alter is null) return new ResultError<string>("Lỗi Không xóa được học phần thay thế này");
            _db.AlternativeSubjects.Remove(alter);
            return new ResultSuccess<string>("Xóa thành công");
        }

        public List<Subject> GetAlternative(int idClass, Guid idSubject)
        {
            var alter = _db.AlternativeSubjects
                .Where(x => x.IDClass == idClass && x.IDOld == idSubject)
                .Select(x => x).ToList();
            return alter.Count < 1 ? null : alter.Select(item => _db.Subjects.SingleOrDefault(x => x.ID == item.IDNew && x.IDClass == item.IDClass)).ToList();
        }
    }
}
