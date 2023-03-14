using MessageSenderAPI.Domain.Models;
using MessageSenderAPI.Domain.Request;
using MessageSenderAPI.Domain.Response;

namespace MessageSenderAPI.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(User loginUser);
        Task<GeneralResponse> RegisterationAsync(User registerUser);
        Task<GeneralResponse> VerifyAsync(VerifyRequest verifyRequest);
        Task<GeneralResponse> ForgotPasswordAsync(string email);
        Task<GeneralResponse> ResetPasswordAsync(ResetPasswordRequest resetPasswordRequest);
        Task<GeneralResponse> ConfirmCodeAsync(VerifyRequest verifyRequest);
    }
}