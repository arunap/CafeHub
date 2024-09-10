using CafeHub.Application.Common.Contracts;
using CafeHub.Application.Queries.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CafeHub.Application.Queries.Employee
{
    public class GetEmployeeQuery : IRequest<EmployeeModel>
    {
        public string Id { get; set; }

        public GetEmployeeQuery(string id) => Id = id;
    }

    public class GetEmployeeQueryHandler : IRequestHandler<GetEmployeeQuery, EmployeeModel?>
    {
        private readonly ICafeManagementDbContext _context;
        public GetEmployeeQueryHandler(ICafeManagementDbContext context) => _context = context;

        public async Task<EmployeeModel?> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
        {
            // var entity = await _context.Employees.FirstOrDefaultAsync(c => c.Id == request.Id);

            // return entity == null ? null : new EmployeeModel
            // {
            //     Name = entity.Name,
            //     EmailAddress = entity.EmailAddress,
            //     Gender = entity.Gender,
            //     PhoneNumber = entity.PhoneNumber
            // };

            var query = from employee in _context.Employees.Where(employee => employee.Id == request.Id)
                        join cafeEmployee in _context.CafeEmployees on employee.Id equals cafeEmployee.EmployeeId into cafeEmployeeGroup
                        from cafeEmployee in cafeEmployeeGroup.DefaultIfEmpty()
                        join cafe in _context.Cafes on cafeEmployee.CafeId equals cafe.Id into cafeGroup
                        from cafe in cafeGroup.DefaultIfEmpty()
                        where cafeEmployee.EndDate == null
                        select new EmployeeModel
                        {
                            Id = employee.Id,
                            Name = employee.Name,
                            EmailAddress = employee.EmailAddress,
                            Gender = employee.Gender,
                            PhoneNumber = employee.PhoneNumber,
                            CafeName = cafe != null ? cafe.Name : null
                        };

            return await query.FirstOrDefaultAsync() ?? null;
        }
    }
}