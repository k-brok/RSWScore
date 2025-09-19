using System.Net.Http.Json;
using AutoMapper;
using RSW.Shared.Dto;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.APP.Services
{
    public class ScoutService : IScoutService
    {
        private readonly HttpClient _httpClient;
        private const string Endpoint = "api/scout";

        public ScoutService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ScoutReadDto>> GetAllAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<ScoutReadDto>>(Endpoint);

            return result ?? new List<ScoutReadDto>();
        }

        public async Task<ScoutReadDto?> GetByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<ScoutReadDto>($"{Endpoint}/{id}");
        }

        public async Task<IEnumerable<GroupReadDto>> GetGroupsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ScoutReadDto> CreateAsync(ScoutCreateDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync(Endpoint, dto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ScoutReadDto>();
            }

            return null;
        }

        public async Task<ScoutReadDto?> UpdateAsync(Guid id, ScoutUpdateDto dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"{Endpoint}/{id}", dto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ScoutReadDto>();
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
