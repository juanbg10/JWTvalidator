using validacaoJWT.Entities;

namespace validacaoJWT.Repositories
{
    public static class UserRepository
    {
        public static UserEntity Get(string username, string password)
        {
            var users = new List<UserEntity> {
                new UserEntity { Id = 1, UserName = "batman", Password = "batman", Role = "admin" },
                new UserEntity { Id = 2, UserName = "robin", Password = "robin", Role = "member" },
                new UserEntity { Id = 3, UserName = "Juan", Password = "Juan", Role = "external" },
            };
            return users.First(x => x.UserName.Equals(username, StringComparison.OrdinalIgnoreCase) && x.Password.Equals(password, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
