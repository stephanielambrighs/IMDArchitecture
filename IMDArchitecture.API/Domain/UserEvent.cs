using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using IMDArchitecture.API.Domain;
using IMDArchitecture.API.Ports;
using IMDArchitecture.API.Models;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;

namespace IMDArchitecture.API.Domain
{
    // this is a domain model. It contains the full representation of an entity within our domain.
    public class UserEvent
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int UserEventId { get; set; }
        public int UserRelationId { get; set; }
        public int EventRelationId { get; set; }
        public bool Enrolled { get; set; }
        public DateTime RegisterTime { get; set; }
        public User Users { get; set; }
        public Event Events { get; set; }

    }

}