using Microsoft.EntityFrameworkCore;
using RSW.WebApp.Data;
using RSW.WebApp.Entities;
using RSW.WebApp.Interface.Repositories;

namespace RSW.WebApp.Repositories
{
    public class SignupCodeRepository : ISignupCodeRepository
    {
        private readonly ApplicationDbContext _Context;
        public SignupCodeRepository(ApplicationDbContext applicationDbContext)
        {
            _Context = applicationDbContext;
        }
        public async Task<Entities.Group> CheckCode(string Code)
        {
            SignupCode signupCode = _Context.SignupCodes.FirstOrDefault(S => S.Code == Code);
            if (signupCode == null)
                return null;

            if (signupCode.Lock)
                return null;

            if (signupCode.ExpiryDate < DateTime.Now)
                return null;

            Group Returngroup = await _Context.Groups
                    .Include(G => G.Patrols)
                        .ThenInclude(P => P.Scouts)
                    .Include(G => G.Patrols)
                        .ThenInclude(P => P.Scores)
                    .Include(G => G.Patrols)
                        .ThenInclude(P => P.SubGroup)
                            .ThenInclude(S => S.Edition)
                                .ThenInclude(E => E.SubGroups)
                    .Include(G => G.Association)
                .FirstOrDefaultAsync(G => G.Id == signupCode.GroupId);

            return Returngroup;
        }

        public async Task<string> CreateNew(Entities.Group group)
        {
            SignupCode NewSignupCode = new SignupCode();
            if (group != null)
            {
                NewSignupCode.GroupId = group.Id;
                NewSignupCode.ExpiryDate = DateTime.Now.AddDays(14);
                NewSignupCode.Code = System.Guid.NewGuid().ToString();

                _Context.SignupCodes.Add(NewSignupCode);
                await _Context.SaveChangesAsync();
            }

            return NewSignupCode.Code;
        }

        public async Task<List<SignupCode>> GetAllActive()
        {
            return await _Context.SignupCodes
            .Where(S =>
                S.Lock == false &&
                S.ExpiryDate >= DateTime.Now
            ).ToListAsync();
        }
    }
}
