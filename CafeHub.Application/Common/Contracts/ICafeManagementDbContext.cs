using CafeHub.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CafeHub.Application.Common.Contracts
{
    public interface ICafeManagementDbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Cafe> Cafes { get; set; }
        public DbSet<CafeEmployee> CafeEmployees { get; set; }


        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}