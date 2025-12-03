using System.Net.Http.Json;
using RSW.Domain.Entities;
using RSW.Application.Interfaces;

namespace RSW.APP.Application.Services
{
    public class VolunteerTaskService : IVolunteerTaskService
    {
        private readonly HttpClient _httpClient;
        private const string Endpoint = "api/volunteertask";

        public VolunteerTaskService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<VolunteerTask>> GetAllAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<VolunteerTask>>(Endpoint);

            return result ?? new List<VolunteerTask>();
        }

        public async Task<VolunteerTask?> GetByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<VolunteerTask>($"{Endpoint}/{id}");
        }

        public async Task<VolunteerTask> CreateAsync(VolunteerTask model)
        {
            var response = await _httpClient.PostAsJsonAsync(Endpoint, model);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<VolunteerTask>();
            }

            return null;
        }

        public async Task<VolunteerTask?> UpdateAsync(Guid id, VolunteerTask model)
        {
            var response = await _httpClient.PutAsJsonAsync($"{Endpoint}/{id}", model);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<VolunteerTask>();
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
