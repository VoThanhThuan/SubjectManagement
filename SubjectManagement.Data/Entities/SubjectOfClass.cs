using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectManagement.Data.Entities
{
    public class SubjectOfClass
    {
        public Guid IDSubject { get; set; }
        public int IDClass { get; set; }

        public Subject Subject { get; set; }
        public Class Class { get; set; }

    }
}
