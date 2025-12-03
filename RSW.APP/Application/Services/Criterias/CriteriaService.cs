using System.Net.Http.Json;
using RSW.Domain.Entities;
using RSW.Application.Interfaces;

namespace RSW.APP.Application.Services
{
    public class CriteriaService : ICriteriaService
    {
        private readonly HttpClient _httpClient;
        private const string Endpoint = "api/criteria";

        public CriteriaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Criteria>> GetAllAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<Criteria>>(Endpoint);

            return result ?? new List<Criteria>();
        }

        public async Task<Criteria?> GetByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<Criteria>($"{Endpoint}/{id}");
        }

        public async Task<Criteria> CreateAsync(Criteria model)
        {
            var response = await _httpClient.PostAsJsonAsync(Endpoint, model);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Criteria>();
            }

            return null;
        }

        public async Task<Criteria?> UpdateAsync(Guid id, Criteria model)
        {
            var response = await _httpClient.PutAsJsonAsync($"{Endpoint}/{id}", model);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Criteria>();
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
