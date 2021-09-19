using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Volue.Application.Common.Interfaces;

namespace Volue.Infrastructure.Services
{
    public class CalculatorService : ICalculatorService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CalculatorService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<MapResult> Map(float[] data, int count)
        {
            var dataAsString = new StringContent(
                JsonSerializer.Serialize(new { data, count }),
                Encoding.UTF8,
                "application/json");
            
            using (var client = _httpClientFactory.CreateClient("calculator"))
            {
                client.BaseAddress = new Uri("http://localhost:3000");
                var result = await client.PostAsync("/map", dataAsString);
                result.EnsureSuccessStatusCode();

                var r =  await result.Content.ReadFromJsonAsync<MapResult>();
                return r;
            }
        }

        public async Task<ReduceResult> Reduce(MapResult[] data)
        {
            var dataAsString = new StringContent(
                JsonSerializer.Serialize(data ),
                Encoding.UTF8,
                "application/json");
            
            using (var client = _httpClientFactory.CreateClient("calculator"))
            {
                client.BaseAddress = new Uri("http://localhost:3000");
                var result = await client.PostAsync("/reduce", dataAsString);
                result.EnsureSuccessStatusCode();

                var r =  await result.Content.ReadFromJsonAsync<ReduceResult>();
                return r;
            }
        }
    }
}