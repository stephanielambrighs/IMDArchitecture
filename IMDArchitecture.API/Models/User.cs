using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IMDArchitecture.API.Domain;
using IMDArchitecture.API.Ports;
using IMDArchitecture.API.Models;

namespace IMDArchitecture.API.Models
{
    public class UserDb : IUserRepository
    {
        private DatabaseContext _context;

        public UserDb(DatabaseContext context)
        {
            _context = context;
        }

        public async Task DeleteUser(User User)
        {
            var user = await _context.Users.FindAsync(User.UserId);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }


        public async Task<ReadOnlyCollection<User>> GetAllUsers()
        {
            var User = await _context.Users.ToArrayAsync();
            return Array.AsReadOnly(User);
        }


        public async Task<User> GetUserById(Guid UserId)
        {
            return await _context.Users.FindAsync(UserId);
        }

        public async Task<User> UpdateUser(User User)
        {
            if (User.UserId == null)
            {
                await _context.Users.AddAsync(User);
            }
            else
            {
                _context.Users.Update(User);
            }
            await _context.SaveChangesAsync();
            return User;
        }

        public async Task<User> CreateUser(User User)
        {
            if (User.UserId == null)
            {
                await _context.Users.AddAsync(User);
            }
            else
            {
                _context.Users.Update(User);
            }
            await _context.SaveChangesAsync();
            return User;
        }
    }
}