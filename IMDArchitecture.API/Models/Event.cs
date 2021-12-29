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
    public class EventDb : IEventRepository
    {
        private DatabaseContext _context;

        public EventDb(DatabaseContext context)
        {
            _context = context;
        }
        public async Task DeleteEvent(Event Event)
        {
            var events = await _context.Events.FindAsync(Event.EventId);
            _context.Events.Remove(events);
            await _context.SaveChangesAsync();
        }

        public async Task<ReadOnlyCollection<Event>> GetAllEvents()
        {
            var Event = await _context.Events.ToArrayAsync();
            return Array.AsReadOnly(Event);
        }

        public async Task<Event> GetEventById(int EventId)
        {
            return await _context.Events.FindAsync(EventId);
        }

        public async Task<Event[]> GetEventByAge(int age)
        {
            var events = await _context.Events.Where(x => x.MinAge <= age && age <= x.MaxAge).ToArrayAsync();
            return events;
        }

        public async Task<Event> CreateEvent(Event Event)
        {
            _context.Events.Update(Event);
            await _context.SaveChangesAsync();
            return Event;
        }

        public async Task<Event> UpdateEvent(Event Event)
        {
            if (Event.EventId == 0)
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