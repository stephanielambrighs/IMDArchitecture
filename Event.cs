using System;

namespace IMDArchitecture.API
{
    public class Event
    {
        public int EventId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Date { get; set; }
        public int Participants { get; set; }
        public int Target_age { get; set; }
    }
}
