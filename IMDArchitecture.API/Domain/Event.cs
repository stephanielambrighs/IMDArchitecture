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
    public class Event
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid? EventId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Date { get; set; }
        public int ParticipantCount { get; set; }
        public int TargetAge { get; set; }

        [ManyToMany(typeof(UserEvent))]
        public List<User> User { get; set; }
    }
}