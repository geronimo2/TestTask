using Volue.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Volue.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<DataPoint> DataPoints { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
