using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.EntityFrameworkCore;
using SubjectManagement.Application.FacultyApp;
using SubjectManagement.Common.Dialog;
using SubjectManagement.Common.Result;
using SubjectManagement.Data;
using SubjectManagement.Data.Entities;

namespace SubjectManagement.GUI.Controller
{
    class FacultyController
    {
        public FacultyController()
        {
            _facultyService = new FacultyService();
        }

        private readonly IFacultyService _facultyService;

        public async void LoadFacultyAndClass()
        {
            //await Db.Context.Faculties.LoadAsync();
            //await Db.Context.Classes.LoadAsync();
            //await Db.Context.ClassInFaculties.LoadAsync();
        }

        public async void GetFaculty(ComboBox cbb)
        {
            cbb.ItemsSource = await _facultyService.GetFaculty();
            cbb.DisplayMemberPath = "Name";
        }

        public async void GetClass(ComboBox cbb, int? idClass = null)
        {
            cbb.ItemsSource = await _facultyService.GetClass(idClass);
            cbb.DisplayMemberPath = "CodeClass";
        }

        public void AddFaculty(string name)
        {
            var faculty = _facultyService.AddFaculty(name);
        }

        public void AddClass(Class c, int idFaculty)
        {
            var clss = _facultyService.AddClass(c, idFaculty);
            if(!clss.IsSuccessed) return;
            var mess = new MessageDialog()
            {
                tbl_Title = { Text = $"Lỗi thêm mới" },
                tbl_Message = { Text = $"{clss.Message}" },
                title_color = { Background = new SolidColorBrush(Color.FromRgb(255, 0, 0)) },
                Topmost = true
            };
            mess.ShowDialog();
        }

        public void RemoveFaculty(int id)
        {
            var faculty = _facultyService.RemoveFaculty(id);
            if (!faculty.IsSuccessed)
            {
                var mess = new MessageDialog()
                {
                    tbl_Title = { Text = $"Lỗi xóa" },
                    tbl_Message = { Text = $"{faculty.Message}" },
                    title_color = { Background = new SolidColorBrush(Color.FromRgb(255, 0, 0)) },
                    Topmost = true
                };
                mess.ShowDialog();
            }
            var result = new MessageDialog()
            {
                tbl_Title = { Text = $"Xóa thành công" },
                tbl_Message = { Text = $"{faculty.Message}" },
                title_color = { Background = new SolidColorBrush(Color.FromRgb(255, 0, 0)) },
                Topmost = true
            };
            result.ShowDialog();
        }

        public void RemoveClass(int id)
        {
            var clss = _facultyService.RemoveClass(id);
            if (!clss.IsSuccessed)
            {
                var mess = new MessageDialog()
                {
                    tbl_Title = { Text = $"Lỗi xóa" },
                    tbl_Message = { Text = $"{clss.Message}" },
                    title_color = { Background = new SolidColorBrush(Color.FromRgb(255, 0, 0)) },
                    Topmost = true
                };
                mess.ShowDialog();
            }

            var result = new MessageDialog()
            {
                tbl_Title = { Text = $"Xóa thành công" },
                tbl_Message = { Text = $"{clss.Message}" },
                title_color = { Background = new SolidColorBrush(Color.FromRgb(255, 0, 0)) },
                Topmost = true
            };
            result.ShowDialog();
        }

        public void UnlockClass(int id, bool isLock)
        {
            var result = _facultyService.UnlockClass(id, isLock);
            var mess = new MessageDialog()
            {
                tbl_Title = { Text = $"{result.Message}" },
                tbl_Message = { Text = $"{result.Message}" },
                title_color = { Background = new SolidColorBrush(Color.FromRgb(255, 0, 0)) },
                Topmost = true
            };
            mess.ShowDialog();
        }

    }
}
