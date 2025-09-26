using System.Net.Http.Json;
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

        public async Task<IEnumerable<SubCategory>> GetAllAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<SubCategory>>(Endpoint);

            return result ?? new List<SubCategory>();
        }

        public async Task<SubCategory?> GetByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<SubCategory>($"{Endpoint}/{id}");
        }

        public async Task<IEnumerable<Unit>> GetGroupsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<SubCategory> CreateAsync(SubCategory model)
        {
            var response = await _httpClient.PostAsJsonAsync(Endpoint, model);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<SubCategory>();
            }

            return null;
        }

        public async Task<SubCategory?> UpdateAsync(Guid id, SubCategory model)
        {
            var response = await _httpClient.PutAsJsonAsync($"{Endpoint}/{id}", model);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<SubCategory>();
            }

            return null;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"{Endpoint}/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<Criteria>> GetCriteriasAsync(Guid id)
        {
            var result = await _httpClient.GetFromJsonAsync<List<Criteria>>($"{Endpoint}/{id}/criterias");

            return result ?? new List<Criteria>();
        }
    }
}
