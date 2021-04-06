using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectManagement.Data.Entities
{
    public class SubjectInKnowledgeGroup
    {
        public int ID { get; set; }

        public Guid IDSubject { get; set; }
        public int IDClass { get; set; }
        public Guid IDKnowledgeGroup { get; set; }

        public Subject Subject { get; set; }
        public KnowledgeGroup KnowledgeGroup { get; set; }
    }
}
