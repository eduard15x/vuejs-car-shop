using AutoMapper;
using server.Dtos.User;
using server.Models;
using server.Repositories.Auth;
using server.Services.Token;

namespace server.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly IAuthRepository _authRepository;
        private readonly ITokenService _tokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IMapper mapper, IAuthRepository authRepository, ITokenService tokenService, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _authRepository = authRepository;
            _tokenService = tokenService;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<UserTokenDto> Register(RegisterUserDto registerUserDto)
        {
            if (registerUserDto.Password != registerUserDto.ConfirmPassword)
            {
                throw new Exception("Passwords don't match");
            }

            var newUser = new User()
            {
                Name = registerUserDto.Name,
                Email = registerUserDto.Email,
                Password = registerUserDto.Password,
                Age = registerUserDto.Age,
                PhoneNumber = registerUserDto.PhoneNumber,
            };

            var newUserEntity = await _authRepository.Register(newUser);

            return _tokenService.GenerateToken(newUserEntity);
        }

        public async Task<UserTokenDto> Login(LoginUserDto userCredentials)
        {
            if (userCredentials == null || string.IsNullOrEmpty(userCredentials.Email) || string.IsNullOrEmpty(userCredentials.Password))
            {
                throw new Exception("User credentials are missing");
            }

            var userInfo = await _authRepository.Login(userCredentials);

            return _tokenService.GenerateToken(userInfo);
        }
    }
}
