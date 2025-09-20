using System.Net.Http.Json;
using AutoMapper;
using RSW.Shared.Dto;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.APP.Services
{
    public class EditionService : IEditionService
    {
        private readonly HttpClient _httpClient;
        private const string Endpoint = "api/edition";

        public EditionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<EditionDto>> GetAllAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<EditionDto>>(Endpoint);

            return result ?? new List<EditionDto>();
        }

        public async Task<EditionDto?> GetByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<EditionDto>($"{Endpoint}/{id}");
        }

        public async Task<IEnumerable<GroupDto>> GetGroupsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<EditionDto> CreateAsync(EditionCreateDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync(Endpoint, dto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<EditionDto>();
            }

            return null;
        }

        public async Task<EditionDto?> UpdateAsync(Guid id, EditionDto dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"{Endpoint}/{id}", dto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<EditionDto>();
            }

            return null;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"{Endpoint}/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<EditionDto?> GetActiveAsync()
        {
            return await _httpClient.GetFromJsonAsync<EditionDto>($"{Endpoint}/active");
        }

        public async Task<EditionDto?> ActivateAsync(Guid id)
        {
            var response = await _httpClient.PutAsJsonAsync($"{Endpoint}/{id}/activate", (object?)null);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<EditionDto>();
            }

            return null;
        }
    }
}
