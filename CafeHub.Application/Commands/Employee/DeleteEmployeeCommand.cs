using CafeHub.Application.Common.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CafeHub.Application.Commands.Employee
{
    public class DeleteEmployeeCommand : IRequest
    {
        public DeleteEmployeeCommand(string id) => this.Id = id;

        public string Id { get; set; }
    }

    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand>
    {
        private readonly ICafeManagementDbContext _context;

        public DeleteEmployeeCommandHandler(ICafeManagementDbContext context) => _context = context;

        public async Task Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(c => c.Id == request.Id) ?? throw new InvalidOperationException();

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }
    }
}