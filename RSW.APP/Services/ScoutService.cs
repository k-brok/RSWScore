using System.Net.Http.Json;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.APP.Services
{
    public class ScoutService : IScoutService
    {
        private readonly HttpClient _httpClient;
        private const string Endpoint = "api/scout";

        public ScoutService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Scout>> GetAllAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<Scout>>(Endpoint);

            return result ?? new List<Scout>();
        }

        public async Task<Scout?> GetByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<Scout>($"{Endpoint}/{id}");
        }

        public async Task<IEnumerable<Group>> GetGroupsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Scout> CreateAsync(Scout model)
        {
            var response = await _httpClient.PostAsJsonAsync(Endpoint, model);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Scout>();
            }

            return null;
        }

        public async Task<Scout?> UpdateAsync(Guid id, Scout model)
        {
            var response = await _httpClient.PutAsJsonAsync($"{Endpoint}/{id}", model);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Scout>();
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
