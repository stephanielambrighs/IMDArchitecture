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
    public class SqliteDatabase : IDatabase
    {
        private EventContext _context;

        public SqliteDatabase(EventContext context)
        {
            _context = context;
        }
        public async Task DeleteEvent(Event Event)
        {
            var events = await _context.Events.FindAsync(Event.EventId);
            _context.Events.Remove(events);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUser(User User)
        {
            var user = await _context.Users.FindAsync(User.UserId);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<ReadOnlyCollection<Event>> GetAllEvents()
        {
            var Event = await _context.Events.ToArrayAsync();
            return Array.AsReadOnly(Event);
        }

        public async Task<ReadOnlyCollection<User>> GetAllUsers()
        {
            var User = await _context.Users.ToArrayAsync();
            return Array.AsReadOnly(User);
        }

        public async Task<Event> GetEventById(Guid EventId)
        {
            return await _context.Events.FindAsync(EventId);
        }

        public async Task<User> GetUserById(Guid UserId)
        {
            return await _context.Users.FindAsync(UserId);
        }

        public async Task<Event> CreateEvent(Event Event)
        {
            if (Event.EventId == null)
            {
                await _context.Events.AddAsync(Event);
            }
            else
            {
                _context.Events.Update(Event);
            }
            await _context.SaveChangesAsync();
            return Event;
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

        public async Task<Event> UpdateEvent(Event Event)
        {

            if (Event.EventId == null)
            {
                await _context.Events.AddAsync(Event);
            }
            else
            {
                _context.Events.Update(Event);
            }
            await _context.SaveChangesAsync();
            return Event;
        }
    }
}