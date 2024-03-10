 using Microsoft.AspNetCore.Mvc;
using validacaoJWT.Model;
using validacaoJWT.Repositories;
using validacaoJWT.Services;

namespace validacaoJWT.Controllers
{
    [ApiController]
    [Route("v1")]
    public class ApiAuth : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> AuthenticateAsync([FromBody] UserDto model)
        {
            var user = UserRepository.Get(model.UserName, model.Password);

            if(user == null)
                return BadRequest("User wrong");
            
            var token = TokenService.GenerateToken(user);

            user.Password = String.Empty;

            return new
            {
                user = user,
                token = token
            };
        }
    }
}
