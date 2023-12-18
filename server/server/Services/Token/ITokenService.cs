using server.Dtos.User;
using server.Models;

namespace server.Services.Token
{
    public interface ITokenService
    {
        UserTokenDto GenerateToken(User user);
    }
}
