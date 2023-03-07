using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using MessageSenderAPI.Domain.Models;
using MessageSenderAPI.Domain.ModelsDTO;
using AutoMapper;
using MessageSenderAPI.Services.Interfaces;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using MessageSenderAPI.Domain.Response;

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
    }
}
