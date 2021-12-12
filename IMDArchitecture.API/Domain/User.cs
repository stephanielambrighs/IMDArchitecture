using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using IMDArchitecture.API.Domain;
using IMDArchitecture.API.Ports;
using IMDArchitecture.API.Models;
using System.Collections.Generic;
using SQLiteNetExtensions.Attributes;

namespace IMDArchitecture.API.Domain
{
    // this is a domain model. It contains the full representation of an entity within our domain.
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid? UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public int DateOfBirth { get; set; }
        public bool Administrator { get; set; }

        [ManyToMany(typeof(UserEvent))]
        public List<Event> Event { get; set; }
    }
}