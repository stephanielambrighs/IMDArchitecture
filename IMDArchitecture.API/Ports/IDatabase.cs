using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using IMDArchitecture.API.Domain;

namespace IMDArchitecture.API.Ports
{
    public interface IDatabase
    {
        // Readonly collection indicates that you can't just add movies to the database via the collection.
        Task<ReadOnlyCollection<Event>> GetAllEvents(string titleStartsWith);
        Task<Event> GetEventById(Guid EventId);
        Task<Event> PersistEvent(Event Event);
        Task DeleteEvent(Guid parsedId);
    }
}