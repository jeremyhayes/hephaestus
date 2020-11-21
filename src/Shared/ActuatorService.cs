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

        public async Task<IDictionary<string,object>> GetHealth()
        {       
            var baseUrl = await _js.InvokeAsync<string>("localStorage.getItem", "baseUrl");
            var endpointUrl = $"{baseUrl}/health";
            return await _httpClient.GetFromJsonAsync<IDictionary<string, object>>(endpointUrl);
        }
    }
}
