using System.Net.Http.Json;
using AutoMapper;
using RSW.Shared.Dto;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.APP.Services
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly HttpClient _httpClient;
        private const string Endpoint = "api/subcategory";

        public SubCategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<SubCategoryReadDto>> GetAllAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<SubCategoryReadDto>>(Endpoint);

            return result ?? new List<SubCategoryReadDto>();
        }

        public async Task<SubCategoryReadDto?> GetByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<SubCategoryReadDto>($"{Endpoint}/{id}");
        }

        public async Task<IEnumerable<GroupReadDto>> GetGroupsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<SubCategoryReadDto> CreateAsync(SubCategoryCreateDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync(Endpoint, dto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<SubCategoryReadDto>();
            }

            return null;
        }

        public async Task<SubCategoryReadDto?> UpdateAsync(Guid id, SubCategoryUpdateDto dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"{Endpoint}/{id}", dto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<SubCategoryReadDto>();
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
