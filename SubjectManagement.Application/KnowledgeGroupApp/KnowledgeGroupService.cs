using SubjectManagement.Common.Result;
using SubjectManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SubjectManagement.Data;
using SubjectManagement.Data.EF;

namespace SubjectManagement.Application.KnowledgeGroupApp
{
    public class KnowledgeGroupService : IKnowledgeGroupService
    {

        public KnowledgeGroupService()
        {
            var connect = new Db();
            _db = connect.Context;
        }

        private readonly SubjectDbContext _db;

        public Result<string> AddKnowledge(string name)
        {
            var group = new KnowledgeGroup()
            {
                ID = Guid.NewGuid(),
                Name = name
            };
            _db.KnowledgeGroups.Add(group);
            _db.SaveChanges();
            return new ResultSuccess<string>("OK");
        }

        public Result<string> EditKnowledge(Guid id, string name)
        {
            var g = _db.KnowledgeGroups.Find(id);
            if (g is null) return new ResultError<string>("Không tìm thầy");
            g.Name = name;
            _db.SaveChanges();
            return new ResultSuccess<string>("OK");
        }

        public List<KnowledgeGroup> GetKnowledgeGroups()
        {
            _db.KnowledgeGroups.LoadAsync();
            var group = _db.KnowledgeGroups.Select(x => x).ToList();
            return group;
        }

        public Result<string> RemoveKnowledge(Guid id)
        {
            var g = _db.KnowledgeGroups.Find(id);
            if (g is null) return new ResultError<string>("Không tìm thầy");
            _db.KnowledgeGroups.Remove(g);
            _db.SaveChanges();
            return new ResultSuccess<string>("Đã xóa thành công");
        }
    }
}
