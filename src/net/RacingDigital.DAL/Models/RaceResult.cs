using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RacingDigital.DAL.Models
{
    public class RaceResult
    {
        [Key]
        public int ID { get; set; }
        public DateTime RaceDate { get; set; } = DateTime.UtcNow;
        public string RaceName { get; set; } = string.Empty;
        public Racecourse Racecourse { get; set; } = new Racecourse();
        public Horse Horse { get; set; } = new Horse();
        public Jockey Jockey { get; set; } = new Jockey();
        public int FinishingPosition;
        public ICollection<Note> Notes { get; set; } = [];
        public string DistanceBeaten { get;  set; } = string.Empty;
        public string TimeBeaten { get;  set; } = string.Empty;
    }
}
