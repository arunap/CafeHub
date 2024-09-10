using CafeHub.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CafeHub.Infrastructure.Configurations
{
    public class CafeConfiguration : IEntityTypeConfiguration<Cafe>
    {
        public void Configure(EntityTypeBuilder<Cafe> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Description)
                .HasMaxLength(500);

            builder.Property(c => c.Logo)
                .HasMaxLength(255);

            builder.Property(c => c.Location)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasMany(c => c.CafeEmployees)
                .WithOne(ce => ce.Cafe)
                .HasForeignKey(ce => ce.CafeId)
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