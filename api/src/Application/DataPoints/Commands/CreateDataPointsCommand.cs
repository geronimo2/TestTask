using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Volue.Application.Common.Exceptions;
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
        private readonly ILogger<CreateDataPointsCommandHandler> _logger;

        public CreateDataPointsCommandHandler(IApplicationDbContext context, ILogger<CreateDataPointsCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }
        
        public async Task<Unit> Handle(CreateDataPointsCommand request, CancellationToken cancellationToken)
        {
            foreach (var dataPoint in request._dataPoints)
            {
                var dp = new DataPoint()
                {
                    Name = dataPoint.Name,
                    TimeStamp = dataPoint.T,
                    Value = dataPoint.V
                };

                dp.DomainEvents.Add(new DataPointCreatedEvent(dp));
                _context.DataPoints.Add(dp);
            }

            
            //TODO: handle cuncurrent insert exception
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (System.ArgumentException ex_)
            {
                _logger.LogWarning(ex_, "An item with the same key has already been added.");
                
                var ve = new ValidationException();
                ve.Errors.Add("[name, t]", new string[] { "An item with the same key has already been added."});
                throw ve;
            }
            return Unit.Value;
        }
    }
}