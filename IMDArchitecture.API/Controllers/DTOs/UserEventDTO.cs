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
    public class CreateUserEvent
    {
        public int UserRelationId { get; set; }
        public int EventRelationId { get; set; }
        public DateTime RegisterTime { get; set; }
        public bool Enrolled { get; set; }
        public UserEvent ToUserEvent() => new UserEvent { UserRelationId = this.UserRelationId, EventRelationId = this.EventRelationId, RegisterTime = this.RegisterTime, Enrolled = this.Enrolled };
    }

    public class ViewUserEvent
    {
        public int UserEventId { get; set; }
        public int UserRelationId { get; set; }
        public int EventRelationId { get; set; }
        public bool Enrolled { get; set; }
        public DateTime RegisterTime { get; set; }
        public static ViewUserEvent FromModel(UserEvent UserEvent) => new ViewUserEvent
        {
            UserEventId = UserEvent.UserEventId,
            UserRelationId = UserEvent.UserRelationId,
            EventRelationId = UserEvent.EventRelationId,
            Enrolled = UserEvent.Enrolled,
            RegisterTime = UserEvent.RegisterTime,
        };
    }
}