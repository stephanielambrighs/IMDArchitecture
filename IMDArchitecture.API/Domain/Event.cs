using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace IMDArchitecture.API.Domain
{
    // this is a domain model. It contains the full representation of an entity within our domain.
    public class Event
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EventId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Date { get; set; }
        public int Participants { get; set; }
        public int Target_age { get; set; }
    }
}