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

        public async Task<IEnumerable<CriteriaDto>> GetAllAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<CriteriaDto>>(Endpoint);

            return result ?? new List<CriteriaDto>();
        }

        public async Task<CriteriaDto?> GetByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<CriteriaDto>($"{Endpoint}/{id}");
        }

        public async Task<IEnumerable<GroupDto>> GetGroupsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<CriteriaDto> CreateAsync(CriteriaCreateDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync(Endpoint, dto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<CriteriaDto>();
            }

            return null;
        }

        public async Task<CriteriaDto?> UpdateAsync(Guid id, CriteriaDto dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"{Endpoint}/{id}", dto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<CriteriaDto>();
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
