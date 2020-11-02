using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;

namespace FocusMark.SDK.Account
{
    [TestClass]
    public class AccessTokenTests
    {
        // A class that represents how the native token comes to us from our auth services before any Json mapping is done.
        class NativeAccessToken
        {
            public string scope = "openid project task";
            public string sub = "foobar";
            public long exp = DateTimeOffset.Now.Subtract(TimeSpan.FromMinutes(30)).ToUnixTimeMilliseconds();

            public string AsJson() => JsonConvert.SerializeObject(this);
        }

        [TestMethod]
        public void JsonDeserialization_AssignsValues()
        {
            // Arrange
            var nativeAccessToken = new NativeAccessToken();
            string accessTokenJson = nativeAccessToken.AsJson();

            // Act
            AccessToken accessToken = JsonConvert.DeserializeObject<AccessToken>(accessTokenJson);

            // Assert
            Assert.AreEqual(nativeAccessToken.sub, accessToken.UserId, "Expected the native access token subject field to map to UserId");
            Assert.AreEqual(nativeAccessToken.exp, accessToken.ExpiresAt, "Expected the native access token exp field to map to ExpiresAt");
            Assert.AreEqual(3, accessToken.Scopes.Length);
            Assert.AreEqual("openid", accessToken.Scopes[0], "expected first index to be openid");
            Assert.AreEqual("project", accessToken.Scopes[1], "expected second index to be project");
            Assert.AreEqual("task", accessToken.Scopes[2], "expected third index to be task");
        }
    }
}
