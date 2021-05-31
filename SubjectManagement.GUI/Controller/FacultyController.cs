using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.EntityFrameworkCore;
using SubjectManagement.Application.FacultyApp;
using SubjectManagement.Common.Result;
using SubjectManagement.Data;
using SubjectManagement.Data.Entities;
using SubjectManagement.GUI.Dialog;

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

        public async void GetClassInFaculty(ComboBox cbb, int idFaculty)
        {
            cbb.ItemsSource = await _facultyService.GetClassInFaculty(idFaculty);
            cbb.DisplayMemberPath = "CodeClass";
        }

        public async void GetClassDifferentClass(ComboBox cbb, int idFaculty, Class currentClass)
        {
            cbb.ItemsSource = await _facultyService.GetClassDifferentClass(idFaculty, currentClass);
            cbb.DisplayMemberPath = "CodeClass";
        }

        public async void GetDifferentClassNewer(ComboBox cbb, int idFaculty, Class currentClass)
        {
            cbb.ItemsSource = await _facultyService.GetDifferentClassNewer(idFaculty, currentClass);
            cbb.DisplayMemberPath = "CodeClass";
        }
        public async void GetDifferentClassOlder(ComboBox cbb, int idFaculty, Class currentClass)
        {
            cbb.ItemsSource = await _facultyService.GetDifferentClassOlder(idFaculty, currentClass);
            cbb.DisplayMemberPath = "CodeClass";
        }

        public async void GetClass(ComboBox cbb)
        {
            cbb.ItemsSource = await _facultyService.GetClass();
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
            MyCommonDialog.MessageDialog("Lưu thành công", $"{clss.Message}");

        }

        public void RemoveFaculty(int id)
        {
            var faculty = _facultyService.RemoveFaculty(id);
            if (!faculty.IsSuccessed)
            {
                MyCommonDialog.MessageDialog("Lỗi xóa", $"{faculty.Message}");

            }
            MyCommonDialog.MessageDialog("Xóa thành công", $"{faculty.Message}");
        }

        public void RemoveClass(int id)
        {
            var clss = _facultyService.RemoveClass(id);
            if (!clss.IsSuccessed)
            {
                MyCommonDialog.MessageDialog("Lỗi xóa", $"{clss.Message}");
            }
            MyCommonDialog.MessageDialog("Xóa thành công", $"{clss.Message}");

        }

        public void UnlockClass(int id, bool isLock)
        {
            var result = _facultyService.UnlockClass(id, isLock);
            MyCommonDialog.MessageDialog("Thông báo", $"{result.Message}");
        }

    }
}
