using MessageSenderAPI.Domain.Models;

namespace MessageSenderAPI.Services.Interfaces
{
    public interface IAdminService
    {
        Task<List<User>> GetAllUsersAsync();
        Task<List<Message>> GetMessagesByUserIdAsync(int id);
    }
}
