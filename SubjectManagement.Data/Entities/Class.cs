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

        public ClassInFaculty ClassInFaculty { get; set; }
        public List<Subject> Subject { get; set; }
        public List<AlternativeSubject> AlternativeSubjects { get; set; }

    }
}
