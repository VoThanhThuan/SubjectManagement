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
        List<Subject> GetSubject(int idClass);
        List<Subject> GetAllSubject();
        List<Subject> GetSubjectOfClass(int idClass);
        List<Subject> GetSubjectOfSemester(int idClass, int semester);

        List<Subject> GetSubjectDifferentSemester(int term, int idClass);
        List<KnowledgeGroup> GetKnowledgeGroup();
        List<Subject> GetSubjectWithGroup(Guid IDGroup, int idClass);

        Result<KnowledgeGroup> FindKnowledgeGroup(Guid idSubject);

        Result<string> AddSubject(SubjectRequest request);

        Result<string> EditSubject(SubjectRequest request);

        Result<string> RemoveSubject(SubjectRequest request);

        Result<Subject> FindSubject(string coursesCode, Class _class);

        Result<Class> FindClassWithIdSubject(Guid idSubject);

        Result<string> CopyListSubject(int idClassOld, int idClassNew);

    }
}
