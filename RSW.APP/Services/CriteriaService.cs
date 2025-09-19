using System.Net.Http.Json;
using AutoMapper;
using RSW.Shared.Dto;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.APP.Services
{
    public class CriteriaService : ICriteriaService
    {
        private readonly HttpClient _httpClient;
        private const string Endpoint = "api/criteria";

        public CriteriaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<CriteriaReadDto>> GetAllAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<CriteriaReadDto>>(Endpoint);

            return result ?? new List<CriteriaReadDto>();
        }

        public async Task<CriteriaReadDto?> GetByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<CriteriaReadDto>($"{Endpoint}/{id}");
        }

        public async Task<IEnumerable<GroupReadDto>> GetGroupsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<CriteriaReadDto> CreateAsync(CriteriaCreateDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync(Endpoint, dto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<CriteriaReadDto>();
            }

            return null;
        }

        public async Task<CriteriaReadDto?> UpdateAsync(Guid id, CriteriaUpdateDto dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"{Endpoint}/{id}", dto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<CriteriaReadDto>();
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
