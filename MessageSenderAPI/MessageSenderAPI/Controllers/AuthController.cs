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

        [HttpPost("registration")]
        public async Task<IActionResult> RegistrationAsync([FromBody] UserRegisterDTO userRegisterDTO)
        {
            var user = _mapper.Map<User>(userRegisterDTO);
            var response = await _authService.RegisterationAsync(user);
            if(response.Flag)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginDTO userLoginDTO)
        {
            var user = _mapper.Map<User>(userLoginDTO);
            var response = await _authService.LoginAsync(user);
            if (response.Token != null)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpPost("verify")]
        public async Task<IActionResult> VerifyAsync([FromBody] VerifyRequest verifyRequest)
        {
            var response = await _authService.VerifyAsync(verifyRequest);
            if (response.Flag)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPasswordAsync([FromBody] GetCodeRequest codeRequest)
        {
            var response = await _authService.ForgotPasswordAsync(codeRequest.Email);
            if(response.Flag)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpPost("confirm-code")]
        public async Task<IActionResult> ConfirmCodeAsync([FromBody] VerifyRequest verifyRequest)
        {
            var response = await _authService.ConfirmCodeAsync(verifyRequest);
            if (response.Flag)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordRequest resetPasswordRequest)
        {
            var response = await _authService.ResetPasswordAsync(resetPasswordRequest);
            if (response.Flag)
                return Ok(response);
            return BadRequest(response);
        }
    }
}
