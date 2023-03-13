using MessageSenderAPI.Domain.Models;

namespace MessageSenderAPI.Services.Interfaces
{
    public interface IEmailService
    {
        Task CheckToSendMessagesAsync();
        Task SendVerifyCodeAsync(string email, int code);
    }
}
