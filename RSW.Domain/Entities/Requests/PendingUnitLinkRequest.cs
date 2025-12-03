using RSW.Domain.Enums;

namespace RSW.Domain.Entities;

public class PendingUnitLinkRequest
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string UserId { get; set; } = default!;
    public Guid UnitId { get; set; }
    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
    public UnitLinkRequestStatus Status { get; set; } = UnitLinkRequestStatus.Pending;
    public string? Message { get; set; }
}