using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dashboard.Data.Entities
{
    public class AppUserRole
    {
        public int ID { get; set; }      
        public string UserID { get; set; }
        public string RoleID { get; set; }

    }
}
