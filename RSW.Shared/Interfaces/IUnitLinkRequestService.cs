using RSW.Shared.Dto;
using RSW.Shared.Entities;

namespace RSW.Shared.Interfaces;

public interface IUnitLinkRequestService
{
    Task<CreateUnitLinkRequestResponse> CreateAsync(CreateUnitLinkRequestRequest req);
    Task<IEnumerable<UnitLinkRequestDto>> GetMineAsync();
    Task<List<PendingUnitLinkRequest>> GetAllAsync();
    Task<bool> CancelAsync(Guid requestId);
    Task<bool> ApproveAsync(Guid requestId);
    Task<bool> RejectAsync(Guid requestId);
}
