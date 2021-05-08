using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectManagement.ViewModels.Subject
{
    public class SubjectCompareVM
    {
        public Guid ID { get; set; }
        public int IdClass { get; set; }
        public string CourseCode { get; set; }
        public string Name { get; set; }
        public string Credit { get; set; }
        public bool TypeCourse { get; set; }
        public string NumberOfTheory { get; set; }
        public string NumberOfPractice { get; set; }
        public string Prerequisite { get; set; } //Tiên Quyết
        public string LearnFirst { get; set; } //Học Trước
        public string Parallel { get; set; } // Song Hành
        public int Semester { get; set; }

        public string Details { get; set; }

        public SubjectDifferent.Different Different { get; set; } //để phục phụ cho việc so sánh 2 subject //true là khác //flase là môn mới //null là không khác

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;

            var subject = (SubjectCompareVM)obj;

            return CourseCode == subject.CourseCode
                   && Name == subject.Name
                   && Credit == subject.Credit
                   && TypeCourse == subject.TypeCourse
                   && NumberOfTheory ==subject.NumberOfTheory
                   && NumberOfPractice ==subject.NumberOfPractice
                   && Prerequisite ==subject.Prerequisite
                   && LearnFirst == subject.LearnFirst
                   && Parallel == subject.Parallel
                   && Semester == subject.Semester;
        }
    }
}
