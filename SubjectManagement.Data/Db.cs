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
        public static SubjectDbContext Context;
        public Db(SubjectDbContext context)
        {
            Context = context;
        }
    }
}
