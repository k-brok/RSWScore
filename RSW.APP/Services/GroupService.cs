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

        public async Task<IEnumerable<GroupDto>> GetAllAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<GroupDto>>(Endpoint);

            return result ?? new List<GroupDto>();
        }

        public async Task<GroupDto?> GetByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<GroupDto>($"{Endpoint}/{id}");
        }

        public async Task<IEnumerable<GroupDto>> GetGroupsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<GroupDto> CreateAsync(GroupCreateDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync(Endpoint, dto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<GroupDto>();
            }

            return null;
        }

        public async Task<GroupDto?> UpdateAsync(Guid id, GroupDto dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"{Endpoint}/{id}", dto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<GroupDto>();
            }

            return null;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"{Endpoint}/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<PatrolDto>>? GetPatrolsAsync(Guid id)
        {
            var result = await _httpClient.GetFromJsonAsync<List<PatrolDto>>($"{Endpoint}/{id}/patrols");

            return result ?? new List<PatrolDto>();
        }
    }
}
