﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SubjectManagement.Common.Result;
using SubjectManagement.Data.Entities;
using SubjectManagement.GUI.Controller;
using SubjectManagement.GUI.Dialog;
using SubjectManagement.ViewModels.Subject;

namespace SubjectManagement.GUI.Main
{
    /// <summary>
    /// Interaction logic for AddSubjectWindow.xaml
    /// </summary>
    public partial class AddListSubjectWindow : Window
    {
        public AddListSubjectWindow(Class _class)
        {
            InitializeComponent();
            loadCombobox();
            _Class = _class;
        }


        public bool IsEdit { get; set; } = false;

        public Guid _IdKnowledgeGroupEdit = Guid.Empty;
        public int _IdSemesterEdit = 0;

        public Hashtable OldValue { get; set; }

        private Class _Class { get; init; }

        private List<SubjectRequest> _listSubject = new();

        #region Methods

        private static readonly Regex _regex = new Regex("[^0-9.,]+"); //regex that matches disallowed text

        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

        private void loadCombobox()
        {
            var load = new SubjectController(_Class);
            cbb_CoursesGroup.ItemsSource = load.LoadCombobox(); ;
            cbb_CoursesGroup.DisplayMemberPath = "Name";
        }

        public void SetValueCombobox()
        {
            if (IsEdit)
            {
                foreach (var item in cbb_CoursesGroup.Items)
                {
                    if (((KnowledgeGroup)item).ID == _IdKnowledgeGroupEdit)
                        cbb_CoursesGroup.SelectedValue = item;
                }

                cbb_Semester.SelectedIndex = _IdSemesterEdit - 1;
            }
        }

        private SubjectRequest InfoSubject()
        {
            try
            {
                var subject = new SubjectRequest()
                {
                    ID = IsEdit is false ? Guid.NewGuid() : (Guid)OldValue["ID"],
                    CourseCode = tbx_CourseCode.Text,
                    Name = tbx_Name.Text,
                    Credit = string.IsNullOrEmpty(tbx_Credit.Text) ? 0 : Convert.ToInt32(tbx_Credit.Text),
                    TypeCourse = chk_TypeCourse.IsChecked ?? true,
                    NumberOfTheory = string.IsNullOrEmpty(tbx_NumberOfTheory.Text) ? 0 : Convert.ToInt32(tbx_NumberOfTheory.Text),
                    NumberOfPractice = string.IsNullOrEmpty(tbx_NumberOfPractice.Text) ? 0 : Convert.ToInt32(tbx_NumberOfPractice.Text),
                    Prerequisite = string.IsNullOrEmpty(tbx_Prerequisite.Text) ? 0 : Convert.ToInt32(tbx_Prerequisite.Text),
                    LearnFirst = string.IsNullOrEmpty(tbx_LearnFirst.Text) ? 0 : Convert.ToInt32(tbx_LearnFirst.Text),
                    Parallel = string.IsNullOrEmpty(tbx_Parallel.Text) ? 0 : Convert.ToInt32(tbx_Parallel.Text),
                    Semester = cbb_Semester.SelectedIndex+1,
                    Details = tbx_Details.Text,

                    IDKnowledgeGroup = ((KnowledgeGroup)cbb_CoursesGroup.SelectedValue).ID,
                    IDKnowledgeGroupOld = (Guid?)OldValue?["IDKnowledgeGroupOld"] ?? ((KnowledgeGroup)cbb_CoursesGroup.SelectedValue).ID,
                    IdClass = _Class.ID
                };
                return subject;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }

        }
        #endregion
        private void Btn_AddCategory_OnClick(object sender, RoutedEventArgs e)
        {
            var prompt = new PromptDialog() { tbl_Title = { Text = "Nhập tên nhóm học phần" } };
            prompt.ShowDialog();
            if (prompt.DialogResult != MyDialogResult.Result.Ok) return;
            var add = new KnowledgeGroupController(_Class);
            add.AddKnowledge(prompt.tbx_Value.Text);
            loadCombobox();
        }

