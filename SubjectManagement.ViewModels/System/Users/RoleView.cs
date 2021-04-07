using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectManagement.ViewModels.System.Users
{
    public class RoleView
    {
        public string Role { get; set; }
        public string Name { get; set; }

        public RoleView(string role, string name) => (Role, Name) = (role, name);
    }
}
