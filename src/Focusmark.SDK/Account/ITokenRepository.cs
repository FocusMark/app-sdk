using System.Threading.Tasks;

namespace FocusMark.SDK.Account
{
    public interface ITokenRepository
    {
        Task SaveToken(JwtTokens tokens);

        Task<JwtTokens> GetToken();

        Task DeleteToken();
    }
}
