using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectManagement.Data.Entities
{
    public class Faculty
    {
        public int ID { get; set; }
        public string Name { get; set; }
        
        public List<ClassInFaculty> ClassInFaculties { get; set; }
    }
}
