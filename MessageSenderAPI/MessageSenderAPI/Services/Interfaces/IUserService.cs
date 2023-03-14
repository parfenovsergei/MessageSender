using MessageSenderAPI.Domain.Models;

namespace MessageSenderAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task CheckUnverifiedUsersAsync();
        Task<(bool, List<User>)> GetAllUsersAsync();
    }
}
