using System;

namespace IMDArchitecture.API
{
    public class User
    {
        public int UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public int Date_of_birth { get; set; }
        public bool Administrator { get; set; }
    }
}
