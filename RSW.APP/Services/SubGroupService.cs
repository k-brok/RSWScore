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

        public async Task<IEnumerable<SubGroupReadDto>> GetAllAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<SubGroupReadDto>>(Endpoint);

            return result ?? new List<SubGroupReadDto>();
        }

        public async Task<SubGroupReadDto?> GetByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<SubGroupReadDto>($"{Endpoint}/{id}");
        }

        public async Task<IEnumerable<GroupReadDto>> GetGroupsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<SubGroupReadDto> CreateAsync(SubGroupCreateDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync(Endpoint, dto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<SubGroupReadDto>();
            }

            return null;
        }

        public async Task<SubGroupReadDto?> UpdateAsync(Guid id, SubGroupUpdateDto dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"{Endpoint}/{id}", dto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<SubGroupReadDto>();
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
