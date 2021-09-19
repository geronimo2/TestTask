using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Volue.Application.Common.Interfaces;
using Volue.Domain.Entities;

namespace Volue.Application.DataPoints.Commands
{
    public class CalculateAvgSumCommand : IRequest<AvgSumDto>
    {
        public string Name { get; }
        public int? From { get; }
        public int? To { get; }

        public CalculateAvgSumCommand(string name, int? from, int? to)
        {
            Name = name;
            From = from ?? 0;
            To = to ?? int.MaxValue;
        }
    }

    public class CalculateAvgSumCommandHandler : IRequestHandler<CalculateAvgSumCommand, AvgSumDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICalculatorService _calculatorService;

        public CalculateAvgSumCommandHandler(IApplicationDbContext context, ICalculatorService calculatorService)
        {
            _context = context;
            _calculatorService = calculatorService;
        }
        
        // public async Task<AvgSumDto> Handle2(CalculateAvgSumCommand request, CancellationToken cancellationToken)
        // {
        //     var data = await _context.DataPoints
        //         .AsNoTracking()
        //         .Where(r => r.TimeStamp >= request.From && r.TimeStamp <= request.To && r.Name == request.Name)
        //         .ToListAsync(cancellationToken);
        //
        //     var (avg, sum) = await _calculatorService.Calculate(data.Select(a => a.Value).ToArray());
        //     return new AvgSumDto
        //     {
        //         Avg = avg,
        //         Sum = sum
        //     };
        // }
        
        public async Task<AvgSumDto> Handle(CalculateAvgSumCommand request, CancellationToken cancellationToken)
        {
            var batchSize = 100;
            var query = GetQuery(
                request.Name,
                request.From.Value,
                request.To.Value);
            var nrOfRecords = await query.CountAsync(cancellationToken);
            int numberOfBatches = (int)Math.Ceiling((double)nrOfRecords / batchSize);
            var tasks = new List<Task<MapResult>>();
            for(int i = 0; i < numberOfBatches; i++)
            {
                var values = await query.Skip(i * batchSize).Take(batchSize).Select(a => a.Value).ToArrayAsync();
                tasks.Add(_calculatorService.Map(values, nrOfRecords));
            }
            
            // map
            var mapResults = await Task.WhenAll(tasks);

            // reduce
            var reduceResult = await _calculatorService.Reduce(mapResults);

            return new AvgSumDto
            {
                Avg = reduceResult.Avg,
                Sum = reduceResult.Sum 
            };

        }

        private IQueryable<DataPoint> GetQuery(string name, int from, int to) => 
            _context.DataPoints
                .AsNoTracking()
                .Where(r => r.TimeStamp >= from && r.TimeStamp <= to && r.Name == name);

        // public async Task<MapDto> Map(IEnumerable<float> userIds)
        // {
        //     var users = new List<UserDto>();
        //     var batchSize = 2;
        //     int numberOfBatches = (int)Math.Ceiling((double)userIds.Count() / batchSize);
        //
        //     for(int i = 0; i < numberOfBatches; i++)
        //     {
        //         var currentIds = userIds.Skip(i * batchSize).Take(batchSize);
        //         var tasks = currentIds.Select(id => client.GetUser(id));
        //         users.AddRange(await Task.WhenAll(tasks));
        //     }
        //     
        //     return users;
        // }
    }
}