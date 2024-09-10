using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CafeHub.Infrastructure
{
    // This factory is used by EF Core when running design-time commands like Add-Migration and Update-Database.
    public class CafeManagementDbContextFactory : IDesignTimeDbContextFactory<CafeManagementDbContext>
    {
        private readonly IConfiguration _configuration;
        public CafeManagementDbContextFactory(IConfiguration configuration) => _configuration = configuration;

        public CafeManagementDbContext CreateDbContext(string[] args)
        {
            // get connectionstring
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            // Configure shared CafeManagementDbContext
            var optionsBuilder = new DbContextOptionsBuilder<CafeManagementDbContext>();
            optionsBuilder
                    .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
                    builder => builder.MigrationsAssembly(typeof(CafeManagementDbContext).Assembly.FullName));

            // Return the DbContext instance using the default constructor
            return new CafeManagementDbContext(optionsBuilder.Options);
        }
    }
}