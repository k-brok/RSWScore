using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using RSW.WebApp.Data;
using RSW.WebApp.Entities;

namespace RSW.WebApp.Repositories
{
    public class SubCategoryRepository : ISubCategoryRepository
    {
        private readonly ApplicationDbContext _Context;
        public SubCategoryRepository(ApplicationDbContext applicationDbContext)
        {
            _Context = applicationDbContext;
        }
        public async Task<List<SubCategory>> GetAllAsync()
        {
            return await _Context.SubCategories.ToListAsync();
        }

        public async Task<SubCategory> GetByNameAsync(string name)
        {
            return await _Context.SubCategories.FirstOrDefaultAsync(A => A.Name == name);
        }

        public async Task Save(SubCategory association)
        {
            if(association.Id == 0)
            {
                _Context.SubCategories.Add(association);
            }
            else
            {
                _Context.SubCategories.Update(association);
            }

            await _Context.SaveChangesAsync();
        }
        public async Task RevertEdits(SubCategory subcategory)
        {
            var subcategoryEntry = _Context.Entry(subcategory);
            if (subcategoryEntry.State == EntityState.Modified)
            {
                subcategoryEntry.CurrentValues.SetValues(subcategoryEntry.OriginalValues);
                subcategoryEntry.State = EntityState.Unchanged;
            }
        }
        public async Task Delete(SubCategory subcategory)
        {
            try
            {
                SubCategory DeleteSubCategory = await _Context.SubCategories.FirstOrDefaultAsync(S => S.Id == subcategory.Id);
                _Context.SubCategories.Remove(DeleteSubCategory);
                await _Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
