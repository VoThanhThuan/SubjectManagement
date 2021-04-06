using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectManagement.ViewModels.SubjectOfClass
{
    public class SubjectOfClassRequest
    {
        public Guid ID { get; set; }
        public string CourseCode { get; set; }
        public string Name { get; set; }
        public int Credit { get; set; }
        public bool TypeCourse { get; set; }
        public int NumberOfTheory { get; set; }
        public int NumberOfPractice { get; set; }
        public int Prerequisite { get; set; } //Tiên Quyết
        public int LearnFirst { get; set; } //Học Trước
        public int Parallel { get; set; } // Song Hành

        public int Semester { get; set; }

        public int IdClass { get; set; }
    }
}
