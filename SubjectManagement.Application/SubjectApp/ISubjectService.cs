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
        List<Subject> LoadSubject();
        List<Subject> LoadSubjectOfClass(int idClass);
        List<Subject> LoadSubjectDifferentSemester(int? term, int idClass);
        List<KnowledgeGroup> LoadKnowledgeGroup();
        List<Subject> LoadSubjectWithGroup(Guid IDGroup, int idClass);

        Result<KnowledgeGroup> FindKnowledgeGroup(Guid idSubject);

        List<Subject> GetSubject(int idClass);
        Result<string> AddSubject(SubjectRequest request);

        Result<string> EditSubject(SubjectRequest request);

        Result<string> RemoveSubject(SubjectRequest request);

        Result<Subject> FindSubject(string coursesCode);

        Result<string> CopyListSubject(int idClassOld, int idClassNew);

    }
}
