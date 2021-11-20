using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using IMDArchitecture.API.Domain;
using IMDArchitecture.API.Ports;
using IMDArchitecture.API.Models;



namespace IMDArchitecture.API.Models
{

    public class EventContext : DbContext
    {
        public EventContext(DbContextOptions<EventContext> ctx) : base(ctx)
        {

        }

        public DbSet<Event> Events { get; set; }
        public DbSet<User> Users { get; set; }

    }
}