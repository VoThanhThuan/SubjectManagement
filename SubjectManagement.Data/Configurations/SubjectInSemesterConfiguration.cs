using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubjectManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SubjectManagement.Data.Configurations
{
    public class SubjectInSemesterConfiguration : IEntityTypeConfiguration<SubjectInSemester>
    {
        public void Configure(EntityTypeBuilder<SubjectInSemester> builder)
        {
            builder.ToTable("SubjectInSemester");
            builder.HasKey(x => new {x.IDSubject, x.IDSemester});

            builder.HasOne(x => x.Subject).WithOne(x => x.SubjectInSemester)
                .HasForeignKey<SubjectInSemester>(x => x.IDSubject);

            builder.HasOne(x => x.Semester).WithMany(x => x.SubjectInSemesters)
                .HasForeignKey(x => x.IDSemester);
        }
    }
}
