using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectManagement.ViewModels.System.Users
{
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }

    public class InfoLogin
    {
        public string Role { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
    }
}
