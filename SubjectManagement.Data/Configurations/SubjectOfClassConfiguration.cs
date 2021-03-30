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
    public class SubjectOfClassConfiguration : IEntityTypeConfiguration<SubjectOfClass>
    {
        public void Configure(EntityTypeBuilder<SubjectOfClass> builder)
        {
            builder.ToTable("SubjectOfClass");
            builder.HasKey(x => new {x.IDClass, x.IDSubject});

            builder.HasOne(x => x.Class).WithMany(x => x.SubjectOfClass)
                .HasForeignKey(x => x.IDClass);

            builder.HasOne(x => x.Subject).WithOne(x => x.SubjectOfClass)
                .HasForeignKey<SubjectOfClass>(x => x.IDSubject);
        }
    }
}
