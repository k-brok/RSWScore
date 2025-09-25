using System.Net.Http.Json;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.APP.Services
{
    public class WebSettingService : IWebSettingService
    {
        private readonly HttpClient _httpClient;
        private const string Endpoint = "api/websetting";

        public WebSettingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<WebSetting>> GetAllAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<WebSetting>>(Endpoint);

            return result ?? new List<WebSetting>();
        }

        public async Task<WebSetting?> GetByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<WebSetting>($"{Endpoint}/{id}");
        }

        public async Task<WebSetting> CreateAsync(WebSetting model)
        {
            var response = await _httpClient.PostAsJsonAsync(Endpoint, model);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<WebSetting>();
            }

            return null;
        }

        public async Task<WebSetting?> UpdateAsync(Guid id, WebSetting model)
        {
            var response = await _httpClient.PutAsJsonAsync($"{Endpoint}/{id}", model);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<WebSetting>();
            }

            return null;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"{Endpoint}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
