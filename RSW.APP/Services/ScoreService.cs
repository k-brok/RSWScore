using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.APP.Services
{
    public class ScoreService : IScoreService
    {
        private readonly HttpClient _httpClient;
        private const string Endpoint = "api/score";

        public ScoreService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Score>> GetAllAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<Score>>(Endpoint);

            return result ?? new List<Score>();
        }

        public async Task<Score?> GetByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<Score>($"{Endpoint}/{id}");
        }

        public async Task<IEnumerable<Unit>> GetGroupsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Score> CreateAsync(Score model)
        {
            var response = await _httpClient.PostAsJsonAsync(Endpoint, model);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Score>();
            }

            return null;
        }

        public async Task<Score?> UpdateAsync(Guid id, Score model)
        {
            var response = await _httpClient.PutAsJsonAsync($"{Endpoint}/{id}", model);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Score>();
            }

            return null;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"{Endpoint}/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<Score?> SetValueAsync(Score model)
        {
            Console.WriteLine($"Nieuwe waarde is {model.Value}");
            var response = await _httpClient.PostAsJsonAsync($"{Endpoint}/setvalue", model);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Score>();
            }

            return null;
        }
    }
}
