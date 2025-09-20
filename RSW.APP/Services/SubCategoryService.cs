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

        public async Task<IEnumerable<SubCategoryDto>> GetAllAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<SubCategoryDto>>(Endpoint);

            return result ?? new List<SubCategoryDto>();
        }

        public async Task<SubCategoryDto?> GetByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<SubCategoryDto>($"{Endpoint}/{id}");
        }

        public async Task<IEnumerable<GroupDto>> GetGroupsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<SubCategoryDto> CreateAsync(SubCategoryCreateDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync(Endpoint, dto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<SubCategoryDto>();
            }

            return null;
        }

        public async Task<SubCategoryDto?> UpdateAsync(Guid id, SubCategoryDto dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"{Endpoint}/{id}", dto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<SubCategoryDto>();
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
