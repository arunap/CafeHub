using System.Text;
using CafeHub.Core.Entities;
using CafeHub.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace CafeHub.Infrastructure.Seeds
{
    public class EmployeeDataInitializer
    {
        private const string Prefix = "UI";
        private const int Length = 7;
        private const string Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
        private static readonly List<Employee> employees = new()
        {
            new Employee { Id = GenerateEmployeeId(), Name = "Alice Smith", EmailAddress = "alice.smith@example.com", PhoneNumber = "555-1234", Gender = Gender.Female , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "Bob Johnson", EmailAddress = "bob.johnson@example.com", PhoneNumber = "555-5678", Gender = Gender.Male , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "Charlie Brown", EmailAddress = "charlie.brown@example.com", PhoneNumber = "555-8765", Gender = Gender.Male , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "Diana Prince", EmailAddress = "diana.prince@example.com", PhoneNumber = "555-4321", Gender = Gender.Female , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "Edward Davis", EmailAddress = "edward.davis@example.com", PhoneNumber = "555-3456", Gender = Gender.Male , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "Fiona Green", EmailAddress = "fiona.green@example.com", PhoneNumber = "555-6543", Gender = Gender.Female , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "George Harris", EmailAddress = "george.harris@example.com", PhoneNumber = "555-7890", Gender = Gender.Male , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "Hannah Lee", EmailAddress = "hannah.lee@example.com", PhoneNumber = "555-0987", Gender = Gender.Female , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "Ian Walker", EmailAddress = "ian.walker@example.com", PhoneNumber = "555-2109", Gender = Gender.Male , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "Julia Adams", EmailAddress = "julia.adams@example.com", PhoneNumber = "555-4320", Gender = Gender.Female , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "Kyle Martin", EmailAddress = "kyle.martin@example.com", PhoneNumber = "555-6540", Gender = Gender.Male , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "Laura Nelson", EmailAddress = "laura.nelson@example.com", PhoneNumber = "555-9876", Gender = Gender.Female , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "Mike Scott", EmailAddress = "mike.scott@example.com", PhoneNumber = "555-3450", Gender = Gender.Male , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "Nina Patel", EmailAddress = "nina.patel@example.com", PhoneNumber = "555-6789", Gender = Gender.Female , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "Oscar Martinez", EmailAddress = "oscar.martinez@example.com", PhoneNumber = "555-1230", Gender = Gender.Male , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "Pamela Anderson", EmailAddress = "pamela.anderson@example.com", PhoneNumber = "555-3457", Gender = Gender.Female , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "Quincy Adams", EmailAddress = "quincy.adams@example.com", PhoneNumber = "555-8901", Gender = Gender.Male , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "Rachel Green", EmailAddress = "rachel.green@example.com", PhoneNumber = "555-2345", Gender = Gender.Female , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "Sam Wilson", EmailAddress = "sam.wilson@example.com", PhoneNumber = "555-4567", Gender = Gender.Male , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "Tina Brown", EmailAddress = "tina.brown@example.com", PhoneNumber = "555-5679", Gender = Gender.Female , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "Ursula Grant", EmailAddress = "ursula.grant@example.com", PhoneNumber = "555-6781", Gender = Gender.Female , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "Victor Lee", EmailAddress = "victor.lee@example.com", PhoneNumber = "555-7891", Gender = Gender.Male , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "Wendy Hill", EmailAddress = "wendy.hill@example.com", PhoneNumber = "555-8902", Gender = Gender.Female , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "Xander Cole", EmailAddress = "xander.cole@example.com", PhoneNumber = "555-9012", Gender = Gender.Male , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "Yvonne Moore", EmailAddress = "yvonne.moore@example.com", PhoneNumber = "555-0123", Gender = Gender.Female , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "Zachary Hughes", EmailAddress = "zachary.hughes@example.com", PhoneNumber = "555-1236", Gender = Gender.Male , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "Amy Turner", EmailAddress = "amy.turner@example.com", PhoneNumber = "555-2346", Gender = Gender.Female , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "Ben Lewis", EmailAddress = "ben.lewis@example.com", PhoneNumber = "555-3458", Gender = Gender.Male , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "Clara Scott", EmailAddress = "clara.scott@example.com", PhoneNumber = "555-4568", Gender = Gender.Female , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "David King", EmailAddress = "david.king@example.com", PhoneNumber = "555-5670", Gender = Gender.Male , CafeEmployees = new()}
        };
        private readonly CafeManagementDbContext context;
        public EmployeeDataInitializer(CafeManagementDbContext context) => this.context = context;

        public async Task SeedAsync()
        {
            Random random = new Random();
            if (!await context.Employees.AnyAsync())
            {
                List<Cafe> cafes = context.Cafes.ToList();

                employees.ForEach(emp =>
                {
                    // Get a random index
                    int randomIndex = random.Next(cafes.Count);
                    Cafe randomCafe = cafes[randomIndex];
                    Guid tempCafeId = randomCafe.Id;

                    emp.CafeEmployees.Add(new CafeEmployee
                    {
                        Cafe = randomCafe,
                        StartDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-5)),
                    });
                });

                await context.Employees.AddRangeAsync(employees);
                await context.SaveChangesAsync();
            }


        }

        private static string GenerateEmployeeId()
        {
            var random = new Random();
            var result = new StringBuilder();

            for (int i = 0; i < Length; i++)
                result.Append(Characters[random.Next(Characters.Length)]);

            return $"{Prefix}{result}";
        }

    }

}