using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubjectManagement.Data.Entities;

namespace SubjectManagement.Data.Configurations
{
    public class ElectiveGroupConfiguration : IEntityTypeConfiguration<ElectiveGroup>
    {
        public void Configure(EntityTypeBuilder<ElectiveGroup> builder)
        {
            builder.ToTable("ElectiveGroup");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.ID).HasDefaultValue(Guid.Empty);
            builder.Property(x => x.TotalSubject).HasDefaultValue(0);
        }
    }
}
