using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CafeHub.Infrastructure
{
    // This factory is used by EF Core when running design-time commands like Add-Migration and Update-Database.
    public class CafeManagementDbContextFactory : IDesignTimeDbContextFactory<CafeManagementDbContext>
    {
        public CafeManagementDbContext CreateDbContext(string[] args)
        {
            // Build configuration
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // get connectionstring
            var connectionString = configuration.GetConnectionString("DefaultConnection");

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