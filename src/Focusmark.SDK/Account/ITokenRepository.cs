using System.Threading.Tasks;

namespace Focusmark.SDK.Account
{
    public interface ITokenRepository
    {
        Task SaveToken(JwtTokens tokens);

        Task<JwtTokens> GetToken();

        Task DeleteToken();
    }
}
