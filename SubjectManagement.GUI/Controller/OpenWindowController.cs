﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.EntityFrameworkCore;
using SubjectManagement.Application.System.Users;
using SubjectManagement.Common.Dialog;
using SubjectManagement.Data;
using SubjectManagement.ViewModels.System.Users;

namespace SubjectManagement.GUI.Controller
{
    public class OpenWindowController
    {

        public OpenWindowController()
        {
            _userService = new UserService(Db.Context);
        }

        private readonly IUserService _userService;

        public async void OpenWindow(LoginRequest request, Grid grid_Loading)
        {
            grid_Loading.Visibility = Visibility.Visible;

            var result = await _userService.Authenticate(request);
            if (result.IsSuccessed is true)
                ((Window)request.ListWindows[result.ResultObj.Role])?.Show();
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

            await Db.Context.Faculties.LoadAsync();
            await Db.Context.Classes.LoadAsync();

            grid_Loading.Visibility = Visibility.Hidden;
        }
    }
}
