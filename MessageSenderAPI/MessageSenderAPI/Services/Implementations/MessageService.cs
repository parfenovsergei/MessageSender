using MessageSenderAPI.Services.Interfaces;

namespace MessageSenderAPI.Services.Implementations
{
    public class MessageService : IMessageService
    {
        private readonly ApplicationDbContext _context;
        public MessageService(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
