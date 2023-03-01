using MessageSenderAPI.Domain.Enums;
using MessageSenderAPI.Domain.Helpers;
using MessageSenderAPI.Domain.Models;
using MessageSenderAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MessageSenderAPI.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        public AuthService(ApplicationDbContext context, IConfiguration configuration) 
        { 
            _context = context;
            _configuration = configuration;
        }

        public async Task<string> RegisterAsync(User registerUser)
        {
            if (!IsExist(registerUser.Email))
            {
                var salt = HashHelper.GenerateSalt();
                var hashPassword = HashHelper.HashPassword(registerUser.Password, salt);
                User newUser = new User
                {
                    Id = registerUser.Id,
                    Email = registerUser.Email,
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

        public async Task<string> LoginAsync(User loginUser)
        {
            if (IsExist(loginUser.Email))
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginUser.Email);
                if (HashHelper.VerifyPassword(loginUser.Password, user.Password, user.Salt))
                {
                    string token = CreateToken(loginUser);
                    return token;
                }
                return "Wrong password.";
            }
            return "User with the same email is not found.";
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetValue<string>("AppSettings:Key")));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        private bool IsExist(string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            return user != null ? true : false;
        }
    }
}
