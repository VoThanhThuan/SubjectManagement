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

        public Result<string> AddAlternative(int idClassOld, Guid idSubject, Guid idSubjectAlter)
        {
            var result = _db.AlternativeSubjects.FirstOrDefault(x =>
                x.IDClass == idClassOld && x.IDNew == idSubjectAlter && x.IDOld == idSubject);
            if (result is not null)
                return new ResultError<string>("Môn này thay thế này đã tồn tại, không thể thêm mới");
            var alter = new AlternativeSubject()
            {
                IDNew = idSubjectAlter,
                IDOld = idSubject,
                IDClass = idClassOld
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
            _db.SaveChanges();
            return new ResultSuccess<string>("Xóa thành công");
        }

        public List<Subject> GetAlternative(int idClass, Guid idSubject, int idClassOld)
        {
            var alter = _db.AlternativeSubjects
                .Where(x => x.IDClass == idClassOld && x.IDOld == idSubject)
                .Select(x => x).ToList();

            var subjects = alter.Select(item => _db.Subjects.FirstOrDefault(x => x.ID == item.IDNew && x.IDClass == idClassOld)).Where(sub => sub != null).ToList();

            return alter.Count < 1 ? null : subjects;
        }

        public List<Subject> FindAlternative(int idClass, int idClassOld, Guid idSubject)
        {
            var alter = _db.AlternativeSubjects
                .Where(x => x.IDClass == idClass && x.IDNew == idSubject)
                .Select(x => x).ToList();

            var subjects = alter.Select(item => _db.Subjects.FirstOrDefault(x => x.ID == item.IDOld)).Where(sub => sub != null).ToList();

            return alter.Count < 1 ? null : subjects;
        }
    }
}
