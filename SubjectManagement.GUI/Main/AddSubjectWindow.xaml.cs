using System;
using System.Collections;
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
using SubjectManagement.Common.Dialog;
using SubjectManagement.Common.Result;
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
        public AddSubjectWindow(Class _class)
        {
            InitializeComponent();
            loadCombobox();
            _Class = _class;
        }

        private void loadCombobox()
        {
            var load = new SubjectController(_Class);
            load.LoadCombobox(cbb_CoursesGroup);
        }

        public bool IsEdit { get; set; } = false;
        public Hashtable OldValue { get; set; }

        private Class _Class { get; init; }

        private SubjectRequest InfoSubject()
        {

            var subject = new SubjectRequest()
            {
                ID = IsEdit is false ? Guid.NewGuid() : (Guid)OldValue["ID"],
                CourseCode = tbx_CourseCode.Text,
                Name = tbx_Name.Text,
                Credit = string.IsNullOrEmpty(tbx_Credit.Text) ? 0 : Convert.ToInt32(tbx_Credit.Text),
                TypeCourse = chk_TypeCourse.IsChecked ?? true,
                NumberOfTheory = Convert.ToInt32(tbx_NumberOfTheory.Text),
                NumberOfPractice = Convert.ToInt32(tbx_NumberOfPractice.Text),
                Prerequisite = string.IsNullOrEmpty(tbx_Prerequisite.Text) ? 0 :  Convert.ToInt32(tbx_Prerequisite.Text),
                LearnFirst = string.IsNullOrEmpty(tbx_LearnFirst.Text) ? 0 : Convert.ToInt32(tbx_LearnFirst.Text),
                Parallel = string.IsNullOrEmpty(tbx_Parallel.Text) ? 0 : Convert.ToInt32(tbx_Parallel.Text),
                Details = tbx_Details.Text,

                IDKnowledgeGroup = ((KnowledgeGroup)cbb_CoursesGroup.SelectedValue).ID,
                IDKnowledgeGroupOld = (Guid?) OldValue?["IDKnowledgeGroupOld"] ?? Guid.Empty,
                IdClass = _Class.ID 
            };
            return subject;
        }

        private void Btn_AddCategory_OnClick(object sender, RoutedEventArgs e)
        {
            var prompt = new PromptDialog() { tbl_Title = {Text = "Nhập tên nhóm học phần"}};
            prompt.ShowDialog();
            if(prompt.DialogResult != MyDialogResult.Result.Ok) return;
            var add = new KnowledgeGroupController(_Class);
            add.AddKnowledge(prompt.tbx_Value.Text);
            loadCombobox();
        }

        private void Btn_EditCategory_OnClick(object sender, RoutedEventArgs e)
        {
            if(cbb_CoursesGroup.SelectedIndex < 0) return;
            var group = (KnowledgeGroup)cbb_CoursesGroup.SelectedValue;
            var prompt = new PromptDialog() { tbl_Title = { Text = "Nhập tên nhóm học phần" }, tbx_Value = {Text = $"{group.Name}"}};
            prompt.ShowDialog();
            if (prompt.DialogResult != MyDialogResult.Result.Ok) return;
            var edit = new KnowledgeGroupController(_Class);
            
            edit.EditKnowledge(group.ID, prompt.tbx_Value.Text);
            loadCombobox();
        }

        private void Btn_RemoveCategory_OnClick(object sender, RoutedEventArgs e)
        {
            if (cbb_CoursesGroup.SelectedIndex < 0) return;
            var group = (KnowledgeGroup)cbb_CoursesGroup.SelectedValue;

            var mess = new MessageDialog()
            {
                tbl_Title = { Text = $"Lỗi xóa" },
                tbl_Message = { Text = $"Bạn thật sự muôn xóa môn {group.Name}" },
                title_color = { Background = new SolidColorBrush(Color.FromRgb(255, 0, 0)) },
                Topmost = true
            };
            mess.ShowDialog();

            if(mess.DialogResult != MyDialogResult.Result.Ok) return;

            var remove = new KnowledgeGroupController(_Class);
            remove.RemoveKnowledge(group.ID);
            loadCombobox();
        }

        private void btn_AddProduct_Click(object sender, RoutedEventArgs e)
        {
            if (cbb_CoursesGroup.SelectedIndex < -1) return;
            var subject = InfoSubject();
            var add = new SubjectController(_Class);
            if(IsEdit is not true)
                add.AddSubject(subject);
            else
                add.EditSubject(subject);

            this.Close();
        }

        private void Btn_CLose_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
