using System;
using System.IO;
using System.Text;
using System.Text.Json;

namespace FocusMark.SDK.Account
{
    public class TestTokenFactory
    {
        public string GetIdToken(bool isExpired)
        {
            string tokenPayload = string.Empty;
            string tokenHeader = JsonSerializer.Serialize(new { alg = "noop" });
            string tokenSignature = Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes("Invalid SIgnature here"));

            using (var stream = new MemoryStream())
            {
                using (var writer = new Utf8JsonWriter(stream))
                {
                    long expiration = isExpired
                    ? DateTimeOffset.UtcNow.Add(TimeSpan.FromMinutes(90)).ToUnixTimeMilliseconds()
                    : DateTimeOffset.UtcNow.Subtract(TimeSpan.FromMinutes(30)).ToUnixTimeMilliseconds();

                    writer.WriteStartObject();
                    writer.WriteString("sub", "foobar");
                    writer.WriteString("cognito:username", "some_user");
                    writer.WriteNumber("exp", expiration);
                    writer.WriteEndObject();
                }

                tokenPayload = Encoding.UTF8.GetString(stream.ToArray());
            }


            string tokenHeaderHash = Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes(tokenHeader));
            string tokenPayloadHash = Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes(tokenPayload));
            string tokenSignatureHash = Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes(tokenSignature));

            return string.Join(".", new[] { tokenHeaderHash, tokenPayloadHash, tokenSignatureHash });
        }

        public string GetAccessToken(bool isExpired)
        {
            string tokenPayload = string.Empty;
            string tokenHeader = JsonSerializer.Serialize(new { alg = "noop" });
            string tokenSignature = Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes("Invalid SIgnature here"));

            using (var stream = new MemoryStream())
            {
                using (var writer = new Utf8JsonWriter(stream))
                {
                    long expiration = isExpired
                    ? DateTimeOffset.UtcNow.Add(TimeSpan.FromMinutes(90)).ToUnixTimeMilliseconds()
                    : DateTimeOffset.UtcNow.Subtract(TimeSpan.FromMinutes(30)).ToUnixTimeMilliseconds();

                    writer.WriteStartObject();
                    writer.WriteString("sub", "foobar");
                    writer.WriteString("scope", "openid write read");
                    writer.WriteNumber("exp", expiration);
                    writer.WriteEndObject();
                }

                tokenPayload = Encoding.UTF8.GetString(stream.ToArray());
            }


            string tokenHeaderHash = Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes(tokenHeader));
            string tokenPayloadHash = Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes(tokenPayload));
            string tokenSignatureHash = Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes(tokenSignature));

            return string.Join(".", new[] { tokenHeaderHash, tokenPayloadHash, tokenSignatureHash });
        }
    }
}
