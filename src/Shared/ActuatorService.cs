using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
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

        public async Task<JsonElement> GetHealth()
            => await GetResult<JsonElement>("health");

        public async Task<JsonElement> GetInfo()
            => await GetResult<JsonElement>("info");

        private async Task<T> GetResult<T>(string path)
        {
            var baseUrl = await _js.InvokeAsync<string>("localStorage.getItem", "baseUrl");
            var endpointUrl = $"{baseUrl}/{path}";
            return await _httpClient.GetFromJsonAsync<T>(endpointUrl);
        }
    }
}
