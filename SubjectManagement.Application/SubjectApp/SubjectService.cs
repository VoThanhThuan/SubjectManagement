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

        public SubjectService(SubjectDbContext database)
        {
            _db = database;
        }

        private readonly SubjectDbContext _db;

        public List<Subject> LoadSubject()
        {
            var subject = _db.Subjects.Select(x => x).ToList();
            return subject;
        }
        public List<Subject> LoadSubjectOfClass(int idClass)
        {
            var subject = (from s in _db.Subjects
                           join soc in _db.SubjectOfClasses on s.ID equals soc.IDSubject
                           where soc.IDClass == idClass
                           select s).ToList();
            return subject;
        }
        public List<Subject> LoadSubjectDifferentSemester(int? term)
        {
            var subject = _db.Subjects.Select(x => x);

            var semester = _db.SubjectInSemesters
                .Select(x => x.IDSubject);


            var result = subject.Where(x => !semester.Contains(x.ID)).ToList();

            return result;
        }
        public async Task<List<KnowledgeGroup>> LoadKnowledgeGroup()
        {
            await _db.KnowledgeGroups.LoadAsync();
            var group = _db.KnowledgeGroups.Select(x => x).ToList();
            return group;
        }

        public  List<Subject> LoadSubjectWithGroup(Guid IDGroup, int idClass)
        {
            var subjectOfClass = from s in _db.Subjects
                                 join soc in _db.SubjectOfClasses on s.ID equals soc.IDSubject
                                 where soc.IDClass == idClass
                                 select s;
            var subjectInGroup = (from sig in _db.SubjectInKnowledgeGroups
                                  join s in subjectOfClass on sig.IDSubject equals s.ID
                                  where sig.IDKnowledgeGroup == IDGroup
                                  select s).ToList();
            subjectInGroup.Reverse();
            return subjectInGroup;
        }

        public List<KnowledgeGroup> FindKnowledgeGroup(Guid idSubject)
        {
            var knowledge =  (from k in _db.KnowledgeGroups
                             join sig in _db.SubjectInKnowledgeGroups on k.ID equals sig.IDKnowledgeGroup
                             where sig.IDSubject == idSubject
                             select k).ToList();

            return knowledge;
        }

        /// <summary>
        /// Thêm môn học mới
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Result<string> AddSubject(SubjectRequest request)
        {
            //Thêm môn học
            var codeCourses = _db.Subjects.FirstOrDefault(x => x.CourseCode == request.CourseCode);
            if (codeCourses is not null) return new ResultError<string>("Đã tồn tại mã môn học");
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
                IsOffical = request.IsOffical,
                Details = request.Details
            };
            _db.Add(subject);
            _db.SaveChanges();

            //thêm môn học vào khối kiến thức
            var sig = new SubjectInKnowledgeGroup()
            {
                IDSubject = request.ID,
                IDKnowledgeGroup = request.IDKnowledgeGroup
            };

            _db.SubjectInKnowledgeGroups.Add(sig);
            _db.SaveChanges();

            //Thêm môn học vào lớp học
            var soc = new SubjectOfClass()
            {
                IDClass = request.IdClass,
                IDSubject = request.ID
            };

            _db.SubjectOfClasses.Add(soc);
            _db.SaveChanges();

            return new ResultSuccess<string>($"Đã thêm môn học {request.Name} thành công");
        }

        public Result<Subject> FindSubject(string coursesCode)
        {
            var subject = _db.Subjects.FirstOrDefault(x => x.CourseCode == coursesCode);
            if (subject is null) return new ResultError<Subject>($"Không tìm thấy môn học có mã là {coursesCode}");

            return new ResultSuccess<Subject>(subject, "ok");
        }

        public Result<string> EditSubject(SubjectRequest request)
        {
            //edit knowledge group
            if (request.IDKnowledgeGroup != request.IDKnowledgeGroupOld)
            {
                var group = _db.SubjectInKnowledgeGroups.FirstOrDefault(
                    x => x.IDKnowledgeGroup == request.IDKnowledgeGroupOld && x.IDSubject == request.ID);

                if (group is null) return new ResultError<string>("Lỗi tìm kiếm mã nhóm môn học - line 104 - SubjectService");

                group.IDKnowledgeGroup = request.IDKnowledgeGroup;

                _db.SubjectInKnowledgeGroups.Update(group);
                _db.SaveChanges();
            }

            //edit jsubject
            var subject = _db.Subjects.Find(request.ID);

            if (subject is null) return new ResultError<string>("Lỗi tìm kiếm mã môn - line 115 - SubjectService");

            subject.CourseCode = request.CourseCode;
            subject.Name = request.Name;
            subject.Credit = request.Credit;
            subject.TypeCourse = request.TypeCourse;
            subject.NumberOfTheory = request.NumberOfTheory;
            subject.NumberOfPractice = request.NumberOfPractice;
            subject.Prerequisite = request.Prerequisite;
            subject.LearnFirst = request.LearnFirst;
            subject.Parallel = request.Parallel;
            subject.IsOffical = request.IsOffical;
            subject.Details = request.Details;

            _db.Subjects.Update(subject);
            _db.SaveChanges();

            return new ResultSuccess<string>();
        }

        public Result<string> RemoveSubject(SubjectRequest request)
        {
            var error = false;
            var errorMess = "";

            var sig = _db.SubjectInKnowledgeGroups.FirstOrDefault(
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

            var subject = _db.Subjects.FirstOrDefault(x => x.ID == request.ID);

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

    }
}
