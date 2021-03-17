using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SubjectManagement.Common.Result;
using SubjectManagement.Data.Entities;

namespace SubjectManagement.Application.SubjectApp
{
    public interface ISubjectService
    {
        Result<LocalView<Subject>> LoadSubject();
    }
}
