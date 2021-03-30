using System;
using System.Collections.Generic;
using System.Text;

namespace SubjectManagement.Data.Entities
{
    public class SubjectInSemester
    {
        public Guid IDSubject { get; set; }
        public int IDSemester { get; set; }


        public Subject Subject { get; set; }
        public Semester Semester { get; set; }
    }
}
