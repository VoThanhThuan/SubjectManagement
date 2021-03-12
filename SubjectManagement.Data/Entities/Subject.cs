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
        public string Credit { get; set; }
        public string TypeCourse { get; set; }
        public string NumberOfTheory { get; set; }
        public string NumberOfPractice { get; set; }
        public string Details { get; set; }

        public AlternativeSubject AlternativeSubject { get; set; }
        public SubjectInElectiveGroup SubjectInElectiveGroup { get; set; }
        public SubjectInSemeter SubjectInSemeter { get; set; }
        public SubjectInKnowledgeGroup SubjectInKnowledgeGroup { get; set; }

    }
}
