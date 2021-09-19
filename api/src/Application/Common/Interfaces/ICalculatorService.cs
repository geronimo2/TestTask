using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Volue.Application.Common.Interfaces
{
    public interface ICalculatorService
    {
        public Task<MapResult> Map(float[] data, int count);
        public Task<ReduceResult> Reduce(MapResult[] data);
    }

    public class MapResult
    {
        [JsonPropertyName("sum")]
        public float Sum { get; set; }
        [JsonPropertyName("avg")]
        public float Avg { get; set; }
        [JsonPropertyName("weight")]
        public float Weight { get; set; }
    }
    
    public class ReduceResult
    {
        public float Sum { get; set; }
        public float Avg { get; set; }
    }
}