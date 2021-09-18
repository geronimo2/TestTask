using Volue.Application.Common.Mappings;
using Volue.Domain.Entities;

namespace Volue.Application.DataPoints
{
    public class DataPointDto : IMapFrom<DataPoint>
    {
        public string Name { get; set; }
        public int TimeStamp { get; set; }
        public float Value { get; set; }
        
        
        // public void Mapping(Profile profile)
        // {
        //     profile.CreateMap<TodoItem, TodoItemDto>()
        //         .ForMember(d => d.Priority, opt => opt.MapFrom(s => (int)s.Priority));
        // }
    }
}