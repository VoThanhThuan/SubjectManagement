using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectManagement.Data.Entities
{
    public class ClassInFaculty
    {
        public int ID { get; set; }
        public int IDClass { get; set; }
        public int IDFaculty { get; set; }

        public Class Class { get; set; }
        public Faculty Faculty { get; set; }
    }
}
