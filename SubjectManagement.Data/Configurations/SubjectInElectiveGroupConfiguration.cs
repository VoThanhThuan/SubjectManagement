using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubjectManagement.Data.Entities;

namespace SubjectManagement.Data.Configurations
{
    public class SubjectInElectiveGroupConfiguration : IEntityTypeConfiguration<SubjectInElectiveGroup>
    {
        public void Configure(EntityTypeBuilder<SubjectInElectiveGroup> builder)
        {
            builder.ToTable("SubjectInElectiveGroup");
            builder.Property(x => x.ID).UseIdentityColumn();

            builder.HasOne(x => x.Subject).WithOne(x => x.SubjectInElectiveGroup)
                .HasForeignKey<SubjectInElectiveGroup>(x => new {x.IDSubject, x.IDCLass});
            builder.HasOne(x => x.ElectiveGroup).WithMany(x => x.SubjectInElectiveGroups)
                .HasForeignKey(x => x.IDElectiveGroup);

        }
    }
}
