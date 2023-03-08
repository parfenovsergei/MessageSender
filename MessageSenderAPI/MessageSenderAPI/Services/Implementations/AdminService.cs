using MessageSenderAPI.Domain.Models;
using MessageSenderAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MessageSenderAPI.Services.Implementations
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext _context;
        public AdminService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }
    }
}
