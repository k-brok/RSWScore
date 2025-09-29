using System.Threading.Tasks;

namespace RSW.Shared.Interfaces;

public interface ITokenStorage
{
    Task SetTokenAsync(string token, bool rememberMe);
    Task<string?> GetTokenAsync();
    Task RemoveTokenAsync();
}
