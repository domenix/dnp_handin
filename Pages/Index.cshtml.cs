using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using VIAMovies.Models;

namespace VIAMovies.Pages
{
    public class IndexModel : PageModel
    {
        private readonly VIAMovies.Data.ApplicationDbContext _context;
        public List<VIAMovies.Models.Movie> movies;
        public string test;
        public IndexModel(VIAMovies.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public struct JSScreening
        {
            public int id;
            public string title;
            public string start;
            public string end;

            public JSScreening(int id, string title, string startdate, string enddate)
            {
                this.id = id;
                this.title = title;
                this.start = startdate;
                this.end = enddate;
            }
        }

        public string getScreenings()
        {
            List<Screening> screenings = _context.Screenings.Include(m => m.Movie).ToList();

            List<JSScreening> jsscreenings = new List<JSScreening>();

            foreach (Screening s in screenings)
            {
                jsscreenings.Add(new JSScreening(s.Id, s.Movie.Title, s.Date.ToString("yyyy-MM-ddTHH':'mm"), s.Date.AddMinutes(150).ToString("yyyy-MM-ddTHH':'mm")));
            }

            return JsonConvert.SerializeObject(jsscreenings);
        }
        public void OnGetAsync()
        {

        }
    }
}
