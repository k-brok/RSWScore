using System.Linq;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using RSW.WebApp.Data;
using RSW.WebApp.Entities;
using RSW.WebApp.Interface.Repositories;

namespace RSW.WebApp.Repositories
{
    public class PatrolRepository : IPatrolRepository
    {
        private readonly ApplicationDbContext _Context;
        public PatrolRepository(ApplicationDbContext applicationDbContext)
        {
            _Context = applicationDbContext;
        }
        public async Task<List<Patrol>> GetAsync()
        {
            return await _Context.Patrols
                .Include(P => P.Scouts)
                .Include(P => P.SubGroup)
                    .ThenInclude(S=>S.Edition)
                        .ThenInclude(E => E.SubGroups)
                .ToListAsync();
        }
        public async Task<Patrol> GetAsync(int Id)
        {
            return await _Context.Patrols.Include(P => P.Scouts).FirstOrDefaultAsync(P => P.Id == Id);
        }
        public async Task Save(Patrol patrol)
        {
            if(patrol.Id == 0)
            {
                _Context.Patrols.Add(patrol);
                
            }
            else
            {
                Patrol UpdatePatrol = await _Context.Patrols.FirstOrDefaultAsync(P => P.Id == patrol.Id);
                if(UpdatePatrol != null)
                {
                    _Context.Entry(UpdatePatrol).CurrentValues.SetValues(patrol);
                }
            }

            await _Context.SaveChangesAsync();
        }
        public async Task RevertEdits(Patrol patrol)
        {
            var patrolEntry = _Context.Entry(patrol);
            if (patrolEntry.State == EntityState.Modified)
            {
                patrolEntry.CurrentValues.SetValues(patrolEntry.OriginalValues);
                patrolEntry.State = EntityState.Unchanged;
            }
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
        public async Task Delete(Patrol patrol)
        {
            try
            {
                Patrol DeletePatrol = await GetAsync(patrol.Id);
                _Context.Patrols.Remove(DeletePatrol);
                await _Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public async Task Delete(Scout scout)
        {
            try
            {
                Scout DeleteScout = await _Context.Patrols.SelectMany(P => P.Scouts).FirstOrDefaultAsync(S => S.Id == scout.Id);
                if (DeleteScout != null)
                {
                    _Context.Scouts.Remove(DeleteScout);
                    await _Context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public async Task<List<Patrol>> GetAsync(List<SubGroup> subGroups)
        {
            return await _Context.Patrols.Where(P => subGroups.Select(S => S.Id).ToList().Contains((int)P.SubGroupId)).ToListAsync();
        }

        public async Task<List<Patrol>> GetByGroupIdAsync(int GroupId)
        {
            return await _Context.Patrols
                .Include(P => P.Scouts)
                .Include(P => P.SubGroup)
                    .ThenInclude(S => S.Edition)
                        .ThenInclude(E => E.SubGroups)
                .Where(P => P.GroupId == GroupId).ToListAsync();
        }
    }
}
