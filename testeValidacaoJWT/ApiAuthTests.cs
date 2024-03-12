using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using validacaoJWT.Controllers;
using validacaoJWT.Model;
using validacaoJWT.Services;
using Microsoft.AspNetCore.Mvc;

namespace testeValidacaoJWT
{
    public class ApiAuthTests
    {
        [Fact]
        public async Task AuthenticateAsync_ReturnsBadRequest_WhenUserIsWrong()
        {
            // Arrange
            var userRepositoryMock = new Mock<UserRepository>();
            userRepositoryMock.Setup(x => x.Get(It.IsAny<string>(), It.IsAny<string>()))
                             .Returns((User)null);

            var apiAuth = new ApiAuth(userRepositoryMock.Object);

            // Act
            var result = await apiAuth.AuthenticateAsync(new UserDto { UserName = "invalidUser", Password = "invalidPassword" });

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.Equal("User wrong", badRequestResult.Value);
        }

        [Fact]
        public async Task AuthenticateAsync_ReturnsUserAndToken_WhenUserIsValid()
        {
            // Arrange
            var user = new User { UserName = "validUser", Password = "validPassword" };
            var userRepositoryMock = new Mock<UserRepository>();
            userRepositoryMock.Setup(x => x.Get("validUser", "validPassword"))
                             .Returns(user);

            var tokenServiceMock = new Mock<TokenService>();
            tokenServiceMock.Setup(x => x.GenerateToken(user))
                            .Returns("fakeToken");

            var apiAuth = new ApiAuth(userRepositoryMock.Object, tokenServiceMock.Object);

            // Act
            var result = await apiAuth.AuthenticateAsync(new UserDto { UserName = "validUser", Password = "validPassword" });

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            dynamic data = okResult.Value;
            Assert.Equal(user, data.user);
            Assert.Equal("fakeToken", data.token);
        }
    }
}
