using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubjectManagement.Data.Entities;

namespace SubjectManagement.Data.Configurations
{
    public class AlternativeSubjectConfiguration : IEntityTypeConfiguration<AlternativeSubject>
    {
        public void Configure(EntityTypeBuilder<AlternativeSubject> builder)
        {
            builder.ToTable("AlternativeSubject");
            builder.Property(x => x.ID).UseIdentityColumn();

            builder.HasOne(x => x.Class).WithMany(x => x.AlternativeSubjects)
                .HasForeignKey(x => x.IDClass);

        }
    }
}
