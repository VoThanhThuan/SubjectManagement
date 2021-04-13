using SubjectManagement.Data.Entities;
using SubjectManagement.GUI.Controller;
using System;
using System.Windows;
using System.Windows.Controls;
using SubjectManagement.Common.Dialog;
using SubjectManagement.Common.Result;
using SubjectManagement.GUI.Dialog;
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
            clss.GetClass(cbb_class);
        }

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
            var compare = new CompareDialog(_Class) { Owner = System.Windows.Application.Current.MainWindow };
            compare.ShowDialog();
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

    }
}
