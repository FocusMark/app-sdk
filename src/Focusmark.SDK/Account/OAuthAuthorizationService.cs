using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Focusmark.SDK.Account
{
    public class OAuthAuthorizationService : IAccountService
    {
        private readonly ITokenRepository tokenRepository;
        private readonly ILoginService loginService;
        private readonly ILogger<OAuthAuthorizationService> logger;

        public OAuthAuthorizationService(ITokenRepository tokenRepository, ILoginService loginService, ILogger<OAuthAuthorizationService> logger)
        {
            this.tokenRepository = tokenRepository ?? throw new ArgumentNullException(nameof(tokenRepository));
            this.loginService = loginService ?? throw new ArgumentNullException(nameof(loginService));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ServiceResponse<LoginResponse>> AuthorizeUser()
        {
            await this.DeauthorizeUser();

            ServiceResponse<JwtTokens> loginResponse = await this.loginService.Login();
            if (loginResponse?.Data == null)
            {
                ResponseError loginError = new ResponseError(SDKErrors.LoginFailed.Code, SDKErrors.LoginFailed.Message);
                return new ServiceResponse<LoginResponse>(loginError);
            }

            JwtTokens jwtTokens = loginResponse.Data;
            try
            {
                await this.tokenRepository.SaveToken(loginResponse.Data);
                return new ServiceResponse<LoginResponse>(new LoginResponse(jwtTokens));
            }
            catch (Exception)
            {
                var saveTokenError = new ResponseError(SDKErrors.SaveTokenFailed.Code, SDKErrors.SaveTokenFailed.Message);
                return new ServiceResponse<LoginResponse>(saveTokenError);
            }
        }

        public async Task<ServiceResponse> DeauthorizeUser()
        {
            try
            {
                await this.tokenRepository.DeleteToken();
                return new ServiceResponse();
            }
            catch (Exception)
            {
                var deleteTokenError = new ResponseError(SDKErrors.DeleteTokenFailed.Code, SDKErrors.DeleteTokenFailed.Message);
                return new ServiceResponse(deleteTokenError);
            }
        }

        public async Task<ServiceResponse<JwtTokens>> GetTokens()
        {
            this.logger.LogInformation("Fetching previously retrieved tokens.");
            JwtTokens token = await this.tokenRepository.GetToken();

            if (token == null)
            {
                this.logger.LogError("User is not currently logged into their account.");
                var getTokenFailed = new ResponseError(SDKErrors.LoadTokensFailed.Code, SDKErrors.LoadTokensFailed.Message);
                return new ServiceResponse<JwtTokens>(getTokenFailed);
            }

            return new ServiceResponse<JwtTokens>(token);
        }

        public async Task<bool> IsUserAuthorized()
        {
            ServiceResponse<JwtTokens> token = await this.GetTokens();
            if (!token.IsSuccessful)
            {
                return false;
            }

            return !token.Data.IsAccessTokenExpired();
        }

        public Task<ServiceResponse> RefreshAuthorization()
        {
            throw new NotImplementedException();
        }
    }
}
