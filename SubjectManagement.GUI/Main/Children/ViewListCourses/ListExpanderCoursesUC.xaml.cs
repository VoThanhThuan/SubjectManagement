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
using SubjectManagement.Common.Dialog;
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
        public ListExpanderCoursesUC(Grid renderBody)
        {
            InitializeComponent();
            _renderBody = renderBody;
        }

        public Grid _g_loading;

        private Window _mainWindow { get; set; }

        public Class _Class { get; init; }
        private Grid _renderBody;
        private void LoadListSubject()
        {
            var load = new LoadListController(_Class);
            load.LoadList(_renderBody, _g_loading);
        }

        private void btn_AddOpen_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddNewOrOldDialog(_Class){Owner = Window.GetWindow(this) };
            dialog.ShowDialog();
            switch (dialog.IsCopy)
            {
                case true:
                {
                    var copy = new SubjectController(_Class);
                    copy.CopyListSubject(dialog.IdClassNew, _Class.ID);
                    break;
                }
                case false:
                {
                    var addSubject = new AddSubjectWindow(_Class);
                    addSubject.Show();
                    break;
                }
            }
        }

        private void Btn_Reload_OnClick(object sender, RoutedEventArgs e)
        {
            LoadListSubject();
        }

        private void Btn_Edit_OnClick(object sender, RoutedEventArgs e)
        {
            var prompt = new PromptDialog()
            {
                tbl_Title = {Text = "Nhập mã môn học cần sửa."}
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
                    export.ExportForExcel(filename);
                    break;
                case 2:
                    export.ExportForJSON(filename);
                    break;
            }


        }
    }
}
