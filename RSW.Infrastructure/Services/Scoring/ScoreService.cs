
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RSW.Infrastructure.Data;
using RSW.Domain.Entities;
using RSW.Application.Interfaces;

namespace RSW.Infrastructure.Services
{
    public class ScoreService : IScoreService
    {
        private readonly AppDbContext _context;
        private readonly IUpdatesHub _hub;

        public ScoreService(AppDbContext context, IUpdatesHub updatesHub)
        {
            _context = context;
            _hub = updatesHub;
        }

        public async Task<IEnumerable<Score>> GetAllAsync()
        {
            return await _context.Scores
                .ToListAsync();
        }

        public async Task<Score?> GetByIdAsync(Guid id)
        {
            var entity = await _context.Scores.FirstOrDefaultAsync(e => e.Id == id);
            return entity;
        }

        public async Task<Score> CreateAsync(Score model)
        {
            var entity = new Score
            {
                Id = Guid.NewGuid(),
                CriteriaId = model.CriteriaId,
                PatrolId = model.PatrolId,
                Value = model.Value
            };

            _context.Scores.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<Score?> UpdateAsync(Guid id, Score model)
        {
            var existing = await _context.Scores.FirstOrDefaultAsync(e => e.Id == id);
            if (existing == null) return null;

            existing.CriteriaId = model.CriteriaId;
            existing.PatrolId = model.PatrolId;
            existing.Value = model.Value;

            await _context.SaveChangesAsync();

            return existing;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var found = await _context.Scores.FirstOrDefaultAsync(e => e.Id == id);
            if (found == null) return false;

            _context.Scores.Remove(found);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Score?> SetValueAsync(Score model)
        {
            Console.WriteLine($" nieuwe waarde is: {model.Value}");
            var FoundScore = await _context.Scores.FirstOrDefaultAsync(s => s.PatrolId == model.PatrolId && s.CriteriaId == model.CriteriaId);
            if (FoundScore == null)
            {
                _context.Scores.Add(model);
            }
            else
            {
                FoundScore.Value = model.Value;
            }
            await _context.SaveChangesAsync();

            await _hub.SendScoreUpdateAsync(FoundScore);

            return FoundScore;
        }
    }

}