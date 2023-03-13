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
        Task<(bool, string)> ForgotPasswordAsync(string email);
        Task<(bool, string)> ResetPasswordAsync(ResetPasswordRequest resetPasswordRequest);
        Task<(bool, string)> ConfirmCodeAsync(VerifyRequest verifyRequest);
    }
}