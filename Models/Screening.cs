using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VIAMovies.Models
{
    public class Screening
    {
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}