using System;
using System.Collections.Generic;
using System.Text;

namespace SubjectManagement.Data.Entities
{
    public class Semeter
    {
        public int ID { get; set; }
        public string Term { get; set; }
        public string Year { get; set; }


        public List<SubjectInSemeter> SubjectInSemeters { get; set; }

    }
}
