using System.Linq.Expressions;
using System.Reflection;
using CafeHub.Application.Common.Contracts;
using CafeHub.Core.Common;
using CafeHub.Core.Entities;
using CafeHub.Infrastructure.Extensions;
using CafeHub.Infrastructure.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace CafeHub.Infrastructure
{
    public class CafeManagementDbContext : DbContext, ICafeManagementDbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Cafe> Cafes { get; set; }
        public DbSet<CafeEmployee> CafeEmployees { get; set; }

        public readonly EntitySaveChangesInterceptor _saveChangesInterceptor;

        public CafeManagementDbContext(DbContextOptions<CafeManagementDbContext> options) : base(options) { }

        public CafeManagementDbContext(DbContextOptions<CafeManagementDbContext> options, EntitySaveChangesInterceptor saveChangesInterceptor) : base(options)
        {
            _saveChangesInterceptor = saveChangesInterceptor;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // apply model builder configurations from Configuration Assembly
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);

            // Add soft delete global query filter
             modelBuilder.AddSoftDeleteQueryFilter();

           
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_saveChangesInterceptor);
            base.OnConfiguring(optionsBuilder);
        }
    }

    public class QueryFilter<TEntity> where TEntity : class, ISoftDeleteEntity
    {
        public static readonly Expression<Func<TEntity, bool>> Filter = e => !e.IsDeleted;
    }
}