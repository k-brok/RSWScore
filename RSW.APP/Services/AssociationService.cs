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

        public async Task<IEnumerable<AssociationDto>> GetAllAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<AssociationDto>>(Endpoint);

            return result ?? new List<AssociationDto>();
        }

        public async Task<AssociationDto?> GetByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<AssociationDto>($"{Endpoint}/{id}");
        }

        public async Task<IEnumerable<GroupDto>> GetGroupsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<AssociationDto> CreateAsync(AssociationCreateDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync(Endpoint, dto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<AssociationDto>();
            }

            return null;
        }

        public async Task<AssociationDto?> UpdateAsync(Guid id, AssociationDto dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"{Endpoint}/{id}", dto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<AssociationDto>();
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
