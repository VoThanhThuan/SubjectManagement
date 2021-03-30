using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectManagement.Data.Entities
{
    public class Class
    {
        public int ID { get; set; }
        public string CodeClass { get; set; }
        public string Name { get; set; }
        public DateTime Year { get; set; }

        public SemesterOfClass SemesterOfClass { get; set; }
        public ClassInFaculty ClassInFaculty { get; set; }
        public List<SubjectOfClass> SubjectOfClass { get; set; }

    }
}
