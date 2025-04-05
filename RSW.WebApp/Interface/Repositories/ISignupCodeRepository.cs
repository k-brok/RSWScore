using RSW.WebApp.Entities;

namespace RSW.WebApp.Interface.Repositories
{
    public interface ISignupCodeRepository
    {
        Task<List<SignupCode>> GetAllActive();
        Task<string> CreateNew(Group group);
        Task<Group> CheckCode(string Code);
    }
}
