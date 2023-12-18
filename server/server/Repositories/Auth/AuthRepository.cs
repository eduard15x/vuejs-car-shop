using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Dtos.User;
using server.Models;

namespace server.Repositories.Auth
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _appDbContext;

        public AuthRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<User> Register(User newUser)
        {
            if (await _appDbContext.Users.AnyAsync(u => u.Email == newUser.Email))
            {
                throw new Exception("User already exists with this email.");
            }
            // Hash and Salt password
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(newUser.Password);

            newUser.Password = hashedPassword;

            _appDbContext.Users.Add(newUser);
            await _appDbContext.SaveChangesAsync();

            return newUser;
        }

        public async Task<User> Login(LoginUserDto userCredentials)
        {
            var userFromDb = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Email == userCredentials.Email);

            if (userFromDb == null)
            {
                throw new Exception("Email doesn't exist.");
            }

            var verifyPassword = BCrypt.Net.BCrypt.Verify(userCredentials.Password, userFromDb.Password);

            if (!verifyPassword)
            {
                throw new Exception("Password is wrong.");
            }

            return userFromDb;
        }
    }
}
