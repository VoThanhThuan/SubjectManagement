using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SubjectManagement.Data.EF
{
    public class SubjectDbContextFactory : IDesignTimeDbContextFactory<SubjectDbContext>
    {
        public SubjectDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            var connectionString = configuration.GetConnectionString("SubjectDatabase");

            var optionsBuilder = new DbContextOptionsBuilder<SubjectDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new SubjectDbContext(optionsBuilder.Options);
        }
    }
}
