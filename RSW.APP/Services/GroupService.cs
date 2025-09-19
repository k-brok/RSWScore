using System.Net.Http.Json;
using AutoMapper;
using RSW.Shared.Dto;
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

        public async Task<IEnumerable<GroupReadDto>> GetAllAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<GroupReadDto>>(Endpoint);

            return result ?? new List<GroupReadDto>();
        }

        public async Task<GroupReadDto?> GetByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<GroupReadDto>($"{Endpoint}/{id}");
        }

        public async Task<IEnumerable<GroupReadDto>> GetGroupsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<GroupReadDto> CreateAsync(GroupCreateDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync(Endpoint, dto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<GroupReadDto>();
            }

            return null;
        }

        public async Task<GroupReadDto?> UpdateAsync(Guid id, GroupUpdateDto dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"{Endpoint}/{id}", dto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<GroupReadDto>();
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
