using Microsoft.EntityFrameworkCore;
using Rihal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rihal.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
    => options.UseSqlite("Data Source=school.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var classes = new Class[]
            {
                new Class{Id=1,Name="ClassA" },
                new Class{Id=2,Name="ClassB" },
                new Class{Id=3,Name="ClassC" },
            };
            modelBuilder.Entity<Class>().HasData(classes);

            var countries = new Country[]
            {
                new Country{Id=1,Name="Egypt" },
                new Country{Id=2,Name="Oman" },
                new Country{Id=3,Name="USA" },
            };
            modelBuilder.Entity<Country>().HasData(countries);

            var students = new Student[]
           {
                new Student{Id=Guid.NewGuid(),Name="moataz",BirthDate=DateTime.Now.AddYears(-7),ClassId=1,CountryId=1 },
                new Student{Id=Guid.NewGuid(),Name="ali",BirthDate=DateTime.Now.AddYears(-8),ClassId=1,CountryId=2 },
                new Student{Id=Guid.NewGuid(),Name="ahmed",BirthDate=DateTime.Now.AddYears(-7),ClassId=1,CountryId=3 },
           };
            modelBuilder.Entity<Student>().HasData(students);


            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            SetDates();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            SetDates();
            return await base.SaveChangesAsync();
        }

        private void SetDates()
        {
            ChangeTracker.DetectChanges();
            var added = ChangeTracker.Entries()
                .Where(t => t.State == EntityState.Added)
                .Select(t => t.Entity)
                .ToArray();

            foreach (var entity in added)
            {
                if (entity is Base<Guid> trackGuid)
                {
                    trackGuid.CreatedDate = DateTime.Now;
                }
                if (entity is Base<int> trackInt)
                {
                    trackInt.CreatedDate = DateTime.Now;
                }
            }

            var modified = ChangeTracker.Entries()
                .Where(t => t.State == EntityState.Modified)
                .Select(t => t.Entity)
                .ToArray();

            foreach (var entity in modified)
            {
                if (entity is Base<Guid> trackGuid)
                {
                    trackGuid.ModifiedDate = DateTime.Now;
                }
                if (entity is Base<int> trackInt)
                {
                    trackInt.ModifiedDate = DateTime.Now;
                }
            }
        }
    }
}
