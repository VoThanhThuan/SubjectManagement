using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using SubjectManagement.Application.KnowledgeGroupApp;
using SubjectManagement.Application.SubjectApp;
using SubjectManagement.Common.Result;
using SubjectManagement.Data;
using SubjectManagement.Data.Entities;
using SubjectManagement.GUI.Dialog;

namespace SubjectManagement.GUI.Controller
{
    public class KnowledgeGroupController
    {
        public KnowledgeGroupController(Class _class)
        {
            _knowledgeGroupService = new KnowledgeGroupService();
            _Class = _class;
        }

        private readonly IKnowledgeGroupService _knowledgeGroupService;
        public Class _Class { get; init; }

        public KnowledgeGroup FindKnơKnowledgeGroup(Guid idSubject)
        {
            return _knowledgeGroupService.FindKnowledgeGroup(idSubject);
        }

        public List<KnowledgeGroup> GetKnowledgeGroups()
        {
            return _knowledgeGroupService.GetKnowledgeGroups();
        }

        public void AddKnowledge(string name)
        {
            var result = _knowledgeGroupService.AddKnowledge(name);
            if (result.IsSuccessed) return;
            MyCommonDialog.MessageDialog("Lỗi thêm", $"{result.Message}");
        }

        public void EditKnowledge(Guid id, string name)
        {
            var result = _knowledgeGroupService.EditKnowledge(id, name);
            if (result.IsSuccessed) return;
            MyCommonDialog.MessageDialog("Lỗi sửa", $"{result.Message}");

        }
        public void RemoveKnowledge(Guid id)
        {
            var result = _knowledgeGroupService.RemoveKnowledge(id);
            if (result.IsSuccessed) return;
            MyCommonDialog.MessageDialog("Lỗi xóa", $"{result.Message}");
        }

    }
}
