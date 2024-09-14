using System.Text;
using System.Text.Json.Serialization;
using CafeHub.Application.Common.Contracts;
using CafeHub.Core.Entities;
using CafeHub.Core.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CafeHub.Application.Commands.Employee
{
    public class CreateEmployeeCommand : IRequest<string>
    {
        public Guid? CafeId { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Gender Gender { get; set; }
    }

    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, string>
    {
        private readonly ICafeManagementDbContext _context;
        private readonly IDateTimeProvider _dateTimeProvider;

        private const string Prefix = "UI";
        private const int Length = 7;
        private const string Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";

        public CreateEmployeeCommandHandler(ICafeManagementDbContext context, IDateTimeProvider dateTimeProvider)
        {
            _context = context;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<string> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            string employeeId;
            do
            {
                employeeId = GenerateEmployeeId();
            } while (await _context.Employees.AnyAsync(e => e.Id == employeeId));

            var employee = new Core.Entities.Employee
            {
                Id = employeeId,
                EmailAddress = request.EmailAddress,
                PhoneNumber = request.PhoneNumber,
                Gender = request.Gender,
                Name = request.Name,

            };
            if (request.CafeId.HasValue && request.CafeId != Guid.Empty)
            {
                employee.CafeEmployees = new List<CafeEmployee>
                {
                    new() { CafeId = request.CafeId.Value, StartDate = _dateTimeProvider.DateOnly, }
                };
            }

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return employee.Id;
        }

        private string GenerateEmployeeId()
        {
            var random = new Random();
            var result = new StringBuilder();

            for (int i = 0; i < Length; i++)
                result.Append(Characters[random.Next(Characters.Length)]);

            return $"{Prefix}{result}";
        }
    }
}