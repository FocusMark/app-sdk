using Newtonsoft.Json;

namespace Focusmark.SDK.Account
{
    public class AccessToken
    {
        [JsonProperty("scope")]
        private string givenScopes = string.Empty;

        [JsonProperty("sub")]
        public string UserId { get; set; }

        [JsonProperty("exp")]
        public long ExpiresAt { get; set; }

        public string[] Scopes => givenScopes.Split(' ');
    }
}
