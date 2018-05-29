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
    public class MovieController : Controller
    {
        private readonly VIAMovies.Data.ApplicationDbContext _context;
        public MovieController(VIAMovies.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("all")]
        public IActionResult Get()
        {
            var movies = _context.Movies.ToList();

            return Ok(JsonConvert.SerializeObject(movies, Formatting.None));
        }

    }
}