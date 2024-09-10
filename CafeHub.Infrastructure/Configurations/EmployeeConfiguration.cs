using CafeHub.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CafeHub.Infrastructure.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.EmailAddress)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.PhoneNumber)
                .HasMaxLength(15);

            builder.Property(e => e.Gender)
                .IsRequired();

            builder.HasMany(e => e.CafeEmployees)
                .WithOne(ce => ce.Employee)
                .HasForeignKey(ce => ce.EmployeeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(b => b.CreatedAt)
           .IsRequired();

            builder.Property(b => b.CreatedBy)
                .IsRequired();

            builder.Property(b => b.UpdatedAt);

            builder.Property(b => b.UpdatedBy);

            builder.Property(b => b.IsDeleted)
                .HasDefaultValue(null);
        }
    }

}