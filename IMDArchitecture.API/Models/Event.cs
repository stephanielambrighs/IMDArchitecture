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
    // public class EventContext : DbContext
    // {
    // public DbSet<User> Users { get; set; }
    // public DbSet<Event> Events { get; set; }
    // public string DbPath { get; private set; }


    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.UseSqlServer(
    //         @"Server=(localdb)\mssqllocaldb;Database=Blogging;Trusted_Connection=True");
    // }

    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<Event>()
    //         .Property(b => b.Url);
    //         .IsRequired();
    // }
    // public EventContext()
    // {
    //     var folder = Environment.SpecialFolder.LocalApplicationData;
    //     var path = Environment.GetFolderPath(folder);
    //     DbPath = $"{path}{System.IO.Path.DirectorySeparatorChar}event.db";
    // }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    //     protected override void OnConfiguring(DbContextOptionsBuilder options)
    //         => options.UseSqlite($"Data Source={DbPath}");
    // }

    // public class User
    // {
    //     public int UserId { get; set; }
    //     public string Firstname { get; set; }
    //     public string Lastname { get; set; }
    //     public string Email { get; set; }
    //     public int Date_of_birth { get; set; }
    //     public bool Administrator { get; set; }
    // }


    // public class Event
    // {
    //     public int EventId { get; set; }
    //     public string Name { get; set; }
    //     public string Description { get; set; }
    //     public int Date { get; set; }
    //     public int Participants { get; set; }
    //     public int Target_age { get; set; }
    // }



}




// using System;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Logging;
// using IMDArchitecture.API.Domain;

// namespace IMDArchitecture.API.Model
// {
//     public class EventContext : DbContext
//     {
//         public EventContext(DbContextOptions<EventContext> ctx) : base(ctx)
//         {

//         }

//         public DbSet<Event> Events { get; set; }
//     }
// }