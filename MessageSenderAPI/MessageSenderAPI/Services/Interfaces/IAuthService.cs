using MessageSenderAPI.Domain.Models;
using MessageSenderAPI.Domain.ModelsDTO;
using MessageSenderAPI.Domain.Response;

namespace MessageSenderAPI.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(User loginUser);
        Task<RegisterResponse> RegisterAsync(User registerUser);
        Task<RegisterResponse> VerifyAsync(UserVerifyDTO userVerifyDTO);
    }
}