using CafeHub.Core.Entities;

namespace CafeHub.Infrastructure.Extensions
{
    public static class SoftDeleteQueryExtension
    {
        public static void AddSoftDeleteQueryFilter(this Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cafe>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<Employee>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<CafeEmployee>().HasQueryFilter(x => x.IsDeleted == false);
        }
    }
}