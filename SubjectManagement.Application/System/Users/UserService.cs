using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using SubjectManagement.Common.Result;
using SubjectManagement.Data;
using SubjectManagement.Data.EF;
using SubjectManagement.Service.User;
using SubjectManagement.ViewModels.System.Users;

namespace SubjectManagement.Application.System.Users
{
    public class UserService : IUserService
    {
        public Result<InfoLogin> Authentivate(LoginRequest request)
        {
            Db.Context.AppUsers.Load();
            //Tìm tài khoản
            var user = Db.Context.AppUsers.FirstOrDefault(x => x.Username == request.Username);

            if (user is null) return new ResultError<InfoLogin>("Tài khoản không tồn tại");

            var pass = ServiceForUser.PasswordHash(request.Password);

            if (user.PasswordHash != pass) return new ResultError<InfoLogin>("Sai mật khẩu");

            //Tìm quyền cho tài khoản
            var roleID = Db.Context.AppUserRoles.FirstOrDefault(x => x.UserID == user.ID);

            if(roleID is null) return new ResultError<InfoLogin>("Quyền hạn cho tài khoản vẫn chưa được thiết lập bạn có muốn truy cập với quyền khách ?");

            var role = Db.Context.AppRoles.FirstOrDefault(x => x.ID == roleID.RoleID);

            var info = new InfoLogin() {Role = role.Name, Name = $"{user.LastName} {user.FirstName}", ImagePath = user.Avatar};

            return new ResultSuccess<InfoLogin>(info, "Đăng nhập thành công");
        }

    }
}
