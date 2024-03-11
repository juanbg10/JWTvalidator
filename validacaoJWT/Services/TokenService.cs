using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using validacaoJWT.Model;

namespace validacaoJWT.Services
{
    public class TokenService
    {
        
        public static string GenerateToken(User user)
        {
            int primeNumber = GeneratePrimeNumbers();
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("f1854477-fee1-4656-a155-e8c3ffc9ba9f");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim("Seed", primeNumber.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }


        private static int GeneratePrimeNumbers()
        {
            Random random = new Random();
            int num = random.Next(100, 1000); // Geração de um número aleatório entre 100 e 1000

            while (!PrimeNumberValidator(num))
            {
                num = random.Next(100, 1000);
            }

            return num;
        }


        private static bool PrimeNumberValidator(int number)
        {
            if (number <= 1)
            { return false; }

            if (number <= 3)
            { return true; }

            if (number % 2 == 0 || number % 3 == 0)
            { return false; }

            for (int i = 5; i * i <= number; i += 6)
            {
                if (number % i == 0 || number % (i + 2) == 0)
                { return false; }
            }

            return true;
        }
    }
}
