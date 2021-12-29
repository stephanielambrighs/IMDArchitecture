using System;
using System.Linq;
using IMDArchitecture.API.Domain;
using IMDArchitecture.API.Ports;
using IMDArchitecture.API.Models;

namespace IMDArchitecture.API.Controllers
{
    // DTO stands for Data Transfer Object; these are dumb classes that should only be used
    // for transferring data between layers of the application.
    public class CreateUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Administrator { get; set; }

        public User ToUser() => new User { Firstname = this.Firstname, Lastname = this.Lastname, Email = this.Email, DateOfBirth = this.DateOfBirth, Administrator = this.Administrator };
    }

    public class UpdateUser
    {
        public int UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Administrator { get; set; }

        public User updateUser() => new User { UserId = this.UserId, Firstname = this.Firstname, Lastname = this.Lastname, Email = this.Email, DateOfBirth = this.DateOfBirth, Administrator = this.Administrator };
    }

    public class ViewUser
    {
        public int UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Administrator { get; set; }
        public static ViewUser FromModel(User User) => new ViewUser
        {
            UserId = User.UserId,
            Firstname = User.Firstname,
            Lastname = User.Lastname,
            Email = User.Email,
            DateOfBirth = User.DateOfBirth,
            Administrator = User.Administrator,
        };
    }
}