using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using IMDArchitecture.API.Domain;
using IMDArchitecture.API.Ports;
using IMDArchitecture.API.Models;
using System.Collections.Generic;
using System.Xml.Serialization;
using SQLiteNetExtensions.Attributes;


namespace IMDArchitecture.API.Domain
{

    public class UserEvent
    {
        public Guid? UserEventId { get; set; }

        [SQLiteNetExtensions.Attributes.ForeignKey(typeof(User))]
        public int UserId { get; set; }

        [SQLiteNetExtensions.Attributes.ForeignKey(typeof(Event))]
        public int EventId { get; set; }

        public bool Enrolled { get; set; }

        public int RegisterTime { get; set; }

    }

}