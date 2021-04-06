using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SubjectManagement.Common.Result;
using SubjectManagement.Data.Entities;
using SubjectManagement.ViewModels.Subject;

namespace SubjectManagement.Application.CompareApp
{
    public interface ICompareService
    {
        public Result<List<SubjectCompareVM>> CompareSubject(int idClassCurrent, int idClassCompare);
    }
}
