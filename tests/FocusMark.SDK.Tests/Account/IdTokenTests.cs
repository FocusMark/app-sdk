using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;

namespace FocusMark.SDK.Account
{
    [TestClass]
    public class IdTokenTests
    {
        [TestMethod]
        public void JsonDeserialization_AssignsValues()
        {
            // Arrange
            string sub = "foobar";
            string username = "some_user";
            long expiration = DateTimeOffset.Now.Subtract(TimeSpan.FromMinutes(30)).ToUnixTimeMilliseconds();
            string accessTokenJson = $"{{ \"sub\": \"{sub}\", \"cognito:username\": \"{username}\", \"exp\": {expiration} }}";

            // Act
            IdToken idToken = JsonConvert.DeserializeObject<IdToken>(accessTokenJson);

            // Assert
            Assert.AreEqual(sub, idToken.UserId, "Expected the native id token subject field to map to UserId");
            Assert.AreEqual(expiration, idToken.ExpiresAt, "Expected the native id token exp field to map to ExpiresAt");
            Assert.AreEqual(username, idToken.Username, "Expected the native id token username field to map to Username.");
        }
    }
}
