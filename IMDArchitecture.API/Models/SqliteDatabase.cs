using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IMDArchitecture.API.Domain;
using IMDArchitecture.API.Ports;
using IMDArchitecture.API.Models;
// using EFGetStarted;

namespace IMDArchitecture.API.Models
{
    public class SqliteDatabase : IDatabase
    {
        private EventContext _context;

        public SqliteDatabase(EventContext context)
        {
            _context = context;
        }
        public async Task DeleteEvent(Guid parsedId)
        {
            var Event = await _context.Events.FindAsync(parsedId);
            _context.Events.Remove(Event);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUser(Guid parsedId)
        {
            // throw new NotImplementedException();
            var User = await _context.Users.FindAsync(parsedId);
            _context.Users.Remove(User);
            await _context.SaveChangesAsync();
        }

        public async Task<ReadOnlyCollection<Event>> GetAllEvents(string events)
        {

            var Event = await _context.Events.Where(x => EF.Functions.Like(x.Name, $"{events}%")).ToArrayAsync();
            return Array.AsReadOnly(Event);
        }

        public async Task<ReadOnlyCollection<User>> GetAllUsers(string users)
        {
            // throw new NotImplementedException();
            var User = await _context.Users.Where(x => EF.Functions.Like(x.Firstname, $"{users}%")).ToArrayAsync();
            return Array.AsReadOnly(User);
        }

        public async Task<Event> GetEventById(Guid EventId)
        {
            return await _context.Events.FindAsync(EventId);
        }

        public async Task<User> GetUserById(Guid UserId)
        {
            // throw new NotImplementedException();
            return await _context.Users.FindAsync(UserId);
        }

        public async Task<Event> PersistEvent(Event Event)
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

        public async Task<User> PersistUser(User User)
        {
            // throw new NotImplementedException();
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