using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rihal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rihal.Infrastructure
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasOne(d => d.Country)
                .WithMany().HasForeignKey(c => c.CountryId);

            builder.HasOne(d => d.Class)
               .WithMany().HasForeignKey(c => c.ClassId);
        }
    }
}
