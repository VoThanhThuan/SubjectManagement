using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectManagement.Data.Entities
{
    public class ElectiveGroup
    {
        public Guid ID { get; set; }
        public int IDClass { get; set; }
        public int Semester { get; set; }
        public int TotalSubject { get; set; }
        public List<Subject> Subjects { get; set; }
    }
}
