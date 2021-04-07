using System;
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
using Microsoft.Win32;
using SubjectManagement.Common.Dialog;
using SubjectManagement.Data.Entities;
using SubjectManagement.GUI.Controller;
using SubjectManagement.ViewModels.System.Users;

namespace SubjectManagement.GUI.Main
{
    /// <summary>
    /// Interaction logic for AddUser.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {
        public AddUserWindow(Class _class, bool isEdit = false, AppUser user = null)
        {
            InitializeComponent();
            _Class = _class;
            _isEdit = isEdit;
            LoadCombobox();
            if (isEdit is not true) return;
            _UserEdit = user;
            btn_AddUser.Content = "Sửa";
            Fillinfor();
        }

        private Class _Class { get; set; }

        private bool _isEdit { get; init; }

        private string _pathImage = null;

        public AppUser _UserEdit { get; set; }


        private static readonly Regex _regex = new Regex("[^0-9.,]+"); //regex that matches disallowed text

        private void LoadCombobox()
        {
            var lcbb = new List<RoleView>()
            {
                new RoleView("admin", "Tài khoản siêu cấp vjp pro"),
                new RoleView("guest", "Tài khoản thường")
            };
            cbb_Role.ItemsSource = lcbb;
            cbb_Role.DisplayMemberPath = "Name";
        }

        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

        private void CheckText(TextBox txt)
        {
            //var txt = ((TextBox)sender);
            if (string.IsNullOrEmpty(txt.Text)) return;
            if (IsTextAllowed(txt.Text))
            {
                var mess = new MessageDialog() { tbl_Title = { Text = "Không phải số" }, tbl_Message = { Text = "Dữ liệu bạn copy có chứ ký tự." } };
                mess.ShowDialog();
                txt.Clear();
                return;
            }
            //txt.CaretIndex = txt.Text.Length;
        }

        private void Fillinfor()
        {
            var path = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            tbx_ID.Text = _UserEdit.ID;
            tbx_LasttName.Text = _UserEdit.LastName;
            tbx_FirstName.Text = _UserEdit.FirstName;
            tbx_Username.Text = _UserEdit.Username;
            img_Product.Source = string.IsNullOrEmpty(_UserEdit.Avatar) ? null : new BitmapImage(new Uri($@"{path}\{_UserEdit.Avatar}")) ;
            foreach (var item in cbb_Role.Items)
            {
                if (((RoleView) item).Role == _UserEdit.Role)
                    cbb_Role.SelectedValue = item;
                
            }
        }

        private AppUser infor()
        {
            var user = new AppUser()
            {
                ID = tbx_ID.Text,
                LastName = tbx_LasttName.Text,
                FirstName = tbx_FirstName.Text,
                Username = tbx_Username.Text,
                PasswordHash = tbx_Password.Text,
                Avatar = _pathImage,
                Role = ((RoleView)cbb_Role.SelectedValue).Role
            };
            return user;
        }

        private void GroupBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var opf = new OpenFileDialog();
            if (opf.ShowDialog() != true) return;
            _pathImage = opf.FileName;
            img_Product.Source = new BitmapImage(new Uri(_pathImage));
        }

        private void GroupBox_Drop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;
            // Note that you can have more than one file.
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            _pathImage = files[0];
            img_Product.Source = new BitmapImage(new Uri(files[0]));
        }

        private void Tbx_LastName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            CheckText(((TextBox)sender));
        }

        private void Tbx_FirstName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            CheckText(((TextBox)sender));
        }

        private void Btn_AddUser_OnClick(object sender, RoutedEventArgs e)
        {
            if(cbb_Role.SelectedIndex < 0) return;
            var add = new UserController(_Class);
            if(_isEdit is false)
                add.AddUser(infor());
            else
            {
                add.EditUser(infor());

            }
            this.Close();
        }

        private void Btn_CLose_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
