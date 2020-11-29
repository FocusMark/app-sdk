using Newtonsoft.Json;
using System;

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

        public bool IsExpired()
        {
            DateTimeOffset expirationOffset = DateTimeOffset.FromUnixTimeMilliseconds(this.ExpiresAt);
            DateTime expirationDateTime = expirationOffset.UtcDateTime;

            return expirationDateTime >= DateTime.UtcNow;
        }
    }
}
