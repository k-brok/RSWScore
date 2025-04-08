using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using RSW.WebApp.Data;
using RSW.WebApp.Entities;

namespace RSW.WebApp.Repositories
{
    public class CriteriaRepository : ICriteriaRepository
    {
        private readonly ApplicationDbContext _Context;
        public CriteriaRepository(ApplicationDbContext applicationDbContext)
        {
            _Context = applicationDbContext;
        }
        public async Task<List<Criteria>> GetAllAsync()
        {
            return await _Context.Criterias.ToListAsync();
        }

        public async Task Save(Criteria association)
        {
            if(association.Id == 0)
            {
                _Context.Criterias.Add(association);
            }
            else
            {
                _Context.Criterias.Update(association);
            }

            await _Context.SaveChangesAsync();
        }
        public async Task RevertEdits(Criteria criteria)
        {
            var criteriaEntry = _Context.Entry(criteria);
            if (criteriaEntry.State == EntityState.Modified)
            {
                criteriaEntry.CurrentValues.SetValues(criteriaEntry.OriginalValues);
                criteriaEntry.State = EntityState.Unchanged;
            }
        }
        public async Task Delete(Criteria criteria)
        {
            try
            {
                Criteria DeleteCriteria = await _Context.Criterias.FirstOrDefaultAsync(S => S.Id == criteria.Id);
                _Context.Criterias.Remove(DeleteCriteria);
                await _Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
