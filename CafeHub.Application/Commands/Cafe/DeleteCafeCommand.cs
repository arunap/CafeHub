using CafeHub.Application.Common.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CafeHub.Application.Commands.Cafe
{
    public class DeleteCafeCommand : IRequest
    {
        public DeleteCafeCommand(Guid id)
        {
            this.Id = id;

        }
        public Guid Id { get; set; }
    }

    public class DeleteCafeCommandHandler : IRequestHandler<DeleteCafeCommand>
    {
        private readonly ICafeManagementDbContext _context;

        public DeleteCafeCommandHandler(ICafeManagementDbContext context) => _context = context;

        public async Task Handle(DeleteCafeCommand request, CancellationToken cancellationToken)
        {
            var cafe = await _context.Cafes
            .Include(c => c.CafeEmployees)
            .ThenInclude(c => c.Employee)
            .FirstOrDefaultAsync(c => c.Id == request.Id) ?? throw new InvalidOperationException();

            _context.CafeEmployees.RemoveRange(cafe.CafeEmployees);
            _context.Employees.RemoveRange(cafe.CafeEmployees.Select(c => c.Employee));
            _context.Cafes.Remove(cafe);

            await _context.SaveChangesAsync();
        }
    }
}