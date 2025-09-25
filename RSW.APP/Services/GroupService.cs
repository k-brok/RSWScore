using System.Net.Http.Json;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.APP.Services
{
    public class GroupService : IGroupService
    {
        private readonly HttpClient _httpClient;
        private const string Endpoint = "api/group";

        public GroupService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Group>> GetAllAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<Group>>(Endpoint);

            return result ?? new List<Group>();
        }

        public async Task<Group?> GetByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<Group>($"{Endpoint}/{id}");
        }

        public async Task<IEnumerable<Group>> GetGroupsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Group> CreateAsync(Group model)
        {
            var response = await _httpClient.PostAsJsonAsync(Endpoint, model);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Group>();
            }

            return null;
        }

        public async Task<Group?> UpdateAsync(Guid id, Group model)
        {
            var response = await _httpClient.PutAsJsonAsync($"{Endpoint}/{id}", model);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Group>();
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
