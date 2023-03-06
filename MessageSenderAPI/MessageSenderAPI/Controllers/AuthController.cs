using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using MessageSenderAPI.Domain.Models;
using MessageSenderAPI.Domain.ModelsDTO;
using AutoMapper;
using MessageSenderAPI.Services.Interfaces;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

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
        public async Task<string> RegisterAsync([FromBody] UserAuthDTO userAuthDTO)
        {
            var user = _mapper.Map<User>(userAuthDTO);
            var result = await _authService.RegisterAsync(user);
            return JsonConvert.SerializeObject(result);
        }

        [HttpPost("login")]
        public async Task<string> LoginAsync([FromBody] UserAuthDTO userAuthDTO)
        {
            var user = _mapper.Map<User>(userAuthDTO);
            var token = await _authService.LoginAsync(user);
            return JsonConvert.SerializeObject(token);
        }
    }
}
