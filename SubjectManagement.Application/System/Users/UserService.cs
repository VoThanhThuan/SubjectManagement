using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using SubjectManagement.Common.Result;
using SubjectManagement.Common.User;
using SubjectManagement.Data;
using SubjectManagement.Data.EF;

using SubjectManagement.ViewModels.System.Users;

namespace SubjectManagement.Application.System.Users
{
    public class UserService : IUserService
    {
        public async Task<Result<InfoLogin>> Authenticate(LoginRequest request)
        {
            await Db.Context.AppUsers.LoadAsync();
            //Tìm tài khoản
            var user = await Db.Context.AppUsers.FirstOrDefaultAsync(x => x.Username == request.Username);

            if (user is null) return new ResultError<InfoLogin>("Tài khoản không tồn tại");

            var pass = ServiceForUser.PasswordHash(request.Password);

            if (user.PasswordHash != pass) return new ResultError<InfoLogin>("Sai mật khẩu");

            //Tìm quyền cho tài khoản
            var roleID = await Db.Context.AppUserRoles.FirstOrDefaultAsync(x => x.UserID == user.ID);

            if(roleID is null) return new ResultError<InfoLogin>("Quyền hạn cho tài khoản vẫn chưa được thiết lập bạn có muốn truy cập với quyền khách ?");

            var role = await Db.Context.AppRoles.FirstOrDefaultAsync(x => x.ID == roleID.RoleID);

            var info = new InfoLogin() {Role = role.Name, Name = $"{user.LastName} {user.FirstName}", ImagePath = user.Avatar};

            return new ResultSuccess<InfoLogin>(info, "Đăng nhập thành công");
        }

    }
}
