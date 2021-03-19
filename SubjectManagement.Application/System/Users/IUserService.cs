using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SubjectManagement.Common.Result;
using SubjectManagement.ViewModels.System.Users;

namespace SubjectManagement.Application.System.Users
{
    public interface IUserService
    {
        Task<Result<InfoLogin>> Authenticate(LoginRequest request);
    }
}
