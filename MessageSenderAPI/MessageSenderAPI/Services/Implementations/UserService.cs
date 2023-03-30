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

        public async Task CheckUnverifiedUsersAsync()
        {
            var unverifiedUsers = _context.Users
                .Where(u => u.IsVerifed == false && u.CreateAndVerifyTime.AddMinutes(2) <= DateTime.Now);
            if (unverifiedUsers != null)
            {
                _context.Users.RemoveRange(unverifiedUsers);
                await _context.SaveChangesAsync();
                Console.WriteLine("Unverified users deleted!");
            }
            else
                Console.WriteLine("All verified!");
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
