using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using SubjectManagement.Application.KnowledgeGroupApp;
using SubjectManagement.Application.SubjectApp;
using SubjectManagement.Common.Dialog;
using SubjectManagement.Common.Result;
using SubjectManagement.Data;
using SubjectManagement.Data.Entities;

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


        public List<KnowledgeGroup> GetKnowledgeGroups()
        {
            return _knowledgeGroupService.GetKnowledgeGroups();
        }

        public void AddKnowledge(string name)
        {
            var result = _knowledgeGroupService.AddKnowledge(name);
            if (result.IsSuccessed) return;
            var mess = new MessageDialog()
            {
                tbl_Title = { Text = $"Lỗi thêm" },
                tbl_Message = { Text = $"{result.Message}" },
                title_color = { Background = new SolidColorBrush(Color.FromRgb(255, 0, 0)) },
                Topmost = true
            };
            mess.ShowDialog();
        }

        public void EditKnowledge(Guid id, string name)
        {
            var result = _knowledgeGroupService.EditKnowledge(id, name);
            if (result.IsSuccessed) return;
            var mess = new MessageDialog()
            {
                tbl_Title = { Text = $"Lỗi sửa" },
                tbl_Message = { Text = $"{result.Message}" },
                title_color = { Background = new SolidColorBrush(Color.FromRgb(255, 0, 0)) },
                Topmost = true
            };
            mess.ShowDialog();
        }
        public void RemoveKnowledge(Guid id)
        {
            var result = _knowledgeGroupService.RemoveKnowledge(id);
            if (result.IsSuccessed) return;
            var mess = new MessageDialog()
            {
                tbl_Title = { Text = $"Lỗi xóa" },
                tbl_Message = { Text = $"{result.Message}" },
                title_color = { Background = new SolidColorBrush(Color.FromRgb(255, 0, 0)) },
                Topmost = true
            };
            mess.ShowDialog();
        }

    }
}
