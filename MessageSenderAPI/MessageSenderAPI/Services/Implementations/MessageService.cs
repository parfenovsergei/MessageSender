using MessageSenderAPI.Domain.Models;
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

        public async Task<string> CreateMessageAsync(Message message, string userEmail)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == userEmail);
            var newMessage = new Message()
            {
                MessageTheme = message.MessageTheme,
                MessageBody = message.MessageBody,
                SendDate = message.SendDate,
                Owner = user,
                IsSend = false
            };
            await _context.Messages.AddAsync(newMessage);
            await _context.SaveChangesAsync();
            return "Message added to DB.";
        }

        public async Task<List<Message>> GetMessagesAsync(string userEmail)
        {
            var messages = await _context.Messages
                .Where(m => m.Owner.Email == userEmail)
                .ToListAsync();
            return messages;
        }
    }
}
