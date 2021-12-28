using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using IMDArchitecture.API.Domain;
using IMDArchitecture.API.Ports;
using IMDArchitecture.API.Models;

namespace IMDArchitecture.API.Ports
{
    public interface IUserEventRepository
    {
        Task<ReadOnlyCollection<UserEvent>> GetAllUserEvents();
        Task<UserEvent> GetUserEventById(int UserEventId);
        Task<UserEvent> CreateUserEvent(UserEvent UserEvents);
        Task DeleteUserEvent(UserEvent UserEvents);

    }
}