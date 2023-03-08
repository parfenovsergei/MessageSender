using MessageSenderAPI.Domain.Models;

namespace MessageSenderAPI.Services.Interfaces
{
    public interface IAdminService
    {
        Task<List<User>> GetAllUsersAsync();
    }
}
