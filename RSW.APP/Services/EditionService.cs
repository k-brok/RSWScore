using System.Net.Http.Json;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.APP.Services
{
    public class EditionService : IEditionService
    {
        private readonly HttpClient _httpClient;
        private const string Endpoint = "api/edition";

        public EditionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Edition>> GetAllAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<Edition>>(Endpoint);

            return result ?? new List<Edition>();
        }

        public async Task<Edition?> GetByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<Edition>($"{Endpoint}/{id}");
        }

        public async Task<IEnumerable<Group>> GetGroupsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Edition> CreateAsync(Edition model)
        {
            var response = await _httpClient.PostAsJsonAsync(Endpoint, model);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Edition>();
            }

            return null;
        }

        public async Task<Edition?> UpdateAsync(Guid id, Edition model)
        {
            var response = await _httpClient.PutAsJsonAsync($"{Endpoint}/{id}", model);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Edition>();
            }

            return null;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"{Endpoint}/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<Edition?> GetActiveAsync()
        {
            return await _httpClient.GetFromJsonAsync<Edition>($"{Endpoint}/active");
        }

        public async Task<Edition?> ActivateAsync(Guid id)
        {
            var response = await _httpClient.PutAsJsonAsync($"{Endpoint}/{id}/activate", (object?)null);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Edition>();
            }

            return null;
        }
    }
}
