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
    public class SemesterOfClassConfiguration : IEntityTypeConfiguration<SemesterOfClass>
    {
        public void Configure(EntityTypeBuilder<SemesterOfClass> builder)
        {
            builder.ToTable("SemesterOfClass");
            builder.Property(x => x.ID).UseIdentityColumn();
            builder.HasOne(x => x.Class).WithOne(x => x.SemesterOfClass)
                .HasForeignKey<SemesterOfClass>(x => x.IDClass);
            builder.HasOne(x => x.Semester).WithMany(x => x.SemesterOfClasses)
                .HasForeignKey(x => x.IDSemester);
        }
    }
}
