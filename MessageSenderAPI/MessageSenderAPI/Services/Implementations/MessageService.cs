using Azure;
using MessageSenderAPI.Domain.Models;
using MessageSenderAPI.Domain.Response;
using MessageSenderAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MessageSenderAPI.Services.Implementations
{
    public class MessageService : IMessageService
    {
        private readonly ApplicationDbContext _context;
        public MessageService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GeneralResponse> CreateMessageAsync(Message message, string userEmail)
        {
            var response = new GeneralResponse();
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == userEmail);
            var newMessage = new Message()
            {
                MessageTheme = message.MessageTheme,
                MessageBody = message.MessageBody,
                SendDate = message.SendDate.ToLocalTime(),
                Owner = user,
                IsSend = false
            };
            await _context.Messages.AddAsync(newMessage);
            await _context.SaveChangesAsync();
            response.Flag = true;
            response.Message = "Message created";
            return response;
        }

        public async Task<GeneralResponse> DeleteMessageAsync(int id)
        {
            var response = new GeneralResponse();
            var message = await _context.Messages.FirstOrDefaultAsync(m => m.Id == id);
            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();
            response.Flag = true;
            response.Message = "Message deleted";
            return response;
        }

        public async Task<(bool, List<Message>)> GetAllMessagesAsync()
        {
            var messages = await _context.Messages.ToListAsync();
            if (messages == null)
                return (false, messages);
            return (true, messages);
        }

        public async Task<(bool, Message)> GetMessageByIdAsync(int id)
        {
            var message = await _context.Messages.FirstOrDefaultAsync(m => m.Id == id);
            if (message == null)
                return (false, message);
            return (true, message);
        }

        public async Task<(bool, List<Message>)> GetMessagesAsync(string userEmail)
        {
            var messages = await _context.Messages
                .Include(m => m.Owner)
                .Where(m => m.Owner.Email == userEmail)
                .ToListAsync();
            if (messages == null)
                return (false, messages);
            return (true, messages);
        }

        public async Task<(bool, List<Message>)> GetMessagesByUserIdAsync(int id)
        {
            var messages = await _context.Messages
                .Include(m => m.Owner)
                .Where(m => m.Owner.Id == id)
                .ToListAsync();
            if (messages == null)
                return (false, messages);
            return (true, messages);
        }

        public async Task<GeneralResponse> UpdateMessageAsync(int id, Message newMessage)
        {
            var response = new GeneralResponse();
            var message = await _context.Messages.FirstOrDefaultAsync(m => m.Id == id);
            message.MessageTheme = newMessage.MessageTheme;
            message.MessageBody = newMessage.MessageBody;
            message.SendDate = newMessage.SendDate.ToLocalTime();
            _context.Messages.Update(message);
            await _context.SaveChangesAsync();
            response.Flag = true;
            response.Message = "Message updated";
            return response;
        }
    }
}
