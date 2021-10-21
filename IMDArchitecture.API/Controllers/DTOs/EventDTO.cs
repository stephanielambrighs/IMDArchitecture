using System;
using System.Linq;
using RandalsVideoStore.API.Domain;

namespace IMDArchitecture.API.Controllers
{
    // DTO stands for Data Transfer Object; these are dumb classes that should only be used
    // for transferring data between layers of the application.
    public class CreateEvent
    {
        public Guid? Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public Event ToEvent() => new Event { Title = this.Title, Year = this.Year, Genres = this.Genres, Id = this.Id };
    }

    public class ViewEvent
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string FormattedYear { get; set; }
        public static ViewMovie FromModel(Event event) => new ViewEvent
        {
            Id = event.Id.ToString(),
            Title = movie.Title,
            Year = movie.Year,
            Genres = movie.Genres,
            FormattedYear = FormatYear(movie.Year),
        };

    private static string FormatYear(int year)
    {
        var yearAsString = year.ToString();
        return yearAsString.Length == 4 ? $"'{yearAsString.Substring(2, 2)}" : yearAsString;
    }
}
}