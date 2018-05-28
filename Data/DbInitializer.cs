using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using VIAMovies.Data;
using VIAMovies.Models;

public interface IDbInitializer
{
    void Initialize();
}
public class DbInitializer : IDbInitializer
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public DbInitializer(ApplicationDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }
    public async void Initialize()
    {
        if (_context.UserRoles.Any())
        {
            return;
        }

        var roles = new string[] {
            "Admin",
            "User"
        };

        foreach (string r in roles)
        {
            await _roleManager.CreateAsync(new IdentityRole(r));
        }

        if (_context.Users.Any())
        {
            return;
        }

        var users = new User[] {
            new User {UserName = "admin", Email = "admin@admin.com"},
            new User {UserName = "user1", Email = "user1@user.com"},
            new User {UserName = "user2", Email = "user2user.com"},
            new User {UserName = "user3", Email = "user3@user.com"},
            new User {UserName = "user4", Email = "user4@user.com"}
        };

        var password = "1234";

        await _userManager.CreateAsync(users[0], password);
        await _userManager.AddToRoleAsync(users[0], roles[0]);

        for (int i = 1; i < users.Length; i++)
        {
            await _userManager.CreateAsync(users[i], password);
            await _userManager.AddToRoleAsync(users[i], roles[1]);
        }

        if (_context.Movies.Any())
        {
            return;
        }

        var movies = new Movie[] {
            new Movie {Title = "The Great Gatsby", Duration = 143},
            new Movie {Title = "Interstellar", Duration = 169},
            new Movie {Title = "The Shawshank Redemption", Duration = 142},
            new Movie {Title = "The Godfather", Duration = 175},
            new Movie {Title = "The Dark Knight", Duration = 152},
            new Movie {Title = "Inception", Duration = 148},
            new Movie {Title = "Fight Club", Duration = 139}
        };

        foreach (Movie m in movies)
        {
            _context.Add(m);
        }

        _context.SaveChanges();

        if (_context.Screenings.Any())
        {
            return;
        }

        var screenings = new Screening[] {
            new Screening {Movie = movies.Single(s => s.Title == "The Great Gatsby"), Date = DateTime.Parse("2018-06-04 15:00")},
            new Screening {Movie = movies.Single(s => s.Title == "Interstellar"), Date = DateTime.Parse("2018-06-04 20:00")},
            new Screening {Movie = movies.Single(s => s.Title == "Inception"), Date = DateTime.Parse("2018-06-05 12:00")}
        };

        foreach (Screening s in screenings)
        {
            _context.Add(s);
        }

        _context.SaveChanges();

        if (_context.Tickets.Any())
        {
            return;
        }

        var tickets = new Ticket[] {
            new Ticket {Screening = screenings.Single(s => s.Movie.Title == "The Great Gatsby"), User = users.Single(s => s.UserName == "user1"), Seat = "A1"},
            new Ticket {Screening = screenings.Single(s => s.Movie.Title == "The Great Gatsby"), User = users.Single(s => s.UserName == "user2"), Seat = "A2"},
            new Ticket {Screening = screenings.Single(s => s.Movie.Title == "The Great Gatsby"), User = users.Single(s => s.UserName == "user3"), Seat = "B3"},
            new Ticket {Screening = screenings.Single(s => s.Movie.Title == "Interstellar"), User = users.Single(s => s.UserName == "user1"), Seat = "A1"},
            new Ticket {Screening = screenings.Single(s => s.Movie.Title == "Interstellar"), User = users.Single(s => s.UserName == "user2"), Seat = "B1"},
            new Ticket {Screening = screenings.Single(s => s.Movie.Title == "Interstellar"), User = users.Single(s => s.UserName == "user3"), Seat = "C1"},
            new Ticket {Screening = screenings.Single(s => s.Movie.Title == "Inception"), User = users.Single(s => s.UserName == "user3"), Seat = "A5"},
            new Ticket {Screening = screenings.Single(s => s.Movie.Title == "Inception"), User = users.Single(s => s.UserName == "user4"), Seat = "B5"}
        };

        foreach (Ticket t in tickets)
        {
            _context.Add(t);
        }

        _context.SaveChanges();
    }
}