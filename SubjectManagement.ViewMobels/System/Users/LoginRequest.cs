using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SubjectManagement.ViewModels.System.Users
{
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Hashtable ListWindows { get; set; }
    }

}
