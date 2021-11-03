using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using IMDArchitecture.API.Domain;
using IMDArchitecture.API.Ports;
using IMDArchitecture.API.Models;

namespace IMDArchitecture.API.Ports
{
    public interface IDatabase
    {
        // Readonly collection indicates that you can't just add movies to the database via the collection.
        Task<ReadOnlyCollection<Event>> GetAllEvents(string events);
        Task<ReadOnlyCollection<User>> GetAllUsers(string users);

        Task<Event> GetEventById(Guid EventId);
        Task<User> GetUserById(Guid UserId);

        Task<Event> PersistEvent(Event Event);
        Task<User> PersistUser(User User);

        Task DeleteEvent(Guid parsedId);
        Task DeleteUser(Guid parsedId);

    }
}