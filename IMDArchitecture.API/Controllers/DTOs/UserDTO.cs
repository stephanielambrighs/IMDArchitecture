using System;
using System.Linq;
using IMDArchitecture.API.Domain;

namespace IMDArchitecture.API.Controllers
{
    // DTO stands for Data Transfer Object; these are dumb classes that should only be used
    // for transferring data between layers of the application.
    public class CreateUser
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int Date_of_birth { get; set; }
        public User ToUser() => new User { UserId = this.UserId, UserName = this.UserName, Email = this.Email, Date_of_birth = this.Date_of_birth };
    }

    public class ViewUser
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int Date_of_birth { get; set; }
        public static ViewUser FromModel(User User) => new ViewUser
        {
            UserId = User.UserId,
            UserName = User.UserName,
            Email = User.Email,
            Date_of_birth = User.Date_of_birth,
        };
    }
}