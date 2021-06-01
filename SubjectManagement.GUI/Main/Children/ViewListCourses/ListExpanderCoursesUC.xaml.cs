using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using SubjectManagement.Application.SubjectApp;
using SubjectManagement.Common.Result;
using SubjectManagement.Data.Entities;
using SubjectManagement.GUI.Controller;
using SubjectManagement.GUI.Dialog;

namespace SubjectManagement.GUI.Main.Children.ViewListCourses
{
    /// <summary>
    /// Interaction logic for ViewListCoursesUC.xaml
    /// </summary>
    public partial class ListExpanderCoursesUC : UserControl
    {
        public ListExpanderCoursesUC(Grid renderBody, int idFaculty, Class clss)
        {
            InitializeComponent();
            _IdFaculty = idFaculty;
            _Class = clss;
            _renderBody = renderBody;
        }

        public Grid _g_loading;

        private Window _mainWindow { get; set; }
        private int _IdFaculty { get; init; }
        private Class _Class { get; init; }

        private Grid _renderBody;

        private void LoadListSubject()
        {
            _g_loading.Visibility = Visibility.Visible;
            _g_loading.UpdateLayout();

            var load = new LoadListController(_Class, _IdFaculty);
            load.LoadList(_renderBody, _g_loading);

            _g_loading.Visibility = Visibility.Hidden;

        }

        private void LoadListSubjectInSemester()
        {
            _g_loading.Visibility = Visibility.Visible;
            _g_loading.UpdateLayout();

            var load = new LoadListController(_Class, _IdFaculty);
            load.LoadListInSemester(_renderBody, _g_loading);

            _g_loading.Visibility = Visibility.Hidden;
        }

        private void btn_AddOpen_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddNewOrOldDialog(_Class, _IdFaculty) { Owner = Window.GetWindow(this) };
            dialog.ShowDialog();
            switch (dialog.IsCopy)
            {
                case true:
                    {
                        var mess = new MessageDialog()
                        {
                            tbl_Title = { Text = $"Lưu ý nè bà con ơi!!" },
                            tbl_Message = { Text = $"Hành động copy này sẽ xóa toàn bộ dữ liệu cũ á!" },
                            title_color = { Background = new SolidColorBrush(Color.FromRgb(255, 0, 0)) },
                            Topmost = true
                        };
                        mess.ShowDialog();
                        if (mess.DialogResult != MyDialogResult.Result.Ok) return;
                        var copy = new SubjectController(_Class);
                        copy.CopyListSubject(dialog.IdClassNew, _Class.ID);
                        break;
                    }
                case false:
                    {
                        if (dialog.IsAddOneSubject)
                        {
                            var addSubject = new AddSubjectWindow(_Class);
                            addSubject.Show();
                        }
                        else
                        {
                            var addSubject = new AddListSubjectWindow(_Class);
                            addSubject.Show();
                        }

                        break;
                    }
            }
        }

        private void Btn_Reload_OnClick(object sender, RoutedEventArgs e)
        {
            if (_isViewSemester == true)
            {
                LoadListSubjectInSemester();
            }
            else
            {
                LoadListSubject();
            }
        }

        private void Btn_Edit_OnClick(object sender, RoutedEventArgs e)
        {
            var prompt = new PromptDialog()
            {
                tbl_Title = { Text = "Nhập mã môn học cần sửa." }
            };
            prompt.ShowDialog();

            if (prompt.DialogResult != MyDialogResult.Result.Ok) return;
            var edit = new SubjectController(_Class);
            edit.EditWindow(prompt.tbx_Value.Text);

        }

        private void Btn_Remove_OnClick(object sender, RoutedEventArgs e)
        {
            var prompt = new PromptDialog()
            {
                tbl_Title = { Text = "Nhập mã môn học cần xóa." }
            };
            prompt.ShowDialog();

            if (prompt.DialogResult != MyDialogResult.Result.Ok) return;
            var edit = new SubjectController(_Class);
            edit.RemoveSubject(prompt.tbx_Value.Text);
        }

        private void Btn_Export_OnClick(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel (*.xlsx)|*.xlsx|JSON (*.json)|*.json";
            saveFileDialog.Title = "Xuất file";
            saveFileDialog.FileName = DateTime.Now.ToString("yyyyMMddTHHmmss");
            if (saveFileDialog.ShowDialog() != true) return;
            var filename = saveFileDialog.FileName;
            var export = new ExportController(_Class);

            switch (saveFileDialog.FilterIndex)
            {
                case 1:
                    export.ExportSubjectForExcel(filename);
                    break;
                case 2:
                    export.ExportSubjectForJSON(filename);
                    break;
            }
            MyCommonDialog.MessageDialog($"Đã lưu vào {saveFileDialog.FileName}", Colors.DeepSkyBlue);
        }

        private void UIElement_OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (sender is not ScrollViewer scv) return;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        public bool _isViewSemester { get; set; } = false;

        private void Btn_ChangeView_OnClick(object sender, RoutedEventArgs e)
        {
            if (_isViewSemester == false)
            {
                LoadListSubjectInSemester();
                _isViewSemester = true;
            }
            else
            {
                LoadListSubject();
                _isViewSemester = false;
            }
        }
    }
}
