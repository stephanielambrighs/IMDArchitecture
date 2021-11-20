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
        Task<ReadOnlyCollection<Event>> GetAllEvents();
        Task<ReadOnlyCollection<User>> GetAllUsers();
        Task<Event> GetEventById(Guid EventId);
        Task<User> GetUserById(Guid UserId);
        Task<Event> CreateEvent(Event Event);
        Task<User> CreateUser(User User);
        Task<User> UpdateUser(User User);
        Task<Event> UpdateEvent(Event Event);
        Task DeleteEvent(Event Event);
        Task DeleteUser(User User);

    }
}