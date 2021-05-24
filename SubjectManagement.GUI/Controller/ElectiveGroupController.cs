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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subject"> </param>
        /// <param name="credit"> So tin chi trong nhom hoc phan </param>
        /// <returns></returns>
        public bool AddGroup(Subject subject, int credit)
        {
            var add = _electiveGroup.AddGroup(_Class.ID, subject, credit);
            if (add.IsSuccessed) return true;
            MyCommonDialog.MessageDialog("Lỗi thêm nhóm", $"{add.Message}");
            return false;
        }

        public bool RemoveGroup(Guid idSubject)
        {
            var remove = _electiveGroup.RemoveGroup(_Class.ID, idSubject);
            if (remove.IsSuccessed) return true;
            MyCommonDialog.MessageDialog("Lỗi thêm nhóm", $"{remove.Message}");
            return false;

        }

    }
}
