using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SubjectManagement.Data.EF;

namespace SubjectManagement.Data
{
    public class Db
    {
        public static SubjectDbContext _context;
        public Db(SubjectDbContext context)
        {
            _context = context;
        }
    }
}
