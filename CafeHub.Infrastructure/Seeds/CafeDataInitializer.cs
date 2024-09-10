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
            new Cafe { Id = Guid.NewGuid(), Name = "Caffeine Corner", Description = "Specialty coffees and gourmet sandwiches.", Logo = "caffeine_corner_logo.png", Location = "New City" },
            new Cafe { Id = Guid.NewGuid(), Name = "Espresso Express", Description = "Fast and friendly coffee service.", Logo = "espresso_express_logo.png", Location = "City Center" },
            new Cafe { Id = Guid.NewGuid(), Name = "Latte Land", Description = "A place where lattes and relaxation meet.", Logo = "latte_land_logo.png", Location = "Eastside" },
            new Cafe { Id = Guid.NewGuid(), Name = "Café Haven", Description = "An oasis of calm with great coffee.", Logo = "cafe_haven_logo.png", Location = "Westside" },
            new Cafe { Id = Guid.NewGuid(), Name = "Mochas & More", Description = "Exquisite mocha and dessert selection.", Logo = "mochas_more_logo.png", Location = "Southside" },
            new Cafe { Id = Guid.NewGuid(), Name = "Brewed Bliss", Description = "Blissful brews and friendly service.", Logo = "brewed_bliss_logo.png", Location = "Northside" },
            new Cafe { Id = Guid.NewGuid(), Name = "The Daily Grind", Description = "Your daily grind of fresh coffee.", Logo = "daily_grind_logo.png", Location = "Central Park" },
            new Cafe { Id = Guid.NewGuid(), Name = "Cafe Express", Description = "Quick and delicious coffee on the go.", Logo = "cafe_express_logo.png", Location = "Market Street" },
            new Cafe { Id = Guid.NewGuid(), Name = "The Mocha House", Description = "House blend mochas and more.", Logo = "mocha_house_logo.png", Location = "Riverdale" },
            new Cafe { Id = Guid.NewGuid(), Name = "Steam & Beans", Description = "Steaming hot coffee and beans.", Logo = "steam_beans_logo.png", Location = "Lakeview" },
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