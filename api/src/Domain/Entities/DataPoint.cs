using System.Collections.Generic;
using Volue.Domain.Common;

namespace Volue.Domain.Entities
{
    public class DataPoint : IHasDomainEvent
    {
        public string Name { get; set; }
        public int TimeStamp { get; set; }
        public float Value { get; set; }

        public List<DomainEvent> DomainEvents { get; } = new List<DomainEvent>();
    }
}