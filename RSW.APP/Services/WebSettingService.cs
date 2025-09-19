using System.Net.Http.Json;
using AutoMapper;
using RSW.Shared.Dto;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.APP.Services
{
    public class WebSettingService : IWebSettingService
    {
        private readonly HttpClient _httpClient;
        private const string Endpoint = "api/websetting";

        public WebSettingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<WebSettingReadDto>> GetAllAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<WebSettingReadDto>>(Endpoint);

            return result ?? new List<WebSettingReadDto>();
        }

        public async Task<WebSettingReadDto?> GetByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<WebSettingReadDto>($"{Endpoint}/{id}");
        }

        public async Task<IEnumerable<GroupReadDto>> GetGroupsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<WebSettingReadDto> CreateAsync(WebSettingCreateDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync(Endpoint, dto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<WebSettingReadDto>();
            }

            return null;
        }

        public async Task<WebSettingReadDto?> UpdateAsync(Guid id, WebSettingUpdateDto dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"{Endpoint}/{id}", dto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<WebSettingReadDto>();
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
