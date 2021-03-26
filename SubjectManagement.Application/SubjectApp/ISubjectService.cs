using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SubjectManagement.Common.Result;
using SubjectManagement.Data.Entities;
using SubjectManagement.ViewModels.Subject;

namespace SubjectManagement.Application.SubjectApp
{
    public interface ISubjectService
    {
        Task<LocalView<Subject>> LoadSubject();
        Task<List<KnowledgeGroup>> LoadKnowledgeGroup();
        Task<List<Subject>> LoadSubjectWithGroup(Guid IDGroup);

        Task<List<KnowledgeGroup>> FindKnowledgeGroup(Guid idSubject);

        Task<Result<string>> AddSubject(SubjectRequest request);

        Task<Result<string>> EditSubject(SubjectRequest request);

        Task<Result<string>> RemoveSubject(SubjectRequest request);

        Task<Result<Subject>> FindSubject(string coursesCode);



    }
}
