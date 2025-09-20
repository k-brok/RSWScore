using System.Net.Http.Json;
using AutoMapper;
using RSW.Shared.Dto;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.APP.Services
{
    public class JurySlotService : IJurySlotService
    {
        private readonly HttpClient _httpClient;
        private const string Endpoint = "api/juryslot";

        public JurySlotService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<JurySlotDto>> GetAllAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<JurySlotDto>>(Endpoint);

            return result ?? new List<JurySlotDto>();
        }

        public async Task<JurySlotDto?> GetByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<JurySlotDto>($"{Endpoint}/{id}");
        }

        public async Task<IEnumerable<GroupDto>> GetGroupsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<JurySlotDto> CreateAsync(JurySlotCreateDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync(Endpoint, dto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<JurySlotDto>();
            }

            return null;
        }

        public async Task<JurySlotDto?> UpdateAsync(Guid id, JurySlotDto dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"{Endpoint}/{id}", dto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<JurySlotDto>();
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
