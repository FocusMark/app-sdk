using System.Threading.Tasks;

namespace FocusMark.SDK.Account
{
    public interface IAccountService
    {

        Task<ServiceResponse<LoginResponse>> AuthorizeUser();

        Task<ServiceResponse> DeauthorizeUser();

        Task<ServiceResponse> RefreshAuthorization();
        Task<bool> IsUserAuthorized();

        Task<ServiceResponse<JwtTokens>> GetTokens();
    }
}
