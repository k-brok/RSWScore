using System.Net.Http.Json;
using AutoMapper;
using RSW.Shared.Dto;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.APP.Services
{
    public class ScoreService : IScoreService
    {
        private readonly HttpClient _httpClient;
        private const string Endpoint = "api/score";

        public ScoreService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ScoreDto>> GetAllAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<ScoreDto>>(Endpoint);

            return result ?? new List<ScoreDto>();
        }

        public async Task<ScoreDto?> GetByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<ScoreDto>($"{Endpoint}/{id}");
        }

        public async Task<IEnumerable<GroupDto>> GetGroupsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ScoreDto> CreateAsync(ScoreCreateDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync(Endpoint, dto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ScoreDto>();
            }

            return null;
        }

        public async Task<ScoreDto?> UpdateAsync(Guid id, ScoreDto dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"{Endpoint}/{id}", dto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ScoreDto>();
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
