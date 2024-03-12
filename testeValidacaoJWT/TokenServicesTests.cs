using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using validacaoJWT.Model;
using validacaoJWT.Services;
using Xunit;

namespace testeValidacaoJWT
{
    public class TokenServiceTests
    {
        [Fact]
        public void GenerateToken_ReturnsValidToken()
        {
            // Arrange
            var user = new User
            {
                UserName = "batman",
                Role = "manager"
            };

            // Act
            var token = TokenService.GenerateToken(user);

            // Assert
            Assert.NotNull(token);
            Assert.NotEmpty(token);
        }

        [Fact]
        public void GenerateToken_ValidatesTokenClaims()
        {
            // Arrange
            var user = new User
            {
                UserName = "batman",
                Role = "manager"
            };

            // Act
            var token = TokenService.GenerateToken(user);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenValidationParams = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("f1854477-fee1-4656-a155-e8c3ffc9ba9f")),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            var principal = tokenHandler.ValidateToken(token, tokenValidationParams, out _);

            // Assert
            Assert.NotNull(principal);

            var userNameClaim = principal.FindFirst(ClaimTypes.Name);
            var roleClaim = principal.FindFirst(ClaimTypes.Role);
            var seedClaim = principal.FindFirst("Seed");

            Assert.NotNull(userNameClaim);
            Assert.NotNull(roleClaim);
            Assert.Equal("batman", userNameClaim.Value);
            Assert.Equal("manager", roleClaim.Value);

            Assert.NotNull(seedClaim);
            int seedNumber;
            Assert.True(int.TryParse(seedClaim.Value, out seedNumber));
        }
    }
}
