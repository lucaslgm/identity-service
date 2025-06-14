using Identity.Domain.Entities;
using Identity.Domain.Interfaces;
using Identity.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IdentityDbContext _context;

        public UserRepository(IdentityDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<int> AddAsync(User user)
        {
            ArgumentNullException.ThrowIfNull(user);

            var result = await _context.Users.AddAsync(user);
            return result.Entity.Id;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return false;

            _context.Users.Remove(user);
            return true;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var result = await _context.Users.ToListAsync();
            return result ?? Enumerable.Empty<User>();
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            ArgumentNullException.ThrowIfNull(email);

            var result = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            return result;
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetByUserNameAsync(string username)
        {
            ArgumentNullException.ThrowIfNull(username);
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<bool> UpdateAsync(User user)
        {
            ArgumentNullException.ThrowIfNull(user);
            var existingUser = await _context.Users.FindAsync(user.Id);
            if (existingUser == null)
                return false;

            _context.Entry(existingUser).CurrentValues.SetValues(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}