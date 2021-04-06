using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubjectManagement.Data.Entities;

namespace SubjectManagement.Data.Configurations
{
    public class SubjectInKnowledgeGroupConfiguration : IEntityTypeConfiguration<SubjectInKnowledgeGroup>
    {
        public void Configure(EntityTypeBuilder<SubjectInKnowledgeGroup> builder)
        {
            builder.ToTable("SubjectInKnowledgeGroup");
            builder.Property(x => x.ID).UseIdentityColumn();

            builder.HasOne(x => x.Subject).WithOne(x => x.SubjectInKnowledgeGroup)
                .HasForeignKey<SubjectInKnowledgeGroup>(x => new {x.IDSubject, x.IDClass});
            builder.HasOne(x => x.KnowledgeGroup).WithMany(x => x.SubjectInKnowledgeGroups)
                .HasForeignKey(x => x.IDKnowledgeGroup);
        }
    }
}
