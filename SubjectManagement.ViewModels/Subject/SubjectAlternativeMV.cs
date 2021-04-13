using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SubjectManagement.ViewModels.Subject
{
    public class SubjectAlternativeMV
    {
        public Button Button { get; set; }
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
        public string Details { get; set; }

    }
}
