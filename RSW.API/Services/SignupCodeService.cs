using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RSW.API.Data;
using RSW.Shared.Dto;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.API.Services
{
    public class SignupCodeService : ISignupCodeService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public SignupCodeService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SignupCodeReadDto>> GetAllAsync()
        {
            return await _context.SignupCodes
                .Select(e => _mapper.Map<SignupCodeReadDto>(e))
                .ToListAsync();
        }

        public async Task<SignupCodeReadDto?> GetByIdAsync(Guid id)
        {
            var entity = await _context.SignupCodes.FirstOrDefaultAsync(e => e.Id == id);
            return _mapper.Map<SignupCodeReadDto>(entity);
        }

        public async Task<SignupCodeReadDto> CreateAsync(SignupCodeCreateDto dto)
        {
            var entity = new SignupCode
            {
                Id = Guid.NewGuid(),
                GroupId = dto.GroupId,
            };

            _context.SignupCodes.Add(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<SignupCodeReadDto>(entity);
        }

        public async Task<SignupCodeReadDto?> UpdateAsync(Guid id, SignupCodeUpdateDto dto)
        {
            var existing = await _context.SignupCodes.FirstOrDefaultAsync(e => e.Id == id);
            if (existing == null) return null;

            existing.ExpiryDate = dto.ExpiryDate;
            existing.GroupId = dto.GroupId;
            existing.Lock = dto.Lock;

            await _context.SaveChangesAsync();
            return _mapper.Map<SignupCodeReadDto>(existing);
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var found = await _context.SignupCodes.FirstOrDefaultAsync(e => e.Id == id);
            if (found == null) return false;

            _context.SignupCodes.Remove(found);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<SignupCodeReadDto?> ValidateAsync(Guid id)
        {
            var entity = await _context.SignupCodes.FirstOrDefaultAsync(e => e.Id == id);

            if (entity == null)
                return null;

            if (entity.Lock)
                return null;

            if (entity.ExpiryDate < DateTime.UtcNow)
                return null;

            return _mapper.Map<SignupCodeReadDto>(entity);
        }
    }

}