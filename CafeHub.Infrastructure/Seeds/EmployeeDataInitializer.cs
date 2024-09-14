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
            new Employee { Id = GenerateEmployeeId(), Name = "Alice Smith", EmailAddress = "alice.smith@example.com", PhoneNumber = "+6587289722", Gender = Gender.Female , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "Bob Johnson", EmailAddress = "bob.johnson@example.com", PhoneNumber = "+6564429350", Gender = Gender.Male , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "Charlie Brown", EmailAddress = "charlie.brown@example.com", PhoneNumber = "+6531062378", Gender = Gender.Male , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "Diana Prince", EmailAddress = "diana.prince@example.com", PhoneNumber = "+6517668551", Gender = Gender.Female , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "Edward Davis", EmailAddress = "edward.davis@example.com", PhoneNumber = "+6523993258", Gender = Gender.Male , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "Fiona Green", EmailAddress = "fiona.green@example.com", PhoneNumber = "+6519137758", Gender = Gender.Female , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "George Harris", EmailAddress = "george.harris@example.com", PhoneNumber = "+6588241695", Gender = Gender.Male , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "Hannah Lee", EmailAddress = "hannah.lee@example.com", PhoneNumber = "+6574939717", Gender = Gender.Female , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "Ian Walker", EmailAddress = "ian.walker@example.com", PhoneNumber = "+6582840896", Gender = Gender.Male , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "Julia Adams", EmailAddress = "julia.adams@example.com", PhoneNumber = "+6507361383", Gender = Gender.Female , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "Kyle Martin", EmailAddress = "kyle.martin@example.com", PhoneNumber = "+6558996963", Gender = Gender.Male , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "Laura Nelson", EmailAddress = "laura.nelson@example.com", PhoneNumber = "+6591113703", Gender = Gender.Female , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "Mike Scott", EmailAddress = "mike.scott@example.com", PhoneNumber = "+6547145172", Gender = Gender.Male , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "Nina Patel", EmailAddress = "nina.patel@example.com", PhoneNumber = "+6507361383", Gender = Gender.Female , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "Oscar Martinez", EmailAddress = "oscar.martinez@example.com", PhoneNumber = "+6583923344", Gender = Gender.Male , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "Pamela Anderson", EmailAddress = "pamela.anderson@example.com", PhoneNumber = "+6569498058", Gender = Gender.Female , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "Quincy Adams", EmailAddress = "quincy.adams@example.com", PhoneNumber = "+6599287448", Gender = Gender.Male , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "Rachel Green", EmailAddress = "rachel.green@example.com", PhoneNumber = "+6527542242", Gender = Gender.Female , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "Sam Wilson", EmailAddress = "sam.wilson@example.com", PhoneNumber = "+6515980281", Gender = Gender.Male , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "Tina Brown", EmailAddress = "tina.brown@example.com", PhoneNumber = "+6591353642", Gender = Gender.Female , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "Ursula Grant", EmailAddress = "ursula.grant@example.com", PhoneNumber = "+6547145172", Gender = Gender.Female , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "Victor Lee", EmailAddress = "victor.lee@example.com", PhoneNumber = "+6515980281", Gender = Gender.Male , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "Wendy Hill", EmailAddress = "wendy.hill@example.com", PhoneNumber = "+6536366852", Gender = Gender.Female , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "Xander Cole", EmailAddress = "xander.cole@example.com", PhoneNumber = "+6507361383", Gender = Gender.Male , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "Yvonne Moore", EmailAddress = "yvonne.moore@example.com", PhoneNumber = "+6591113703", Gender = Gender.Female , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "Zachary Hughes", EmailAddress = "zachary.hughes@example.com", PhoneNumber = "+6591353642", Gender = Gender.Male , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "Amy Turner", EmailAddress = "amy.turner@example.com", PhoneNumber = "+6591113703", Gender = Gender.Female , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "Ben Lewis", EmailAddress = "ben.lewis@example.com", PhoneNumber = "+6555745072", Gender = Gender.Male , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "Clara Scott", EmailAddress = "clara.scott@example.com", PhoneNumber = "+6591353642", Gender = Gender.Female , CafeEmployees = new() },
            new Employee { Id = GenerateEmployeeId(), Name = "David King", EmailAddress = "david.king@example.com", PhoneNumber = "+6515980281", Gender = Gender.Male , CafeEmployees = new()}
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

                    int randomDays = random.Next(maxValue: 10);
                    emp.CafeEmployees.Add(new CafeEmployee
                    {
                        Cafe = randomCafe,
                        StartDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-randomDays)),
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