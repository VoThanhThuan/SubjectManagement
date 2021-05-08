using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.EntityFrameworkCore;
using SubjectManagement.Application.System.Users;
using SubjectManagement.Data;
using SubjectManagement.GUI.Constant;
using SubjectManagement.GUI.Dialog;
using SubjectManagement.GUI.Main;
using SubjectManagement.GUI.Member;
using SubjectManagement.ViewModels.System.Users;

namespace SubjectManagement.GUI.Controller
{
    public class OpenWindowController
    {

        public OpenWindowController()
        {
            _userService = new UserService();
        }

        private readonly IUserService _userService;
        public async Task<bool> OpenWindow(LoginRequest request, Grid grid_Loading)
        {
            grid_Loading.Visibility = Visibility.Visible;

            //var _userService = new UserService();

            var result = await _userService.Authenticate(request);

            //await Db.Context.Faculties.LoadAsync();
            //await Db.Context.Classes.LoadAsync();

            var isSuccess = false;

            if (result.IsSuccessed is true)
            {
                switch (result.ResultObj.Role)
                {
                    case "admin":
                        ConstantInfor.InforUser = result.ResultObj;
                        var main = new MainWindow();
                        main.Show();
                        isSuccess = true;
                        break;
                    case "guest":
                        var mem = new MemberWindow(result.ResultObj);
                        mem.Show();
                        isSuccess = true;
                        break;
                }
                //((Window)request.ListWindows[result.ResultObj.Role])?.Show();
            }
            else
            {
                var mess = new MessageDialog()
                {
                    tbl_Title = { Text = "Lỗi đăng nhập" },
                    tbl_Message = { Text = $"{result.Message}" },
                    title_color = { Background = new SolidColorBrush(Color.FromRgb(255, 0, 0)) },
                    Topmost = true
                };
                mess.ShowDialog();
            }
            grid_Loading.Visibility = Visibility.Hidden;
            return isSuccess;
        }
    }
}
