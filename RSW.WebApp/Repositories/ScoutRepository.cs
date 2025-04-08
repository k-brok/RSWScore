using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using RSW.WebApp.Data;
using RSW.WebApp.Entities;
using RSW.WebApp.Interface.Repositories;

namespace RSW.WebApp.Repositories
{
    public class ScoutRepository : IScoutRepository
    {
        private readonly ApplicationDbContext _Context;
        public ScoutRepository(ApplicationDbContext applicationDbContext)
        {
            _Context = applicationDbContext;
        }
        public async Task<List<Scout>> GetAsync()
        {
            return await _Context.Scouts
                .ToListAsync();
        }
        public async Task<Scout> GetAsync(int Id)
        {
            return await _Context.Scouts.FirstOrDefaultAsync(P => P.Id == Id);
        }
        public async Task Save(Scout scout)
        {
            if(scout.Id == 0)
            {
                _Context.Scouts.Add(scout);
                
            }
            else
            {
                Scout UpdateScout = await _Context.Scouts.FirstOrDefaultAsync(P => P.Id == scout.Id);
                if(UpdateScout != null)
                {
                    _Context.Entry(UpdateScout).CurrentValues.SetValues(scout);
                }
            }

            await _Context.SaveChangesAsync();
        }
        public async Task RevertEdits(Scout scout)
        {
            var scoutEntry = _Context.Entry(scout);
            if (scoutEntry.State == EntityState.Modified)
            {
                scoutEntry.CurrentValues.SetValues(scoutEntry.OriginalValues);
                scoutEntry.State = EntityState.Unchanged;
            }
        }
        public async Task Delete(Scout scout)
        {
            try
            {
                Scout DeleteScout = await _Context.Scouts.FirstOrDefaultAsync(S => S.Id == scout.Id);
                _Context.Scouts.Remove(DeleteScout);
                await _Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
