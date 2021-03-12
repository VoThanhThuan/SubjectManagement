using System;
using System.Collections.Generic;
using System.Text;

namespace SubjectManagement.Data.Entities
{
    public class ElectiveGroup
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }

        public List<SubjectInElectiveGroup> SubjectInElectiveGroups { get; set; }

    }
}
