using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace Hephaestus
{
    public class ActuatorService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _js;

        public ActuatorService(HttpClient httpClient, IJSRuntime js)
        {
            _httpClient = httpClient;
            _js = js;
        }

        public async Task<IDictionary<string, object>> GetHealth()
            => await GetResult<IDictionary<string, object>>("health");

        public async Task<IDictionary<string, object>> GetInfo()
            => await GetResult<IDictionary<string, object>>("info");

        private async Task<T> GetResult<T>(string path)
        {
            var baseUrl = await _js.InvokeAsync<string>("localStorage.getItem", "baseUrl");
            var endpointUrl = $"{baseUrl}/{path}";
            return await _httpClient.GetFromJsonAsync<T>(endpointUrl);
        }
    }
}
