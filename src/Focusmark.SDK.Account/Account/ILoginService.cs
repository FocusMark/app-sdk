using System.Threading.Tasks;

namespace Focusmark.SDK.Account
{
    public interface ILoginService
    {
        Task<ServiceResponse<JwtTokens>> Login();
    }
}
