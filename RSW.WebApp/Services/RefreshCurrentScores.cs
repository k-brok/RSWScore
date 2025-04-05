using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RSW.WebApp.Entities;
using RSW.WebApp.Repositories;

namespace RSW.WebApp.Services
{
    public class RefreshCurrentScores : BackgroundService
    {
        private readonly ILogger<RefreshCurrentScores> _logger;
        public event Action OnRefreshRequested; // Event dat de UI kan abonneren

        public List<Patrol> Top10 { get; set; } = new List<Patrol>();

        public RefreshCurrentScores(ILogger<RefreshCurrentScores> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Background service running...");
                
                OnRefreshRequested?.Invoke(); // Ververs UI via event
                await Task.Delay(10000, stoppingToken); // 10 s wachten
            }
        }
    }
}

