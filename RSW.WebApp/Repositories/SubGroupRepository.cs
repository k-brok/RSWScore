using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using RSW.WebApp.Data;
using RSW.WebApp.Entities;

namespace RSW.WebApp.Repositories
{
    public class SubGroupRepository : ISubGroupRepository
    {
        private readonly ApplicationDbContext _Context;
        public SubGroupRepository(ApplicationDbContext applicationDbContext)
        {
            _Context = applicationDbContext;
        }
        public async Task<List<SubGroup>> GetAllAsync()
        {
            return await _Context.SubGroups.ToListAsync();
        }

        public async Task Save(SubGroup association)
        {
            if(association.Id == 0)
            {
                _Context.SubGroups.Add(association);
            }
            else
            {
                _Context.SubGroups.Update(association);
            }

            await _Context.SaveChangesAsync();
        }
        public async Task RevertEdits(SubGroup subgroup)
        {
            var subgroupEntry = _Context.Entry(subgroup);
            if (subgroupEntry.State == EntityState.Modified)
            {
                subgroupEntry.CurrentValues.SetValues(subgroupEntry.OriginalValues);
                subgroupEntry.State = EntityState.Unchanged;
            }
        }
        public async Task Delete(SubGroup subgroup)
        {
            try
            {
                SubGroup DeleteSubGroup = await _Context.SubGroups.FirstOrDefaultAsync(S => S.Id == subgroup.Id);
                _Context.SubGroups.Remove(DeleteSubGroup);
                await _Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