        private void Btn_EditCategory_OnClick(object sender, RoutedEventArgs e)
        {
            if (cbb_CoursesGroup.SelectedIndex < 0) return;
            var group = (KnowledgeGroup)cbb_CoursesGroup.SelectedValue;
            var prompt = new PromptDialog() { tbl_Title = { Text = "Nhập tên nhóm học phần" }, tbx_Value = { Text = $"{group.Name}" } };
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

            if (mess.DialogResult != MyDialogResult.Result.Ok) return;

            var remove = new KnowledgeGroupController(_Class);
            remove.RemoveKnowledge(group.ID);
            loadCombobox();
        }

        private void btn_AddProduct_Click(object sender, RoutedEventArgs e)
        {
            if (cbb_CoursesGroup.SelectedIndex < 0) return;
            if (cbb_Semester.SelectedIndex < 0) return;
            var subject = InfoSubject();
            if (subject == null)
            {
                var mess = new MessageDialog()
                {
                    tbl_Title = { Text = $"Dữ liệu bạn nhập có lỗi" },
                    tbl_Message = { Text = $"Bạn hãy nhập lại dữ liệu." },
                    title_color = { Background = new SolidColorBrush(Color.FromRgb(255, 0, 0)) },
                    Topmost = true
                };
                mess.ShowDialog();
                return;
            }

            var checkExist = _listSubject.FirstOrDefault(x => x.CourseCode == subject.CourseCode);
            if (checkExist != null)
            {
                var mess = new MessageDialog()
                {
                    tbl_Title = { Text = $"Lỗi thêm" },
                    tbl_Message = { Text = $"Trùng mã môn rồi bạn ơi!!!" },
                    title_color = { Background = new SolidColorBrush(Color.FromRgb(255, 0, 0)) },
                    Topmost = true
                };
                mess.ShowDialog();
                return;
            }
            _listSubject.Add(subject);
            dg_PreviewAdd.ItemsSource = null;
            dg_PreviewAdd.ItemsSource = _listSubject;
            //var add = new SubjectController(_Class);
            //if (IsEdit is not true)
            //    add.AddSubject(subject);
            //else
            //    add.EditSubject(subject);

            //this.Close();
        }

        private void Btn_CLose_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void chk_TypeCourse_Click(object sender, RoutedEventArgs e)
        {
            tbl_TypeCourse.Text = chk_TypeCourse.IsChecked == true ? "Bắt buộc" : "Tự chọn";
        }

        private void tbx_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void tbx_TextChanged(object sender, TextChangedEventArgs e)
        {
            var txt = ((TextBox)sender);
            if (string.IsNullOrEmpty(txt.Text)) return;
            if (IsTextAllowed(txt.Text)) return;
            var mess = new MessageDialog()
            {
                tbl_Title = { Text = $"Ôi bạn tui ơi!!!" },
                tbl_Message = { Text = $"Chổ này chỉ để nhập số thôi!!" },
                title_color = { Background = new SolidColorBrush(Color.FromRgb(255, 0, 0)) },
                Topmost = true
            };
            mess.ShowDialog();
            txt.Text = "";
        }

        private void Btn_Accept_OnClick(object sender, RoutedEventArgs e)
        {
            var add = new SubjectController(_Class);
            foreach (var subject in _listSubject)
            {
                if (IsEdit is not true)
                    add.AddSubject(subject);
                else
                    add.EditSubject(subject);
            }

            this.Close();
        }

        private void Btn_Remove_OnClick(object sender, RoutedEventArgs e)
        {
            if(dg_PreviewAdd.SelectedIndex < 0) return;
            var subject = (SubjectRequest)dg_PreviewAdd.SelectedValue;
            _listSubject.Remove(subject);
            dg_PreviewAdd.ItemsSource = null;
            dg_PreviewAdd.ItemsSource = _listSubject;
        }

    }
}