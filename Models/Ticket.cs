using System.ComponentModel.DataAnnotations.Schema;
using VIAMovies.Data;

namespace VIAMovies.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string seat { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}