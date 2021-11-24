using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using IMDArchitecture.API.Domain;
using IMDArchitecture.API.Ports;
using IMDArchitecture.API.Models;

namespace IMDArchitecture.API.Ports
{
    public interface IEventRepository
    {
        Task<ReadOnlyCollection<Event>> GetAllEvents();
        Task<Event> GetEventById(Guid EventId);
        Task<Event> CreateEvent(Event Event);
        Task DeleteEvent(Event Event);

    }
}