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

        public async Task<IEnumerable<EditionReadDto>> GetAllAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<EditionReadDto>>(Endpoint);

            return result ?? new List<EditionReadDto>();
        }

        public async Task<EditionReadDto?> GetByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<EditionReadDto>($"{Endpoint}/{id}");
        }

        public async Task<IEnumerable<GroupReadDto>> GetGroupsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<EditionReadDto> CreateAsync(EditionCreateDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync(Endpoint, dto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<EditionReadDto>();
            }

            return null;
        }

        public async Task<EditionReadDto?> UpdateAsync(Guid id, EditionUpdateDto dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"{Endpoint}/{id}", dto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<EditionReadDto>();
            }

            return null;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"{Endpoint}/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<EditionReadDto?> GetActiveAsync()
        {
            return await _httpClient.GetFromJsonAsync<EditionReadDto>($"{Endpoint}/active");
        }

        public async Task<EditionReadDto?> ActivateAsync(Guid id)
        {
            var response = await _httpClient.PutAsJsonAsync($"{Endpoint}/{id}/activate", (object?)null);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<EditionReadDto>();
            }

            return null;
        }
    }
}
