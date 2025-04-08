using Microsoft.EntityFrameworkCore;
using RSW.WebApp.Data;
using RSW.WebApp.Entities;
using RSW.WebApp.Interface.Repositories;

namespace RSW.WebApp.Repositories
{
    public class WebSettingRepository : IWebSettingRepository
    {
        private readonly ApplicationDbContext _Context;
        public WebSettingRepository(ApplicationDbContext applicationDbContext)
        {
            _Context = applicationDbContext;
        }
        public async Task<List<WebSetting>> GetAllAsync()
        {
            return await _Context.WebSettings.ToListAsync();
        }

        public async Task<WebSetting> GetByKeyAsync(string key)
        {
            return await _Context.WebSettings.FirstOrDefaultAsync(A => A.Key == key);
        }

        public async Task Save(WebSetting websetting)
        {
            if (websetting.Id == 0)
            {
                _Context.WebSettings.Add(websetting);
            }
            else
            {
                _Context.WebSettings.Update(websetting);
            }

            await _Context.SaveChangesAsync();
        }
        public async Task RevertEdits(WebSetting websetting)
        {
            var websettingEntry = _Context.Entry(websetting);
            if (websettingEntry.State == EntityState.Modified)
            {
                websettingEntry.CurrentValues.SetValues(websettingEntry.OriginalValues);
                websettingEntry.State = EntityState.Unchanged;
            }
        }
        public async Task Delete(WebSetting websetting)
        {
            try
            {
                WebSetting DeleteWebSetting = await _Context.WebSettings.FirstOrDefaultAsync(S => S.Id == websetting.Id);
                _Context.WebSettings.Remove(DeleteWebSetting);
                await _Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
