using CafeHub.Application.Common.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CafeHub.Application.Commands.Cafe
{
    public class UpdateCafeCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public string Location { get; set; }
    }

    public class UpdateCafeCommandHandler : IRequestHandler<UpdateCafeCommand>
    {
        private readonly ICafeManagementDbContext _context;

        public UpdateCafeCommandHandler(ICafeManagementDbContext context) => _context = context;

        public async Task Handle(UpdateCafeCommand request, CancellationToken cancellationToken)
        {
            var cafe = await _context.Cafes
                .Include(c => c.CafeEmployees)
                .FirstOrDefaultAsync(c => c.Id == request.Id) ?? throw new InvalidOperationException();

            cafe.Name = request.Name;
            cafe.Logo = request.Logo;
            cafe.Location = request.Location;
            cafe.Description = request.Description;

            await _context.SaveChangesAsync();
        }
    }
}