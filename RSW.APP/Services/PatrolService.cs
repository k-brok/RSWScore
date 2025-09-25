using System.Net.Http.Json;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.APP.Services
{
    public class PatrolService : IPatrolService
    {
        private readonly HttpClient _httpClient;
        private const string Endpoint = "api/patrol";

        public PatrolService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Patrol>> GetAllAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<Patrol>>(Endpoint);

            return result ?? new List<Patrol>();
        }

        public async Task<Patrol?> GetByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<Patrol>($"{Endpoint}/{id}");
        }

        public async Task<IEnumerable<Scout>> GetScoutsAsync(Guid id)
        {
            var result = await _httpClient.GetFromJsonAsync<List<Scout>>($"{Endpoint}/{id}/scouts");

            return result ?? new List<Scout>();
        }

        public async Task<IEnumerable<Group>> GetGroupsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Patrol> CreateAsync(Patrol model)
        {
            var response = await _httpClient.PostAsJsonAsync(Endpoint, model);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Patrol>();
            }

            return null;
        }

        public async Task<Patrol?> UpdateAsync(Guid id, Patrol model)
        {
            var response = await _httpClient.PutAsJsonAsync($"{Endpoint}/{id}", model);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Patrol>();
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
