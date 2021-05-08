using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using SubjectManagement.Application.System.Users;
using SubjectManagement.Common.Result;
using SubjectManagement.Data;
using SubjectManagement.Data.Entities;
using SubjectManagement.GUI.Dialog;

namespace SubjectManagement.GUI.Controller
{
    public class UserController
    {
        public UserController()
        {
            _userService = new UserService();
        }

        private IUserService _userService;

        public List<AppUser> GetListUser()
        {
            return _userService.GetListUser();
        }

        public Result<string> AddUser(AppUser infor)
        {
            var result = _userService.AddUser(infor);
            if (result.IsSuccessed == true) return result;
            var mess = new MessageDialog()
            {
                tbl_Title = { Text = $"Lỗi thêm" },
                tbl_Message = { Text = $"{result.Message}" },
                title_color = { Background = new SolidColorBrush(Color.FromRgb(255, 0, 0)) },
                Topmost = true
            };
            mess.ShowDialog();
            return null;

        }

        public Result<string> EditUser(AppUser infor)
        {
            var result = _userService.EditUser(infor);
            if (result.IsSuccessed == true) return result;
            var mess = new MessageDialog()
            {
                tbl_Title = { Text = $"Lỗi sửa" },
                tbl_Message = { Text = $"{result.Message}" },
                title_color = { Background = new SolidColorBrush(Color.FromRgb(255, 0, 0)) },
                Topmost = true
            };
            mess.ShowDialog();
            return null;
        }

        public Result<string> RemoveUser(string idUser)
        {
            var result = _userService.RemoveUser(idUser);
            if (result.IsSuccessed == true) return result;
            var mess = new MessageDialog()
            {
                tbl_Title = { Text = $"Lỗi sửa" },
                tbl_Message = { Text = $"{result.Message}" },
                title_color = { Background = new SolidColorBrush(Color.FromRgb(255, 0, 0)) },
                Topmost = true
            };
            mess.ShowDialog();
            return null;
        }

    }
}
