using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace IMDArchitecture.API.Domain
{
    // this is a domain model. It contains the full representation of an entity within our domain.
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int Date_of_birth { get; set; }
    }
}