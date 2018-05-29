using System;
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

            return Ok(JsonConvert.SerializeObject(screenings, Formatting.Indented));
        }
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