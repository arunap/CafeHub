using CafeHub.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CafeHub.Infrastructure.Seeds
{
    public class CafeDataInitializer
    {
        private static readonly List<Cafe> cafes = new()
        {
            new Cafe { Id = Guid.NewGuid(), Name = "BrewCafé", Description = "A cozy place for coffee lovers.", Logo = "429cfd9a-32b8-4388-9d5a-9e5745e5155b.png", Location = "Downtown" },
            new Cafe { Id = Guid.NewGuid(), Name = "MochaBar", Description = "Modern coffee culture in an urban setting.", Logo = "d5a78c02-7d41-4bc4-9cb4-d15e9672b1f6.png", Location = "Downtown" },
            new Cafe { Id = Guid.NewGuid(), Name = "Beanery", Description = "Your daily dose of coffee and snacks.", Logo = "a3a4f38f-8b6c-41f3-9f0c-4c9a7be6e6f3.png", Location = "Uptown" },
            new Cafe { Id = Guid.NewGuid(), Name = "SipHaus", Description = "Freshly brewed coffee and pastries.", Logo = " d02e6b1d-cf5b-4512-8b68-76f37468a2cb.png", Location = "Midtown" },
            new Cafe { Id = Guid.NewGuid(), Name = "LatteLab", Description = "Family-owned coffee shop with a warm atmosphere.", Logo = "9c12909a-fad4-42ea-97d2-91f9fdf7c2a5.png", Location = "Old Town" },
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