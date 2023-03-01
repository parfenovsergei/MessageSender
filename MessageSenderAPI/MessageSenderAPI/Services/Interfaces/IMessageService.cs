using MessageSenderAPI.Domain.Models;

namespace MessageSenderAPI.Services.Interfaces
{
    public interface IMessageService
    {
        Task<string> CreateMessageAsync(Message message, string userEmail);
        Task<List<Message>> GetMessagesAsync(string userEmail);
    }
}
