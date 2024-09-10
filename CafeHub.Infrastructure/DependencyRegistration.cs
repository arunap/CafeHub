using CafeHub.Application.Common.Contracts;
using CafeHub.Infrastructure.Interceptors;
using CafeHub.Infrastructure.Providers;
using CafeHub.Infrastructure.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CafeHub.Infrastructure
{
    public static class DependencyRegistration
    {
        public static IServiceCollection AddInfraServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            // add interceptor for auditable entitiy data 
            serviceCollection.AddScoped<EntitySaveChangesInterceptor>();
            serviceCollection.AddScoped<IDateTimeProvider, DateTimeProvider>();

            // database initial data initializer registration
            serviceCollection.AddScoped<CafeDataInitializer>();
            serviceCollection.AddScoped<EmployeeDataInitializer>();

            // set up in-memory database
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                serviceCollection.AddDbContext<CafeManagementDbContext>(options =>
                    options.UseInMemoryDatabase("CafeManagementDb"));
            }
            else
            {
                // get connectionstring
                var connectionString = configuration.GetConnectionString("DefaultConnection");

                // Configure shared CafeManagementDbContext
                serviceCollection.AddDbContext<CafeManagementDbContext>(
                    options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
                    builder => builder.MigrationsAssembly(typeof(CafeManagementDbContext).Assembly.FullName)
                ));
            }

             serviceCollection.AddScoped<ICafeManagementDbContext>(provider => provider.GetRequiredService<CafeManagementDbContext>());

            return serviceCollection;
        }
    }
}