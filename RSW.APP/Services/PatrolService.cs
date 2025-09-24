using System.Net.Http.Json;
using AutoMapper;
using RSW.Shared.Dto;
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

        public async Task<IEnumerable<PatrolDto>> GetAllAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<PatrolDto>>(Endpoint);

            return result ?? new List<PatrolDto>();
        }

        public async Task<PatrolDto?> GetByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<PatrolDto>($"{Endpoint}/{id}");
        }

        public async Task<IEnumerable<ScoutDto>> GetScoutsAsync(Guid id)
        {
            var result = await _httpClient.GetFromJsonAsync<List<ScoutDto>>($"{Endpoint}/{id}/scouts");

            return result ?? new List<ScoutDto>();
        }

        public async Task<IEnumerable<GroupDto>> GetGroupsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<PatrolDto> CreateAsync(PatrolCreateDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync(Endpoint, dto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<PatrolDto>();
            }

            return null;
        }

        public async Task<PatrolDto?> UpdateAsync(Guid id, PatrolDto dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"{Endpoint}/{id}", dto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<PatrolDto>();
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
