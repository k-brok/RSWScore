using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RSW.API.Data;
using RSW.Shared.Dto;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.API.Services
{
    public class ScoreService : IScoreService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHubContext<UpdatesHub> _hub;

        public ScoreService(AppDbContext context, IMapper mapper, IHubContext<UpdatesHub> hub)
        {
            _context = context;
            _mapper = mapper;
            _hub = hub;
        }

        public async Task<IEnumerable<ScoreReadDto>> GetAllAsync()
        {
            return await _context.Scores
                .Select(e => _mapper.Map<ScoreReadDto>(e))
                .ToListAsync();
        }

        public async Task<ScoreReadDto?> GetByIdAsync(Guid id)
        {
            var entity = await _context.Scores.FirstOrDefaultAsync(e => e.Id == id);
            return _mapper.Map<ScoreReadDto>(entity);
        }

        public async Task<ScoreReadDto> CreateAsync(ScoreCreateDto dto)
        {
            var entity = new Score
            {
                Id = Guid.NewGuid(),
                CriteriaId = dto.CriteriaId,
                PatrolId = dto.PatrolId,
                Value = dto.Value
            };

            _context.Scores.Add(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<ScoreReadDto>(entity);
        }

        public async Task<ScoreReadDto?> UpdateAsync(Guid id, ScoreUpdateDto dto)
        {
            var existing = await _context.Scores.FirstOrDefaultAsync(e => e.Id == id);
            if (existing == null) return null;

            existing.CriteriaId = dto.CriteriaId;
            existing.PatrolId = dto.PatrolId;
            existing.Value = dto.Value;

            await _context.SaveChangesAsync();

            await _hub.Clients.All.SendAsync("ScoreUpdate", existing);

            return _mapper.Map<ScoreReadDto>(existing);
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var found = await _context.Scores.FirstOrDefaultAsync(e => e.Id == id);
            if (found == null) return false;

            _context.Scores.Remove(found);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}