using MessageSenderAPI.Domain.Models;

namespace MessageSenderAPI.Services.Interfaces
{
    public interface IMessageService
    {
        Task<string> CreateMessageAsync(Message message, string userEmail);
        Task<string> DeleteMessageAsync(int id);
        Task<List<Message>> GetMessagesAsync(string userEmail);
        Task<string> UpdateMessageAsync(int id, Message message);
    }
}
