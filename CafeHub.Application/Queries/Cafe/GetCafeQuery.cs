using CafeHub.Application.Common.Contracts;
using CafeHub.Application.Queries.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CafeHub.Application.Queries.Cafe
{
    public class GetCafeQuery : IRequest<CafeModel> { public Guid CafeId { get; set; } }

    public class GetCafeQueryHandler : IRequestHandler<GetCafeQuery, CafeModel?>
    {
        private readonly ICafeManagementDbContext _context;
        public GetCafeQueryHandler(ICafeManagementDbContext context) => _context = context;

        public async Task<CafeModel?> Handle(GetCafeQuery request, CancellationToken cancellationToken)
        {
            var cafeEntity = await _context.Cafes.FirstOrDefaultAsync(c => c.Id == request.CafeId);

            return cafeEntity == null ? null : new CafeModel
            {
                Id = cafeEntity.Id,
                Name = cafeEntity.Name,
                Description = cafeEntity.Description,
                Location = cafeEntity.Location,
                Logo = cafeEntity.Logo
            };
        }
    }
}