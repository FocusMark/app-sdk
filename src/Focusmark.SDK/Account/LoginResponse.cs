namespace FocusMark.SDK.Account
{
    public class LoginResponse
    {
        public LoginResponse(JwtTokens tokens) => this.JwtTokens = tokens;
        public JwtTokens JwtTokens { get; }
    }
}
