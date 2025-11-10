namespace RSW.Shared.Dto;

public enum UnitLinkRequestStatus
{
    Pending = 0,
    Approved = 1,
    Rejected = 2,
    Cancelled = 3
}

public class CreateUnitLinkRequestRequest
{
    public string? UserId { get; set; }           // optioneel; server kan zelf CurrentUser pakken
    public Guid UnitId { get; set; }              // doel-groep
    public string? Message { get; set; }          // optioneel toelichting
}

public class UnitLinkRequestDto
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = default!;
    public Guid UnitId { get; set; }
    public string? UnitName { get; set; }
    public string? AssociationName { get; set; }
    public DateTime CreatedUtc { get; set; }
    public UnitLinkRequestStatus Status { get; set; }
}

public class CreateUnitLinkRequestResponse
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public UnitLinkRequestDto? Request { get; set; }
}
