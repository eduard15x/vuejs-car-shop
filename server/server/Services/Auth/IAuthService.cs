using server.Dtos.User;

namespace server.Services.Auth
{
    public interface IAuthService
    {
        Task<UserTokenDto> Register(RegisterUserDto registerUserDto);
        Task<UserTokenDto> Login(LoginUserDto userCredentials);
    }
}
