using System.Net.Http.Json;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.APP.Services
{
    public class JurySlotService : IJurySlotService
    {
        private readonly HttpClient _httpClient;
        private const string Endpoint = "api/juryslot";

        public JurySlotService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<JurySlot>> GetAllAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<JurySlot>>(Endpoint);

            return result ?? new List<JurySlot>();
        }

        public async Task<JurySlot?> GetByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<JurySlot>($"{Endpoint}/{id}");
        }

        public async Task<JurySlot?> CreateAsync(JurySlot model)
        {
            var response = await _httpClient.PostAsJsonAsync(Endpoint, model);

            if (response.IsSuccessStatusCode)
            {
                if (response.Content == null)
                    return null;

                return await response.Content.ReadFromJsonAsync<JurySlot>();
            }

            return null;
        }

        public async Task<JurySlot?> UpdateAsync(Guid id, JurySlot model)
        {
            var response = await _httpClient.PutAsJsonAsync($"{Endpoint}/{id}", model);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<JurySlot>();
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
