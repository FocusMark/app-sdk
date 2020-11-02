using Newtonsoft.Json;

namespace FocusMark.SDK.Account
{
    public class IdToken
    {
        [JsonProperty("sub")]
        public string UserId { get; set; }

        [JsonProperty("cognito:username")]
        public string Username { get; set; }

        [JsonProperty("exp")]
        public long ExpiresAt { get; set; }
    }
}
