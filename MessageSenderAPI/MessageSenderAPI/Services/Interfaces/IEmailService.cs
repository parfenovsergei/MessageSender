using MessageSenderAPI.Domain.Models;

namespace MessageSenderAPI.Services.Interfaces
{
    public interface IEmailService
    {
        Task CheckToSendMessages();
    }
}
