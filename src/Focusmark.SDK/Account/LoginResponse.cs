namespace FocusMark.SDK.Account
{
    public class LoginResponse
    {
        public LoginResponse(JwtTokens tokens)
        {
            this.AccessToken = tokens.GetAccessToken();
            this.IdToken = tokens.GetIdToken();
        }

        public AccessToken AccessToken { get; }

        public IdToken IdToken { get; }
    }
}
