using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Volue.Application.Common.Exceptions;
using Volue.Application.Common.Interfaces;
using Volue.Application.Common.Models;
using Volue.Application.Common.Mappings;
using Volue.Domain.Entities;

namespace Volue.Application.DataPoints.Queries
{
    public class GetDataPointsWithPaginationQuery : IRequest<PaginatedList<DataPointDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public int From { get; set; } = 0;
        public int To { get; set; } = int.MaxValue;
        
        public string Name { get; set;  }
    }
    
    public class GetDataPointsQueryHandler : IRequestHandler<GetDataPointsWithPaginationQuery, PaginatedList<DataPointDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetDataPointsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<PaginatedList<DataPointDto>> Handle(GetDataPointsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var q = _context.DataPoints
                .AsNoTracking()
                .Where(r => r.TimeStamp > request.From && r.TimeStamp < request.To);
            
            if (!string.IsNullOrEmpty(request.Name))
            {
                q = q.Where(n => n.Name == request.Name);
            }

            return await q.OrderBy(x => x.TimeStamp)
                .ProjectTo<DataPointDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}