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

    public class UserEvent
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int UserEventId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int EventId { get; set; }

        public bool Enrolled { get; set; }

        public int RegisterTime { get; set; }

        public User Users { get; set; }

        public Event Events { get; set; }

    }

}