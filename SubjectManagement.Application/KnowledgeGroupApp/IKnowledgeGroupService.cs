using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SubjectManagement.Common.Result;
using SubjectManagement.Data.Entities;

namespace SubjectManagement.Application.KnowledgeGroupApp
{
    public interface IKnowledgeGroupService
    {
        public List<KnowledgeGroup> GetKnowledgeGroups();
        public Result<string> AddKnowledge(string name);
        public Result<string> EditKnowledge(Guid id, string name);
        public Result<string> RemoveKnowledge(Guid id);

    }
}