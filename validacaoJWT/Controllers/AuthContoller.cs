using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using validacaoJWT.Helpers;
using validacaoJWT.Model;
using validacaoJWT.Repositories;
using validacaoJWT.Services;

namespace validacaoJWT.Controllers
{
    [ApiController]
    [Route("v1")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly ITokenService _service;
        private readonly IMapper _mapper;

        public AuthController(ITokenService service, IMapper mapper, ILogger<AuthController> logger)
        {
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));

            _service = service ??
                throw new ArgumentNullException(nameof(service));

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        [Route("login")]
        public ActionResult<dynamic> Authenticate([FromBody] UserLoginDto model)
        {
            var user = UserRepository.Get(model.UserName, model.Password);

            if (user == null)
                return BadRequest("User wrong");

            var token = _service.GenerateToken(user);

            var userdto = _mapper.Map<Model.UserDto>(user);

            return new
            {
                user = userdto,
                token = token
            };
        }

        [HttpPost]
        [Route("validacao")]
        public ActionResult<bool> ValidateJwt([FromBody] JwtDto token)
        {
            try
            {
                var result = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = false,
                    ValidateLifetime = false,
                    LifetimeValidator = null,
                    RequireExpirationTime = false,
                    ClockSkew = TimeSpan.Zero,

                    SignatureValidator = delegate (string token, TokenValidationParameters parameters)
                    {
                        var jwt = new JwtSecurityToken(token);
                        return jwt;
                    },
                };
                result.RequireSignedTokens = false;

                var handler = new JwtSecurityTokenHandler();
                var tokenSecure = handler.ReadToken(token.jwt) as SecurityToken;

                var claims = handler.ValidateToken(token.jwt, result, out tokenSecure);

                if ((claims.Claims.Count() == 3) &&
                   claims.Claims.Any(c => c.Type == "Name" && !c.Value.Any(char.IsDigit) && c.Value.Length <= 256) &&
                   claims.Claims.Any(c => c.Type == "Role" && (c.Value == "Admin" || c.Value == "Member" || c.Value == "External")) &&
                   claims.Claims.Any(c => c.Type == "Seed" && Helper.IsPrimeNumber(int.Parse(c.Value))))
                    return true;

                _logger.LogError($"Valores das claims inválidos");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Token inválido {0}", ex);
                return false;
            }
        }
    }
}
