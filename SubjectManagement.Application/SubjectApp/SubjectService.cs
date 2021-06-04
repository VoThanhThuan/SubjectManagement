using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Automation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SubjectManagement.Common.Result;
using SubjectManagement.Data;
using SubjectManagement.Data.EF;
using SubjectManagement.Data.Entities;
using SubjectManagement.ViewModels.Subject;

namespace SubjectManagement.Application.SubjectApp
{
    public class SubjectService : ISubjectService
    {

        public SubjectService()
        {
            //_db = database;
            var connect = new Db();
            _db = connect.Context;
        }

        private readonly SubjectDbContext _db;

        public List<Subject> GetSubject(int idClass)
        {
            var subject = _db.Subjects.Where(x => x.IDClass == idClass).Select(x => x).ToList();
            return subject;
        }

        public List<Subject> GetAllSubject()
        {
            var subject = _db.Subjects.Select(x => x).ToList();
            return subject;
        }

        public List<Subject> GetSubjectOfClass(int idClass)
        {
            var subject = _db.Subjects.Where(x => x.IDClass == idClass).Select(x => x).ToList();
            return subject;
        }

        public List<Subject> GetSubjectOfSemester(int idClass, int semester)
        {
            var subject = _db.Subjects.Where(x => x.IDClass == idClass && x.Semester == semester).Select(x => x).ToList();
            return subject;
        }

        public List<Subject> GetSubjectDifferentSemester(int term, int idClass)
        {
            var subject = _db.Subjects.Where(x => x.IDClass == idClass).Select(x => x);

            //var semester = _db.SubjectInSemesters
            //    .Select(x => x.IDSubject);

            var result = subject.Where(x => x.Semester == 0).ToList();

            return result;
        }
        public List<KnowledgeGroup> GetKnowledgeGroup()
        {
            _db.KnowledgeGroups.Load();
            var group = _db.KnowledgeGroups.Select(x => x).ToList();
            return group;
        }

        public List<Subject> GetSubjectWithGroup(Guid IDGroup, int idClass)
        {
            var subjectInGroup = from sig in _db.SubjectInKnowledgeGroups
                                 where sig.IDClass == idClass && sig.IDKnowledgeGroup == IDGroup
                                 select sig;

            var subject = from s in _db.Subjects where s.IDClass == idClass select s;

            var result = (from sig in subjectInGroup
                          join s in subject on sig.IDSubject equals s.ID
                          select s).ToList();

            return result;
        }

        public Result<KnowledgeGroup> FindKnowledgeGroup(Guid idSubject)
        {
            var sik = _db.SubjectInKnowledgeGroups.FirstOrDefault(x => x.IDSubject == idSubject);
            if (sik is null) return new ResultError<KnowledgeGroup>("Lỗi tìm mã môn trong nhóm môn học");

            var knowG = _db.KnowledgeGroups.Find(sik.IDKnowledgeGroup);
            if (knowG is null) return new ResultError<KnowledgeGroup>("Lỗi truy suất nhóm môn học");

            return new ResultSuccess<KnowledgeGroup>(knowG, "OK");
        }

        /// <summary>
        /// Thêm môn học mới
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Result<string> AddSubject(SubjectRequest request)
        {
            //Thêm môn học
            if (!request.IsPlan)
            {
                var codeCourses = _db.Subjects.FirstOrDefault(x => x.CourseCode == request.CourseCode);
                if (codeCourses is not null) return new ResultError<string>("Đã tồn tại mã môn học");
            }
            var subject = new Subject()
            {
                ID = request.ID,
                CourseCode = request.CourseCode,
                Name = request.Name,
                Credit = request.Credit,
                TypeCourse = request.TypeCourse,
                NumberOfTheory = request.NumberOfTheory,
                NumberOfPractice = request.NumberOfPractice,
                Prerequisite = request.Prerequisite,
                LearnFirst = request.LearnFirst,
                Parallel = request.Parallel,
                Semester = request.Semester,
                Details = request.Details,
                IDClass = request.IdClass,
                IsPlan = request.IsPlan
            };
            _db.Add(subject);
            _db.SaveChanges();

            //thêm môn học vào khối kiến thức
            var sig = new SubjectInKnowledgeGroup()
            {
                IDSubject = request.ID,
                IDClass = request.IdClass,
                IDKnowledgeGroup = request.IDKnowledgeGroup
            };

            _db.SubjectInKnowledgeGroups.Add(sig);
            _db.SaveChanges();


            return new ResultSuccess<string>($"Đã thêm môn học {request.Name} thành công");
        }

        public Result<Subject> FindSubject(string coursesCode, Class _class)
        {
            var subject = _db.Subjects.FirstOrDefault(x => x.CourseCode == coursesCode && x.IDClass == _class.ID);
            if (subject is null) return new ResultError<Subject>($"Không tìm thấy môn học có mã là {coursesCode} trong lớp {_class.CodeClass}");

            return new ResultSuccess<Subject>(subject, "ok");
        }

