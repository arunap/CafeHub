using CafeHub.Core.Entities;
using CafeHub.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace CafeHub.Infrastructure.Seeds
{
    public class EmployeeDataInitializer
    {
        private static readonly List<Employee> employees = new()
        {
            new Employee { Id = "E1", Name = "Alice Smith", EmailAddress = "alice.smith@example.com", PhoneNumber = "555-1234", Gender = Gender.Female },
            new Employee { Id = "E2", Name = "Bob Johnson", EmailAddress = "bob.johnson@example.com", PhoneNumber = "555-5678", Gender = Gender.Male },
            new Employee { Id = "E3", Name = "Charlie Brown", EmailAddress = "charlie.brown@example.com", PhoneNumber = "555-8765", Gender = Gender.Male },
            new Employee { Id = "E4", Name = "Diana Prince", EmailAddress = "diana.prince@example.com", PhoneNumber = "555-4321", Gender = Gender.Female },
            new Employee { Id = "E5", Name = "Edward Davis", EmailAddress = "edward.davis@example.com", PhoneNumber = "555-3456", Gender = Gender.Male },
            new Employee { Id = "E6", Name = "Fiona Green", EmailAddress = "fiona.green@example.com", PhoneNumber = "555-6543", Gender = Gender.Female },
            new Employee { Id = "E7", Name = "George Harris", EmailAddress = "george.harris@example.com", PhoneNumber = "555-7890", Gender = Gender.Male },
            new Employee { Id = "E8", Name = "Hannah Lee", EmailAddress = "hannah.lee@example.com", PhoneNumber = "555-0987", Gender = Gender.Female },
            new Employee { Id = "E9", Name = "Ian Walker", EmailAddress = "ian.walker@example.com", PhoneNumber = "555-2109", Gender = Gender.Male },
            new Employee { Id = "E10", Name = "Julia Adams", EmailAddress = "julia.adams@example.com", PhoneNumber = "555-4320", Gender = Gender.Female },
            new Employee { Id = "E11", Name = "Kyle Martin", EmailAddress = "kyle.martin@example.com", PhoneNumber = "555-6540", Gender = Gender.Male },
            new Employee { Id = "E12", Name = "Laura Nelson", EmailAddress = "laura.nelson@example.com", PhoneNumber = "555-9876", Gender = Gender.Female },
            new Employee { Id = "E13", Name = "Mike Scott", EmailAddress = "mike.scott@example.com", PhoneNumber = "555-3450", Gender = Gender.Male },
            new Employee { Id = "E14", Name = "Nina Patel", EmailAddress = "nina.patel@example.com", PhoneNumber = "555-6789", Gender = Gender.Female },
            new Employee { Id = "E15", Name = "Oscar Martinez", EmailAddress = "oscar.martinez@example.com", PhoneNumber = "555-1230", Gender = Gender.Male },
            new Employee { Id = "E16", Name = "Pamela Anderson", EmailAddress = "pamela.anderson@example.com", PhoneNumber = "555-3457", Gender = Gender.Female },
            new Employee { Id = "E17", Name = "Quincy Adams", EmailAddress = "quincy.adams@example.com", PhoneNumber = "555-8901", Gender = Gender.Male },
            new Employee { Id = "E18", Name = "Rachel Green", EmailAddress = "rachel.green@example.com", PhoneNumber = "555-2345", Gender = Gender.Female },
            new Employee { Id = "E19", Name = "Sam Wilson", EmailAddress = "sam.wilson@example.com", PhoneNumber = "555-4567", Gender = Gender.Male },
            new Employee { Id = "E20", Name = "Tina Brown", EmailAddress = "tina.brown@example.com", PhoneNumber = "555-5679", Gender = Gender.Female },
            new Employee { Id = "E21", Name = "Ursula Grant", EmailAddress = "ursula.grant@example.com", PhoneNumber = "555-6781", Gender = Gender.Female },
            new Employee { Id = "E22", Name = "Victor Lee", EmailAddress = "victor.lee@example.com", PhoneNumber = "555-7891", Gender = Gender.Male },
            new Employee { Id = "E23", Name = "Wendy Hill", EmailAddress = "wendy.hill@example.com", PhoneNumber = "555-8902", Gender = Gender.Female },
            new Employee { Id = "E24", Name = "Xander Cole", EmailAddress = "xander.cole@example.com", PhoneNumber = "555-9012", Gender = Gender.Male },
            new Employee { Id = "E25", Name = "Yvonne Moore", EmailAddress = "yvonne.moore@example.com", PhoneNumber = "555-0123", Gender = Gender.Female },
            new Employee { Id = "E26", Name = "Zachary Hughes", EmailAddress = "zachary.hughes@example.com", PhoneNumber = "555-1236", Gender = Gender.Male },
            new Employee { Id = "E27", Name = "Amy Turner", EmailAddress = "amy.turner@example.com", PhoneNumber = "555-2346", Gender = Gender.Female },
            new Employee { Id = "E28", Name = "Ben Lewis", EmailAddress = "ben.lewis@example.com", PhoneNumber = "555-3458", Gender = Gender.Male },
            new Employee { Id = "E29", Name = "Clara Scott", EmailAddress = "clara.scott@example.com", PhoneNumber = "555-4568", Gender = Gender.Female },
            new Employee { Id = "E30", Name = "David King", EmailAddress = "david.king@example.com", PhoneNumber = "555-5670", Gender = Gender.Male }
        };
        private readonly CafeManagementDbContext context;
        public EmployeeDataInitializer(CafeManagementDbContext context) => this.context = context;

        public async Task SeedAsync()
        {
            if (!await context.Employees.AnyAsync())
            {
                await context.Employees.AddRangeAsync(employees);
                await context.SaveChangesAsync();
            }

            if (!await context.CafeEmployees.AnyAsync())
                await SetEmployeeCafeAsync();
        }

        private async Task SetEmployeeCafeAsync()
        {
            var cafes = await context.Cafes.ToListAsync();
            var cafeEmployees = new List<CafeEmployee>
            {
                new CafeEmployee { Id = Guid.NewGuid(), EmployeeId = "E1", CafeId = cafes[0].Id, StartDate = new DateOnly(2024, 1, 1) },
                new CafeEmployee { Id = Guid.NewGuid(), EmployeeId = "E2", CafeId = cafes[1].Id, StartDate = new DateOnly(2024, 2, 1) },
                new CafeEmployee { Id = Guid.NewGuid(), EmployeeId = "E3", CafeId = cafes[2].Id, StartDate = new DateOnly(2024, 3, 1) },
                new CafeEmployee { Id = Guid.NewGuid(), EmployeeId = "E4", CafeId = cafes[3].Id, StartDate = new DateOnly(2024, 4, 1) },
                new CafeEmployee { Id = Guid.NewGuid(), EmployeeId = "E5", CafeId = cafes[4].Id, StartDate = new DateOnly(2024, 5, 1) },
                new CafeEmployee { Id = Guid.NewGuid(), EmployeeId = "E6", CafeId = cafes[5].Id, StartDate = new DateOnly(2024, 6, 1) },
                new CafeEmployee { Id = Guid.NewGuid(), EmployeeId = "E7", CafeId = cafes[6].Id, StartDate = new DateOnly(2024, 7, 1) },
                new CafeEmployee { Id = Guid.NewGuid(), EmployeeId = "E8", CafeId = cafes[7].Id, StartDate = new DateOnly(2024, 8, 1) },
                new CafeEmployee { Id = Guid.NewGuid(), EmployeeId = "E9", CafeId = cafes[8].Id, StartDate = new DateOnly(2024, 9, 1) },
                new CafeEmployee { Id = Guid.NewGuid(), EmployeeId = "E10", CafeId = cafes[0].Id, StartDate = new DateOnly(2023, 10, 1), EndDate = new DateOnly(2024, 5, 1) },
                new CafeEmployee { Id = Guid.NewGuid(), EmployeeId = "E11", CafeId = cafes[10].Id, StartDate = new DateOnly(2023, 11, 1), EndDate = new DateOnly(2024, 6, 1) },
                new CafeEmployee { Id = Guid.NewGuid(), EmployeeId = "E12", CafeId = cafes[1].Id, StartDate = new DateOnly(2024, 1, 1)},
                new CafeEmployee { Id = Guid.NewGuid(), EmployeeId = "E13", CafeId = cafes[1].Id, StartDate = new DateOnly(2024, 1, 15)},
                new CafeEmployee { Id = Guid.NewGuid(), EmployeeId = "E14", CafeId = cafes[1].Id, StartDate = new DateOnly(2024, 2, 15)},
                new CafeEmployee { Id = Guid.NewGuid(), EmployeeId = "E15", CafeId = cafes[1].Id, StartDate = new DateOnly(2024, 3, 15)},
                new CafeEmployee { Id = Guid.NewGuid(), EmployeeId = "E16", CafeId = cafes[0].Id, StartDate = new DateOnly(2024, 4, 15)},
                new CafeEmployee { Id = Guid.NewGuid(), EmployeeId = "E17", CafeId = cafes[1].Id, StartDate = new DateOnly(2024, 5, 15)},
                new CafeEmployee { Id = Guid.NewGuid(), EmployeeId = "E18", CafeId = cafes[2].Id, StartDate = new DateOnly(2024, 6, 15)},
                new CafeEmployee { Id = Guid.NewGuid(), EmployeeId = "E19", CafeId = cafes[3].Id, StartDate = new DateOnly(2024, 7, 15)},
                new CafeEmployee { Id = Guid.NewGuid(), EmployeeId = "E20", CafeId = cafes[4].Id, StartDate = new DateOnly(2024, 8, 15)},
                new CafeEmployee { Id = Guid.NewGuid(), EmployeeId = "E21", CafeId = cafes[5].Id, StartDate = new DateOnly(2024, 9, 15)},
                new CafeEmployee { Id = Guid.NewGuid(), EmployeeId = "E22", CafeId = cafes[6].Id, StartDate = new DateOnly(2024, 2, 15)},
                new CafeEmployee { Id = Guid.NewGuid(), EmployeeId = "E23", CafeId = cafes[7].Id, StartDate = new DateOnly(2024, 2, 15)},
                new CafeEmployee { Id = Guid.NewGuid(), EmployeeId = "E24", CafeId = cafes[8].Id, StartDate = new DateOnly(2023, 2, 15)},
                new CafeEmployee { Id = Guid.NewGuid(), EmployeeId = "E25", CafeId = cafes[9].Id, StartDate = new DateOnly(2024, 1, 30)},
                new CafeEmployee { Id = Guid.NewGuid(), EmployeeId = "E26", CafeId = cafes[10].Id, StartDate = new DateOnly(2024, 2, 28)},
                new CafeEmployee { Id = Guid.NewGuid(), EmployeeId = "E27", CafeId = cafes[11].Id, StartDate = new DateOnly(2024, 3, 15)},
                new CafeEmployee { Id = Guid.NewGuid(), EmployeeId = "E28", CafeId = cafes[12].Id, StartDate = new DateOnly(2024, 4, 10)},
                new CafeEmployee { Id = Guid.NewGuid(), EmployeeId = "E29", CafeId = cafes[13].Id, StartDate = new DateOnly(2024, 5, 5)},
                new CafeEmployee { Id = Guid.NewGuid(), EmployeeId = "E30", CafeId = cafes[14].Id, StartDate = new DateOnly(2024, 6, 20)}
            };

            await context.CafeEmployees.AddRangeAsync(cafeEmployees);
            await context.SaveChangesAsync();

        }
    };

}