using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace FocusMark.SDK.Account
{
    [TestClass]
    public class JwtTokenTests
    {
        [TestMethod]
        public void JsonDeserialization_AssignsValues()
        {
            // Arrange
            var tokenFactory = new TestTokenFactory();
            string idTokenJson = tokenFactory.GetIdToken(false);
            string accessTokenJson = tokenFactory.GetAccessToken(false);
            string jwtTokenJson = JsonSerializer.Serialize(new { id_token = idTokenJson, access_token = accessTokenJson });

            // Act
            JwtTokens jwtTokens = Newtonsoft.Json.JsonConvert.DeserializeObject<JwtTokens>(jwtTokenJson);

            // Assert
            Assert.AreEqual(idTokenJson, jwtTokens.IdToken, "Expected the id_token value to have been assigned");
            Assert.AreEqual(accessTokenJson, jwtTokens.AccessToken, "Expected the access_token value to have been assigned");
        }

        [TestMethod]
        public void IdToken_HasExpiredToken()
        {
            // Arrange
            var tokenFactory = new TestTokenFactory();
            string idTokenJson = tokenFactory.GetIdToken(true);
            string accessTokenJson = tokenFactory.GetAccessToken(false);
            string jwtTokenJson = JsonSerializer.Serialize(new { id_token = idTokenJson, access_token = accessTokenJson });
            
            JwtTokens jwtTokens = Newtonsoft.Json.JsonConvert.DeserializeObject<JwtTokens>(jwtTokenJson);

            // Act
            bool isExpired = jwtTokens.IsIdTokenExpired();

            // Assert
            Assert.IsTrue(isExpired);
        }

        [TestMethod]
        public void IdToken_HasNotExpiredToken()
        {
            // Arrange
            var tokenFactory = new TestTokenFactory();
            string idTokenJson = tokenFactory.GetIdToken(false);
            string accessTokenJson = tokenFactory.GetAccessToken(false);
            string jwtTokenJson = JsonSerializer.Serialize(new { id_token = idTokenJson, access_token = accessTokenJson });

            JwtTokens jwtTokens = Newtonsoft.Json.JsonConvert.DeserializeObject<JwtTokens>(jwtTokenJson);

            // Act
            bool isExpired = jwtTokens.IsIdTokenExpired();

            // Assert
            Assert.IsFalse(isExpired);
        }

        [TestMethod]
        public void IdToken_GetTokenReturnsValue()
        {
            // Arrange
            var tokenFactory = new TestTokenFactory();
            string idTokenJson = tokenFactory.GetIdToken(true);
            string accessTokenJson = tokenFactory.GetAccessToken(false);
            string jwtTokenJson = JsonSerializer.Serialize(new { id_token = idTokenJson, access_token = accessTokenJson });

            JwtTokens jwtTokens = Newtonsoft.Json.JsonConvert.DeserializeObject<JwtTokens>(jwtTokenJson);

            // Act
            IdToken idToken = jwtTokens.GetIdToken();

            // Assert
            Assert.IsNotNull(idToken.UserId);
            Assert.IsNotNull(idToken.Username);
            Assert.AreNotEqual(0, idToken.ExpiresAt);
        }

        [TestMethod]
        public void AccessToken_HasExpiredToken()
        {
            // Arrange
            var tokenFactory = new TestTokenFactory();
            string idTokenJson = tokenFactory.GetIdToken(false);
            string accessTokenJson = tokenFactory.GetAccessToken(true);
            string jwtTokenJson = JsonSerializer.Serialize(new { id_token = idTokenJson, access_token = accessTokenJson });

            JwtTokens jwtTokens = Newtonsoft.Json.JsonConvert.DeserializeObject<JwtTokens>(jwtTokenJson);

            // Act
            bool isExpired = jwtTokens.IsAccessTokenExpired();

            // Assert
            Assert.IsTrue(isExpired);
        }

        [TestMethod]
        public void AccessToken_HasNotExpiredToken()
        {
            // Arrange
            var tokenFactory = new TestTokenFactory();
            string idTokenJson = tokenFactory.GetIdToken(false);
            string accessTokenJson = tokenFactory.GetAccessToken(false);
            string jwtTokenJson = JsonSerializer.Serialize(new { id_token = idTokenJson, access_token = accessTokenJson });

            JwtTokens jwtTokens = Newtonsoft.Json.JsonConvert.DeserializeObject<JwtTokens>(jwtTokenJson);

            // Act
            bool isExpired = jwtTokens.IsAccessTokenExpired();

            // Assert
            Assert.IsFalse(isExpired);
        }

        [TestMethod]
        public void AccessToken_GetTokenReturnsValue()
        {
            // Arrange
            var tokenFactory = new TestTokenFactory();
            string idTokenJson = tokenFactory.GetIdToken(true);
            string accessTokenJson = tokenFactory.GetAccessToken(false);
            string jwtTokenJson = JsonSerializer.Serialize(new { id_token = idTokenJson, access_token = accessTokenJson });

            JwtTokens jwtTokens = Newtonsoft.Json.JsonConvert.DeserializeObject<JwtTokens>(jwtTokenJson);

            // Act
            AccessToken accessToken = jwtTokens.GetAccessToken();

            // Assert
            Assert.IsNotNull(accessToken.UserId);
            Assert.AreNotEqual(0, accessToken.Scopes.Length);
            Assert.AreNotEqual(0, accessToken.ExpiresAt);
        }
    }
}
