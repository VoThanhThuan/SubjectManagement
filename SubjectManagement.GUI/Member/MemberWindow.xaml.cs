using SubjectManagement.Data.Entities;
using SubjectManagement.GUI.Controller;
using System;
using System.Windows;
using System.Windows.Controls;
using SubjectManagement.Common.Result;
using SubjectManagement.GUI.Dialog;
using SubjectManagement.GUI.Main.Children.Compare;
using SubjectManagement.ViewModels.System.Users;

namespace SubjectManagement.GUI.Member
{
    /// <summary>
    /// Interaction logic for MemberWindow.xaml
    /// </summary>
    public partial class MemberWindow : Window
    {
        public MemberWindow(InfoLogin user)
        {
            InitializeComponent();
            loadCombobox();
        }

        private void loadCombobox()
        {
            var clss = new FacultyController();
            clss.GetFaculty(cbb_Faculty);
        }
        private int _IdFaculty { get; set; }
        private Class _Class { get; set; }

        private void Cbb_class_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbb_class.SelectedIndex < 0) return;
            _Class = (Class)cbb_class.SelectedValue;
        }

        private void Btn_ViewList_OnClick(object sender, RoutedEventArgs e)
        {
            if (cbb_class.SelectedIndex < 0) return;
            var viewSubject = new ViewSubjectClass(_Class);
            viewSubject.ShowDialog();
        }

        private void Btn_Compare_OnClick(object sender, RoutedEventArgs e)
        {
            if (cbb_class.SelectedIndex < 0) return;
            var compare = new CompareDialog(_Class, _IdFaculty);
            compare.ShowDialog();
            if (compare.DialogResult != true) return;
            var containerCompare = new ContainerSubjectCompare(_Class, compare._ClassCompare, g_loading);
            var view = new ViewCompare();
            view.RenderBody.Children.Clear();
            view.RenderBody.Children.Add(containerCompare);
            view.ShowDialog();
        }

        private void Btn_AlternativeSubject_OnClick(object sender, RoutedEventArgs e)
        {
            if (cbb_class.SelectedIndex < 0) return;

            var prompt = new PromptDialog();
            prompt.ShowDialog();
            if (prompt.DialogResult != MyDialogResult.Result.Ok) return;
            var subject = new SubjectController(_Class);
            var code = subject.FindSubject(prompt.tbx_Value.Text);
            if (code is null)
                return;
            var alter = new ViewAlternativeSubject(_Class, code);
            alter.ShowDialog();
        }

        private void Cbb_Faculty_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var clss = new FacultyController();
            _IdFaculty = ((Faculty) cbb_Faculty.SelectedValue).ID;
            clss.GetClassInFaculty(cbb_class, _IdFaculty);
        }
    }
}
