using Microsoft.AspNetCore.SignalR;
using RSW.Application.Interfaces;
using RSW.Domain.Entities;

public class UpdatesHub : Hub, IUpdatesHub
{
    public Task SendScoreUpdateAsync(Score UpdateScore)
        => Clients.All.SendAsync("ScoreUpdate", UpdateScore);
}