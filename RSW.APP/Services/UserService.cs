using System.Net.Http.Json;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.APP.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private const string Endpoint = "api/users";

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<ApplicationUser>>(Endpoint);
            return result ?? new List<ApplicationUser>();
        }

        public async Task<ApplicationUser?> GetByIdAsync(string id)
        {
            return await _httpClient.GetFromJsonAsync<ApplicationUser>($"{Endpoint}/{id}");
        }

        public async Task<ApplicationUser> CreateAsync(ApplicationUser model, string password)
        {
            var response = await _httpClient.PostAsJsonAsync(Endpoint, model);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ApplicationUser>();
            }

            throw new InvalidOperationException("Failed to create user");
        }

        public async Task<ApplicationUser?> UpdateAsync(string id, ApplicationUser model)
        {
            var response = await _httpClient.PutAsJsonAsync($"{Endpoint}/{id}", model);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ApplicationUser>();
            }

            return null;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"{Endpoint}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
