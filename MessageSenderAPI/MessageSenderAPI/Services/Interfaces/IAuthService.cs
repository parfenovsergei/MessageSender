using MessageSenderAPI.Domain.Models;

namespace MessageSenderAPI.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> LoginAsync(User loginUser);
        Task<string> RegisterAsync(User registerUser);
    }
}