using System.ComponentModel.DataAnnotations;

namespace validacaoJWT.Model
{
    public class UserLoginDto
    {
        [Required(ErrorMessage = "O campo '{0}' é obrigatório")]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "O campo '{0}' é obrigatório")]
        public required string Password { get; set; }
    }
}
