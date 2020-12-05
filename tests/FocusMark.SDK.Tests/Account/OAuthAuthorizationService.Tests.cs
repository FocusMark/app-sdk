using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace FocusMark.SDK.Account
{
    [TestClass]
    public class OAuthAuthorizationServiceTests
    {
        /// <summary>
        /// Tests that the <see cref="OAuthAuthorizationService" /> returns a set of <see cref="JwtTokens"/> to the caller.
        /// The <see cref="JwtTokens"/> are generated via fake data from a mocked implementation of <see cref="ILoginService"/>
        /// </summary>
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

            // Ensure we cleaned up previous auth sessions, attempted to perform a login and then saved the new auth tokens.
            repositoryMock.Verify(repo => repo.DeleteToken(), Times.Once);
            repositoryMock.Verify(repo => repo.SaveToken(It.IsAny<JwtTokens>()), Times.Once);
            loginMock.Verify(repo => repo.Login(), Times.Once);
        }

        /// <summary>
        /// Tests that the <see cref="OAuthAuthorizationService"/> returns an instance of <see cref=" ServiceResponse"/> with a <see cref="LoginResponse"/>.
        /// An instance of <see cref="ResponseError"/> should be present in the <see cref="ServiceResponse.Errors"/> array.
        /// </summary>
        [TestMethod]
        public async Task AuthorizeUser_ReturnsErrorWhenLoginFails()
        {
            // Arrange
            var responseError = new ResponseError(SDKErrors.LoginFailed.Code, SDKErrors.LoginFailed.Message);
            var serviceResponse = new ServiceResponse<JwtTokens>(responseError);

            Mock<ITokenRepository> repositoryMock = new Mock<ITokenRepository>();
            Mock<ILogger<OAuthAuthorizationService>> loggerMock = new Mock<ILogger<OAuthAuthorizationService>>();
            Mock<ILoginService> loginMock = new Mock<ILoginService>();
            loginMock
                .Setup(mock => mock.Login())
                .Returns(Task.FromResult(serviceResponse));

            IAccountService accountService = new OAuthAuthorizationService(repositoryMock.Object, loginMock.Object, loggerMock.Object);

            // Act
            ServiceResponse<LoginResponse> authResponse = await accountService.AuthorizeUser();

            // Assert
            Assert.IsNull(authResponse.Data);
            Assert.AreEqual(1, authResponse.Errors.Length);
            Assert.AreEqual(SDKErrors.LoginFailed.Code, authResponse.Errors[0].Code);
            Assert.AreEqual(SDKErrors.LoginFailed.Message, authResponse.Errors[0].Message);

            // Ensure that the saving of a token is never called. In this test we never receive tokens to save.
            repositoryMock.Verify(repo => repo.SaveToken(It.IsAny<JwtTokens>()), Times.Never);

            // Ensure we cleaned up previous auth sessions and attempted to perform a Login.
            repositoryMock.Verify(repo => repo.DeleteToken(), Times.Once);
            loginMock.Verify(repo => repo.Login(), Times.Once);
        }

        [TestMethod]
        public async Task AuthorizeUser_ReturnsErrorWhenSavingFails()
        {
            // Arrange
            var tokenFactory = new TestTokenFactory();
            string id_token = tokenFactory.GetIdToken(true);
            string access_token = tokenFactory.GetAccessToken(false);
            JwtTokens tokens = new JwtTokens { AccessToken = access_token, IdToken = id_token };

            Mock<ILogger<OAuthAuthorizationService>> loggerMock = new Mock<ILogger<OAuthAuthorizationService>>();

            Mock<ITokenRepository> repositoryMock = new Mock<ITokenRepository>();
            repositoryMock
                .Setup(repo => repo.SaveToken(It.IsAny<JwtTokens>()))
                .Throws<Exception>();
            
            Mock<ILoginService> loginMock = new Mock<ILoginService>();
            loginMock
                .Setup(mock => mock.Login())
                .Returns(Task.FromResult(new ServiceResponse<JwtTokens>(tokens)));

            IAccountService accountService = new OAuthAuthorizationService(repositoryMock.Object, loginMock.Object, loggerMock.Object);

            // Act
            ServiceResponse<LoginResponse> authResponse = await accountService.AuthorizeUser();

            // Assert
            Assert.IsNull(authResponse.Data);
            Assert.AreEqual(1, authResponse.Errors.Length);
            Assert.AreEqual(SDKErrors.SaveTokenFailed.Code, authResponse.Errors[0].Code);
            Assert.AreEqual(SDKErrors.SaveTokenFailed.Message, authResponse.Errors[0].Message);
            repositoryMock.Verify(repo => repo.DeleteToken(), Times.Once);
            repositoryMock.Verify(repo => repo.SaveToken(It.IsAny<JwtTokens>()), Times.Once);
            loginMock.Verify(repo => repo.Login(), Times.Once);
        }
    }
}
