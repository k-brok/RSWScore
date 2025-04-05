using RSW.WebApp.Entities;

namespace RSW.WebApp.Interface.Repositories
{
    public interface IJurySlotRepository
    {
        Task<JurySlot> GetByCode( string code );
        Task<List<JurySlot>> GetAllAsync();
        Task Save(JurySlot juryslot);
        Task RevertEdits(JurySlot juryslot);
        Task Delete(JurySlot juryslot);
        Task<string> GetCode(Category category, SubGroup subGroup, Edition edition);
    }
}
