using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SubjectManagement.Common.Result;
using SubjectManagement.Data;
using SubjectManagement.Data.Entities;

namespace SubjectManagement.Application.SubjectApp
{
    public class SubjectService : ISubjectService
    {
        public async Task<Result<LocalView<Subject>>> LoadSubject()
        {
            await Db.Context.Subjects.LoadAsync();
            var subject = Db.Context.Subjects.Local;
            return new ResultSuccess<LocalView<Subject>>(subject, "OK");
        }
    }
}
