using System.Threading.Tasks;

namespace Focusmark.SDK.Account
{
    public interface ITokenRepository
    {
        Task SaveToken(JwtTokens tokens);

        Task<JwtTokens> GetTokens();

        Task DeleteTokens();
    }

    public class TokenLiteDbRepository : ITokenRepository
    {
        public Task DeleteTokens()
        {
            throw new System.NotImplementedException();
        }

        public Task<JwtTokens> GetTokens()
        {
            throw new System.NotImplementedException();
        }

        public Task SaveToken(JwtTokens tokens)
        {
            throw new System.NotImplementedException();
        }
    }
}
