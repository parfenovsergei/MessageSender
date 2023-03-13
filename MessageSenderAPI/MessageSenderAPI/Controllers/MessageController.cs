using AutoMapper;
using MessageSenderAPI.Domain.Enums;
using MessageSenderAPI.Domain.Models;
using MessageSenderAPI.Domain.ModelsDTO;
using MessageSenderAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace MessageSenderAPI.Controllers
{
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
        public async Task<IActionResult> GetMessagesAsync()
        {
            var userEmail = HttpContext.User.Claims.Single(x => x.Type == ClaimTypes.Email).Value;
            var messages = await _messageService.GetMessagesAsync(userEmail);
            if(messages.Item1)
            {
                var messagesViewDto = _mapper.Map<List<MessageViewDTO>>(messages.Item2);
                return Ok(messagesViewDto);
            }
            return NoContent();
        }

        [HttpGet("messages/{id}")]
        public async Task<IActionResult> GetMessageByIdAsync(int id)
        {
            var message = await _messageService.GetMessageByIdAsync(id);
            if(message.Item1)
            {
                var messageViewDto = _mapper.Map<MessageViewDTO>(message.Item2);
                return Ok(messageViewDto);
            }
            return NoContent();
           
        }

        [HttpPost("messages")]
        public async Task<ActionResult<string>> CreateMessageAsync([FromBody] MessageDTO messageDTO)
        {
            var userEmail = HttpContext.User.Claims.Single(x => x.Type == ClaimTypes.Email).Value;
            var message = _mapper.Map<Message>(messageDTO);
            var result = await _messageService.CreateMessageAsync(message, userEmail);
            if(result.Item1)
                return Ok(result.Item2);
            return BadRequest(result.Item2);
        }

        [HttpPut("messages/{id}")]
        public async Task<ActionResult<string>> UpdateMessageAsync(int id, [FromBody] MessageDTO messageDTO)
        {
            var message = _mapper.Map<Message>(messageDTO);
            var result = await _messageService.UpdateMessageAsync(id, message);
            if (result.Item1)
                return Ok(result.Item2);
            return BadRequest(result.Item2);
        }

        [HttpDelete("messages/{id}")]
        public async Task<ActionResult<string>> DeleteMessageAsync(int id)
        {
            var result = await _messageService.DeleteMessageAsync(id);
            if (result.Item1)
                return Ok(result.Item2);
            return BadRequest(result.Item2);
        }
    }
}