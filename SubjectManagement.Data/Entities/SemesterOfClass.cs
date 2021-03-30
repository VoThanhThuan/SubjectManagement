using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectManagement.Data.Entities
{
    public class SemesterOfClass
    {
        public int ID { get; set; }
        public int IDSemester { get; set; }
        public int IDClass { get; set; }


        public Semester Semester { get; set; }
        public Class Class { get; set; }

    }
}
