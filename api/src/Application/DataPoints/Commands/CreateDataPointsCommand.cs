using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Volue.Application.Common.Interfaces;
using Volue.Domain.Entities;
using Volue.Domain.Events;

namespace Volue.Application.DataPoints.Commands
{
    public class CreateDataPointsCommand : IRequest
    {
        public IEnumerable<DataPointDto> _dataPoints;
        
        public CreateDataPointsCommand(IEnumerable<DataPointDto> dataPoints)
        {
            _dataPoints = dataPoints;
        }
    }
    
    public class CreateDataPointsCommandHandler : IRequestHandler<CreateDataPointsCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateDataPointsCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<Unit> Handle(CreateDataPointsCommand request, CancellationToken cancellationToken)
        {
            foreach (var dataPoint in request._dataPoints)
            {
                var dp = new DataPoint()
                {
                    Name = dataPoint.Name,
                    TimeStamp = dataPoint.TimeStamp,
                    Value = dataPoint.Value
                };
                
                _context.DataPoints.Add(dp);
            }

            //entity.DomainEvents.Add(new TodoItemCreatedEvent(entity));


            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}