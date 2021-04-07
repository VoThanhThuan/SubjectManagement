using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SubjectManagement.Common.Result;
using SubjectManagement.Data.Entities;
using SubjectManagement.ViewModels.System.Users;

namespace SubjectManagement.Application.System.Users
{
    public interface IUserService
    {
        Task<Result<InfoLogin>> Authenticate(LoginRequest request);

        List<AppUser> GetListUser();

        Result<string> AddUser(AppUser infor);

        Result<string> EditUser(AppUser infor);

        Result<string> RemoveUser(string idUser);

    }
}
