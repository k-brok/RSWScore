using System.Net.Http.Json;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.APP.Services
{
    public class SignupCodeService : ISignupCodeService
    {
        private readonly HttpClient _httpClient;
        private const string Endpoint = "api/signupcode";

        public SignupCodeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<SignupCode>> GetAllAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<SignupCode>>(Endpoint);

            return result ?? new List<SignupCode>();
        }

        public async Task<SignupCode?> GetByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<SignupCode>($"{Endpoint}/{id}");
        }

        public async Task<IEnumerable<Unit>> GetGroupsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<SignupCode> CreateAsync(SignupCode model)
        {
            var response = await _httpClient.PostAsJsonAsync(Endpoint, model);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<SignupCode>();
            }

            return null;
        }

        public async Task<SignupCode?> UpdateAsync(Guid id, SignupCode model)
        {
            var response = await _httpClient.PutAsJsonAsync($"{Endpoint}/{id}", model);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<SignupCode>();
            }

            return null;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"{Endpoint}/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<SignupCode?> ValidateAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<SignupCode>($"{Endpoint}/validate/{id}");
        }
    }
}
