using System;
using System.Linq;
using IMDArchitecture.API.Domain;
using IMDArchitecture.API.Ports;
using IMDArchitecture.API.Models;

namespace IMDArchitecture.API.Controllers
{
    // DTO stands for Data Transfer Object; these are dumb classes that should only be used
    // for transferring data between layers of the application.
    public class CreateEvent
    {
        public Guid? EventId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Date { get; set; }
        public int Participants { get; set; }
        public int Target_age { get; set; }
        public Event ToEvent() => new Event { EventId = this.EventId, Name = this.Name, Description = this.Description, Date = this.Date, Participants = this.Participants, Target_age = this.Target_age };
    }

    public class ViewEvent
    {
        public string EventId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Date { get; set; }
        public int Participants { get; set; }
        public int Target_age { get; set; }
        public static ViewEvent FromModel(Event Event) => new ViewEvent
        {
            EventId = Event.EventId.ToString(),
            Name = Event.Name,
            Description = Event.Description,
            Date = Event.Date,
            Participants = Event.Participants,
            Target_age = Event.Target_age,
        };
    }
}