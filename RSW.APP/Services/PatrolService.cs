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

        public async Task<IEnumerable<PatrolReadDto>> GetAllAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<PatrolReadDto>>(Endpoint);

            return result ?? new List<PatrolReadDto>();
        }

        public async Task<PatrolReadDto?> GetByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<PatrolReadDto>($"{Endpoint}/{id}");
        }

        public async Task<IEnumerable<GroupReadDto>> GetGroupsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<PatrolReadDto> CreateAsync(PatrolCreateDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync(Endpoint, dto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<PatrolReadDto>();
            }

            return null;
        }

        public async Task<PatrolReadDto?> UpdateAsync(Guid id, PatrolUpdateDto dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"{Endpoint}/{id}", dto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<PatrolReadDto>();
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
