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
            return subjectInGroup;
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
    }
}
