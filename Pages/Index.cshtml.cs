using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
            public string startdate;
            public string enddate;

            public JSScreening(int id, string title, string startdate, string enddate)
            {
                this.id = id;
                this.title = title;
                this.startdate = startdate;
                this.enddate = enddate;
            }
        }

        public string getScreenings()
        {
            List<Screening> screenings = _context.Screenings.ToList();

            List<JSScreening> jsscreenings = new List<JSScreening>();

            foreach (Screening s in screenings)
            {
                //s.Movie.Title, s.Movie.Duration, s.Date

                jsscreenings.Add(new JSScreening(s.Id, s.MovieId.ToString(), s.Date.ToString("yyyy-MM-ddTHH':'mm"), s.Date.AddMinutes(150).ToString("yyyy-MM-ddTHH':'mm")));
            }

            return JsonConvert.SerializeObject(jsscreenings);
        }
        public void OnGetAsync()
        {

        }
    }
}
