using RSW.Shared.Dto;

namespace RSW.Shared.Entities;

public class EmailConfig
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = default!;
    public string? Subject { get; set; } = default;
    public string Template { get; set; } = default!;
}