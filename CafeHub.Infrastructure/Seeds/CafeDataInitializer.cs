using CafeHub.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CafeHub.Infrastructure.Seeds
{
    public class CafeDataInitializer
    {
        private static readonly List<Cafe> cafes = new()
        {
            new Cafe { Id = Guid.NewGuid(), Name = "The Coffee Spot", Description = "A cozy place for coffee lovers.", Logo = "coffee_spot_logo.png", Location = "Downtown" },
            new Cafe { Id = Guid.NewGuid(), Name = "Urban Grind", Description = "Modern coffee culture in an urban setting.", Logo = "urban_grind_logo.png", Location = "Downtown" },
            new Cafe { Id = Guid.NewGuid(), Name = "Java Junction", Description = "Your daily dose of coffee and snacks.", Logo = "java_junction_logo.png", Location = "Uptown" },
            new Cafe { Id = Guid.NewGuid(), Name = "Brewed Awakenings", Description = "Freshly brewed coffee and pastries.", Logo = "brewed_awakenings_logo.png", Location = "Midtown" },
            new Cafe { Id = Guid.NewGuid(), Name = "Bean Brothers", Description = "Family-owned coffee shop with a warm atmosphere.", Logo = "bean_brothers_logo.png", Location = "Old Town" },
        };

        private readonly CafeManagementDbContext context;
        public CafeDataInitializer(CafeManagementDbContext context) => this.context = context;

        public async Task SeedAsync()
        {
            if (!await context.Cafes.AnyAsync())
            {
                await context.Cafes.AddRangeAsync(cafes);
                await context.SaveChangesAsync();
            }
        }
    }
}