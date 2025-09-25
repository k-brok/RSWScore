using System.Net.Http.Json;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.APP.Services
{
    public class SubGroupService : ISubGroupService
    {
        private readonly HttpClient _httpClient;
        private const string Endpoint = "api/subgroup";

        public SubGroupService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<SubGroup>> GetAllAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<SubGroup>>(Endpoint);

            return result ?? new List<SubGroup>();
        }

        public async Task<SubGroup?> GetByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<SubGroup>($"{Endpoint}/{id}");
        }

        public async Task<IEnumerable<Unit>> GetGroupsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<SubGroup> CreateAsync(SubGroup model)
        {
            var response = await _httpClient.PostAsJsonAsync(Endpoint, model);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<SubGroup>();
            }

            return null;
        }

        public async Task<SubGroup?> UpdateAsync(Guid id, SubGroup model)
        {
            var response = await _httpClient.PutAsJsonAsync($"{Endpoint}/{id}", model);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<SubGroup>();
            }

            return null;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"{Endpoint}/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<PatrolDto>?> GetPatrolsAsync(Guid id)
        {
            var result = await _httpClient.GetFromJsonAsync<List<PatrolDto>>($"{Endpoint}/{id}/patrols");

            return result ?? new List<PatrolDto>();
        }
    }
}
