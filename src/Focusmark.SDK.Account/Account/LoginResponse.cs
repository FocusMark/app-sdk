namespace Focusmark.SDK.Account
{
    public class LoginResponse
    {
        public LoginResponse(JwtTokens tokens) => this.JwtTokens = tokens;
        JwtTokens JwtTokens { get; }
    }
}
