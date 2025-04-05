using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using RSW.WebApp.Data;
using RSW.WebApp.Entities;
using RSW.WebApp.Interface.Repositories;

namespace RSW.WebApp.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly ApplicationDbContext _Context;
        public GroupRepository(ApplicationDbContext applicationDbContext)
        {
            _Context = applicationDbContext;
        }
        public async Task<List<Group>> GetAllAsync()
        {
            return await _Context.Groups.Include(G => G.Association).ToListAsync();
        }

        public async Task<Group> GetByNameAsync(string name)
        {
            return await _Context.Groups.FirstOrDefaultAsync(A => A.Name == name);
        }
        public async Task<Group> GetByNameAsync(string name, Association association)
        {
            return await _Context.Groups.Include(G => G.Association).FirstOrDefaultAsync(A => A.Name == name && A.Association.Id == association.Id);
        }
        public async Task<Group> GetAsync(int Id)
        {
            return await _Context.Groups.Include(G => G.Association).FirstOrDefaultAsync(A => A.Id == Id);
        }
        public async Task RevertEdits(Group group)
        {
            var groupEntry = _Context.Entry(group);
            if (groupEntry.State == EntityState.Modified)
            {
                groupEntry.CurrentValues.SetValues(groupEntry.OriginalValues);
                groupEntry.State = EntityState.Unchanged;
            }
        }
        public async Task Save(Group group)
        {
            if(group.Id == 0)
            {
                _Context.Groups.Add(group);
            }
            else
            {
                _Context.Groups.Update(group);
            }

            await _Context.SaveChangesAsync();
        }
        public async Task Delete(Group group)
        {
            try
            {
                Group DeleteGroup = await _Context.Groups.FirstOrDefaultAsync(S => S.Id == group.Id);
                _Context.Groups.Remove(DeleteGroup);
                await _Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
