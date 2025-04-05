using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using RSW.WebApp.Data;
using RSW.WebApp.Entities;
using RSW.WebApp.Interface.Repositories;

namespace RSW.WebApp.Repositories
{
    public class EditionRepository : IEditionRepository
    {
        private readonly ApplicationDbContext _Context;
        public EditionRepository(ApplicationDbContext applicationDbContext)
        {
            _Context = applicationDbContext;
        }
        public async Task<List<Edition>> GetAllAsync()
        {
            return await _Context.Editions.Include(A => A.SubGroups).ThenInclude(S => S.patrols).ToListAsync();
        }

        public async Task<Edition> GetByYearAsync(int year)
        {
            return await _Context.Editions
                .Include(A => A.SubGroups)
                    .ThenInclude(S => S.patrols)
                        .ThenInclude(P => P.Scores)
                .Include(A => A.SubGroups)
                    .ThenInclude(S => S.patrols)
                        .ThenInclude(P => P.Scouts)
                .FirstOrDefaultAsync(A => A.RSWStartDate.Year == year);
        }

        public async Task<List<int>> GetListYears()
        {
            return await _Context.Editions.Select(E => E.RSWStartDate.Year).ToListAsync();
        }

        public async Task Save(Edition edition)
        {
            try{
                if (edition.Id == 0)
                {
                    _Context.Editions.Add(edition);
                }
                else
                {
                    _Context.Editions.Update(edition);
                }

                await _Context.SaveChangesAsync();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public async Task Activate(Edition edition)
        {
            List<Edition> ActiveEditions = await _Context.Editions.Where(E => E.IsActive).ToListAsync();
            foreach (Edition ActiveEdition in ActiveEditions) {
                ActiveEdition.IsActive = false;
            }
            _Context.Editions
            .FirstOrDefault(E => E.Id == edition.Id).IsActive = true;

            await _Context.SaveChangesAsync();
        }
        public async Task<Edition> GetActive()
        {
            return await _Context.Editions
                .Include(E => E.SubGroups)
                    .ThenInclude(S => S.patrols)
                        .ThenInclude(P => P.Scores)
                .Include(E => E.SubGroups)
                    .ThenInclude(S => S.patrols)
                        .ThenInclude(P => P.Scouts)
                .Include(E => E.SubGroups)
                    .ThenInclude(S => S.patrols)
                        .ThenInclude(P => P.Group)
                            .ThenInclude(G => G.Association)
                .FirstOrDefaultAsync(E => E.IsActive == true);
        }
        public async Task RevertEdits(Edition edition)
        {
            var editionEntry = _Context.Entry(edition);
            if (editionEntry.State == EntityState.Modified)
            {
                editionEntry.CurrentValues.SetValues(editionEntry.OriginalValues);
                editionEntry.State = EntityState.Unchanged;
            }
        }
        public async Task Delete(Edition edition)
        {
            try
            {
                Edition DeleteEdition = await _Context.Editions.FirstOrDefaultAsync(S => S.Id == edition.Id);
                _Context.Editions.Remove(DeleteEdition);
                await _Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
