using CafeHub.Application.Common.Contracts;
using CafeHub.Core.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CafeHub.Application.Commands.Employee
{
    public class UpdateEmployeeCommand : IRequest
    {
        public string Id { get; set; }
        public Guid? CafeId { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public Gender Gender { get; set; }
    }

    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand>
    {
        private readonly ICafeManagementDbContext _context;
        private readonly IDateTimeProvider _dateTimeProvider;

        public UpdateEmployeeCommandHandler(ICafeManagementDbContext context, IDateTimeProvider dateTimeProvider)
        {
            _context = context;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _context.Employees
                .Include(c => c.CafeEmployees)
                .FirstOrDefaultAsync(c => c.Id == request.Id) ?? throw new InvalidOperationException();

            employee.Name = request.Name;
            employee.EmailAddress = request.EmailAddress;
            employee.PhoneNumber = request.PhoneNumber;
            employee.Gender = request.Gender;

            if (request.CafeId.HasValue && request.CafeId != Guid.Empty)
            {
                // end the existing assignment if the cafe is different
                employee.CafeEmployees.Where(c => c.CafeId != request.CafeId.Value).ToList()
                .ForEach(c =>
                {
                    c.EndDate = _dateTimeProvider.DateOnly;
                    _context.CafeEmployees.Remove(c);
                });

                if (employee.CafeEmployees.Count == 0 || employee.CafeEmployees.Any(c => c.CafeId != request.CafeId))
                {
                    employee.CafeEmployees.Add(new Core.Entities.CafeEmployee
                    {
                        CafeId = request.CafeId.Value,
                        StartDate = _dateTimeProvider.DateOnly,
                    });
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}