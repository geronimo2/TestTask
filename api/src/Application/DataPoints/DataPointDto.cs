using AutoMapper;
using Volue.Application.Common.Mappings;
using Volue.Domain.Entities;

namespace Volue.Application.DataPoints
{
    public class DataPointDto : IMapFrom<DataPoint>
    {
        public string Name { get; set; }
        
        public int T { get; set; }
        
        public float V { get; set; }
        
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<DataPoint, DataPointDto>()
                .ForMember(d => d.T, opt => opt.MapFrom(s => s.TimeStamp))
                .ForMember(d => d.V, opt => opt.MapFrom(s => s.Value));
        }
    }
}