using Volue.Domain.Common;
using Volue.Domain.Entities;

namespace Volue.Domain.Events
{
    public class DataPointCreatedEvent : DomainEvent
    {
        public DataPointCreatedEvent(DataPoint dataPoint)
        {
            Item = dataPoint;
        }

        public DataPoint Item { get; }
    }
}
