using validacaoJWT.Model;

namespace validacaoJWT.Repositories
{
    public static class UserRepository
    {
        public static User Get(string username, string password)
        {
            var users = new List<User> {
                new User { Id = 1, UserName = "batman", Password = "batman", Role = "manager" },
                new User { Id = 2, UserName = "robin", Password = "robin", Role = "employee" }
            };
            return users.First(x => x.UserName.Equals(username, StringComparison.OrdinalIgnoreCase) && x.Password.Equals(password, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
