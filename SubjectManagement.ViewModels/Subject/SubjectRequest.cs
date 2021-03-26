using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectManagement.ViewModels.Subject
{
    public class SubjectRequest
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
        public bool IsOffical { get; set; }
        public string Details { get; set; }

        public Guid IDKnowledgeGroup { get; set; }
        public Guid IDKnowledgeGroupOld { get; set; }
    }
}
