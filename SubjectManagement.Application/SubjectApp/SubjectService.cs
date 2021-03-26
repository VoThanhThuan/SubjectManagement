using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SubjectManagement.Common.Result;
using SubjectManagement.Data;
using SubjectManagement.Data.Entities;
using SubjectManagement.ViewModels.Subject;

namespace SubjectManagement.Application.SubjectApp
{
    public class SubjectService : ISubjectService
    {
        public async Task<LocalView<Subject>> LoadSubject()
        {
            var subject = Db.Context.Subjects.Local;
            return subject;
        }

        public async Task<List<KnowledgeGroup>> LoadKnowledgeGroup()
        {
            await Db.Context.KnowledgeGroups.LoadAsync();
            var group = Db.Context.KnowledgeGroups.Select(x => x).ToList();
            return group;
        }

        public async Task<List<Subject>> LoadSubjectWithGroup(Guid IDGroup)
        {
            var subjectInGroup = (from sig in Db.Context.SubjectInKnowledgeGroups
                                        join s in Db.Context.Subjects on sig.IDSubject equals s.ID
                                        where sig.IDKnowledgeGroup == IDGroup
                                        select s).ToList();
            subjectInGroup.Reverse();
            return subjectInGroup;
        }

        public async Task<List<KnowledgeGroup>> FindKnowledgeGroup(Guid idSubject)
        {
            var knowledge = (from k in Db.Context.KnowledgeGroups
                        join sig in Db.Context.SubjectInKnowledgeGroups on k.ID equals sig.IDKnowledgeGroup
                        where sig.IDSubject == idSubject
                        select k).ToList();

            return knowledge;
        }

        public async Task<Result<string>> AddSubject(SubjectRequest request)
        {
            var codeCourses = Db.Context.Subjects.FirstOrDefaultAsync(x => x.CourseCode == request.CourseCode);
            if (codeCourses is null) return new ResultError<string>("Đã tồn tại mã môn học");
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
            await Db.Context.AddAsync(subject);

            await Db.Context.SaveChangesAsync();

            var sig = new SubjectInKnowledgeGroup()
            {
                IDSubject = request.ID,
                IDKnowledgeGroup = request.IDKnowledgeGroup
            };

            await Db.Context.SubjectInKnowledgeGroups.AddAsync(sig);

            await Db.Context.SaveChangesAsync();

            return new ResultSuccess<string>("0");
        }

        public async Task<Result<Subject>> FindSubject(string coursesCode)
        {
            var subject = await Db.Context.Subjects.FirstOrDefaultAsync(x => x.CourseCode == coursesCode);
            if (subject is null) return new ResultError<Subject>($"Không tìm thấy môn học có mã là {coursesCode}");

            return new ResultSuccess<Subject>(subject, "ok");
        }

        public async Task<Result<string>> EditSubject(SubjectRequest request)
        {
            //edit knowledge group
            if (request.IDKnowledgeGroup != request.IDKnowledgeGroupOld)
            {
                var group = await Db.Context.SubjectInKnowledgeGroups.FirstOrDefaultAsync(
                    x => x.IDKnowledgeGroup == request.IDKnowledgeGroupOld && x.IDSubject == request.ID);

                if (group is null) return new ResultError<string>("Lỗi tìm kiếm mã nhóm môn học - line 104 - SubjectService");
               
                group.IDKnowledgeGroup = request.IDKnowledgeGroup;

                Db.Context.SubjectInKnowledgeGroups.Update(group);
                await Db.Context.SaveChangesAsync();
            }

            //edit jsubject
            var subject = await Db.Context.Subjects.FindAsync(request.ID);

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

            Db.Context.Subjects.Update(subject);
            await Db.Context.SaveChangesAsync();

            return new ResultSuccess<string>();
        }

        public async Task<Result<string>> RemoveSubject(SubjectRequest request)
        {
            var error = false;
            var errorMess = "";

            var sig = await Db.Context.SubjectInKnowledgeGroups.FirstOrDefaultAsync(
                x => x.IDSubject == request.ID && x.IDKnowledgeGroup == request.IDKnowledgeGroup);

            if (sig is not null)
            {
                Db.Context.SubjectInKnowledgeGroups.Remove(sig);
                await Db.Context.SaveChangesAsync();
            }
            else
            {
                error = true;
                errorMess = "Lỗi xóa nhóm học phần - SubjectService";
            }

            var subject = await Db.Context.Subjects.FirstOrDefaultAsync(x => x.CourseCode == request.CourseCode);

            if (subject is not null)
            {
                Db.Context.Subjects.Remove(subject);
                await Db.Context.SaveChangesAsync();
            }
            else
            {
                error = true;
                errorMess = "Lỗi xóa học phần - SubjectService";
            }
            

            if(error)
                return new ResultError<string>(errorMess);

            return new ResultSuccess<string>();
        }

    }
}
