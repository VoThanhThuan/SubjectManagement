using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SubjectManagement.Data;
using SubjectManagement.Service.Common;
using SubjectManagement.Service.User;
using SubjectManagement.ViewModels.System.Users;

namespace SubjectManagement.Application.System.Users
{
    public class UserService : IUserService
    {
        public Result<string> Authentivate(LoginRequest request)
        {
            Db._context.AppUsers.Load();
            var user = Db._context.AppUsers.FirstOrDefault(x => x.Username == request.Username);

            if (user is null) return new ResultError<string>("Tài khoản không tồn tại");

            var pass = ServiceForUser.PasswordHash(request.Password);

            if (user.PasswordHash != pass) return new ResultError<string>("Sai mật khẩu");

            return new ResultSuccess<string>();
        }
    }
}
