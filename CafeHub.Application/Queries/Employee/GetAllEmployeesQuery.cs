using CafeHub.Application.Common.Contracts;
using CafeHub.Application.Queries.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CafeHub.Application.Queries.Employee
{
    public class GetAllEmployeesQuery : IRequest<IEnumerable<EmployeeModel>>
    {
        public GetAllEmployeesQuery(string? cafeName) => CafeName = cafeName;

        public string? CafeName { get; }
    }

    public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, IEnumerable<EmployeeModel>?>
    {
        private readonly ICafeManagementDbContext _context;
        private readonly IDateTimeProvider _dateTimeProvider;

        public GetAllEmployeesQueryHandler(ICafeManagementDbContext context, IDateTimeProvider dateTimeProvider)
        {
            _context = context;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<IEnumerable<EmployeeModel>?> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            var query = from employee in _context.Employees
                        join cafeEmployee in _context.CafeEmployees on employee.Id equals cafeEmployee.EmployeeId into cafeEmployeeGroup
                        from cafeEmployee in cafeEmployeeGroup.DefaultIfEmpty()
                        join cafe in _context.Cafes on cafeEmployee.CafeId equals cafe.Id into cafeGroup
                        from cafe in cafeGroup.DefaultIfEmpty()
                        where (string.IsNullOrEmpty(request.CafeName) || cafe.Name == request.CafeName) &&  cafeEmployee.EndDate == null
                        select new EmployeeModel
                        {
                            Id = employee.Id,
                            Name = employee.Name,
                            EmailAddress = employee.EmailAddress,
                            Gender = employee.Gender,
                            PhoneNumber = employee.PhoneNumber,
                            DaysWorked = cafeEmployee == null ? 0 : _dateTimeProvider.DateOnly.DayNumber - cafeEmployee.StartDate.DayNumber,
                            CafeName = cafe != null ? cafe.Name : null
                        };

            var entities = await query.ToListAsync();
            return entities?.OrderByDescending(e => e.DaysWorked);
        }
    }
}