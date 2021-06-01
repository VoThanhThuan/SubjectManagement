using System;
using System.Threading;
using System.Threading.Tasks;
using Dragablz;
using SubjectManagement.Common.Result;
using SubjectManagement.Data.Entities;
using SubjectManagement.GUI.Controller;
using SubjectManagement.GUI.Dialog;
using SubjectManagement.GUI.Main.Children.Alternative;
using SubjectManagement.GUI.Main.Children.Compare;
using SubjectManagement.GUI.Main.Children.Semester;
using SubjectManagement.GUI.Main.Children.User;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace SubjectManagement.GUI.Main.Children.Common
{
    /// <summary>
    /// Interaction logic for NewTabUC.xaml
    /// </summary>
    public partial class NewTabUC : UserControl
    {
        public NewTabUC(TabItem Tab, Window mainWindow, Grid loading, TextBlock title)
        {
            InitializeComponent();

            _LoadingFull = loading;
            loading.Visibility = Visibility.Visible;

            _MainWindow = mainWindow;
            _TabItem = Tab;
            _title = title;

            var faculty = new FacultyDialog() { Owner = mainWindow };
            faculty.ShowDialog();
            _Class = faculty._Class;
            _IdFaculty = faculty._IdFaculty;
            tbl_Class.Text = $"{_Class.Name} - {_Class.CodeClass}";
            _title.Text = $"{_Class.Name} - {_Class.CodeClass}";
            _TabItem.Tag = $"{_Class.Name} - {_Class.CodeClass}";

            loading.Visibility = Visibility.Hidden;
        }

        private TextBlock _title { get; init; }

        private TabItem _TabItem { get; init; }
        private int _IdFaculty { get; init; }
        public Class _Class { get; init; }
        private Window _MainWindow { get; set; }
        private Grid _LoadingFull { get; init; }



        private void Btn_ViewList_OnClick(object sender, RoutedEventArgs e)
        {
            g_loading.Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(
                () =>
                {
                    g_loading.Visibility = Visibility.Visible;
                })).Wait();

            _TabItem.Header = "View List";
            _TabItem.Tag = $"{_Class.Name} - {_Class.CodeClass} - Danh sách môn học";
            _title.Text = $"{_Class.Name} - {_Class.CodeClass} - Danh sách môn học";
            var listCourses = new LoadListController(_Class, _IdFaculty) { _Class = _Class };
            listCourses.LoadList(MainBody, g_loading);
        }

        private void Btn_AddSemester_OnClick(object sender, RoutedEventArgs e)
        {
            _TabItem.Header = "Tùy Chỉnh Học Kỳ";
            _TabItem.Tag = $"{_Class.Name} - {_Class.CodeClass} - Học kỳ";
            _title.Text = $"{_Class.Name} - {_Class.CodeClass} - Học kỳ";
            var semester = new AddSemesterUC(_Class);
            MainBody.Children.Clear();
            MainBody.Children.Add(semester);

        }

        private void Btn_Compare_OnClick(object sender, RoutedEventArgs e)
        {

            var compare = new CompareDialog(_Class, _IdFaculty) { Owner = _MainWindow };
            compare.ShowDialog();
            if (compare.DialogResult != true) return;
            _TabItem.Header = "So Sánh";
            _TabItem.Tag = $"{_Class.Name} - {_Class.CodeClass} - So sánh môn học";
            _title.Text = $"{_Class.Name} - {_Class.CodeClass} - So sánh môn học";
            ////var compareUC = new SubjectCompareUC(_Class, compare._ClassCompare);
            //var compareUC = new SubjectCompare2TableUC(_Class, compare._ClassCompare);
            //MainBody.Children.Clear();
            //MainBody.Children.Add(compareUC);
            var containerCompare = new ContainerSubjectCompare(_Class, compare._ClassCompare, g_loading);
            MainBody.Children.Clear();
            MainBody.Children.Add(containerCompare);

        }

        private void Btn_AlternativeSubject_OnClick(object sender, RoutedEventArgs e)
        {
            _TabItem.Header = "Học Phần Thay Thế";
            _TabItem.Tag = $"{_Class.Name} - {_Class.CodeClass} - Học phần thay thế";
            _title.Text = $"{_Class.Name} - {_Class.CodeClass} - Học phần thay thế";
            var alter = new AlternativeSubjectUC(_Class, _IdFaculty);
            MainBody.Children.Clear();
            MainBody.Children.Add(alter);

        }

        private void Btn_UserManager_OnClick(object sender, RoutedEventArgs e)
        {

            _TabItem.Header = "Quản Lý Người Dùng";
            _TabItem.Tag = $"{_Class.Name} - {_Class.CodeClass} - Quản lý người dùng";
            _title.Text = $"{_Class.Name} - {_Class.CodeClass} - Quản lý người dùng";
            var user = new UserManagerUC();
            MainBody.Children.Clear();
            MainBody.Children.Add(user);
        }

        private void Btn_Setting_OnClick(object sender, RoutedEventArgs e)
        {
            _LoadingFull.Visibility = Visibility.Visible;

            new SettingController().OpenSetting();

            _LoadingFull.Visibility = Visibility.Hidden;
        }

    }
}
