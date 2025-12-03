using RSW.Domain.Entities;

namespace RSW.Application.Interfaces;

public interface IUpdatesHub
{
    Task SendScoreUpdateAsync(Score UpdateScore);
}