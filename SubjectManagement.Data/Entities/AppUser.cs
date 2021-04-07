using System;
using System.Collections.Generic;
using System.Text;

namespace SubjectManagement.Data.Entities
{
    public class AppUser
    {
        public string ID { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Avatar { get; set; }

        public string Role { get; set; }

    }
}
