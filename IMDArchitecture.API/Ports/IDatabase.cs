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
        Task<ReadOnlyCollection<Event>> GetAllEvents(string events);
        Task<ReadOnlyCollection<User>> GetAllUsers(string users);

        Task<Event> GetEventById(Guid EventId);
        Task<User> GetUserById(Guid UserId);

        Task<Event> PersistEvent(Event Event);
        Task<User> PersistUser(User User);

        Task DeleteEvent(Event Event);
        Task DeleteUser(User User);

    }
}