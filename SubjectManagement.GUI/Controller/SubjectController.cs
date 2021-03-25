using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using SubjectManagement.Application.SubjectApp;
using SubjectManagement.Data.Entities;
using SubjectManagement.ViewModels.Subject;

namespace SubjectManagement.GUI.Controller
{
    public class SubjectController
    {
        public SubjectController()
        {
            var subjectService = new SubjectService();
            _subjectService = subjectService;
        }

        private readonly ISubjectService _subjectService;

        public async void LoadCombobox(ComboBox cbb)
        {
            var value = await _subjectService.LoadKnowledgeGroup();
            cbb.ItemsSource = value;
            cbb.DisplayMemberPath = "Name";
        }

        public async void AddSubject(SubjectRequest request)
        {
            await _subjectService.AddSubject(request);
        }

        public async void EditSubject(SubjectRequest request)
        {
            await _subjectService.AddSubject(request);
        }

    }
}
