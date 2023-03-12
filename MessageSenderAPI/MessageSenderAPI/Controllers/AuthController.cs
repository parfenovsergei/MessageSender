using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using MessageSenderAPI.Domain.Models;
using MessageSenderAPI.Domain.ModelsDTO;
using AutoMapper;
using MessageSenderAPI.Services.Interfaces;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using MessageSenderAPI.Domain.Response;
using MessageSenderAPI.Services.Implementations;
using MessageSenderAPI.Domain.Request;

namespace MessageSenderAPI.Controllers
{
    [Route("user")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        public AuthController(IMapper mapper, IAuthService authService)
        {
            _mapper = mapper;
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<RegisterResponse> RegisterAsync([FromBody] UserRegisterDTO userRegisterDTO)
        {
            var user = _mapper.Map<User>(userRegisterDTO);
            var response = await _authService.RegisterAsync(user);
            return response;
        }

        [HttpPost("login")]
        public async Task<LoginResponse> LoginAsync([FromBody] UserLoginDTO userLoginDTO)
        {
            var user = _mapper.Map<User>(userLoginDTO);
            var response = await _authService.LoginAsync(user);
            return response;
        }

        [HttpPost("verify")]
        public async Task<RegisterResponse> VerifyAsync([FromBody] VerifyRequest verifyRequest)
        {
            var response = await _authService.VerifyAsync(verifyRequest);
            return response;
        }

        [HttpPost("forgot-password")]
        public async Task<ActionResult<string>> ForgotPasswordAsync([FromBody] string email)
        {
            var response = await _authService.ForgotPasswordAsync(email);
            return response;
        }

        [HttpPost("confirm-code")]
        public async Task<ActionResult<string>> ConfirmCodeAsync([FromBody] VerifyRequest verifyRequest)
        {
            var response = await _authService.ConfirmCodeAsync(verifyRequest);
            return response;
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult<string>> ResetPasswordAsync([FromBody] ResetPasswordRequest resetPasswordRequest)
        {
            var response = await _authService.ResetPasswordAsync(resetPasswordRequest);
            return response;
        }
    }
}
