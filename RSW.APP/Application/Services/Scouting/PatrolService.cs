using System.Net.Http.Json;
using RSW.Domain.Entities;
using RSW.Application.Interfaces;

namespace RSW.APP.Application.Services
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

        public async Task<IEnumerable<Score>> GetScoresAsync(Guid id)
        {
            var result = await _httpClient.GetFromJsonAsync<List<Score>>($"{Endpoint}/{id}/scores");

            return result ?? new List<Score>();
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
