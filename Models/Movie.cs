using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VIAMovies.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [StringLength(300, MinimumLength = 1)]
        [Required]
        public string Title { get; set; }
        [Required]
        public int Duration { get; set; }
        public List<Screening> Screenings { get; set; }
    }
}