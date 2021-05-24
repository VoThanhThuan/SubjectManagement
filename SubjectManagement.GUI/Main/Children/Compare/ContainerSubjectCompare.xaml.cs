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
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using SubjectManagement.Data.Entities;
using SubjectManagement.GUI.Controller;
using SubjectManagement.GUI.Dialog;

namespace SubjectManagement.GUI.Main.Children.Compare
{
    /// <summary>
    /// Interaction logic for ContainerSubjectCompare.xaml
    /// </summary>
    public partial class ContainerSubjectCompare : UserControl
    {
        public ContainerSubjectCompare(Class _class, Class _classCompare)
        {
            InitializeComponent();
            _Class = _class;
            _ClassCompare = _classCompare;
            LoadTable();
        }

        private Class _Class { get; init; }
        private Class _ClassCompare { get; init; }

        private bool _mode = false;

        private void LoadTable()
        {
            RenderBody.Children.Clear();
            if (_mode)
            {
                var compareUC = new SubjectCompare2TableUC(_Class, _ClassCompare);
                RenderBody.Children.Add(compareUC);
                _mode = false;
            }
            else
            {
                var compareUC = new SubjectCompareUC(_Class, _ClassCompare);
                RenderBody.Children.Add(compareUC);
                _mode = true;
            }
        }

        private void Btn_Export_OnClick(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel (*.xlsx)|*.xlsx|JSON (*.json)|*.json";
            saveFileDialog.Title = "Xuất file";
            saveFileDialog.FileName = DateTime.Now.ToString("yyyyMMddTHHmmss");
            if (saveFileDialog.ShowDialog() != true) return;
            var filename = saveFileDialog.FileName;
            var export = new ExportController(_Class, _ClassCompare);

            switch (saveFileDialog.FilterIndex)
            {
                case 1:
                    export.ExportCompareForExcel(filename);
                    break;
                case 2:
                    export.ExportCompareForJson(filename);
                    break;
            }
            MyCommonDialog.MessageDialog($"Đã lưu vào {saveFileDialog.FileName}");
        }

        private void Btn_ViewTwoInOne_OnClick(object sender, RoutedEventArgs e)
        {
            RenderBody.Children.Clear();
            var compareUC = new SubjectCompareUC(_Class, _ClassCompare);
            RenderBody.Children.Add(compareUC);
        }

        private void Btn_ChangeInTwo_OnClick(object sender, RoutedEventArgs e)
        {
            RenderBody.Children.Clear();
            var compareUC = new SubjectCompare2TableUC(_Class, _ClassCompare);
            RenderBody.Children.Add(compareUC);
        }

        private void Btn_ViewOneClass_OnClick(object sender, RoutedEventArgs e)
        {
            RenderBody.Children.Clear();
            var compareUC = new SubjectCompareOnlyClass(_Class, _ClassCompare);
            RenderBody.Children.Add(compareUC);
        }

    }
}