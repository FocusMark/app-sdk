using System.Threading.Tasks;

namespace FocusMark.SDK.Account
{
    public interface ILoginService
    {
        Task<ServiceResponse<JwtTokens>> Login();
    }
}
