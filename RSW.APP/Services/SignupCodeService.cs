using System.Net.Http.Json;
using AutoMapper;
using RSW.Shared.Dto;
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

        public async Task<IEnumerable<SignupCodeReadDto>> GetAllAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<SignupCodeReadDto>>(Endpoint);

            return result ?? new List<SignupCodeReadDto>();
        }

        public async Task<SignupCodeReadDto?> GetByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<SignupCodeReadDto>($"{Endpoint}/{id}");
        }

        public async Task<IEnumerable<GroupReadDto>> GetGroupsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<SignupCodeReadDto> CreateAsync(SignupCodeCreateDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync(Endpoint, dto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<SignupCodeReadDto>();
            }

            return null;
        }

        public async Task<SignupCodeReadDto?> UpdateAsync(Guid id, SignupCodeUpdateDto dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"{Endpoint}/{id}", dto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<SignupCodeReadDto>();
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
