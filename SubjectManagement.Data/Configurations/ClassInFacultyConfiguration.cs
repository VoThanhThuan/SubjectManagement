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
    public class ClassInFacultyConfiguration : IEntityTypeConfiguration<ClassInFaculty>
    {
        public void Configure(EntityTypeBuilder<ClassInFaculty> builder)
        {
            builder.ToTable("ClassInFaculty");
            builder.Property(x => x.ID).UseIdentityColumn();

            builder.HasOne(x => x.Class).WithOne(x => x.ClassInFaculty)
                .HasForeignKey<ClassInFaculty>(x => x.IDClass);
            builder.HasOne(x => x.Faculty).WithMany(x => x.ClassInFaculties)
                .HasForeignKey(x => x.IDFaculty);
        }
    }
}
