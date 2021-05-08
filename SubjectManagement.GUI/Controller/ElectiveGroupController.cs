using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using SubjectManagement.Application.ElectiveGroupApp;
using SubjectManagement.Application.SubjectApp;
using SubjectManagement.Common.Result;
using SubjectManagement.Data.Entities;
using SubjectManagement.GUI.Dialog;

namespace SubjectManagement.GUI.Controller
{
    public class ElectiveGroupController
    {
        public ElectiveGroupController(Class _class)
        {
            _electiveGroup = new ElectiveGroupService();
            _Class = _class;
        }

        private readonly IElectiveGroupService _electiveGroup;

        public Class _Class { get; init; }

        public bool AddGroup(Subject subject)
        {
            var add = _electiveGroup.AddGroup(_Class.ID, subject);
            if (add.IsSuccessed) return true;
            var mess = new MessageDialog()
            {
                tbl_Title = { Text = $"Lỗi thêm" },
                tbl_Message = { Text = $"{add.Message}" },
                title_color = { Background = new SolidColorBrush(Color.FromRgb(255, 0, 0)) },
                Topmost = true
            };
            mess.ShowDialog();
            return false;
        }

        public bool RemoveGroup(Guid idSubject)
        {
            var remove = _electiveGroup.RemoveGroup(_Class.ID, idSubject);
            if (remove.IsSuccessed) return true;
            var mess = new MessageDialog()
            {
                tbl_Title = { Text = $"Lỗi remove group" },
                tbl_Message = { Text = $"{remove.Message}" },
                title_color = { Background = new SolidColorBrush(Color.FromRgb(255, 0, 0)) },
                Topmost = true
            };
            mess.ShowDialog();
            return false;

        }

    }
}
