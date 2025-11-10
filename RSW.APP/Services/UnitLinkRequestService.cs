using System.Net.Http.Json;
using RSW.Shared.Dto;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.APP.Services;

public class UnitLinkRequestService : IUnitLinkRequestService
{
    private readonly HttpClient _http;
    private const string Endpoint = "api/unit-links";

    public UnitLinkRequestService(HttpClient http)
    {
        _http = http;
    }

    public async Task<CreateUnitLinkRequestResponse> CreateAsync(CreateUnitLinkRequestRequest req)
    {
        var resp = await _http.PostAsJsonAsync($"{Endpoint}/request", req);
        if (!resp.IsSuccessStatusCode)
        {
            var msg = await resp.Content.ReadFromJsonAsync<CreateUnitLinkRequestResponse>();
            if(msg != null)
                return new CreateUnitLinkRequestResponse { Success = msg.Success, Message = msg.Message};
        }

        return await resp.Content.ReadFromJsonAsync<CreateUnitLinkRequestResponse>()
               ?? new CreateUnitLinkRequestResponse { Success = false, Message = "Onbekende fout." };
    }

    public async Task<IEnumerable<UnitLinkRequestDto>> GetMineAsync()
    {
        var resp = await _http.GetAsync($"{Endpoint}/mine");
        if (!resp.IsSuccessStatusCode) return Enumerable.Empty<UnitLinkRequestDto>();
        return await resp.Content.ReadFromJsonAsync<IEnumerable<UnitLinkRequestDto>>() 
               ?? Enumerable.Empty<UnitLinkRequestDto>();
    }

    public async Task<List<PendingUnitLinkRequest>> GetAllAsync()
    {
        var resp = await _http.GetAsync($"{Endpoint}");
        if (!resp.IsSuccessStatusCode) return Enumerable.Empty<PendingUnitLinkRequest>().ToList();
        return (await resp.Content.ReadFromJsonAsync<IEnumerable<PendingUnitLinkRequest>>()).ToList()
               ?? new List<PendingUnitLinkRequest>();
    }

    public async Task<bool> CancelAsync(Guid requestId)
    {
        var resp = await _http.PostAsync($"{Endpoint}/cancel/{requestId}", null);
        return resp.IsSuccessStatusCode;
    }

    public async Task<bool> ApproveAsync(Guid requestId)
    {
        var resp = await _http.PostAsync($"{Endpoint}/approve/{requestId}", null);
        return resp.IsSuccessStatusCode;
    }

    public async Task<bool> RejectAsync(Guid requestId)
    {
        var resp = await _http.PostAsync($"{Endpoint}/reject/{requestId}", null);
        return resp.IsSuccessStatusCode;
    }
}
