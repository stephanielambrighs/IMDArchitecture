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
    public class UserEventDb : IUserEventRepository
    {
        private DatabaseContext _context;

        public UserEventDb(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<ReadOnlyCollection<UserEvent>> GetAllUserEvents()
        {
            var userEvent = await _context.UserEvents.ToArrayAsync();
            return Array.AsReadOnly(userEvent);
        }
        public async Task DeleteUserEvent(UserEvent UserEvents)
        {
            var userEventId = await _context.UserEvents.FindAsync(UserEvents.UserEventId);
            _context.UserEvents.Remove(userEventId);
            await _context.SaveChangesAsync();
        }
        public async Task<UserEvent> GetUserEventById(int UserEventId)
        {
            return await _context.UserEvents.FindAsync(UserEventId);
        }
        public async Task<UserEvent> CreateUserEvent(UserEvent UserEvents)
        {
            _context.UserEvents.Update(UserEvents);
            await _context.SaveChangesAsync();
            return UserEvents;
        }
    }
}