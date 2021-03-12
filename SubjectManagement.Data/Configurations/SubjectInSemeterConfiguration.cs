using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubjectManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SubjectManagement.Data.Configurations
{
    public class SubjectInSemeterConfiguration : IEntityTypeConfiguration<SubjectInSemeter>
    {
        public void Configure(EntityTypeBuilder<SubjectInSemeter> builder)
        {
            builder.ToTable("SubjectInSemeter");
            builder.Property(x => x.ID).UseIdentityColumn();

            builder.HasOne(x => x.Subject).WithOne(x => x.SubjectInSemeter)
                .HasForeignKey<SubjectInSemeter>(x => x.IDSubject);

            builder.HasOne(x => x.Semeter).WithMany(x => x.SubjectInSemeters)
                .HasForeignKey(x => x.IDSemeter);
        }
    }
}
