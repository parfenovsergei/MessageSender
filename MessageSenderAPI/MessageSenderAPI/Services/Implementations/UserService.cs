using MessageSenderAPI.Domain.Models;
using MessageSenderAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MessageSenderAPI.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public async Task<List<Message>> GetMessagesByUserIdAsync(int id)
        {
            var messages = await _context.Messages
                .Include(m => m.Owner)
                .Where(m => m.Owner.Id == id)
                .ToListAsync();
            return messages;
        }
    }
}
