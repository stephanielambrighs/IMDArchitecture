using System;
using System.Linq;
using IMDArchitecture.API.Domain;
using IMDArchitecture.API.Ports;
using IMDArchitecture.API.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IMDArchitecture.API.Controllers
{
    // DTO stands for Data Transfer Object; these are dumb classes that should only be used
    // for transferring data between layers of the application.
    public class CreateEvent
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Date { get; set; }
        public int ParticipantCount { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public Event ToEvent() => new Event { Name = this.Name, Description = this.Description, Date = this.Date, ParticipantCount = this.ParticipantCount, MinAge = this.MinAge, MaxAge = this.MaxAge };
    }

    public class UpdateEvent
    {
        public int EventId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Date { get; set; }
        public int ParticipantCount { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public Event updateEvent() => new Event { EventId = this.EventId, Name = this.Name, Description = this.Description, Date = this.Date, ParticipantCount = this.ParticipantCount, MinAge = this.MinAge, MaxAge = this.MaxAge };
    }

    public class ViewEvent
    {
        public int EventId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Date { get; set; }
        public int ParticipantCount { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public static ViewEvent FromModel(Event Event) => new ViewEvent
        {
            EventId = Event.EventId,
            Name = Event.Name,
            Description = Event.Description,
            Date = Event.Date,
            ParticipantCount = Event.ParticipantCount,
            MinAge = Event.MinAge,
            MaxAge = Event.MaxAge,
        };
    }
}