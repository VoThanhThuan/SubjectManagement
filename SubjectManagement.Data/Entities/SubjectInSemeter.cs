using System;
using System.Collections.Generic;
using System.Text;

namespace SubjectManagement.Data.Entities
{
    public class SubjectInSemeter
    { 
        public int ID { get; set; }
        public Guid IDSubject { get; set; }
        public int IDSemeter { get; set; }


        public Subject Subject { get; set; }
        public Semeter Semeter { get; set; }
    }
}
