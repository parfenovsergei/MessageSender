using MessageSenderAPI.Domain.Enums;
using MessageSenderAPI.Domain.Helpers;
using MessageSenderAPI.Domain.Models;
using MessageSenderAPI.Domain.ModelsDTO;
using MessageSenderAPI.Domain.Response;
using MessageSenderAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.CodeDom.Compiler;
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
        private readonly IEmailService _emailService;
        public AuthService(
            ApplicationDbContext context,
            IConfiguration configuration,
            IEmailService emailService) 
        { 
            _context = context;
            _configuration = configuration;
            _emailService = emailService;
        }

        public async Task<RegisterResponse> RegisterAsync(User registerUser)
        {
            var response = new RegisterResponse();
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
                    IsVerifed = false,
                    VerifyCode = GenerateVerifyCode()
                };
                await _context.Users.AddAsync(newUser);
                await _context.SaveChangesAsync();
                await _emailService.SendVerifyCodeAsync(newUser.Email, newUser.VerifyCode);
                response.IsRegister = true;
                response.Message = "We send six-digit code on your email";
                return response;
            }
            response.IsRegister = false;
            response.Message = "User with the same login is already exist";
            return response;
        }

        public async Task<LoginResponse> LoginAsync(User loginUser)
        {
            var response = new LoginResponse();
            if (IsExist(loginUser.Email))
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginUser.Email);
                if (user.IsVerifed) {
                    if (HashHelper.VerifyPassword(loginUser.Password, user.Password, user.Salt))
                    {
                        string token = CreateToken(user);
                        response.Token = token;
                        response.Message = "You are in.";
                        return response;
                    }
                    response.Message = "Wrong password.";
                    return response;
                }
                response.Message = "You are not verifed";
                return response;
            }
            response.Message = "User with the same email is not found.";
            return response;
        }

        public async Task<RegisterResponse> VerifyAsync(UserVerifyDTO userVerifyDTO)
        {
            var response = new RegisterResponse();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userVerifyDTO.Email);
            if(user != null)
            {
                if(userVerifyDTO.VerifyCode == user.VerifyCode)
                {
                    user.IsVerifed = true;
                    await _context.SaveChangesAsync();
                    response.IsRegister = true;
                    response.Message = "You verifed!";
                    return response;
                }
                response.IsRegister = false;
                response.Message = "Inccorect verify code";
                return response;
            }
            response.IsRegister = false;
            response.Message = "User with the same email is not found.";
            return response;
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
            return user != null;
        }

        private int GenerateVerifyCode()
        {
            Random rnd = new Random();
            return rnd.Next(100000, 999999);
        }
    }
}
