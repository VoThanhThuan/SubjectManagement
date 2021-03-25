﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SubjectManagement.Data.Entities;
using SubjectManagement.GUI.Controller;
using SubjectManagement.ViewModels.Subject;

namespace SubjectManagement.GUI.Main
{
    /// <summary>
    /// Interaction logic for AddSubjectWindow.xaml
    /// </summary>
    public partial class AddSubjectWindow : Window
    {
        public AddSubjectWindow()
        {
            InitializeComponent();
            loadCombobox();
        }

        private void loadCombobox()
        {
            var load = new SubjectController();
            load.LoadCombobox(cbb_CoursesGroup);
        }

        private SubjectRequest InfoSubject()
        {

            var subject = new SubjectRequest()
            {
                ID = Guid.NewGuid(),
                CourseCode = tbx_CourseCode.Text,
                Name = tbx_Name.Text,
                Credit = Convert.ToInt32(tbx_Credit.Text),
                TypeCourse = chk_TypeCourse.IsChecked ?? true,
                NumberOfTheory = Convert.ToInt32(tbx_NumberOfTheory.Text),
                NumberOfPractice = Convert.ToInt32(tbx_NumberOfPractice.Text),
                Prerequisite = string.IsNullOrEmpty(tbx_Prerequisite.Text) ? 0 :  Convert.ToInt32(tbx_Prerequisite.Text),
                LearnFirst = string.IsNullOrEmpty(tbx_LearnFirst.Text) ? 0 : Convert.ToInt32(tbx_LearnFirst.Text),
                Parallel = string.IsNullOrEmpty(tbx_Parallel.Text) ? 0 : Convert.ToInt32(tbx_Parallel.Text),
                IsOffical = chk_IsOffical.IsChecked ?? true,
                Details = tbx_Details.Text,
                IDKnowledgeGroup = ((KnowledgeGroup)cbb_CoursesGroup.SelectedValue).ID
            };
            return subject;
        }

        private void Btn_AddCategory_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Btn_EditCategory_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Btn_RemoveCategory_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void btn_AddProduct_Click(object sender, RoutedEventArgs e)
        {
            if (cbb_CoursesGroup.SelectedIndex < -1) return;
            var subject = InfoSubject();
            var add = new SubjectController();
            add.AddSubject(subject);
            
        }
    }
}