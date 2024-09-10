using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using CafeHub.Application.Common.Contracts;
using CafeHub.Application.Queries.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CafeHub.Application.Queries.Cafe
{
    public class GetAllCafesQuery : IRequest<IEnumerable<CafeModel>>
    {
        public GetAllCafesQuery(string? location)
        {
            Location = location;
        }

        public string? Location { get; set; }
    }

    public class GetAllCafesQueryHandler : IRequestHandler<GetAllCafesQuery, IEnumerable<CafeModel>?>
    {
        private readonly ICafeManagementDbContext _context;
        public GetAllCafesQueryHandler(ICafeManagementDbContext context) => _context = context;

        public async Task<IEnumerable<CafeModel>?> Handle(GetAllCafesQuery request, CancellationToken cancellationToken)
        {
            var cafeEntities = await _context.Cafes.Include(c => c.CafeEmployees).Where(c => string.IsNullOrEmpty(request.Location) || c.Location == request.Location).ToListAsync();

            return cafeEntities?.Select(cafeEntity => new CafeModel
            {
                Id = cafeEntity.Id,
                Name = cafeEntity.Name,
                Description = cafeEntity.Description,
                Location = cafeEntity.Location,
                Logo = cafeEntity.Logo,
                Employees = cafeEntity.CafeEmployees.Count

            }).OrderByDescending(c => c.Employees).ToList();
        }
    }
}