using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Dashboard.Data.Configurations;
using SubjectManagement.Data.Configurations;
using SubjectManagement.Data.Entities;
using SubjectManagement.Data.Extensions;

namespace SubjectManagement.Data.EF
{
    public class SubjectDbContext : DbContext
    {
        public SubjectDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());

            modelBuilder.ApplyConfiguration(new AlternativeSubjectConfiguration());

            modelBuilder.ApplyConfiguration(new KnowledgeGroupConfiguration());
            modelBuilder.ApplyConfiguration(new SubjectInKnowledgeGroupConfiguration());

            modelBuilder.ApplyConfiguration(new SubjectConfiguration());
            modelBuilder.ApplyConfiguration(new ElectiveGroupConfiguration());

            modelBuilder.ApplyConfiguration(new ClassConfiguration());
            modelBuilder.ApplyConfiguration(new FacultyConfiguration());
            modelBuilder.ApplyConfiguration(new ClassInFacultyConfiguration());

            modelBuilder.Seed();
        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AlternativeSubject> AlternativeSubjects { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<ElectiveGroup> ElectiveGroups { get; set; }
        public DbSet<KnowledgeGroup> KnowledgeGroups { get; set; }
        public DbSet<SubjectInKnowledgeGroup> SubjectInKnowledgeGroups { get; set; }

        public DbSet<Class> Classes { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<ClassInFaculty> ClassInFaculties { get; set; }

    }
}
