using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using RSW.WebApp.Data;
using RSW.WebApp.Entities;
using RSW.WebApp.Interface.Repositories;

namespace RSW.WebApp.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _Context;
        public CategoryRepository(ApplicationDbContext applicationDbContext)
        {
            _Context = applicationDbContext;
        }
        public async Task<List<Category>> GetAllAsync()
        {
            return await _Context.Categories.Include(C => C.SubCategories).ThenInclude(S => S.criterias).ToListAsync();
        }

        public async Task<Category> GetByNameAsync(string name)
        {
            return await _Context.Categories.Include(C => C.SubCategories).ThenInclude(S => S.criterias).FirstOrDefaultAsync(A => A.Name == name);
        }

        public async Task Save(Category association)
        {
            if(association.Id == 0)
            {
                _Context.Categories.Add(association);
            }
            else
            {
                _Context.Categories.Update(association);
            }

            await _Context.SaveChangesAsync();
        }
        public async Task RevertEdits(Category category)
        {
            var categoryEntry = _Context.Entry(category);
            if (categoryEntry.State == EntityState.Modified)
            {
                categoryEntry.CurrentValues.SetValues(categoryEntry.OriginalValues);
                categoryEntry.State = EntityState.Unchanged;
            }
        }
        public async Task Delete(Category category)
        {
            try
            {
                Category DeleteCategory = await _Context.Categories.FirstOrDefaultAsync(S => S.Id == category.Id);
                _Context.Categories.Remove(DeleteCategory);
                await _Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
