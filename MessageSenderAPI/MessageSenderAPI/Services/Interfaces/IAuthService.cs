using MessageSenderAPI.Domain.Models;
using MessageSenderAPI.Domain.Request;
using MessageSenderAPI.Domain.Response;

namespace MessageSenderAPI.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(User loginUser);
        Task<RegisterResponse> RegisterAsync(User registerUser);
        Task<RegisterResponse> VerifyAsync(VerifyRequest verifyRequest);
        Task<string> ForgotPasswordAsync(string email);
        Task<string> ResetPasswordAsync(ResetPasswordRequest resetPasswordRequest);
        Task<string> ConfirmCodeAsync(VerifyRequest verifyRequest);
    }
}