using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace FocusMark.SDK.Account
{
    [TestClass]
    public class OAuthAuthorizationServiceTests
    {
        [TestMethod]
        public async Task AuthorizeUser_ReturnsTokens()
        {
            // Arrange
            var tokenFactory = new TestTokenFactory();
            string id_token = tokenFactory.GetIdToken(true);
            string access_token = tokenFactory.GetAccessToken(false);
            JwtTokens tokens = new JwtTokens { AccessToken = access_token, IdToken = id_token };

            Mock<ITokenRepository> repositoryMock = new Mock<ITokenRepository>();
            Mock<ILogger<OAuthAuthorizationService>> loggerMock = new Mock<ILogger<OAuthAuthorizationService>>();
            Mock<ILoginService> loginMock = new Mock<ILoginService>();
            loginMock
                .Setup(mock => mock.Login())
                .Returns(Task.FromResult(new ServiceResponse<JwtTokens>(tokens)));

            IAccountService accountService = new OAuthAuthorizationService(repositoryMock.Object, loginMock.Object, loggerMock.Object);

            // Act
            ServiceResponse<LoginResponse> authResponse = await accountService.AuthorizeUser();

            // Assert
            Assert.IsNotNull(authResponse.Data.AccessToken.UserId);
            Assert.AreNotEqual(0, authResponse.Data.AccessToken.Scopes.Length);
            Assert.AreNotEqual(0, authResponse.Data.AccessToken.ExpiresAt);
            repositoryMock.Verify(repo => repo.DeleteToken(), Times.Once);
            repositoryMock.Verify(repo => repo.SaveToken(It.IsAny<JwtTokens>()), Times.Once);
            loginMock.Verify(repo => repo.Login(), Times.Once);
        }
    }
}
