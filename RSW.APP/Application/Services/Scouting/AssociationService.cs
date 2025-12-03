using System.Net.Http.Json;
using RSW.Domain.Entities;
using RSW.Application.Interfaces;

namespace RSW.APP.Application.Services
{
    public class AssociationService : IAssociationService
    {
        private readonly HttpClient _httpClient;
        private const string Endpoint = "api/association";

        public AssociationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Association>> GetAllAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<Association>>(Endpoint);

            return result ?? new List<Association>();
        }

        public async Task<Association?> GetByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<Association>($"{Endpoint}/{id}");
        }

        public async Task<IEnumerable<Unit>> GetUnitsAsync(Guid id)
        {
            
            var result = await _httpClient.GetFromJsonAsync<List<Unit>>($"{Endpoint}/{id}/units");

            return result ?? new List<Unit>();
        }

        public async Task<Association> CreateAsync(Association model)
        {
            var response = await _httpClient.PostAsJsonAsync(Endpoint, model);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Association>();
            }

            return null;
        }

        public async Task<Association?> UpdateAsync(Guid id, Association model)
        {
            var response = await _httpClient.PutAsJsonAsync($"{Endpoint}/{id}", model);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Association>();
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
