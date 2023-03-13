using AutoMapper;
using MessageSenderAPI.Domain.Enums;
using MessageSenderAPI.Domain.Helpers;
using MessageSenderAPI.Domain.ModelsDTO;
using MessageSenderAPI.Services.Implementations;
using MessageSenderAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MessageSenderAPI.Controllers
{
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UserController(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        [AuthorizeRoles(Role.Admin)]
        [HttpGet("users")]
        public async Task<List<UserDTO>> GetAllUsersAsync()
        {
            var users = await _userService.GetAllUsersAsync();
            var result = _mapper.Map<List<UserDTO>>(users);
            return result;
        }

        [AuthorizeRoles(Role.Admin)]
        [HttpGet("users/{id}/messages")]
        public async Task<List<MessageViewDTO>> GetUserMessagesAsync(int id)
        {
            var messages = await _userService.GetMessagesByUserIdAsync(id);
            var messagesViewDto = _mapper.Map<List<MessageViewDTO>>(messages);
            return messagesViewDto;
        }//в message controller
    }
}
