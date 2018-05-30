using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VIAMovies.Models;

namespace VIAMovies.Pages.Account.Manage
{
    public class TicketsModel : PageModel
    {
        private readonly VIAMovies.Data.ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<ChangePasswordModel> _logger;

        public TicketsModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<ChangePasswordModel> logger,
            VIAMovies.Data.ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
        }

        public List<Ticket> getTickets()
        {
            List<Ticket> tickets = _context.Tickets.Include(s => s.Screening).ThenInclude(m => m.Movie).Include(u => u.User).Where(u => u.User.Id == _userManager.GetUserId(HttpContext.User)).ToList();
            return tickets;
        }
    }
}
