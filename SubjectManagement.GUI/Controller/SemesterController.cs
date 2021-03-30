using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using SubjectManagement.Application.SemesterApp;
using SubjectManagement.Common.Dialog;
using SubjectManagement.Data;
using SubjectManagement.Data.Entities;

namespace SubjectManagement.GUI.Controller
{
    public class SemesterController
    {
        public void AddSubject(Guid idSubject, int term)
        {
            var add = new SemesterService(Db.Context);
            var result = add.AddSubject(idSubject, term);
            var mess = new MessageDialog()
            {
                tbl_Title = { Text = $"Lỗi thêm" },
                tbl_Message = { Text = $"{result.Message}" },
                title_color = { Background = new SolidColorBrush(Color.FromRgb(255, 0, 0)) },
                Topmost = true
            };
            mess.ShowDialog();
        }
        public void RemoveSubject(Guid idSubject, int term)
        {
            var remove = new SemesterService(Db.Context);
            var result = remove.RemoveSubject(idSubject, term);

            var mess = new MessageDialog()
            {
                tbl_Title = { Text = $"Lỗi xóa" },
                tbl_Message = { Text = $"{result.Message}" },
                title_color = { Background = new SolidColorBrush(Color.FromRgb(255, 0, 0)) },
                Topmost = true
            };
            mess.ShowDialog();
        }
        public List<Subject> LoadSubject(int idSemester)
        {
            var load = new SemesterService(Db.Context);
            return load.LoadSubject(idSemester);

        }
    }
}
