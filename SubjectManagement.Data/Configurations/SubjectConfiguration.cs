using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubjectManagement.Data.Entities;

namespace SubjectManagement.Data.Configurations
{
    public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.ToTable("Subject");
            builder.HasKey(x => new {x.ID, x.IDClass});
            builder.Property(x => x.Semester).HasDefaultValue(0);
            builder.Property(x => x.IDElectiveGroup).HasDefaultValue(null);
            builder.HasOne(x => x.Class).WithMany(x => x.Subject)
                .HasForeignKey(x => x.IDClass);
            builder.HasOne(x => x.ElectiveGroup).WithMany(x => x.Subjects)
                .HasForeignKey(x => x.IDElectiveGroup);
            builder.Property(x => x.IsPlan).HasDefaultValue(false);
        }
    }
}
