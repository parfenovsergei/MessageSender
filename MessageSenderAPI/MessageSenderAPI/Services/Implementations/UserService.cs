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

        public async Task<(bool, List<User>)> GetAllUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            if (users != null)
            {
                return (true, users);
            }
            return (false, users);
        }
    }
}
