using System.Net.Http.Json;
using AutoMapper;
using RSW.Shared.Dto;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.APP.Services
{
    public class AssociationService : IAssociationService
    {
        private readonly HttpClient _httpClient;
        private const string Endpoint = "api/association";

        public AssociationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<AssociationReadDto>> GetAllAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<AssociationReadDto>>(Endpoint);

            return result ?? new List<AssociationReadDto>();
        }

        public async Task<AssociationReadDto?> GetByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<AssociationReadDto>($"{Endpoint}/{id}");
        }

        public async Task<IEnumerable<GroupReadDto>> GetGroupsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<AssociationReadDto> CreateAsync(AssociationCreateDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync(Endpoint, dto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<AssociationReadDto>();
            }

            return null;
        }

        public async Task<AssociationReadDto?> UpdateAsync(Guid id, AssociationUpdateDto dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"{Endpoint}/{id}", dto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<AssociationReadDto>();
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
