using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Volue.Application.Common.Interfaces;
using Volue.Application.Common.Models;
using Volue.Application.Common.Mappings;

namespace Volue.Application.DataPoints.Queries
{
    public class GetDataPointsWithPaginationQuery : IRequest<PaginatedList<DataPointDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        
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
            return await _context.DataPoints
                .OrderBy(x => x.TimeStamp)
                .ProjectTo<DataPointDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}