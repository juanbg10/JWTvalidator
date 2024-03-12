using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using validacaoJWT.Model;
using validacaoJWT.Repositories;

namespace testeValidacaoJWT
{
    public class UserRepositoryTests
    {
        [Fact]
        public void Get_ReturnsUser_WhenUsernameAndPasswordMatch()
        {
            // Arrange
            string username = "batman";
            string password = "batman";

            // Act
            var result = UserRepository.Get(username, password);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("batman", result.UserName);
            Assert.Equal("manager", result.Role);
        }

        [Fact]
        public void Get_ReturnsNull_WhenUsernameAndPasswordDoNotMatch()
        {
            // Arrange
            string username = "invalidUser";
            string password = "invalidPassword";

            // Act
            var result = UserRepository.Get(username, password);

            // Assert
            Assert.Null(result);
        }
    }
}
