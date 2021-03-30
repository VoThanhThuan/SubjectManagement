using System;
using System.Collections.Generic;
using System.Text;

namespace SubjectManagement.Data.Entities
{
    public class Semester
    {
        public int ID { get; set; }
        public int Term { get; set; }

        public List<SubjectInSemester> SubjectInSemesters { get; set; }
        public List<SemesterOfClass> SemesterOfClasses { get; set; }


    }
}
