using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using SubjectManagement.Common.Result;
using SubjectManagement.Common.User;
using SubjectManagement.Data;
using SubjectManagement.Data.EF;
using SubjectManagement.Data.Entities;
using SubjectManagement.ViewModels.System.Users;

namespace SubjectManagement.Application.System.Users
{
    public class UserService : IUserService
    {

        public UserService(SubjectDbContext db)
        {
            _db = db;
        }

        private readonly SubjectDbContext _db;

        public async Task<Result<InfoLogin>> Authenticate(LoginRequest request)
        {
            await Db.Context.AppUsers.LoadAsync();
            //Tìm tài khoản
            var user = await _db.AppUsers.FirstOrDefaultAsync(x => x.Username == request.Username);

            if (user is null) return new ResultError<InfoLogin>("Tài khoản không tồn tại");

            var pass = ServiceForUser.PasswordHash(request.Password);

            if (user.PasswordHash != pass) return new ResultError<InfoLogin>("Sai mật khẩu");

            //Tìm quyền cho tài khoản

            if (user.Role == "") return new ResultError<InfoLogin>("Quyền hạn cho tài khoản vẫn chưa được thiết lập bạn có muốn truy cập với quyền khách ?");

            var info = new InfoLogin() { Role = user.Role, Name = $"{user.LastName} {user.FirstName}", ImagePath = user.Avatar };

            return new ResultSuccess<InfoLogin>(info, "Đăng nhập thành công");
        }

        public List<AppUser> GetListUser()
        {
            return _db.AppUsers.Select(x => x).ToList();
        }

        public Result<string> AddUser(AppUser infor)
        {
            var check = _db.AppUsers.Find(infor.ID);
            if (check is not null) return new ResultError<string>("Mã người dùng đã tồn tại");
            infor.Avatar ??= SaveFile(infor.Avatar);

            _db.AppUsers.Add(infor);
            _db.SaveChanges();
            return new ResultSuccess<string>("Ok");
        }

        public Result<string> EditUser(AppUser infor)
        {
            var user = _db.AppUsers.Find(infor.ID);
            if (user is null) return new ResultError<string>("Người dùng không tồn tại");
            user.Username = infor.Username;
            user.PasswordHash = string.IsNullOrEmpty(infor.PasswordHash) ? user.PasswordHash : ServiceForUser.PasswordHash(infor.PasswordHash);
            user.FirstName = infor.FirstName;
            user.LastName = infor.LastName;
            user.Avatar = infor.Avatar == null ? user.Avatar : SaveFile(infor.Avatar);
                _db.SaveChanges();
            return new ResultSuccess<string>("Ok");
        }

        public Result<string> RemoveUser(string idUser)
        {
            var user = _db.AppUsers.Find(idUser);
            if (user is null) return new ResultError<string>("Người dùng không tồn tại");
            _db.AppUsers.Remove(user);
            return new ResultSuccess<string>("OK");
        }

        private string SaveFile(string path)
        {
            var originalFileName = Path.GetFileName(path);
            var fileName = $@"imageProduct\{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            if (File.Exists($@"imageProduct\{originalFileName}"))
                fileName = $@"imageProduct\{Path.GetExtension(originalFileName)}";

            if (!Directory.Exists("imageProduct"))
                Directory.CreateDirectory("imageProduct");
            File.Copy(path, fileName);
            return fileName;
        }

    }
}
