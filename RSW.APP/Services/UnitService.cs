using System.Net.Http.Json;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.APP.Services
{
    public class UnitService : IUnitService
    {
        private readonly HttpClient _httpClient;
        private const string Endpoint = "api/unit";

        public UnitService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Unit>> GetAllAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<Unit>>(Endpoint);

            return result ?? new List<Unit>();
        }

        public async Task<Unit?> GetByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<Unit>($"{Endpoint}/{id}");
        }
        public async Task<Unit> CreateAsync(Unit model)
        {
            var response = await _httpClient.PostAsJsonAsync(Endpoint, model);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Unit>();
            }

            return null;
        }

        public async Task<Unit?> UpdateAsync(Guid id, Unit model)
        {
            var response = await _httpClient.PutAsJsonAsync($"{Endpoint}/{id}", model);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Unit>();
            }

            return null;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"{Endpoint}/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<Patrol>>? GetPatrolsAsync(Guid id)
        {
            var result = await _httpClient.GetFromJsonAsync<List<Patrol>>($"{Endpoint}/{id}/patrols");

            return result ?? new List<Patrol>();
        }
    }
}
