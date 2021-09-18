using Volue.Domain.Common;
using System.Threading.Tasks;

namespace Volue.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
