using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using IMDArchitecture.API.Domain;
using IMDArchitecture.API.Ports;
using IMDArchitecture.API.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IMDArchitecture.API.Domain
{
    // this is a domain model. It contains the full representation of an entity within our domain.
    public class Event
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int EventId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int ParticipantCount { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public ICollection<UserEvent> UserEvents { get; set; }
    }
}