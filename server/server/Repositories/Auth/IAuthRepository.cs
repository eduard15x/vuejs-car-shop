using server.Dtos.User;
using server.Models;

namespace server.Repositories.Auth
{
    public interface IAuthRepository
    {
        Task<User> Register(User newUser);
        Task<User> Login(LoginUserDto userCredentials);
    }
}
