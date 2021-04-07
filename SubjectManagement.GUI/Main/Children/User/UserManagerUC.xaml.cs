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
using SubjectManagement.Common.Dialog;
using SubjectManagement.Common.Result;
using SubjectManagement.Data.Entities;
using SubjectManagement.GUI.Controller;

namespace SubjectManagement.GUI.Main.Children.User
{
    /// <summary>
    /// Interaction logic for UserManagerUC.xaml
    /// </summary>
    public partial class UserManagerUC : UserControl
    {
        public UserManagerUC(Class _class)
        {
            InitializeComponent();
            _Class = _class;
            LoadListUser();
        }

        private Class _Class { get; set; }

        private void LoadListUser()
        {
            var list = new UserController(_Class);
            dg_ListUser.ItemsSource = list.GetListUser();
        }

        private void btn_AddOpen_Click(object sender, RoutedEventArgs e)
        {
            var viewAdd = new AddUserWindow(_Class);
            viewAdd.Show();
        }

        private void Btn_Edit_OnClick(object sender, RoutedEventArgs e)
        {
            if (dg_ListUser.SelectedIndex < 0) return;
            var viewEdit = new AddUserWindow(_Class, true, ((AppUser)dg_ListUser.SelectedValue));
            viewEdit.Show();
        }

        private void Btn_Remove_OnClick(object sender, RoutedEventArgs e)
        {
            if(dg_ListUser.SelectedIndex < 0) return;
            var remove = new UserController(_Class);
            var user = (AppUser)dg_ListUser.SelectedValue;
            remove.RemoveUser(user.ID);
        }

        private void Btn_Reload_OnClick(object sender, RoutedEventArgs e)
        {
            LoadListUser();
        }
    }
}