        public Result<string> CopyListSubject(int idClassOld, int idClassNew)
        {

            var oldSubject = from sik in _db.SubjectInKnowledgeGroups
                             join s in _db.Subjects on sik.IDSubject equals s.ID
                             where sik.IDClass == idClassOld && s.IDClass == idClassOld
                             select new
                             {
                                 s.ID,
                                 s.CourseCode,
                                 s.Name,
                                 s.Credit,
                                 s.TypeCourse,
                                 s.NumberOfTheory,
                                 s.NumberOfPractice,
                                 s.Prerequisite,
                                 s.LearnFirst,
                                 s.Parallel,
                                 s.Details,
                                 s.Semester,
                                 s.IDClass,
                                 s.IsPlan,
                                 sik.IDKnowledgeGroup
                             };
            //var oldSubject = _db.Subjects.Where(x => x.IDClass == idClassOld).Select(x => x);
            if (oldSubject.ToList().Count <= 0)
                return new ResultError<string>("Không có dữ liệu để sao chép");

            var newSubject = _db.Subjects.Where(x => x.IDClass == idClassNew).Select(x => x);

            //Xóa danh sách củ
            foreach (var subject in newSubject)
            {
                _db.Subjects.Remove(subject);
            }

            _db.SaveChanges();

            //Copy
            foreach (var item in oldSubject)
            {
                var id = Guid.NewGuid();
                var subject = new Subject()
                {
                    ID = id,
                    IDClass = idClassNew,
                    CourseCode = item.CourseCode,
                    Name = item.Name,
                    Credit = item.Credit,
                    TypeCourse = item.TypeCourse,
                    NumberOfTheory = item.NumberOfTheory,
                    NumberOfPractice = item.NumberOfPractice,
                    Prerequisite = item.Prerequisite,
                    LearnFirst = item.LearnFirst,
                    Parallel = item.Parallel,
                    Semester = item.Semester,
                    Details = item.Details,
                    IsPlan = item.IsPlan
                };

                _db.Subjects.Add(subject);
                //thêm môn học vào khối kiến thức
                var sig = new SubjectInKnowledgeGroup()
                {
                    IDSubject = id,
                    IDClass = idClassNew,
                    IDKnowledgeGroup = item.IDKnowledgeGroup
                };
                _db.SubjectInKnowledgeGroups.Add(sig);
            }

            _db.SaveChanges();

            return new ResultSuccess<string>("Copy hoàn thành");
        }

        public Result<string> EditSubject(SubjectRequest request)
        {
            //edit group elective
            if (request.TypeCourse == false)
                new ElectiveGroupApp.ElectiveGroupService().RemoveGroup(request.IDClass, request.ID);

            //edit knowledge group
            if (request.IDKnowledgeGroup != request.IDKnowledgeGroupOld)
            {
                var group = _db.SubjectInKnowledgeGroups.FirstOrDefault(
                    x => x.IDKnowledgeGroup == request.IDKnowledgeGroupOld && x.IDSubject == request.ID);

                if (group is null) return new ResultError<string>("Lỗi tìm kiếm mã nhóm môn học");

                group.IDKnowledgeGroup = request.IDKnowledgeGroup;

                _db.SubjectInKnowledgeGroups.Update(group);
                _db.SaveChanges();
            }

            //edit jsubject
            var subject = _db.Subjects.Find(request.ID, request.IdClass);

            if (subject is null) return new ResultError<string>("Lỗi tìm kiếm mã môn - line 115 - SubjectService");

            if (subject.TypeCourse == false && subject.TypeCourse != request.TypeCourse)
                new ElectiveGroupApp.ElectiveGroupService().RemoveGroup(subject.IDClass, subject.ID);

            subject.CourseCode = request.CourseCode;
            subject.Name = request.Name;
            subject.Credit = request.Credit;
            subject.TypeCourse = request.TypeCourse;
            subject.NumberOfTheory = request.NumberOfTheory;
            subject.NumberOfPractice = request.NumberOfPractice;
            subject.Prerequisite = request.Prerequisite;
            subject.LearnFirst = request.LearnFirst;
            subject.Parallel = request.Parallel;
            subject.Semester = request.Semester;
            subject.TypeCourse = request.TypeCourse;
            subject.Details = request.Details;

            _db.Subjects.Update(subject);
            _db.SaveChanges();

            return new ResultSuccess<string>();
        }

        public Result<string> RemoveSubject(SubjectRequest request)
        {
            //edit group elective
            if (request.TypeCourse == false)
                new ElectiveGroupApp.ElectiveGroupService().RemoveGroup(request.IDClass, request.ID);


            var error = false;
            var errorMess = "";

            SubjectInKnowledgeGroup sig = null;
            if (request.IDKnowledgeGroup != Guid.Empty)
                sig = _db.SubjectInKnowledgeGroups.FirstOrDefault(
                    x => x.IDSubject == request.ID && x.IDKnowledgeGroup == request.IDKnowledgeGroup);

            if (sig is not null)
            {
                _db.SubjectInKnowledgeGroups.Remove(sig);
                _db.SaveChanges();
            }
            else
            {
                error = true;
                errorMess = "Lỗi xóa nhóm học phần - SubjectService";
            }

            //Tìm môn học
            var subject = _db.Subjects.FirstOrDefault(x => x.ID == request.ID && x.IDClass == request.IDClass);

            if (subject is not null)
            {
                _db.Subjects.Remove(subject);
                _db.SaveChanges();
            }
            else
            {
                error = true;
                errorMess = "Lỗi xóa học phần - SubjectService";
            }


            if (error)
                return new ResultError<string>(errorMess);

            return new ResultSuccess<string>($"Đã xóa thành công môn học {subject.Name}");
        }

        public Result<Class> FindClassWithIdSubject(Guid idSubject)
        {
            var sub = _db.Subjects.FirstOrDefault(x => x.ID == idSubject);
            if (sub == null) return new ResultError<Class>("Không tìm thấy mã môn");
            var clss = _db.Classes.FirstOrDefault(x => x.ID == sub.IDClass);
            if (clss == null) return new ResultError<Class>("Không tìm thấy lớp");
            return new ResultSuccess<Class>(clss);
        }

    }
}
