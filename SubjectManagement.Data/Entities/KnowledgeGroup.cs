using System;
using System.Collections.Generic;
using System.Text;

namespace SubjectManagement.Data.Entities
{
    public class KnowledgeGroup
    {
        public Guid ID { get; set; }
        public string Name { get; set; }

        public List<SubjectInKnowledgeGroup> SubjectInKnowledgeGroups { get; set; }
    }
}
