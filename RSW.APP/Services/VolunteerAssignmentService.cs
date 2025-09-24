using System.Net.Http.Json;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.APP.Services
{
    public class VolunteerAssignmentService : IVolunteerAssignmentService
    {
        private readonly HttpClient _httpClient;
        private const string Endpoint = "api/volunteerassignment";

        public VolunteerAssignmentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<VolunteerAssignment>> GetAllAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<VolunteerAssignment>>(Endpoint);

            return result ?? new List<VolunteerAssignment>();
        }

        public async Task<VolunteerAssignment?> GetByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<VolunteerAssignment>($"{Endpoint}/{id}");
        }

        public async Task<VolunteerAssignment> CreateAsync(VolunteerAssignment model)
        {
            var response = await _httpClient.PostAsJsonAsync(Endpoint, model);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<VolunteerAssignment>();
            }

            return null;
        }

        public async Task<VolunteerAssignment?> UpdateAsync(Guid id, VolunteerAssignment model)
        {
            var response = await _httpClient.PutAsJsonAsync($"{Endpoint}/{id}", model);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<VolunteerAssignment>();
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
