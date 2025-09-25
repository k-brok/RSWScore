using System.Net.Http.Json;
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

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<Category>>(Endpoint);

            return result ?? new List<Category>();
        }

        public async Task<Category?> GetByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<Category>($"{Endpoint}/{id}");
        }

        public async Task<Category> CreateAsync(Category model)
        {
            var response = await _httpClient.PostAsJsonAsync(Endpoint, model);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Category>();
            }

            return null;
        }

        public async Task<Category?> UpdateAsync(Guid id, Category model)
        {
            var response = await _httpClient.PutAsJsonAsync($"{Endpoint}/{id}", model);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Category>();
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
