using System;
using System.Collections.Generic;
using System.Text;

namespace SubjectManagement.Data.Entities
{
    public class SubjectInElectiveGroup
    {
        public int ID { get; set; }
        public Guid IDSubject { get; set; }
        public Guid IDElectiveGroup { get; set; }


        public Subject Subject { get; set; }
        public ElectiveGroup ElectiveGroup { get; set; }
    }
}
