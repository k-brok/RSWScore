using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using RSW.WebApp.Data;
using RSW.WebApp.Entities;
using RSW.WebApp.Interface.Repositories;

namespace RSW.WebApp.Repositories
{
    public class AssociationRepository : IAssociationRepository
    {
        private readonly ApplicationDbContext _Context;
        public AssociationRepository(ApplicationDbContext applicationDbContext)
        {
            _Context = applicationDbContext;
        }
        public async Task<List<Association>> GetAllAsync()
        {
            return await _Context.Associations.Include(A => A.Groups).ToListAsync();
        }
        public async Task<Association> GetAsync(int Id)
        {
            return await _Context.Associations.FirstOrDefaultAsync(A => A.Id == Id);
        }
        public async Task<Association> GetByNameAsync(string name)
        {
            return await _Context.Associations.Include(A => A.Groups).ThenInclude(G => G.Patrols).FirstOrDefaultAsync(A => A.Name == name);
        }
        public async Task<Association> GetByAbbAsync(string abbreviation)
        {
            return await _Context.Associations.Include(A => A.Groups).ThenInclude(G => G.Patrols).FirstOrDefaultAsync(A => A.Abbreviation == abbreviation);
        }
        public async Task RevertEdits(Association association)
        {
            var associationEntry = _Context.Entry(association);
            if (associationEntry.State == EntityState.Modified)
            {
                associationEntry.CurrentValues.SetValues(associationEntry.OriginalValues);
                associationEntry.State = EntityState.Unchanged;
            }
        }
        public async Task Save(Association association)
        {
            if(association.Id == 0)
            {
                _Context.Associations.Add(association);
            }
            else
            {
                _Context.Associations.Update(association);
            }

            await _Context.SaveChangesAsync();
        }
        public async Task Delete(Association association)
        {
            try
            {
                Association DeleteAssociation = await _Context.Associations.FirstOrDefaultAsync(S => S.Id == association.Id);
                _Context.Associations.Remove(DeleteAssociation);
                await _Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
