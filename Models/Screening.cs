using System;
using System.ComponentModel.DataAnnotations;

namespace VIAMovies.Models
{
    public class Screening
    {
        public int Id { get; set; }
        [Required]
        public DateTime date { get; set; }

        public Movie Movie { get; set; }
    }
}