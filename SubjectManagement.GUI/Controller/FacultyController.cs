using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using SubjectManagement.Application.FacultyApp;
using SubjectManagement.Data;

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
            await Db.Context.Faculties.LoadAsync();
            await Db.Context.Classes.LoadAsync();
            await Db.Context.ClassInFaculties.LoadAsync();
        }

        public void GetFaculty(ComboBox cbb)
        {
            cbb.ItemsSource = _facultyService.GetFaculty();
            cbb.DisplayMemberPath = "Name";
        }

        public void GetClass(ComboBox cbb, int IDFaculty)
        {
            cbb.ItemsSource = _facultyService.GetClass(IDFaculty);
            cbb.DisplayMemberPath = "Name";
        }

    }
}
