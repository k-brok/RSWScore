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

        public async Task<IEnumerable<JurySlotReadDto>> GetAllAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<JurySlotReadDto>>(Endpoint);

            return result ?? new List<JurySlotReadDto>();
        }

        public async Task<JurySlotReadDto?> GetByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<JurySlotReadDto>($"{Endpoint}/{id}");
        }

        public async Task<IEnumerable<GroupReadDto>> GetGroupsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<JurySlotReadDto> CreateAsync(JurySlotCreateDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync(Endpoint, dto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<JurySlotReadDto>();
            }

            return null;
        }

        public async Task<JurySlotReadDto?> UpdateAsync(Guid id, JurySlotUpdateDto dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"{Endpoint}/{id}", dto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<JurySlotReadDto>();
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
