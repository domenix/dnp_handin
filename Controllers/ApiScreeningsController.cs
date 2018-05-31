using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VIAMovies.Models;
using Newtonsoft.Json;

namespace VIAMovies.Controllers
{
    [Route("api/[controller]")]
    public class ScreeningsController : Controller
    {
        private readonly VIAMovies.Data.ApplicationDbContext _context;
        public ScreeningsController(VIAMovies.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("")]
        public IActionResult Get()
        {
            var screenings = from s in _context.Screenings
                             select new
                             {
                                 s.Id,
                                 s.Date,
                                 Movie = new { s.Movie.Id, s.Movie.Title, s.Movie.Duration },
                                 Tickets = s.Tickets.Select(t => new { t.Seat })
                             };

            return Ok(JsonConvert.SerializeObject(screenings));
        }

        //Should have authorization :(, no time to implement OpenID connect but imagine we pass an AccessToken in the Authorization header
        [HttpPost("")]
        public async Task<IActionResult> Post()
        {
            var body = new StreamReader(Request.Body).ReadToEnd();
            var json = JsonConvert.DeserializeAnonymousType(body, new { date = "", movieId = 1 });
            var startDate = DateTime.Parse(json.date);
            var movie = _context.Movies.Single(m => m.Id == json.movieId);
            var endDate = startDate.AddMinutes(movie.Duration);
            var results = from r in _context.Screenings
                          where r.Date.CompareTo(endDate) < 0 &&
                          startDate.CompareTo(r.Date.AddMinutes(r.Movie.Duration)) < 0
                          select r;
            if (results.Count() != 0)
            {
                Response.StatusCode = 400;
                return Content("Invalid date");
            }
            var screening = new Screening { Movie = movie, Date = startDate };
            await _context.Screenings.AddAsync(screening);
            await _context.SaveChangesAsync();
            return Ok();
        }

        //Should have authorization :(
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var result =
            (from s in _context.Screenings
             where s.Id == id
             select new { Screening = s, s.Tickets }).FirstOrDefault();
            _context.Tickets.RemoveRange(result.Tickets);
            _context.Screenings.Remove(result.Screening);
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}