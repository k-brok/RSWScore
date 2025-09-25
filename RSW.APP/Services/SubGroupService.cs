using System.Net.Http.Json;
using AutoMapper;
using RSW.Shared.Dto;
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

        public async Task<IEnumerable<SubGroupDto>> GetAllAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<SubGroupDto>>(Endpoint);

            return result ?? new List<SubGroupDto>();
        }

        public async Task<SubGroupDto?> GetByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<SubGroupDto>($"{Endpoint}/{id}");
        }

        public async Task<SubGroupDto> CreateAsync(SubGroupCreateDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync(Endpoint, dto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<SubGroupDto>();
            }

            return null;
        }

        public async Task<SubGroupDto?> UpdateAsync(Guid id, SubGroupDto dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"{Endpoint}/{id}", dto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<SubGroupDto>();
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
