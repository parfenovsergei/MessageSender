using MessageSenderAPI.Domain.Models;

namespace MessageSenderAPI.Services.Interfaces
{
    public interface IMessageService
    {
        Task<(bool, string)> CreateMessageAsync(Message message, string userEmail);
        Task<(bool, string)> DeleteMessageAsync(int id);
        Task<(bool, List<Message>)> GetAllMessagesAsync();
        Task<(bool, Message)> GetMessageByIdAsync(int id);
        Task<(bool, List<Message>)> GetMessagesAsync(string userEmail);
        Task<(bool, string)> UpdateMessageAsync(int id, Message message);
    }
}
