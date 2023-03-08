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
    [Route("admin")]
    [ApiController]
    [AuthorizeRoles(Role.Admin)]
    public class AdminController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAdminService _adminService;

        public AdminController(IMapper mapper, IAdminService adminService)
        {
            _mapper = mapper;
            _adminService = adminService;
        }

        [HttpGet("users")]
        public async Task<List<UserDTO>> GetAllUsersAsync()
        {
            var users = await _adminService.GetAllUsersAsync();
            var result = _mapper.Map<List<UserDTO>>(users);
            return result;
        }

        [HttpGet("users/{id}/messages")]
        public async Task<List<MessageViewDTO>> GetMessageByUserIdAsync(int id)
        {
            var messages = await _adminService.GetMessagesByUserIdAsync(id);
            var messagesViewDto = _mapper.Map<List<MessageViewDTO>>(messages);
            return messagesViewDto;
        }
    }
}
