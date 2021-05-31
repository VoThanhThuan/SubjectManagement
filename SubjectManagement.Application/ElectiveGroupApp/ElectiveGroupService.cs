using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SubjectManagement.Common.Result;
using SubjectManagement.Data;
using SubjectManagement.Data.EF;
using SubjectManagement.Data.Entities;

namespace SubjectManagement.Application.ElectiveGroupApp
{
    public class ElectiveGroupService : IElectiveGroupService
    {
        public ElectiveGroupService()
        {
            var connect = new Db();
            _db = connect.Context;
        }

        private readonly SubjectDbContext _db;
        public Result<string> AddGroup(int idClass, Subject subject, int credit)
        {
            var group = _db.ElectiveGroups.FirstOrDefault(x => x.IDClass == idClass && x.Semester == subject.Semester);
            var idElc = Guid.NewGuid();
            if (group == null)
            {
                group = new ElectiveGroup()
                {
                    ID = idElc,
                    IDClass = idClass,
                    Semester = subject.Semester,
                    TotalSubject = 0,
                    Credit = credit
                };
                _db.ElectiveGroups.Add(group);
                _db.SaveChanges();
            }
            var s = _db.Subjects.Find(subject.ID, subject.IDClass);
            if (s == null) return new ResultError<string>("Lỗi tìm môn học");
            s.IDElectiveGroup = group.ID;
            ++group.TotalSubject;
            _db.SaveChanges();

            return new ResultSuccess<string>("Add Thành Công");
        }

        public Result<string> RemoveGroup(int idClass, Guid idSubject)
        {
            var subject = _db.Subjects.Find(idSubject, idClass);
            if (subject == null) return new ResultError<string>("Lỗi tìm môn học");

            var group = _db.ElectiveGroups.Find(subject.IDElectiveGroup);

            subject.IDElectiveGroup = null;
            --group.TotalSubject;
            _db.SaveChanges();

            if (@group.TotalSubject >= 1) return new ResultSuccess<string>("Đã xóa môn khỏi nhóm môn tự chọn");
            _db.ElectiveGroups.Remove(@group);
            _db.SaveChanges();

            return new ResultSuccess<string>("Đã xóa môn khỏi nhóm môn tự chọn");
        }
    }
}
