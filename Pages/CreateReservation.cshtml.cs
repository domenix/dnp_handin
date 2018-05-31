using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VIAMovies.Models;
using VIAMovies.Services;

namespace VIAMovies.Pages
{
    public class CreateReservationModel : PageModel
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private int seats = 10;

        public CreateReservationModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public Task<bool> IsAdmin(User user)
        {
            return _userManager.IsInRoleAsync(user, "Admin");
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Index");
            }
            else if (!await IsAdmin(user))
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }

        public int numberOfSeats()
        {
            return seats;
        }
    }
}
