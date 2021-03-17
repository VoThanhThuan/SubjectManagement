using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SubjectManagement.Common.Result;
using SubjectManagement.Service.Common;
using SubjectManagement.ViewModels.System.Users;

namespace SubjectManagement.Application.System.Users
{
    public interface IUserService
    {
        Result<InfoLogin> Authentivate(LoginRequest request);
    }
}
