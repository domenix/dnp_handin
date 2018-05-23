using System;
using System.Linq;
using VIAMovies.Data;
using VIAMovies.Models;

public static class DbInitializer
{
    public static void Initialize(ApplicationDbContext context)
    {
        if (context.Movies.Any())
        {
            return;
        }

        var movies = new Movie[] {
            new Movie {title = "The Great Gatsby", duration = 143},
            new Movie {title = "Interstellar", duration = 169}
        };

        foreach (Movie m in movies)
        {
            context.Add(m);
        }

        context.SaveChanges();

        if (context.Screenings.Any())
        {
            return;
        }

        var screenings = new Screening[] {
            new Screening {Movie = movies.Single(s => s.title == "The Great Gatsby"), date = DateTime.Parse("2018-06-04 15:00")},
            new Screening {Movie = movies.Single(s => s.title == "Interstellar"), date = DateTime.Parse("2018-06-04 20:00")}
        };

        foreach (Screening s in screenings)
        {
            context.Add(s);
        }

        context.SaveChanges();
    }
}