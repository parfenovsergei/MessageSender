using MessageSenderAPI.Domain.Models;

namespace MessageSenderAPI.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> Register(User user);
    }
}