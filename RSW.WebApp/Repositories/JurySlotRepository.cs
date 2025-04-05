using Microsoft.EntityFrameworkCore;
using RSW.WebApp.Data;
using RSW.WebApp.Entities;
using RSW.WebApp.Interface.Repositories;

namespace RSW.WebApp.Repositories
{
    public class JurySlotRepository : IJurySlotRepository
    {
        private readonly ApplicationDbContext _context;
        public JurySlotRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public async Task<JurySlot> GetByCode(string code)
        {
            JurySlot FoundSlot = await _context.JurySlots
                .Include(J => J.SubGroup)
                    .ThenInclude(S => S.patrols)
                        .ThenInclude(P => P.Scores)
                .Include(J => J.Category)
                .Include(J => J.Edition)
                .FirstOrDefaultAsync(J => J.Code == code);

            if (FoundSlot == null)
                return null;

            Console.WriteLine(DateTime.UtcNow.ToString());

            if (FoundSlot.OpeningTime > DateTime.UtcNow || FoundSlot.ClosingTime < DateTime.UtcNow)
                return null;

            return FoundSlot;
        }
        public async Task<List<JurySlot>> GetAllAsync()
        {
            return await _context.JurySlots
                .Include(J => J.SubGroup)
                    .ThenInclude(S => S.patrols)
                        .ThenInclude(P => P.Scores)
                .Include(J => J.Category)
                .Include(J => J.Edition)
                .ToListAsync();
        }

        public async Task Save(JurySlot association)
        {
            if (association.Id == 0)
            {
                _context.JurySlots.Add(association);
            }
            else
            {
                _context.JurySlots.Update(association);
            }

            await _context.SaveChangesAsync();
        }
        public async Task RevertEdits(JurySlot juryslot)
        {
            var juryslotEntry = _context.Entry(juryslot);
            if (juryslotEntry.State == EntityState.Modified)
            {
                juryslotEntry.CurrentValues.SetValues(juryslotEntry.OriginalValues);
                juryslotEntry.State = EntityState.Unchanged;
            }
        }
        public async Task Delete(JurySlot juryslot)
        {
            try
            {
                JurySlot DeleteJurySlot = await _context.JurySlots.FirstOrDefaultAsync(S => S.Id == juryslot.Id);
                _context.JurySlots.Remove(DeleteJurySlot);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public async Task<string> GetCode(Category category, SubGroup subGroup, Edition edition)
        {
            string code = "Unknown";

            JurySlot Slot = await _context.JurySlots.FirstOrDefaultAsync(J => J.SubgroupId == subGroup.Id && J.CategoryId == category.Id && J.EditionId == edition.Id);

            if (Slot != null)
                code = Slot.Code;

            return code;
        }
    }
}
