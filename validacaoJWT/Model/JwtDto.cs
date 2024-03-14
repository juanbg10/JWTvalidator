using System.ComponentModel.DataAnnotations;

namespace validacaoJWT.Model
{
    public class JwtDto
    {
        [Required(ErrorMessage = "O campo '{0}' é obrigatório")]
        public required string jwt { get; set; }
    }
}
