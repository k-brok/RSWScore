using System.Threading.Tasks;

namespace RSW.APP.Services;

public interface ITokenStorage
{
    Task SetTokenAsync(string token, bool rememberMe);
    Task<string?> GetTokenAsync();
    Task RemoveTokenAsync();
}
