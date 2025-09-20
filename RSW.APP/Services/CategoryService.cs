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

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<CategoryDto>>(Endpoint);

            return result ?? new List<CategoryDto>();
        }

        public async Task<CategoryDto?> GetByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<CategoryDto>($"{Endpoint}/{id}");
        }

        public async Task<IEnumerable<GroupDto>> GetGroupsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<CategoryDto> CreateAsync(CategoryCreateDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync(Endpoint, dto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<CategoryDto>();
            }

            return null;
        }

        public async Task<CategoryDto?> UpdateAsync(Guid id, CategoryDto dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"{Endpoint}/{id}", dto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<CategoryDto>();
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
