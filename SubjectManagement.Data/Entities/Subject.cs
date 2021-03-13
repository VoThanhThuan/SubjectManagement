using System;
using System.Collections.Generic;
using System.Text;

namespace SubjectManagement.Data.Entities
{
    public class Subject
    {
        public Guid ID { get; set; }
        public string CourseCode { get; set; }
        public string Name { get; set; }
        public int Credit { get; set; }
        public bool TypeCourse { get; set; }
        public int NumberOfTheory { get; set; }
        public int NumberOfPractice { get; set; }
        public bool IsOffical { get; set; }
        public string Details { get; set; }

        public AlternativeSubject AlternativeSubject { get; set; }
        public SubjectInElectiveGroup SubjectInElectiveGroup { get; set; }
        public SubjectInSemeter SubjectInSemeter { get; set; }
        public SubjectInKnowledgeGroup SubjectInKnowledgeGroup { get; set; }

    }
}
