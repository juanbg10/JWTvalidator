using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace validacaoJWT.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous()
        {
            _logger.LogInformation("Anônimo");
            return "Anônimo";
        }

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated()
        {
            _logger.LogInformation($"Usuário: {User?.Identity?.Name} logado");
            return $"Authenticated - {User?.Identity?.Name}";
        }

        [HttpGet]
        [Route("external")]
        [Authorize(Roles = "external,admin")]
        public string External()
        {
            _logger.LogInformation($"Usuário: {User?.Identity?.Name} logado");
            return $"Authenticated - {User?.Identity?.Name}";
        }

        [HttpGet]
        [Route("employee")]
        [Authorize(Roles = "member,admin")]
        public string Employee()
        {
            _logger.LogInformation($"Usuário: {User?.Identity?.Name} logado");
            return $"Funcionário - {User?.Identity?.Name}";
        }

        [HttpGet]
        [Route("manager")]
        [Authorize(Roles = "admin")]
        public string Manager()
        {
            _logger.LogInformation($"Usuário: {User?.Identity?.Name} logado");
            return $"Gerente - {User?.Identity?.Name}";
        }
    }
}