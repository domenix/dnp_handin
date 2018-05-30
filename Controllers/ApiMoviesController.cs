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
    public class MoviesController : Controller
    {
        private readonly VIAMovies.Data.ApplicationDbContext _context;
        public MoviesController(VIAMovies.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("")]
        public IActionResult Get()
        {
            var movies = _context.Movies.ToList();

            return Ok(JsonConvert.SerializeObject(movies, Formatting.None));
        }
        //Should have authorization :(, no time to implement OpenID connect but imagine we pass an AccessToken in the Authorization header
        [HttpPost("")]
        public async Task<IActionResult> Post()
        {
            var body = new StreamReader(Request.Body).ReadToEnd();
            var json = JsonConvert.DeserializeAnonymousType(body, new { title = "", duration = 1 });
            var movie = new Movie { Title = json.title, Duration = json.duration };
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
            return Ok();
        }

        //Should have authorization :(
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var result =
            (from m in _context.Movies
             where m.Id == id
             select new { Movie = m, Screenings = m.Screenings }).FirstOrDefault();
            _context.Tickets.RemoveRange(result.Screenings.SelectMany(s => s.Tickets));
            _context.Screenings.RemoveRange(result.Screenings);
            _context.Movies.Remove(result.Movie);
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}