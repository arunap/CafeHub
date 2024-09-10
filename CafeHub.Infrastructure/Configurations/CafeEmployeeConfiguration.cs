using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CafeHub.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CafeHub.Infrastructure.Configurations
{
    public class CafeEmployeeConfiguration : IEntityTypeConfiguration<CafeEmployee>
    {
        public void Configure(EntityTypeBuilder<CafeEmployee> builder)
        {
            builder.HasKey(ce => ce.Id);

            builder.Property(ce => ce.EmployeeId)
                .IsRequired();

            builder.Property(ce => ce.StartDate)
                .IsRequired();

            builder.Property(ce => ce.EndDate);

            builder.HasOne(ce => ce.Cafe)
                .WithMany(c => c.CafeEmployees)
                .HasForeignKey(ce => ce.CafeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(ce => ce.Employee)
                .WithMany(e => e.CafeEmployees)
                .HasForeignKey(ce => ce.EmployeeId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }

}