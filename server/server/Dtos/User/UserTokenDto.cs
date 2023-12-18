namespace server.Dtos.User
{
    public class UserTokenDto
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public bool IsLoggedIn { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
    }
}
