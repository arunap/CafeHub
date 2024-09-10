using CafeHub.Application.Common.Contracts;
using CafeHub.Core.Entities;
using MediatR;

namespace CafeHub.Application.Commands.Cafe
{
    public class CreateCafeCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public string Location { get; set; }
    }

    public class CreateCafeCommandHandler : IRequestHandler<CreateCafeCommand, Guid>
    {
        private readonly ICafeManagementDbContext _context;

        public CreateCafeCommandHandler(ICafeManagementDbContext context) => _context = context;

        public async Task<Guid> Handle(CreateCafeCommand request, CancellationToken cancellationToken)
        {
            var cafe = new Core.Entities.Cafe
            {
                Name = request.Name,
                Description = request.Description,
                Logo = request.Logo,
                Location = request.Location
            };

            _context.Cafes.Add(cafe);
            await _context.SaveChangesAsync();

            return cafe.Id;
        }
    }
}