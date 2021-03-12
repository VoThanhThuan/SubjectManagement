using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubjectManagement.Data.Entities;

namespace SubjectManagement.Data.Configurations
{
    public class KnowledgeGroupConfiguration : IEntityTypeConfiguration<KnowledgeGroup>
    {
        public void Configure(EntityTypeBuilder<KnowledgeGroup> builder)
        {
            builder.ToTable("KnowledgeGroup");
        }
    }
}
