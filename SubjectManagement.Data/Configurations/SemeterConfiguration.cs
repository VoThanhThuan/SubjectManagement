using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubjectManagement.Data.Entities;

namespace SubjectManagement.Data.Configurations
{
    public class SemeterConfiguration : IEntityTypeConfiguration<Semeter>
    {
        public void Configure(EntityTypeBuilder<Semeter> builder)
        {
            builder.ToTable("Semeter");
            builder.Property(x => x.ID).UseIdentityColumn();
        }
    }
}
