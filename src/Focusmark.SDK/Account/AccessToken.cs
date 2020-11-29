using Newtonsoft.Json;
using System;

namespace FocusMark.SDK.Account
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

        public bool IsExpired()
        {
            DateTimeOffset expirationOffset = DateTimeOffset.FromUnixTimeMilliseconds(this.ExpiresAt);
            DateTime expirationDateTime = expirationOffset.UtcDateTime;

            return expirationDateTime >= DateTime.UtcNow;
        }
    }
}
