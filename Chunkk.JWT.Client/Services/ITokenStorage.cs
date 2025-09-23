using System.Threading.Tasks;

namespace Chunkk.JWT.Client;

public interface ITokenStorage
{
    Task SetTokenAsync(string token, bool rememberMe);
    Task<string?> GetTokenAsync();
    Task RemoveTokenAsync();
}
