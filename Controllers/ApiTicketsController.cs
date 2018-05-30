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
    public class TicketsController : Controller
    {
        private readonly VIAMovies.Data.ApplicationDbContext _context;
        public TicketsController(VIAMovies.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("")]
        public IActionResult Get()
        {
            var tickets = from s in _context.Tickets
                             select new
                             {
                                 s.Id,
                                 s.UserId,
                                 s.Seat,
                                 s.Screening
                             };

            return Ok(JsonConvert.SerializeObject(tickets, Formatting.Indented));
        }

        [HttpPost("")]
        public async Task<IActionResult> Post()
        {
            var body = new StreamReader(Request.Body).ReadToEnd();
            var json = JsonConvert.DeserializeAnonymousType(body, new { screeningId = 1, userId = "", seat = "" });
            var ticket = new Ticket { Screening = _context.Screenings.Single(m => m.Id == json.screeningId), UserId = json.userId, Seat = json.seat };
            await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}