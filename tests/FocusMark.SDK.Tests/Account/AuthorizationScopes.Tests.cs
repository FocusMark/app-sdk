using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace FocusMark.SDK.Account
{
    [TestClass]
    public class AuthorizationScopesTests
    {
        [TestMethod]
        public void ToArray_ReturnsAllScopesInArray()
        {
            // Arrange
            string[] scopes = Array.Empty<string>();

            // Act
            scopes = AuthorizationScopes.ToArray();

            // Assert
            Assert.IsTrue(scopes.Contains(AuthorizationScopes.ApiProjectDelete));
            Assert.IsTrue(scopes.Contains(AuthorizationScopes.ApiProjectRead));
            Assert.IsTrue(scopes.Contains(AuthorizationScopes.ApiProjectWrite));
            Assert.IsTrue(scopes.Contains(AuthorizationScopes.ApiTaskDelete));
            Assert.IsTrue(scopes.Contains(AuthorizationScopes.ApiTaskRead));
            Assert.IsTrue(scopes.Contains(AuthorizationScopes.ApiTaskWrite));
            Assert.IsTrue(scopes.Contains(AuthorizationScopes.OpenId));
        }
    }
}
