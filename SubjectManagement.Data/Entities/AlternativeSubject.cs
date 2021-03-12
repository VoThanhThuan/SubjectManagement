using System;
using System.Collections.Generic;
using System.Text;

namespace SubjectManagement.Data.Entities
{
    public class AlternativeSubject
    {
        public int ID { get; set; }
        public Guid IDOld { get; set; }
        public Guid IDNew { get; set; }
        public Subject Subject { get; set; }

    }
}
