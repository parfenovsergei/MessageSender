using MessageSenderAPI.Domain.Enums;
using MessageSenderAPI.Domain.Helpers;
using MessageSenderAPI.Domain.Models;
using MessageSenderAPI.Services.Interfaces;

namespace MessageSenderAPI.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        public AuthService(ApplicationDbContext context) 
        { 
            _context = context;
        }
        public async Task<string> Register(User user)
        {
            if (!IsExist(user.Email))
            {
                var salt = HashHelper.GenerateSalt();
                var hashPassword = HashHelper.HashPassword(user.Password, salt);
                User newUser = new User
                {
                    Id = user.Id,
                    Email = user.Email,
                    Password = hashPassword,
                    Salt = salt,
                    Role = Role.User,
                };
                await _context.Users.AddAsync(newUser);
                await _context.SaveChangesAsync();
                return "Registration completed successfully!";
            }
            return "User with the same login is already exist";
        }
        private bool IsExist(string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            return user == null ? false : true;
        }
    }
}
