﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace VIAMovies.Pages
{
    public class ShowReservationsModel : PageModel
    {
        public string Message { get; set; }

        public void OnGet()
        {
            Message = "This will return all of your registrations, in theory";
        }
    }
}