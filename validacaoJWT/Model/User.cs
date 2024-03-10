namespace validacaoJWT.Model
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

    }

    public class UserDto
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
}
