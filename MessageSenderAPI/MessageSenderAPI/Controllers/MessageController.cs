using AutoMapper;
using MessageSenderAPI.Domain.Models;
using MessageSenderAPI.Domain.ModelsDTO;
using MessageSenderAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MessageSenderAPI.Controllers
{
    [Route("user")]
    [ApiController]
    [Authorize]
    public class MessageController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMessageService _messageService;
        public MessageController(IMapper mapper, IMessageService messageService)
        {
            _mapper = mapper;
            _messageService = messageService;
        }

        [HttpGet("messages")]
        public async Task<List<MessageViewDTO>> GetMessagesAsync()
        {
            var userEmail = HttpContext.User.Claims.Single(x => x.Type == ClaimTypes.Email).Value;
            var messages = await _messageService.GetMessagesAsync(userEmail);
            var messagesViewDto = _mapper.Map<List<MessageViewDTO>>(messages);
            return messagesViewDto;
        }

        [HttpPost("messages")]
        public async Task<string> CreateMessageAsync([FromBody] MessageDTO messageDTO)
        {
            var userEmail = HttpContext.User.Claims.Single(x => x.Type == ClaimTypes.Email).Value;
            var message = _mapper.Map<Message>(messageDTO);
            var result = await _messageService.CreateMessageAsync(message, userEmail);
            return result;
        }
    }
}
