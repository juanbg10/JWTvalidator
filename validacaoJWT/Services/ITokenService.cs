using validacaoJWT.Entities;

namespace validacaoJWT.Services
{
    public interface ITokenService
    {
        string GenerateToken(UserEntity user);
    }
}
