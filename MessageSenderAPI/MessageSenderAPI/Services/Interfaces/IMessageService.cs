using MessageSenderAPI.Domain.Models;
using MessageSenderAPI.Domain.Response;

namespace MessageSenderAPI.Services.Interfaces
{
    public interface IMessageService
    {
        Task<GeneralResponse> CreateMessageAsync(Message message, string userEmail);
        Task<GeneralResponse> DeleteMessageAsync(int id);
        Task<(bool, List<Message>)> GetAllMessagesAsync();
        Task<(bool, Message)> GetMessageByIdAsync(int id);
        Task<(bool, List<Message>)> GetMessagesAsync(string userEmail);
        Task<(bool, List<Message>)> GetMessagesByUserIdAsync(int id);
        Task<GeneralResponse> UpdateMessageAsync(int id, Message message);
    }
}
