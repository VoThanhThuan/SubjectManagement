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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SubjectManagement.Application.SubjectApp;
using SubjectManagement.Common.Dialog;
using SubjectManagement.Common.Result;
using SubjectManagement.Data.Entities;
using SubjectManagement.GUI.Controller;

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

        public Class _Class { get; init; }
        private Grid _renderBody;
        private void LoadListSubject()
        {
            var load = new LoadListController(_Class);
            load.LoadList(_renderBody, _g_loading);
        }

        private void btn_AddOpen_Click(object sender, RoutedEventArgs e)
        {
            var addSubject = new AddSubjectWindow(_Class);
            addSubject.Show();
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
    }
}
