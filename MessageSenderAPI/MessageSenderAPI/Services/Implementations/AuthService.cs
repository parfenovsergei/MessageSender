using Azure;
using MessageSenderAPI.Domain.Enums;
using MessageSenderAPI.Domain.Helpers;
using MessageSenderAPI.Domain.Models;
using MessageSenderAPI.Domain.Request;
using MessageSenderAPI.Domain.Response;
using MessageSenderAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<GeneralResponse> RegisterationAsync(User registerUser)
        {
            var response = new GeneralResponse();
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
                    VerifyCode = GenerateVerifyCode(),
                    CreateAndVerifyTime = DateTime.Now
                };
                await _context.Users.AddAsync(newUser);
                await _context.SaveChangesAsync();
                await _emailService.SendVerifyCodeAsync(newUser.Email, newUser.VerifyCode);
                response.Flag = true;
                response.Message = "Code sended on your email";
                return response;
            }
            response.Flag = false;
            response.Message = "Wrong credentials";
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
                    response.Message = "Wrong credentials";
                    return response;
                }
                response.Message = "You are not verified";
                return response;
            }
            response.Message = "Wrong credentials";
            return response;
        }

        public async Task<GeneralResponse> VerifyAsync(VerifyRequest verifyRequest)
        {
            var response = new GeneralResponse();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == verifyRequest.Email);
            if(user != null)
            {
                if(verifyRequest.VerifyCode == user.VerifyCode)
                {
                    user.IsVerifed = true;
                    await _context.SaveChangesAsync();
                    response.Flag = true;
                    response.Message = "You verified!";
                    return response;
                }
                response.Flag = false;
                response.Message = "Inccorect verify code";
                return response;
            }
            response.Flag = false;
            response.Message = "Wrong credentials";
            return response;
        }

        public async Task<GeneralResponse> ForgotPasswordAsync(string email)
        {
            var response = new GeneralResponse();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user != null)
            {
                var code = GenerateVerifyCode();
                user.VerifyCode = code;
                await _context.SaveChangesAsync();
                await _emailService.SendVerifyCodeAsync(email, code);
                response.Flag = true;
                response.Message = "Verify code is send on your email";
                return response;
            }
            response.Flag = false;
            response.Message = "Wrong credentials";
            return response;
        }

        public async Task<GeneralResponse> ResetPasswordAsync(ResetPasswordRequest resetPasswordRequest)
        {
            var response = new GeneralResponse();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == resetPasswordRequest.Email);
            if(user != null)
            {
                if(user.IsChangingPassword)
                {
                    var salt = HashHelper.GenerateSalt();
                    var hashPassword = HashHelper.HashPassword(resetPasswordRequest.Password, salt);
                    user.Salt = salt;
                    user.Password = hashPassword;
                    user.IsChangingPassword = false;
                    await _context.SaveChangesAsync();
                    response.Flag = true;
                    response.Message = "Password changed";
                    return response;
                }
                response.Flag = false;
                response.Message = "You are not confirmed";
                return response;
            }
            response.Flag = false;
            response.Message = "Wrong credentials";
            return response;
        }

        public async Task<GeneralResponse> ConfirmCodeAsync(VerifyRequest verifyRequest)
        {
            var response = new GeneralResponse();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == verifyRequest.Email);
            if (user != null)
            {
                if (verifyRequest.VerifyCode == user.VerifyCode)
                {
                    user.IsChangingPassword = true;
                    await _context.SaveChangesAsync();
                    response.Flag = true;
                    response.Message = "Right code";
                    return response;
                }
                response.Flag = false;
                response.Message = "Inccorect code";
                return response;
            }
            response.Flag = false;
            response.Message = "Wrong credentials";
            return response;
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim("email", user.Email),
                new Claim("role", user.Role.ToString())
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
