using CafeHub.Application.Commands.Employee;
using CafeHub.Core.Entities;
using CafeHub.Core.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CafeHub.Infrastructure.Seeds
{
    public class EmployeeDataInitializer
    {
        private readonly IMediator _mediator;

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
        public EmployeeDataInitializer(CafeManagementDbContext context, IMediator mediator)
        {
            this.context = context;
            _mediator = mediator;
        }

        public async Task SeedAsync()
        {
            Random random = new Random();
            List<Cafe> cafes = context.Cafes.ToList();

            if (!await context.Employees.AnyAsync())
            {
                employees.ForEach(async emp =>
                {
                    // Get a random index
                    int randomIndex = random.Next(cafes.Count);
                    Cafe randomCafe = cafes[randomIndex];
                    Guid tempCafeId = randomCafe.Id;

                    await _mediator.Send(new CreateEmployeeCommand
                    {
                        EmailAddress = emp.EmailAddress,
                        Name = emp.Name,
                        PhoneNumber = emp.PhoneNumber,
                        Gender = emp.Gender,
                        CafeId = tempCafeId
                    });
                });

            }
        }
    };

}