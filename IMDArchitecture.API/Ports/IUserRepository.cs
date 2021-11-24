using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using IMDArchitecture.API.Domain;
using IMDArchitecture.API.Ports;
using IMDArchitecture.API.Models;

namespace IMDArchitecture.API.Ports
{
    public interface IUserRepository
    {
        Task<ReadOnlyCollection<User>> GetAllUsers();
        Task<User> GetUserById(Guid UserId);
        Task<User> CreateUser(User User);
        Task<User> UpdateUser(User User);
        Task DeleteUser(User User);
    }
}