using System.Net.Http.Json;
using AutoMapper;
using RSW.Shared.Dto;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.APP.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;
        private const string Endpoint = "api/category";

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<CategoryReadDto>> GetAllAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<CategoryReadDto>>(Endpoint);

            return result ?? new List<CategoryReadDto>();
        }

        public async Task<CategoryReadDto?> GetByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<CategoryReadDto>($"{Endpoint}/{id}");
        }

        public async Task<IEnumerable<GroupReadDto>> GetGroupsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<CategoryReadDto> CreateAsync(CategoryCreateDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync(Endpoint, dto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<CategoryReadDto>();
            }

            return null;
        }

        public async Task<CategoryReadDto?> UpdateAsync(Guid id, CategoryUpdateDto dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"{Endpoint}/{id}", dto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<CategoryReadDto>();
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
